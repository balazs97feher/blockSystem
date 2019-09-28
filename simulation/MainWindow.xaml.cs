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
        public int k;
        public MainWindow()
        {
            InitializeComponent();
            AppCanvas = ApplicationCanvas;

            Layout.Initialize();
            Layout.Draw();

            Control.Initialize();
            DisplayVelocity.DataContext = Control.Fecske;
            slValue.DataContext = Control.SetSpeed;
            
        }





        private void DirectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (sender as ComboBox).SelectedItem as ComboBoxItem;
            if (item.Content.ToString() == "Clockwise")
            {
                Control.DirectionCW = true;
            }
            else Control.DirectionCW = false;
        }

        private void SwitchLeft(object sender, RoutedEventArgs e)
        {
            Layout.LeftSwitch.DoSwitch();
        }

        private void SwitchRight(object sender, RoutedEventArgs e)
        {
            Layout.RightSwitch.DoSwitch();

        }

        private void DepartureChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (sender as ComboBox).SelectedItem as ComboBoxItem;
            switch (item.Content.ToString())
            {
                case "Track#1":
                    Control.SetDeparture(0);
                    break;
                case "Track#2":
                    Control.SetDeparture(1);
                    break;
            }
        }

        private void SpeedChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Control.SetSpeed = (int)(sender as Slider).Value;
        }
    }
}
