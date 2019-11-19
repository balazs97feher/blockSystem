using System.Windows;
using System.Windows.Controls;
using System.IO.Ports;
using System.Threading;

namespace simulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SimulationWindow : Window
    {
        private static SimulationWindow Instance = null;
        public Canvas AppCanvas;
        private Control Controller;
        private Communication Messenger;

        private SimulationWindow(Control C, Communication M)
        {
            InitializeComponent();
            AppCanvas = ApplicationCanvas;
            Controller = C;
            Messenger = M;

            DisplayVelocity.DataContext = Controller.Fecske;
            SpeedSlide.DataContext = Controller;
            Information.DataContext = Controller;
            BoosterReceivedData.DataContext = Messenger;
            OccupationReceivedData.DataContext = Messenger;

        }

        public static SimulationWindow CreateWindow(Control C, Communication M)
        {
            if (Instance != null) return Instance;
            else return new SimulationWindow(C, M);
        }

        private void DirectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (sender as ComboBox).SelectedItem as ComboBoxItem;
            if (Controller != null)
            {
                if (item.Content.ToString() == "Balra")
                {
                    Controller.SetCWDirection(true);
                }
                else Controller.SetCWDirection(false);
            }
        }

        private void SwitchLeft(object sender, RoutedEventArgs e)
        {
            Controller.SwitchLeft();
        }

        private void SwitchRight(object sender, RoutedEventArgs e)
        {
            Controller.SwitchRight();
        }

        private void DepartureChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Controller != null)
            {
                ComboBoxItem item = (sender as ComboBox).SelectedItem as ComboBoxItem;
                switch (item.Content.ToString())
                {
                    case "Felső vágány":
                        Controller.SetDeparture(0);
                        break;
                    case "Alsó vágány":
                        Controller.SetDeparture(1);
                        break;
                }
            }
        }

        private void SpeedChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Controller.SetSpeed = (int)(sender as Slider).Value;
        }

        private string[] ArrayComPortNames;
        private void DiscoverPorts(object sender, RoutedEventArgs e)
        {
            ArrayComPortNames = null;
            ArrayComPortNames = SerialPort.GetPortNames();
            if (ArrayComPortNames.Length == 0) Control.SetInformation("Nem találhatók portok.");
            else
            {
                ControlPort.ItemsSource = ArrayComPortNames;
                OccupationPort.ItemsSource = ArrayComPortNames;
                Control.SetInformation("Soros portok felderítve.");
            }
        }

        private void SetControlPort(object sender, SelectionChangedEventArgs e)
        {
            string S = ControlPort.SelectedItem.ToString();
            Messenger.SetControlPort(S);
        }

        private void SetOccupationPort(object sender, SelectionChangedEventArgs e)
        {
            string S = OccupationPort.SelectedItem.ToString();
            Messenger.SetOccupationPort(S);
            Controller.SubscribeToOccupationPort();
        }

        private void BoosterOnOff(object sender, RoutedEventArgs e)
        {
            if(Messenger.BoosterConnected==false)
            {
                if (Messenger.BoosterConnect() == true)
                { // synchronize layout and hardware status
                    (sender as Button).Content = "Booster OFF";
                    Thread.Sleep(2000); // wait for power
                    Controller.SetCWDirection(true);
                    Controller.SetSpeed = 0;
                    Control.SetInformation("Ellenőrizze a váltók állását a szoftverben és hardverben.");
                }
                else Control.SetInformation("Jelenleg nem lehet a Boosterhez csatlakozni.");
            }
            else
            {
                Messenger.BoosterDisconnect();
                (sender as Button).Content = "Booster ON";
            }
        }
    }
}
