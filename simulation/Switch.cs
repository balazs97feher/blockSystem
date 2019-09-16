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
        PointBladesLeft, //a gyok jobb oldalon
        PointBladesRight //a gyok bal oldalon
    }

    public class Switch : Track
    {
        public Track DivergingTrack;
        public bool Straight;
        public SwitchOrientation Orientation;
        public override Track NextTrack()
        {
            if (Straight) return StraightTrack;
            else return DivergingTrack; //MUKODES?????? mindket iranyban????
        }





        public override void Draw()
        {
            base.Draw();
            Line L1 = new Line();
            Line L2 = new Line();
            Line L3 = new Line();
            Line L4 = new Line(); //diverging
            Line L5 = new Line(); //diverging
            L1.StrokeThickness = L2.StrokeThickness = L3.StrokeThickness = L4.StrokeThickness = L5.StrokeThickness = 10;
            Canvas.SetLeft(L1, Coord.X);
            Canvas.SetLeft(L2, Coord.X);
            Canvas.SetLeft(L3, Coord.X);
            Canvas.SetLeft(L4, Coord.X);
            Canvas.SetLeft(L5, Coord.X);
            Canvas.SetTop(L1, Coord.Y);
            Canvas.SetTop(L2, Coord.Y);
            Canvas.SetTop(L3, Coord.Y);
            Canvas.SetTop(L4, Coord.Y);
            Canvas.SetTop(L5, Coord.Y);
            L1.Stroke = L4.Stroke = System.Windows.Media.Brushes.Black;
            L2.Stroke = L3.Stroke = L5.Stroke = GetStateColor(TrackState.Default);
            if (Straight == true) L2.Stroke = L3.Stroke = GetStateColor(State);
            else if (Orientation == SwitchOrientation.PointBladesLeft) L2.Stroke = L5.Stroke = GetStateColor(State); //set the color of the proper lights,
            else L3.Stroke = L5.Stroke = GetStateColor(State); //depending on the orientation and direction of the switch
            L4.StrokeStartLineCap = L4.StrokeEndLineCap = System.Windows.Media.PenLineCap.Round;
            L1.Y1 = L1.Y2 = L2.Y1 = L2.Y2 = L3.Y1 = L3.Y2 = Field.SquareSize / 2;
            L1.X1 = 0;
            L1.X2 = Field.SquareSize;
            L2.X1 = Field.SquareSize / 12;
            L2.X2 = Field.SquareSize * 5 / 12;
            L3.X1 = Field.SquareSize * 7 / 12;
            L3.X2 = Field.SquareSize * 11 / 12;
            if (Orientation == SwitchOrientation.PointBladesLeft)
            {
                L4.X1 = Field.SquareSize;
                L4.Y1 = Field.SquareSize / 2;
                L4.X2 = Field.SquareSize / 2;
                L4.Y2 = Field.SquareSize;
                L5.X1 = Field.SquareSize * 11 / 12;
                L5.Y1 = Field.SquareSize * 7 / 12;
                L5.X2 = Field.SquareSize * 7 / 12;
                L5.Y2 = Field.SquareSize * 11 / 12;
            }
            else
            {
                L4.X1 = 0;
                L4.Y1 = Field.SquareSize / 2;
                L4.X2 = Field.SquareSize / 2;
                L4.Y2 = Field.SquareSize;
                L5.X1 = Field.SquareSize * 1 / 12;
                L5.Y1 = Field.SquareSize * 7 / 12;
                L5.X2 = Field.SquareSize * 5 / 12;
                L5.Y2 = Field.SquareSize * 11 / 12;
            }
            MainWindow.AppCanvas.Children.Add(L1);
            MainWindow.AppCanvas.Children.Add(L2);
            MainWindow.AppCanvas.Children.Add(L3);
            MainWindow.AppCanvas.Children.Add(L4);
            MainWindow.AppCanvas.Children.Add(L5);
        }

    }
}
