using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace simulation
{
    static class Layout
    {
        static public List<Block> Blocks=new List<Block>();
        static public Switch LeftSwitch;
        static public Switch RightSwitch;
        static public bool DirectionCW = true;

        static public void Initialize()
        {
            int x = 0, y = 0;
            DrawBackground();

            // ***************** Block#0 *****************
            Block B_0 = new Block(0);
            x = Field.SquareSize * 4; y = Field.SquareSize * 2;
            for (int i = 0; i < 4; i++)
            {
                B_0.Tracks.Add(new StraightTrack(x, y, TrackOrientation.HorizontalCenter));
                x += Field.SquareSize;
            }
            B_0.AddCWSignal(new Signal(480, 340, SignalOrientation.CW, B_0));
            B_0.AddCCWSignal(new Signal(960, 310, SignalOrientation.CCW, B_0));
            Blocks.Add(B_0);

            // ***************** Block#1 *****************
            Block B_1 = new Block(1);
            x = Field.SquareSize * 4; y = Field.SquareSize * 3;
            for (int i = 0; i < 4; i++)
            {
                B_1.Tracks.Add(new StraightTrack(x, y, TrackOrientation.HorizontalCenter));
                x += Field.SquareSize;
            }
            B_1.AddCWSignal(new Signal(480, 410, SignalOrientation.CW, B_1));
            B_1.AddCCWSignal(new Signal(960, 380, SignalOrientation.CCW, B_1));
            Blocks.Add(B_1);

            // ***************** Block#2 *****************
            SwitchBlock B_2 = new SwitchBlock(2);
            x = Field.SquareSize * 3; y = Field.SquareSize * 2;
            B_2.Bottom= new StraightTrack(x, y, TrackOrientation.BottomRight);
            y += Field.SquareSize;
            LeftSwitch = new Switch(x, y, SwitchOrientation.CCW);
            B_2.S = LeftSwitch;
            x -= Field.SquareSize;
            B_2.Horizontal= new StraightTrack(x, y, TrackOrientation.HorizontalCenter);
            x -= Field.SquareSize;
            Blocks.Add(B_2);

            // ***************** Block#3 *****************
            Block B_3 = new Block(3);
            B_3.Tracks.Add(new StraightTrack(x, y, TrackOrientation.HorizontalCenter));
            x -= Field.SquareSize;
            B_3.Tracks.Add(new StraightTrack(x, y, TrackOrientation.TopRight));
            x = 0; y = Field.SquareSize * 2;
            B_3.Tracks.Add(new StraightTrack(x, y, TrackOrientation.VerticalCenter));
            y -= Field.SquareSize;
            B_3.Tracks.Add(new StraightTrack(x, y, TrackOrientation.VerticalCenter));
            y -= Field.SquareSize;
            B_3.Tracks.Add(new StraightTrack(x, y, TrackOrientation.BottomRight));
            B_3.AddCCWSignal(new Signal(230, 380, SignalOrientation.CCW, B_3));
            Blocks.Add(B_3);

            // ***************** Block#4 *****************
            Block B_4 = new Block(4);
            x = Field.SquareSize * 1; y = 0;
            for (int i = 0; i < 10; i++)
            {
                B_4.Tracks.Add(new StraightTrack(x, y, TrackOrientation.HorizontalCenter));
                x += Field.SquareSize;
            }
            Blocks.Add(B_4);

            // ***************** Block#5 *****************
            Block B_5 = new Block(5);
            x = Field.SquareSize * 11; y = 0;
            B_5.Tracks.Add(new StraightTrack(x, y, TrackOrientation.BottomLeft));
            y += Field.SquareSize;
            B_5.Tracks.Add(new StraightTrack(x, y, TrackOrientation.VerticalCenter));
            y += Field.SquareSize;
            B_5.Tracks.Add(new StraightTrack(x, y, TrackOrientation.VerticalCenter));
            y += Field.SquareSize;
            B_5.Tracks.Add(new StraightTrack(x, y, TrackOrientation.TopLeft));
            x -= Field.SquareSize;
            B_5.Tracks.Add(new StraightTrack(x, y, TrackOrientation.HorizontalCenter));
            B_5.AddCWSignal(new Signal(1210, 410, SignalOrientation.CW, B_5));
            Blocks.Add(B_5);

            // ***************** Block#6 *****************
            SwitchBlock B_6 = new SwitchBlock(6);
            x = Field.SquareSize * 9; y = Field.SquareSize * 3;
            B_6.Horizontal= new StraightTrack(x, y, TrackOrientation.HorizontalCenter);
            x -= Field.SquareSize;
            RightSwitch = new Switch(x, y, SwitchOrientation.CW);
            B_6.S= RightSwitch;
            y -= Field.SquareSize;
            B_6.Bottom= new StraightTrack(x, y, TrackOrientation.BottomLeft);
            Blocks.Add(B_6);
        }

        static public void Draw()
        {
            Blocks.ForEach(b => b.Draw());
        }

        static private void DrawBackground()
        {
            int x = 0, y = 0;
            for (int i = 0; i < 4; i++)
            {
                x = 0;
                for (int j = 0; j < 12; j++)
                {
                    (new Field(x, y)).Draw();
                    x += Field.SquareSize;
                }
                y += Field.SquareSize;
            }
        }

        static public int GetNextBlock(int CurrentBlock)
        {
            if (DirectionCW)
            {
                if (CurrentBlock == 0) return 2;
                if (CurrentBlock > 0 && CurrentBlock < 6) return CurrentBlock + 1;
                if (Layout.RightSwitch.Straight == true) return 1; // Block#6
                else return 0;
            }
            else
            {
                if (CurrentBlock <= 1) return 6;
                if (CurrentBlock > 2 && CurrentBlock <= 6) return CurrentBlock - 1;
                if (Layout.LeftSwitch.Straight == true) return 1; // Block#3
                else return 0;
            }
        }

        static public void SetCWDirection(bool IsCW)
        {
            DirectionCW = IsCW;
            Blocks.ForEach(B => B.UpdateEOBSpeed());
        }

    }
}
