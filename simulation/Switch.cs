using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace simulation
{
    public enum SwitchOrientation
    {
        CW, // points on the left
        CCW // points on the right
    }

    public class Switch : Track
    {
        private SwitchOrientation Orientation;
        public bool Straight;
        private Visual Display;

        public Switch(int x, int y, SwitchOrientation so) : base(x, y)
        {
            Straight = true;
            Orientation = so;
            Display = new Visual(this);
        }

        public override void SetState(TrackState S)
        {
            State = S;
            Display.Update();
        }

        public void DoSwitch()
        {
            Straight = !Straight;
            Display.Update();
        }

        public override void Draw(Canvas Canvas)
        {
            base.Draw(Canvas);
            Display.Draw(Canvas);
        }

        class Visual
        {
            private Switch S;
            private Line BkgdStr; // Background straight
            private Line MidStr; // Middle straight
            private Line BkgdDiv; // Background diverging
            private Line MidDiv;  // Middle diverging

            public Visual(Switch S)
            {
                this.S = S;
                BkgdStr = new Line();
                MidStr = new Line();
                BkgdDiv = new Line();
                MidDiv = new Line();
            }

            public void Update()
            {
                if (S.Straight)
                {
                    if (S.State == TrackState.Occupied) MidStr.Stroke = S.GetStateColor(TrackState.Occupied);
                    else MidStr.Stroke = S.GetStateColor(TrackState.Highlighted);
                    MidDiv.Stroke = S.GetStateColor(TrackState.Default);
                }
                else
                {
                    if (S.State == TrackState.Occupied) MidDiv.Stroke = S.GetStateColor(TrackState.Occupied);
                    else MidDiv.Stroke = S.GetStateColor(TrackState.Highlighted);
                    MidStr.Stroke = S.GetStateColor(TrackState.Default);
                }
            }

            public void Draw(Canvas Canvas)
            {
                List<Line> Lines = new List<Line> { BkgdStr, MidStr, BkgdDiv, MidDiv };
                Lines.ForEach(L => L.StrokeThickness = 10);
                Lines.ForEach(L => Canvas.SetLeft(L, S.Coord.X));
                Lines.ForEach(L => Canvas.SetTop(L, S.Coord.Y));
                Lines.ForEach(L => L.Stroke = Brushes.Black);
                if (S.Straight)
                {
                    MidStr.Stroke = S.GetStateColor(TrackState.Highlighted);
                    MidDiv.Stroke = S.GetStateColor(TrackState.Default);
                }
                else
                {
                    MidDiv.Stroke = S.GetStateColor(TrackState.Highlighted);
                    MidStr.Stroke = S.GetStateColor(TrackState.Default);
                }
                BkgdDiv.StrokeStartLineCap = BkgdDiv.StrokeEndLineCap = PenLineCap.Round;
                BkgdStr.Y1 = BkgdStr.Y2 = MidStr.Y1 = MidStr.Y2 = Field.SquareSize / 2;
                BkgdStr.X1 = 0;
                BkgdStr.X2 = Field.SquareSize;
                MidStr.X1 = Field.SquareSize * 1 / 5;
                MidStr.X2 = Field.SquareSize * 4 / 5;
                if (S.Orientation == SwitchOrientation.CW)
                {
                    BkgdDiv.X1 = Field.SquareSize;
                    BkgdDiv.Y1 = Field.SquareSize / 2;
                    BkgdDiv.X2 = Field.SquareSize / 2;
                    BkgdDiv.Y2 = 0;
                    MidDiv.X1 = Field.SquareSize * 11 / 12;
                    MidDiv.Y1 = Field.SquareSize * 5 / 12;
                    MidDiv.X2 = Field.SquareSize * 7 / 12;
                    MidDiv.Y2 = Field.SquareSize * 1 / 12;
                }
                else
                {
                    BkgdDiv.X1 = 0;
                    BkgdDiv.Y1 = Field.SquareSize / 2;
                    BkgdDiv.X2 = Field.SquareSize / 2;
                    BkgdDiv.Y2 = 0;
                    MidDiv.X1 = Field.SquareSize * 1 / 12;
                    MidDiv.Y1 = Field.SquareSize * 5 / 12;
                    MidDiv.X2 = Field.SquareSize * 5 / 12;
                    MidDiv.Y2 = Field.SquareSize * 1 / 12;
                }
                Lines.ForEach(L => Canvas.Children.Add(L));
            }

        }

    }
}
