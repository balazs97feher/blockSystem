using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace simulation
{
    public enum TrackOrientation
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
        public TrackOrientation Orientation;

        public StraightTrack(int x, int y, TrackOrientation o) : base(x,y)
        {
            Orientation = o;
        }

        public override void Draw()
        {
            base.Draw();

            switch (Orientation)
            {
                case TrackOrientation.HorizontalCenter:
                    DrawCenter();
                    break;
                case TrackOrientation.VerticalCenter:
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
            if (Orientation == TrackOrientation.VerticalCenter)
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
            List<Line> Lines = new List<Line> { L1, L2 };
            Lines.ForEach(L => L.StrokeThickness = 10);
            Lines.ForEach(L => Canvas.SetLeft(L, Coord.X));
            Lines.ForEach(L => Canvas.SetTop(L, Coord.Y));
            L1.Stroke = System.Windows.Media.Brushes.Black;
            L2.Stroke = GetStateColor(State);
            L1.StrokeStartLineCap = L1.StrokeEndLineCap = System.Windows.Media.PenLineCap.Round;
            switch (Orientation)
            {
                case TrackOrientation.TopRight:
                    L1.X1 = Field.SquareSize / 2;
                    L1.Y1 = 0;
                    L1.X2 = Field.SquareSize;
                    L1.Y2 = Field.SquareSize / 2;
                    L2.X1 = Field.SquareSize * 7 / 12;
                    L2.Y1 = Field.SquareSize * 1 / 12;
                    L2.X2 = Field.SquareSize * 11 / 12;
                    L2.Y2 = Field.SquareSize * 5 / 12;
                    break;
                case TrackOrientation.BottomLeft:
                    L1.X1 = 0;
                    L1.Y1 = Field.SquareSize / 2;
                    L1.X2 = Field.SquareSize / 2;
                    L1.Y2 = Field.SquareSize;
                    L2.X1 = Field.SquareSize * 1 / 12;
                    L2.Y1 = Field.SquareSize * 7 / 12;
                    L2.X2 = Field.SquareSize * 5 / 12;
                    L2.Y2 = Field.SquareSize * 11 / 12;
                    break;
                case TrackOrientation.TopLeft:
                    L1.X1 = Field.SquareSize / 2;
                    L1.Y1 = 0;
                    L1.X2 = 0;
                    L1.Y2 = Field.SquareSize / 2;
                    L2.X1 = Field.SquareSize * 5 / 12;
                    L2.Y1 = Field.SquareSize * 1 / 12;
                    L2.X2 = Field.SquareSize * 1 / 12;
                    L2.Y2 = Field.SquareSize * 5 / 12;
                    break;
                case TrackOrientation.BottomRight:
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
            Lines.ForEach(L => MainWindow.AppCanvas.Children.Add(L));
        }

    }
}
