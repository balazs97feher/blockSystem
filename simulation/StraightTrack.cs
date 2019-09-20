using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace simulation
{
    public enum Orientation
    {
        TopRight,
        TopLeft,
        BottomRight,
        BottomLeft,
        VerticalCenter,
        HorizontalCenter
    }

    public class StraightTrack : Track
    {
        public Orientation TrackOrientation;

        public StraightTrack(int x, int y, Orientation o) : base(x,y)
        {
            TrackOrientation = o;
        }

        public override void Draw()
        {
            base.Draw();

            switch (TrackOrientation)
            {
                case Orientation.HorizontalCenter:
                    DrawCenter();
                    break;
                case Orientation.VerticalCenter:
                    DrawCenter();
                    break;
                default:
                    DrawCorner();
                    break;
            }
        }

        void DrawCenter()
        {
            Line L1 = new Line();
            Line L2 = new Line();
            Line L3 = new Line();
            List<Line> Lines = new List<Line> { L1, L2, L3 };
            Lines.ForEach(L => L.StrokeThickness = 10);
            Lines.ForEach(L => Canvas.SetLeft(L, Coord.X));
            Lines.ForEach(L => Canvas.SetTop(L, Coord.Y));
            L1.Stroke = System.Windows.Media.Brushes.Black;
            L2.Stroke = L3.Stroke = GetStateColor(State);
            if (TrackOrientation == Orientation.VerticalCenter)
            {
                L1.X1 = L1.X2 = L2.X1 = L2.X2 = L3.X1 = L3.X2 = Field.SquareSize / 2;
                L1.Y1 = 0;
                L1.Y2 = Field.SquareSize;
                L2.Y1 = Field.SquareSize / 12;
                L2.Y2 = Field.SquareSize * 5 / 12;
                L3.Y1 = Field.SquareSize * 7 / 12;
                L3.Y2 = Field.SquareSize * 11 / 12;
            }
            else
            {
                L1.Y1 = L1.Y2 = L2.Y1 = L2.Y2 = L3.Y1 = L3.Y2 = Field.SquareSize / 2;
                L1.X1 = 0;
                L1.X2 = Field.SquareSize;
                L2.X1 = Field.SquareSize / 12;
                L2.X2 = Field.SquareSize * 5 / 12;
                L3.X1 = Field.SquareSize * 7 / 12;
                L3.X2 = Field.SquareSize * 11 / 12;
            }
            Lines.ForEach(L => MainWindow.AppCanvas.Children.Add(L));
        }

        void DrawCorner()
        {
            Line L1 = new Line();
            Line L2 = new Line();
            L1.StrokeThickness = L2.StrokeThickness = 10;
            Canvas.SetLeft(L1, Coord.X);
            Canvas.SetLeft(L2, Coord.X);
            Canvas.SetTop(L1, Coord.Y);
            Canvas.SetTop(L2, Coord.Y);
            L1.Stroke = System.Windows.Media.Brushes.Black;
            L2.Stroke = GetStateColor(State);
            L1.StrokeStartLineCap = L1.StrokeEndLineCap = System.Windows.Media.PenLineCap.Round;
            switch (TrackOrientation)
            {
                case Orientation.TopRight:
                    L1.X1 = Field.SquareSize / 2;
                    L1.Y1 = 0;
                    L1.X2 = Field.SquareSize;
                    L1.Y2 = Field.SquareSize / 2;
                    L2.X1 = Field.SquareSize * 7 / 12;
                    L2.Y1 = Field.SquareSize * 1 / 12;
                    L2.X2 = Field.SquareSize * 11 / 12;
                    L2.Y2 = Field.SquareSize * 5 / 12;
                    break;
                case Orientation.BottomLeft:
                    L1.X1 = 0;
                    L1.Y1 = Field.SquareSize / 2;
                    L1.X2 = Field.SquareSize / 2;
                    L1.Y2 = Field.SquareSize;
                    L2.X1 = Field.SquareSize * 1 / 12;
                    L2.Y1 = Field.SquareSize * 7 / 12;
                    L2.X2 = Field.SquareSize * 5 / 12;
                    L2.Y2 = Field.SquareSize * 11 / 12;
                    break;
                case Orientation.TopLeft:
                    L1.X1 = Field.SquareSize / 2;
                    L1.Y1 = 0;
                    L1.X2 = 0;
                    L1.Y2 = Field.SquareSize / 2;
                    L2.X1 = Field.SquareSize * 5 / 12;
                    L2.Y1 = Field.SquareSize * 1 / 12;
                    L2.X2 = Field.SquareSize * 1 / 12;
                    L2.Y2 = Field.SquareSize * 5 / 12;
                    break;
                case Orientation.BottomRight:
                    L1.X1 = Field.SquareSize;
                    L1.Y1 = Field.SquareSize / 2;
                    L1.X2 = Field.SquareSize / 2;
                    L1.Y2 = Field.SquareSize;
                    L2.X1 = Field.SquareSize * 11 / 12;
                    L2.Y1 = Field.SquareSize * 7 / 12;
                    L2.X2 = Field.SquareSize * 7 / 12;
                    L2.Y2 = Field.SquareSize * 11 / 12;
                    break;
            }
            MainWindow.AppCanvas.Children.Add(L1);
            MainWindow.AppCanvas.Children.Add(L2);
        }

    }
}
