using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace simulation
{
    public struct Coordinates
    {
        public int X, Y;
    }

    public abstract class Field
    {
        public Coordinates Coord;
        protected const int SquareSize = 60;

        public virtual void Draw()
        {
            Rectangle Rect = new System.Windows.Shapes.Rectangle
            {
                Stroke = System.Windows.Media.Brushes.LightGray,
                Fill = System.Windows.Media.Brushes.Green,
                Height = SquareSize,
                Width = SquareSize
            };

            Canvas.SetTop(Rect, Coord.Y);
            Canvas.SetLeft(Rect, Coord.X);
            MainWindow.AppCanvas.Children.Add(Rect);
        }


        
    }
}
