using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;

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
            private Line BackgroundStraight;
            private Line MiddleStraight;
            private Line BackgroundDiverging; //diverging
            private Line MiddleDiverging;  //diverging

            public Visual(Switch S)
            {
                this.S = S;
                BackgroundStraight = new Line();
                MiddleStraight = new Line();
                BackgroundDiverging = new Line();
                MiddleDiverging = new Line();
            }

            public void Update()
            {
                if (S.Straight)
                {
                    if (S.State == TrackState.Occupied) MiddleStraight.Stroke = S.GetStateColor(TrackState.Occupied);
                    else MiddleStraight.Stroke = S.GetStateColor(TrackState.Highlighted);
                    MiddleDiverging.Stroke = S.GetStateColor(TrackState.Default);
                }
                else
                {
                    if (S.State == TrackState.Occupied) MiddleDiverging.Stroke = S.GetStateColor(TrackState.Occupied);
                    else MiddleDiverging.Stroke = S.GetStateColor(TrackState.Highlighted);
                    MiddleStraight.Stroke = S.GetStateColor(TrackState.Default);
                }
            }

            public void Draw(Canvas Canvas)
            {
                List<Line> Lines = new List<Line> { BackgroundStraight, MiddleStraight, BackgroundDiverging, MiddleDiverging };
                Lines.ForEach(L => L.StrokeThickness = 10);
                Lines.ForEach(L => Canvas.SetLeft(L, S.Coord.X));
                Lines.ForEach(L => Canvas.SetTop(L, S.Coord.Y));
                Lines.ForEach(L => L.Stroke = System.Windows.Media.Brushes.Black);
                if (S.Straight)
                {
                    MiddleStraight.Stroke = S.GetStateColor(TrackState.Highlighted);
                    MiddleDiverging.Stroke = S.GetStateColor(TrackState.Default);
                }
                else
                {
                    MiddleDiverging.Stroke = S.GetStateColor(TrackState.Highlighted);
                    MiddleStraight.Stroke = S.GetStateColor(TrackState.Default);
                }
                BackgroundDiverging.StrokeStartLineCap = BackgroundDiverging.StrokeEndLineCap = System.Windows.Media.PenLineCap.Round;
                BackgroundStraight.Y1 = BackgroundStraight.Y2 = MiddleStraight.Y1 = MiddleStraight.Y2 = Field.SquareSize / 2;
                BackgroundStraight.X1 = 0;
                BackgroundStraight.X2 = Field.SquareSize;
                MiddleStraight.X1 = Field.SquareSize * 1 / 5;
                MiddleStraight.X2 = Field.SquareSize * 4 / 5;
                if (S.Orientation == SwitchOrientation.CW)
                {
                    BackgroundDiverging.X1 = Field.SquareSize;
                    BackgroundDiverging.Y1 = Field.SquareSize / 2;
                    BackgroundDiverging.X2 = Field.SquareSize / 2;
                    BackgroundDiverging.Y2 = 0;
                    MiddleDiverging.X1 = Field.SquareSize * 11 / 12;
                    MiddleDiverging.Y1 = Field.SquareSize * 5 / 12;
                    MiddleDiverging.X2 = Field.SquareSize * 7 / 12;
                    MiddleDiverging.Y2 = Field.SquareSize * 1 / 12;
                }
                else
                {
                    BackgroundDiverging.X1 = 0;
                    BackgroundDiverging.Y1 = Field.SquareSize / 2;
                    BackgroundDiverging.X2 = Field.SquareSize / 2;
                    BackgroundDiverging.Y2 = 0;
                    MiddleDiverging.X1 = Field.SquareSize * 1 / 12;
                    MiddleDiverging.Y1 = Field.SquareSize * 5 / 12;
                    MiddleDiverging.X2 = Field.SquareSize * 5 / 12;
                    MiddleDiverging.Y2 = Field.SquareSize * 1 / 12;
                }
                Lines.ForEach(L => Canvas.Children.Add(L));
            }

        }

    }
}
