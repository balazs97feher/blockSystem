﻿using System.Windows.Controls;
using System.Windows.Shapes;

namespace simulation
{
    public struct Coordinates
    {
        public int X, Y;
    }

    public class Field
    {
        public Coordinates Coord;
        public const int SquareSize = 120;
        public Field(int x, int y)
        {
            Coord.X = x;
            Coord.Y = y;
        }

        public virtual void Draw(Canvas Canvas)
        {
            Rectangle Rect = new System.Windows.Shapes.Rectangle
            {
                Stroke = System.Windows.Media.Brushes.LightGray,
                StrokeThickness = 0.5,
                Fill = System.Windows.Media.Brushes.Green,
                Height = Field.SquareSize,
                Width = Field.SquareSize
            };

            Canvas.SetTop(Rect, Coord.Y);
            Canvas.SetLeft(Rect, Coord.X);
            Canvas.Children.Add(Rect);
        }

    }
}
