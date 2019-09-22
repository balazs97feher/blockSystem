using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace simulation
{
    public enum SwitchOrientation
    {
        CW, //a gyok jobb oldalon; CW fele valt
        CCW //a gyok bal oldalon; CCW fele valt
    }

    public class Switch : Track
    {
        public SwitchOrientation Orientation;
        public bool Straight;
        private Visual Display;

        public Switch(int x, int y, SwitchOrientation so) : base(x,y)
        {
            Straight = true;
            Orientation = so;
            Display = new Visual();
        }

        public override void Draw()
        {
            base.Draw();
            Display.Draw(this);
        }

        class Visual
        {
            public Line BackgroundStraight;
            public Line MiddleStraight;
            public Line BackgroundDiverging; //diverging
            public Line MiddleDiverging;  //diverging

            public Visual()
            {
                BackgroundStraight = new Line();
                MiddleStraight = new Line();
                BackgroundDiverging = new Line();
                MiddleDiverging = new Line();
            }

            public void Draw(Switch S)
            { 
                List<Line> Lines = new List<Line> { BackgroundStraight, MiddleStraight, BackgroundDiverging, MiddleDiverging };
                Lines.ForEach(L => L.StrokeThickness = 10);
                Lines.ForEach(L => Canvas.SetLeft(L, S.Coord.X));
                Lines.ForEach(L => Canvas.SetTop(L, S.Coord.Y));
                Lines.ForEach(L => L.Stroke = System.Windows.Media.Brushes.Black);
                if (S.Straight) MiddleStraight.Stroke = S.GetStateColor(TrackState.Default);
                else MiddleDiverging.Stroke = S.GetStateColor(TrackState.Default);
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
                MiddleDiverging.MouseDown += S.DoSwitch;
                Lines.ForEach(L => MainWindow.AppCanvas.Children.Add(L));
            }

        }



        private void DoSwitch(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Straight=!Straight;
            System.Windows.MessageBox.Show("Switch successful.","Feedback", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            
        }
    }
}
