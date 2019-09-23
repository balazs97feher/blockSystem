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

        static public void Initialize()
        {
            for (int i = 1; i < 8; i++) Blocks.Add(new Block(i));
            int x, y;
            // ***************** Block#0 *****************
            x = Field.SquareSize * 2; y = Field.SquareSize * 2;
            Blocks[0].Tracks.Add(new StraightTrack(x, y, TrackOrientation.BottomRight));
            x += Field.SquareSize;
            for (int i = 0; i < 4; i++)
            {
                Blocks[0].Tracks.Add(new StraightTrack(x, y, TrackOrientation.HorizontalCenter));
                x += Field.SquareSize;
            }
            Blocks[0].Tracks.Add(new StraightTrack(x, y, TrackOrientation.BottomLeft));
            Blocks[0].CWSignal = new Signal(360, 340, SignalOrientation.CW);
            Blocks[0].CCWSignal = new Signal(840, 310, SignalOrientation.CCW);
            Blocks[0].Length = 1000;
            // ***************** Block#1 *****************
            x = Field.SquareSize * 3; y = Field.SquareSize * 3;
            for (int i = 0; i < 4; i++)
            {
                Blocks[1].Tracks.Add(new StraightTrack(x, y, TrackOrientation.HorizontalCenter));
                x += Field.SquareSize;
            }
            Blocks[1].CWSignal = new Signal(285, 410, SignalOrientation.CW);
            Blocks[1].CCWSignal = new Signal(915, 380, SignalOrientation.CCW);
            Blocks[1].Length = 1000;
            // ***************** Block#2 *****************
            x = Field.SquareSize * 2; y = Field.SquareSize * 3;
            LeftSwitch = new Switch(x, y, SwitchOrientation.CCW);
            Blocks[2].Tracks.Add(LeftSwitch);
            x -= Field.SquareSize;
            Blocks[2].Tracks.Add(new StraightTrack(x, y, TrackOrientation.HorizontalCenter));
            x -= Field.SquareSize;
            Blocks[2].Tracks.Add(new StraightTrack(x, y, TrackOrientation.TopRight));
            Blocks[2].CCWSignal = new Signal(230, 380, SignalOrientation.CCW);
            Blocks[2].Length = 1000;
            // ***************** Block#3 *****************
            x = 0; y = Field.SquareSize * 2;
            Blocks[3].Tracks.Add(new StraightTrack(x, y, TrackOrientation.VerticalCenter));
            y -= Field.SquareSize;
            Blocks[3].Tracks.Add(new StraightTrack(x, y, TrackOrientation.VerticalCenter));
            y -= Field.SquareSize;
            Blocks[3].Tracks.Add(new StraightTrack(x, y, TrackOrientation.BottomRight));
            Blocks[3].Length = 1000;
            // ***************** Block#4 *****************
            x = Field.SquareSize * 1; y = 0;
            for (int i = 0; i < 8; i++)
            {
                Blocks[4].Tracks.Add(new StraightTrack(x, y, TrackOrientation.HorizontalCenter));
                x += Field.SquareSize;
            }
            Blocks[4].Length = 1000;
            // ***************** Block#5 *****************
            x = Field.SquareSize * 9; y = 0;
            Blocks[5].Tracks.Add(new StraightTrack(x, y, TrackOrientation.BottomLeft));
            y += Field.SquareSize;
            Blocks[5].Tracks.Add(new StraightTrack(x, y, TrackOrientation.VerticalCenter));
            y += Field.SquareSize;
            Blocks[5].Tracks.Add(new StraightTrack(x, y, TrackOrientation.VerticalCenter));
            y += Field.SquareSize;
            Blocks[5].Length = 1000;
            // ***************** Block#6 *****************
            x = Field.SquareSize * 9; y = Field.SquareSize * 3;
            Blocks[6].Tracks.Add(new StraightTrack(x, y, TrackOrientation.TopLeft));
            x -= Field.SquareSize;
            Blocks[6].Tracks.Add(new StraightTrack(x, y, TrackOrientation.HorizontalCenter));
            x -= Field.SquareSize;
            RightSwitch = new Switch(x, y, SwitchOrientation.CW);
            Blocks[6].Tracks.Add(RightSwitch);
            x -= Field.SquareSize;
            Blocks[6].CWSignal = new Signal(970, 410, SignalOrientation.CW);
            Blocks[6].Length = 1000;
        }

        static public void Draw()
        {
            Blocks.ForEach(b => b.Draw());
        }

        static public int GetNextBlock(int CurrentBlock)
        {
            if (Control.DirectionCW)
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





    }
}
