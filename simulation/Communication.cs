using System.ComponentModel;
using System.IO.Ports;

namespace simulation
{
    public class Communication : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged; // need to implement the above interface for data binding
        private void NotifyPropertyChanged(string Property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Property));
        }

        private string boosterReceived;
        public string BoosterReceived
        {
            get { return boosterReceived; }
            set
            {
                boosterReceived = value;
                NotifyPropertyChanged("BoosterReceived");
            }
        }
        private void PrintBoosterReceived(byte b)
        {
            if (BoosterReceived.Length > 25) BoosterReceived = "";
            BoosterReceived += b.ToString() + " ";
        }

        private string occupationReceived;
        public string OccupationReceived
        {
            get { return occupationReceived; }
            set
            {
                if (occupationReceived.Length > 24) occupationReceived = "";
                occupationReceived = value;
                NotifyPropertyChanged("OccupationReceived");
            }
        }
        private void PrintOccupationReceived(byte b)
        {
            if (OccupationReceived.Length > 25) OccupationReceived = "";
            OccupationReceived += b.ToString() + " ";
        }


        private static Communication Instance = null;
        public SerialPort ControlPort;
        public SerialPort OccupationPort;
        private bool Connected;

        private Communication()
        {
            boosterReceived = "";
            occupationReceived = "";
        }

        static public Communication CreateMessenger()
        {
            if (Instance == null) return new Communication();
            else return Instance;
        }

        public void SetOccupationPort(string PortName)
        {
            OccupationPort = new SerialPort
            {
                BaudRate = 9600,
                PortName = PortName
            };
            OccupationPort.Open();
            OccupationPort.DataReceived += OccupationPort_DataReceived;
        }

        private void OccupationPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        public void SetControlPort(string PortName)
        {
            ControlPort = new SerialPort
            {
                BaudRate = 38400,
                PortName = PortName
            };
            ControlPort.Open();
            BoosterConnect();
        }

        public void ClosePorts()
        {
            if (ControlPort != null) ControlPort.Close();
            if (OccupationPort != null) OccupationPort.Close();
        }

        private void SendControlByte(byte b)
        {
            if (ControlPort != null)
            {
                byte[] c = new byte[1];
                c[0] = b;
                ControlPort.Write(c, 0, 1);
                PrintBoosterReceived(b); // DEBUG
            }
        }

        public void SetSignal(int Id, SignalState State)
        {
            switch (Id)
            {
                default:
                    switch (State)
                    {
                        case SignalState.Green:
                            SendControlByte(0x8A);
                            break;
                        case SignalState.Red:
                            SendControlByte(0x9A);
                            break;
                        case SignalState.Yellow:
                            SendControlByte(0xA2);
                            break;
                        case SignalState.Blank:
                            SendControlByte(0x82);
                            break;
                    }
                    break;
            }
        }

        public void SetSwitch(int Id, bool Straight)
        {
            switch (Id)
            {
                case 0:
                    if (Straight) SendControlByte(0xC1);
                    else SendControlByte(0xC9);
                    break;
                case 1:
                    if (Straight) SendControlByte(0xC2);
                    else SendControlByte(0xCA);
                    break;
            }
        }

        private void BoosterConnect()
        {
            SendControlByte(0xFF);
            Connected = true;
        }

        public void BoosterDisconnect()
        {
            if (Connected) SendControlByte(0xEE);
        }



    }
}
