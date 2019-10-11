using System;
using System.Collections.Generic;
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

    public class Signal
    {
        public Coordinates Coord;
        private Block ContainerBlock;
        public SignalState State;
        public bool Settable;
        private SignalOrientation Orientation;
        private Visual Display;
        public int Id;
        private static int NextId = 0;



        public Signal(int x, int y, SignalOrientation o, Block b, bool settable, Canvas Canvas)
        {
            Id = NextId++;
            Coord.X = x;
            Coord.Y = y;
            Orientation = o;
            ContainerBlock = b;
            Settable = settable;
            Display = new Visual(this, Canvas);
            State = SignalState.Red;
            Display.Update();
        }

        public void SetState(SignalState s)
        {
            State = s;
            if (State == SignalState.Blank) Settable = false;
            Layout.UpdateBlockMaxSpeed(Id, State);
            Display.Update();
        }

        public void Draw(Canvas Canvas)
        {
            Display.Draw(Canvas);
        }

        class Visual
        {
            private Canvas Canvas;
            private Signal S;
            private BitmapImage bitmap;
            private Image SignalImg;

            public Visual(Signal S, Canvas Canvas)
            {
                this.Canvas = Canvas;
                this.S = S;
                SignalImg = new Image { Width = 30 };
                SignalImg.MouseDown += SignalClicked;
            }

            private void SignalClicked(object sender, MouseButtonEventArgs e)
            {
                if (S.Settable)
                {
                    ComboBox c = new ComboBox();
                    c.ItemsSource = new List<string> { "Zöld", "Piros", "Sárga" };
                    Canvas.SetLeft(c, S.Coord.X);
                    Canvas.SetTop(c, S.Coord.Y);
                    Canvas.Children.Add(c);
                    c.SelectionChanged += SignalChanged;
                }
                else Control.SetInformation("Ez a jelző jelenleg nem állítható át.");
            }

            private void SignalChanged(object sender, SelectionChangedEventArgs e)
            {
                ComboBox C = (ComboBox)sender;
                switch ((string)C.SelectedItem)
                {
                    case "Zöld":
                        S.SetState(SignalState.Green);
                        break;
                    case "Piros":
                        S.SetState(SignalState.Red);
                        break;
                    case "Sárga":
                        S.SetState(SignalState.Yellow);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                Canvas.Children.Remove(C);
                Control.SetInformation("Jelző sikeresen átállítva.");
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
                    case SignalState.Blank:
                        bitmap.UriSource = new Uri(@"pack://application:,,,/3l_blank.png");
                        break;
                }
                bitmap.EndInit();
                SignalImg.Source = bitmap;
            }

            public void Draw(Canvas Canvas)
            {
                Update();
                RotateTransform rotateTransform = new RotateTransform((double)S.Orientation);
                SignalImg.RenderTransform = rotateTransform;
                Canvas.SetTop(SignalImg, S.Coord.Y);
                Canvas.SetLeft(SignalImg, S.Coord.X);
                Canvas.SetZIndex(SignalImg, 1);
                Canvas.Children.Add(SignalImg);
            }

        }

    }
}
