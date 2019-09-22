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
        private static int NextId = 1;
        private int Id;
        private Visual Display;


        public Signal(int x, int y, SignalOrientation o) : base(x, y)
        {
            Orientation = o;
            State = SignalState.Red;
            Display = new Visual(this);
            Id = NextId++;
        }

        public void SetState(SignalState s)
        {
            State = s;
            Display.Update();
        }

        public override void Draw()
        {
            Display.Draw();
        }

        class Visual
        {
            private Signal S;
            private BitmapImage bitmap;
            private Image SignalImg;

            public Visual(Signal S)
            {
                this.S = S;
                SignalImg = new Image { Width = 30 };
                SignalImg.MouseDown += SignalClicked;
            }

            public void Update()
            {
                bitmap = new BitmapImage();
                bitmap.BeginInit();
                switch (S.State)
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
            }

            public void Draw()
            {
                Update();
                SignalImg.Source = bitmap;
                RotateTransform rotateTransform = new RotateTransform((double)S.Orientation);
                SignalImg.RenderTransform = rotateTransform;
                Canvas.SetTop(SignalImg, S.Coord.Y);
                Canvas.SetLeft(SignalImg, S.Coord.X);
                Canvas.SetZIndex(SignalImg, 1);
                MainWindow.AppCanvas.Children.Add(SignalImg);
            }


            private void SignalClicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
            {
                Control.SelectedSignal = S;
            }
        }

    }
}
