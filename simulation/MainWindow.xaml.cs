using System;
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

            StraightTrack t2 = new StraightTrack();
            t2.Coord.X = 150;
            t2.Coord.Y = 50;
            t2.TrackOrientation = Orientation.VerticalCenter;
            t2.Draw();
            StraightTrack t = new StraightTrack();
            t.Coord.X = 100;
            t.Coord.Y = 100;
            t.TrackOrientation = Orientation.BottomRight;
            t.Draw();
            StraightTrack t4 = new StraightTrack();
            t4.Coord.X = 150;
            t4.Coord.Y = 100;
            t4.TrackOrientation = Orientation.TopLeft;
            t4.Draw();
            StraightTrack t3 = new StraightTrack();
            t3.Coord.X = 50;
            t3.Coord.Y = 150;
            t3.TrackOrientation = Orientation.HorizontalCenter;
            t3.Draw();
            StraightTrack t5 = new StraightTrack();
            t5.Coord.X = 100;
            t5.Coord.Y = 150;
            t5.TrackOrientation = Orientation.TopLeft;
            t5.Draw();

        }
    }
}
