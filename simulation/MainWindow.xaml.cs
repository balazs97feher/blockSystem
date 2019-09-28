﻿using System;
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
        }

        private void DirectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (sender as ComboBox).SelectedItem as ComboBoxItem;
            if (item.Content.ToString() == "Clockwise")
            {
                Layout.SetCWDirection(true);
            }
            else Layout.SetCWDirection(false);
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
            if (Control.Initialized)
            {
                ComboBoxItem item = (sender as ComboBox).SelectedItem as ComboBoxItem;
                switch (item.Content.ToString())
                {
                    case "Track#1":
                        Controller.SetDeparture(0);
                        break;
                    case "Track#2":
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
