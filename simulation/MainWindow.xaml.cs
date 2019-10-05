using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Windows.Threading;

namespace simulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Canvas AppCanvas;
        public static Control Controller;

        public MainWindow()
        {
            InitializeComponent();
            AppCanvas = ApplicationCanvas;

            Layout.Initialize();
            Layout.Draw();
            Controller = new Control();

            DisplayVelocity.DataContext = Controller.Fecske;
            SpeedSlide.DataContext = Controller;
            Information.DataContext = Controller;
        }

        private void DirectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (sender as ComboBox).SelectedItem as ComboBoxItem;
            if (item.Content.ToString() == "Balra")
            {
                Layout.SetCWDirection(true);
            }
            else Layout.SetCWDirection(false);
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
            if (Control.Initialized) // checks if there is a train to depart at all
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
