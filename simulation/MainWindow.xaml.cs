﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace simulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Canvas AppCanvas;
        public MainWindow()
        {
            InitializeComponent();
            AppCanvas = Layout;


            /*EmptyField e = new EmptyField();
            e.Coord.X = 100;
            e.Coord.Y = 100;
            e.Draw();*/
        }
    }
}
