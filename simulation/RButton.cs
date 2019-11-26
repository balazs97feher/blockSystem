using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace simulation
{
    class RButton
    {
        public Coordinates Coord;
        private Visual Display;
        private bool Clicked;
        public bool Active; // is automatic mode turned on
        public int Id;
        private static int NextId = 0;

        public RButton(int x, int y, Canvas C)
        {
            Id = NextId++;
            Coord.X = x;
            Coord.Y = y;
            Clicked = false;
            Active = false;
            Display = new Visual(this, C);
            Display.Update();
        }

        public void Draw(Canvas Canvas)
        {
            Display.Draw(Canvas);
        }

        public void Reset()
        {
            Clicked = false;
            Display.Update();
        }

        class Visual
        {
            private Canvas Canvas;
            private RButton B;
            private BitmapImage bitmap;
            private Image ButtonImg;

            public Visual(RButton B, Canvas Canvas)
            {
                this.Canvas = Canvas;
                this.B = B;
                ButtonImg = new Image { Width = 30 };
                ButtonImg.MouseDown += ButtonClicked;
            }

            private void ButtonClicked(object sender, MouseButtonEventArgs e)
            {
                if (B.Active)
                {
                    B.Clicked = !B.Clicked;
                    Update();
                    Layout.ButtonClicked(B.Id, B.Clicked);
                }
                else Control.SetInformation("Automata térköz inaktív.");
            }

            public void Update()
            {
                bitmap = new BitmapImage();
                bitmap.BeginInit();
                if (B.Clicked) bitmap.UriSource = new Uri(@"pack://application:,,,/button_clicked2.png");
                else bitmap.UriSource = new Uri(@"pack://application:,,,/button.png");
                bitmap.EndInit();
                ButtonImg.Source = bitmap;
            }

            public void Draw(Canvas Canvas)
            {
                //Update();
                Canvas.SetTop(ButtonImg, B.Coord.Y);
                Canvas.SetLeft(ButtonImg, B.Coord.X);
                Canvas.SetZIndex(ButtonImg, 1);
                Canvas.Children.Add(ButtonImg);
            }




        }




    }
}
