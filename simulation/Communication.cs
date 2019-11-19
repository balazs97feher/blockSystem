using System;
using System.Collections;
using System.ComponentModel;
using System.IO.Ports;
using System.Threading;

namespace simulation
{
    public class Communication : INotifyPropertyChanged
    {
        private Queue ToBeSent;
        private bool ready = true; // serial port ready
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
        private void PrintBoosterReceived(string b) //prints the byte received from the booster
        {
            if (BoosterReceived.Length > 16) BoosterReceived = "";
            BoosterReceived += b + " ";
        }

        private string occupationReceived;
        public string OccupationReceived
        {
            get { return occupationReceived; }
            set
            {
                occupationReceived = value;
                NotifyPropertyChanged("OccupationReceived");
            }
        }
        private void PrintOccupationReceived(string b) //prints the byte received from the hall sensors
        {
            if (OccupationReceived.Length > 16) OccupationReceived = "";
            OccupationReceived += b + " ";
        }


        private static Communication Instance = null;
        public SerialPort ControlPort;
        public SerialPort OccupationPort;
        public string OccupationLastReceived; // last received data from occupation port
        public bool BoosterConnected; // is the software connected to the booster

        private Communication()
        {
            boosterReceived = "";
            occupationReceived = "";
            BoosterConnected = false;
            ToBeSent = new Queue();
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
            if (OccupationPort.IsOpen == true)
            {
                Control.SetInformation("bt ok");
                OccupationPort.DataReceived += OccupationPort_DataReceived;
            }
            else Control.SetInformation("bt nem ok");
        }

        private void OccupationPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            OccupationLastReceived = (sender as SerialPort).ReadExisting();
            PrintOccupationReceived(OccupationLastReceived);
        }

        public void SetControlPort(string PortName)
        {
            ControlPort = new SerialPort
            {
                BaudRate = 38400,
                PortName = PortName
            };
            ControlPort.Open();
            ControlPort.DataReceived += ControlPort_DataReceived;
        }

        private void ControlPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            PrintBoosterReceived((sender as SerialPort).ReadExisting());
            if (ToBeSent.Count != 0)
            {
                byte[] c = new byte[1];
                c[0] = (byte)ToBeSent.Dequeue();
                ControlPort.Write(c, 0, 1);
                if (ToBeSent.Count == 0) ready = true;
            }
            else ready = true;
        }

        private void SendControlByte(byte b)
        {
            if (BoosterConnected && ControlPort != null)
            {
                if (ready == true)
                {
                    byte[] c = new byte[1];
                    c[0] = b;
                    ControlPort.Write(c, 0, 1);
                    ready = false;
                }
                else
                {
                    ToBeSent.Enqueue(b);
                }
            }
        }

        public void SetSpeed(int SetSpeed)
        {
            if (Layout.DirectionCW == true)
            {
                int b = 0x50;
                b += (int)System.Math.Ceiling(Convert.ToDouble(SetSpeed) / 10D);
                SendControlByte((byte)b);
            }
            else
            {
                int b = 0x40;
                b += SetSpeed / 10;
                SendControlByte((byte)b);
            }
        }

        public void SetSignal(int Id, SignalState State)
        {
            switch (Id)
            {
                case 2:
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
                case 0:
                    switch (State)
                    {
                        case SignalState.Green:
                            SendControlByte(0x8B);
                            break;
                        case SignalState.Red:
                            SendControlByte(0x9B);
                            break;
                        case SignalState.Yellow:
                            SendControlByte(0xAB);
                            break;
                        case SignalState.Blank:
                            SendControlByte(0x83);
                            break;
                    }
                    break;
                case 3:
                    switch (State)
                    {
                        case SignalState.Green:
                            SendControlByte(0x8C);
                            break;
                        case SignalState.Red:
                            SendControlByte(0x9C);
                            break;
                        case SignalState.Yellow:
                            SendControlByte(0xAC);
                            break;
                        case SignalState.Blank:
                            SendControlByte(0x84);
                            break;
                    }
                    break;
                case 1:
                    switch (State)
                    {
                        case SignalState.Green:
                            SendControlByte(0x8D);
                            break;
                        case SignalState.Red:
                            SendControlByte(0x9D);
                            break;
                        case SignalState.Yellow:
                            SendControlByte(0xAD);
                            break;
                        case SignalState.Blank:
                            SendControlByte(0x85);
                            break;
                    }
                    break;
                case 5:
                    switch (State)
                    {
                        case SignalState.Green:
                            SendControlByte(0x8E);
                            break;
                        case SignalState.Red:
                            SendControlByte(0x9E);
                            break;
                        case SignalState.Yellow:
                            SendControlByte(0xAE);
                            break;
                        case SignalState.Blank:
                            SendControlByte(0x86);
                            break;
                    }
                    break;
                case 4:
                    switch (State)
                    {
                        case SignalState.Green:
                            SendControlByte(0x89);
                            break;
                        case SignalState.Red:
                            SendControlByte(0x99);
                            break;
                        case SignalState.Yellow:
                            SendControlByte(0xA9);
                            break;
                        case SignalState.Blank:
                            SendControlByte(0x81);
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

        public bool BoosterConnect() //returns true, if connection was successful
        {
            if (ControlPort != null)
            {
                BoosterConnected = true;
                SendControlByte(0xFF);
                return true;
            }
            else return false;
        }

        public void BoosterDisconnect()
        {
            if (BoosterConnected)
            {
                SendControlByte(0xEE);
                BoosterConnected = false;
            }
        }

        public void ClosePorts()
        {
            if (ControlPort != null && ControlPort.IsOpen) ControlPort.Close();
            if (OccupationPort != null && OccupationPort.IsOpen) OccupationPort.Close();
        }

    }
}
