using System.Windows;
using System.Windows.Controls;

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

        private SimulationWindow(Control C)
        {
            InitializeComponent();
            AppCanvas = ApplicationCanvas;
            Controller = C;

            DisplayVelocity.DataContext = Controller.Fecske;
            SpeedSlide.DataContext = Controller;
            Information.DataContext = Controller;
        }

        public static SimulationWindow CreateWindow(Control C)
        {
            if (Instance != null) return Instance;
            else return new SimulationWindow(C);
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

    }
}
