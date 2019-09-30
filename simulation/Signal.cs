using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace simulation
{
    public enum SignalState
    {
        Red,
        Green,
        Yellow,
        Blank
    }

    public enum SignalOrientation
    {
        CW = -90,
        CCW = 90
    }

    public class Signal : Field
    {
        private Block ContainerBlock;
        public SignalState State;
        public bool Settable;
        public int MaxSpeed;
        private SignalOrientation Orientation;
        private Visual Display;


        public Signal(int x, int y, SignalOrientation o, Block b) : base(x, y)
        {
            Orientation = o;
            ContainerBlock = b;
            Display = new Visual(this);
            SetState(SignalState.Red);
            MaxSpeed = 0;
            Settable = true;
        }

        public void SetState(SignalState s)
        {
            State = s;
            switch (s)
            {
                case SignalState.Green:
                    MaxSpeed = 120;
                    break;
                case SignalState.Red:
                    MaxSpeed = 0;
                    break;
                case SignalState.Yellow:
                    MaxSpeed = 40;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ContainerBlock.UpdateEOBSpeed();
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

            private void SignalClicked(object sender, MouseButtonEventArgs e)
            {
                if (S.Settable)
                {
                    ComboBox c = new ComboBox();
                    c.ItemsSource = new List<string> { "Green", "Red", "Yellow" };
                    Canvas.SetLeft(c, S.Coord.X);
                    Canvas.SetTop(c, S.Coord.Y);
                    MainWindow.AppCanvas.Children.Add(c);
                    c.SelectionChanged += SignalChanged;
                }
                else MessageBox.Show("Signal cannot be set. Please check switches.", "Warning",
                MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            private void SignalChanged(object sender, SelectionChangedEventArgs e)
            {
                ComboBox C = (ComboBox)sender;
                switch ((string)C.SelectedItem)
                {
                    case "Green":
                        S.SetState(SignalState.Green);
                        break;
                    case "Red":
                        S.SetState(SignalState.Red);
                        break;
                    case "Yellow":
                        S.SetState(SignalState.Yellow);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                MainWindow.AppCanvas.Children.Remove(C);
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
                    case SignalState.Yellow:
                        bitmap.UriSource = new Uri(@"pack://application:,,,/3l_yellow.png");
                        break;
                }
                bitmap.EndInit();
                SignalImg.Source = bitmap;
            }

            public void Draw()
            {
                Update();
                RotateTransform rotateTransform = new RotateTransform((double)S.Orientation);
                SignalImg.RenderTransform = rotateTransform;
                Canvas.SetTop(SignalImg, S.Coord.Y);
                Canvas.SetLeft(SignalImg, S.Coord.X);
                Canvas.SetZIndex(SignalImg, 1);
                MainWindow.AppCanvas.Children.Add(SignalImg);
            }


        }

    }
}
