using System.IO.Ports;

namespace simulation
{
    public class Communication
    {
        private static Communication Instance = null;
        public SerialPort ControlPort;
        public SerialPort OccupationPort;

        private Communication() {}

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
        }

        public void SetControlPort(string PortName)
        {
            ControlPort = new SerialPort
            {
                BaudRate = 38400,
                PortName = PortName
            };
            ControlPort.Open();
        }

        public void ClosePorts()
        {
            if (ControlPort != null) ControlPort.Close();
            if (OccupationPort != null) OccupationPort.Close();
        }

        private void SendControlByte(byte b)
        {
            byte[] c = new byte[1];
            c[0]= b;
            ControlPort.Write(c, 0, 1);
        }



    }
}
