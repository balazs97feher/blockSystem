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

        public Switch(int x, int y, SwitchOrientation so) : base(x,y)
        {
            Orientation = so;
        }


        public override void Draw()
        {
            base.Draw();
            Line L1 = new Line();
            Line L2 = new Line();
            Line L3 = new Line();
            Line L4 = new Line(); //diverging
            Line L5 = new Line(); //diverging
            List<Line> Lines = new List<Line> { L1, L2, L3, L4, L5 };
            Lines.ForEach(L => L.StrokeThickness = 10);
            Lines.ForEach(L => Canvas.SetLeft(L, Coord.X));
            Lines.ForEach(L => Canvas.SetTop(L, Coord.Y));
            L1.Stroke = L4.Stroke = System.Windows.Media.Brushes.Black;
            L2.Stroke = L3.Stroke = L5.Stroke = GetStateColor(TrackState.Default);
            if (Straight == true) L2.Stroke = L3.Stroke = GetStateColor(State);
            else if (Orientation == SwitchOrientation.CW) L2.Stroke = L5.Stroke = GetStateColor(State); //set the color of the proper lights,
            else L3.Stroke = L5.Stroke = GetStateColor(State); //depending on the orientation and direction of the switch
            L4.StrokeStartLineCap = L4.StrokeEndLineCap = System.Windows.Media.PenLineCap.Round;
            L1.Y1 = L1.Y2 = L2.Y1 = L2.Y2 = L3.Y1 = L3.Y2 = Field.SquareSize / 2;
            L1.X1 = 0;
            L1.X2 = Field.SquareSize;
            L2.X1 = Field.SquareSize / 12;
            L2.X2 = Field.SquareSize * 5 / 12;
            L3.X1 = Field.SquareSize * 7 / 12;
            L3.X2 = Field.SquareSize * 11 / 12;
            if (Orientation == SwitchOrientation.CW)
            {
                L4.X1 = Field.SquareSize;
                L4.Y1 = Field.SquareSize / 2;
                L4.X2 = Field.SquareSize / 2;
                L4.Y2 = 0;
                L5.X1 = Field.SquareSize * 11 / 12;
                L5.Y1 = Field.SquareSize * 5 / 12;
                L5.X2 = Field.SquareSize * 7 / 12;
                L5.Y2 = Field.SquareSize * 1 / 12;
            }
            else
            {
                L4.X1 = 0;
                L4.Y1 = Field.SquareSize / 2;
                L4.X2 = Field.SquareSize / 2;
                L4.Y2 = 0;
                L5.X1 = Field.SquareSize * 1 / 12;
                L5.Y1 = Field.SquareSize * 5 / 12;
                L5.X2 = Field.SquareSize * 5 / 12;
                L5.Y2 = Field.SquareSize * 1 / 12;
            }
            L5.MouseDown += DoSwitch;
            Lines.ForEach(L => MainWindow.AppCanvas.Children.Add(L));
        }

        private void DoSwitch(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Straight = false;
            System.Windows.MessageBox.Show("Switch successful.","Feedback", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

        }
    }
}
