using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

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
        private TrackOrientation Orientation;
        private Visual Display;

        public StraightTrack(int x, int y, TrackOrientation o) : base(x, y)
        {
            Orientation = o;
            Display = new Visual(this);
        }

        public override void SetState(TrackState S)
        {
            State = S;
            Display.Update();
        }

        public override void Draw(Canvas Canvas)
        {
            base.Draw(Canvas);
            Display.Draw(Canvas);
        }

        class Visual
        {
            private StraightTrack T;
            private Line Background;
            private Line Middle;

            public Visual(StraightTrack T)
            {
                this.T = T;
                Background = new Line();
                Middle = new Line();
            }

            public void Update()
            {
                Middle.Stroke = T.GetStateColor(T.State);
            }

            public void Draw(Canvas Canvas)
            {
                switch (T.Orientation)
                {
                    case TrackOrientation.HorizontalCenter:
                        DrawCenter(Canvas);
                        break;
                    case TrackOrientation.VerticalCenter:
                        DrawCenter(Canvas);
                        break;
                    default:
                        DrawCorner(Canvas);
                        break;
                }
            }

            private void DrawCenter(Canvas Canvas)
            {
                List<Line> Lines = new List<Line> { Background, Middle };
                Lines.ForEach(L => L.StrokeThickness = 10);
                Lines.ForEach(L => Canvas.SetLeft(L, T.Coord.X));
                Lines.ForEach(L => Canvas.SetTop(L, T.Coord.Y));
                Background.Stroke = Brushes.Black;
                Middle.Stroke = T.GetStateColor(T.State);
                if (T.Orientation == TrackOrientation.VerticalCenter)
                {
                    Background.X1 = Background.X2 = Middle.X1 = Middle.X2 = Field.SquareSize / 2;
                    Background.Y1 = 0;
                    Background.Y2 = Field.SquareSize;
                    Middle.Y1 = Field.SquareSize * 1 / 5;
                    Middle.Y2 = Field.SquareSize * 4 / 5;
                }
                else
                {
                    Background.Y1 = Background.Y2 = Middle.Y1 = Middle.Y2 = Field.SquareSize / 2;
                    Background.X1 = 0;
                    Background.X2 = Field.SquareSize;
                    Middle.X1 = Field.SquareSize * 1 / 5;
                    Middle.X2 = Field.SquareSize * 4 / 5;
                }
                Lines.ForEach(L => Canvas.Children.Add(L));
            }

            private void DrawCorner(Canvas Canvas)
            {
                List<Line> Lines = new List<Line> { Background, Middle };
                Lines.ForEach(L => L.StrokeThickness = 10);
                Lines.ForEach(L => Canvas.SetLeft(L, T.Coord.X));
                Lines.ForEach(L => Canvas.SetTop(L, T.Coord.Y));
                Background.Stroke = Brushes.Black;
                Middle.Stroke = T.GetStateColor(T.State);
                Background.StrokeStartLineCap = Background.StrokeEndLineCap = PenLineCap.Round;
                switch (T.Orientation)
                {
                    case TrackOrientation.TopRight:
                        Background.X1 = Field.SquareSize / 2;
                        Background.Y1 = 0;
                        Background.X2 = Field.SquareSize;
                        Background.Y2 = Field.SquareSize / 2;
                        Middle.X1 = Field.SquareSize * 7 / 12;
                        Middle.Y1 = Field.SquareSize * 1 / 12;
                        Middle.X2 = Field.SquareSize * 11 / 12;
                        Middle.Y2 = Field.SquareSize * 5 / 12;
                        break;
                    case TrackOrientation.BottomLeft:
                        Background.X1 = 0;
                        Background.Y1 = Field.SquareSize / 2;
                        Background.X2 = Field.SquareSize / 2;
                        Background.Y2 = Field.SquareSize;
                        Middle.X1 = Field.SquareSize * 1 / 12;
                        Middle.Y1 = Field.SquareSize * 7 / 12;
                        Middle.X2 = Field.SquareSize * 5 / 12;
                        Middle.Y2 = Field.SquareSize * 11 / 12;
                        break;
                    case TrackOrientation.TopLeft:
                        Background.X1 = Field.SquareSize / 2;
                        Background.Y1 = 0;
                        Background.X2 = 0;
                        Background.Y2 = Field.SquareSize / 2;
                        Middle.X1 = Field.SquareSize * 5 / 12;
                        Middle.Y1 = Field.SquareSize * 1 / 12;
                        Middle.X2 = Field.SquareSize * 1 / 12;
                        Middle.Y2 = Field.SquareSize * 5 / 12;
                        break;
                    case TrackOrientation.BottomRight:
                        Background.X1 = Field.SquareSize;
                        Background.Y1 = Field.SquareSize / 2;
                        Background.X2 = Field.SquareSize / 2;
                        Background.Y2 = Field.SquareSize;
                        Middle.X1 = Field.SquareSize * 11 / 12;
                        Middle.Y1 = Field.SquareSize * 7 / 12;
                        Middle.X2 = Field.SquareSize * 7 / 12;
                        Middle.Y2 = Field.SquareSize * 11 / 12;
                        break;
                }
                Lines.ForEach(L => Canvas.Children.Add(L));
            }

        }

    }
}
