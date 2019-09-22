using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace simulation
{
    public enum SignalState
    {
        Red,
        Green,
        White,
        Blank
    }

    public enum SignalOrientation
    {
        CW = -90,
        CCW = 90
    }

    public class Signal : Field
    {
        public SignalState State;
        public SignalOrientation Orientation;
        public Image SignalImg;

        public Signal(int x, int y, SignalOrientation o) : base(x, y)
        {
            Orientation = o;
            State = SignalState.Red;
            SignalImg = new Image { Width = 30 };
            SetState(State);
        }


        public void SetState(SignalState s)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            switch(s)
            {
                case SignalState.Green:
                    bitmap.UriSource = new Uri(@"pack://application:,,,/3l_green.png");
                    break;
                case SignalState.Red:
                    bitmap.UriSource = new Uri(@"pack://application:,,,/3l_red.png");
                    break;
                case SignalState.White:
                    bitmap.UriSource = new Uri(@"pack://application:,,,/3l_white.png");
                    break;
            }
            bitmap.EndInit();
            SignalImg.Source = bitmap;
            Canvas.SetTop(SignalImg, Coord.Y);
            Canvas.SetLeft(SignalImg, Coord.X);
            Canvas.SetZIndex(SignalImg, 1);
            RotateTransform rotateTransform = new RotateTransform((double)Orientation);
            SignalImg.RenderTransform = rotateTransform;
        }

        public override void Draw()
        {

            MainWindow.AppCanvas.Children.Add(SignalImg);
        }


    }
}
