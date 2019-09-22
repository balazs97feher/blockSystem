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

        private void SignalChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Control.SelectedSignal != null)
            {
                ComboBoxItem item = (sender as ComboBox).SelectedItem as ComboBoxItem;
                switch (item.Content.ToString())
                {
                    case "Green":
                        Control.SelectedSignal.SetState(SignalState.Green);
                        break;
                    case "Red":
                        Control.SelectedSignal.SetState(SignalState.Red);
                        break;
                    case "White":
                        Control.SelectedSignal.SetState(SignalState.White);
                        break;
                }
            }
            else MessageBox.Show("Please select a signal by clicking on it.", "Warning", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
        }


        private void SwitchLeft(object sender, RoutedEventArgs e)
        {
            Layout.LeftSwitch.DoSwitch();
        }

        private void SwitchRight(object sender, RoutedEventArgs e)
        {
            Layout.RightSwitch.DoSwitch();
        }
    }
}
