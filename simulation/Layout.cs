using System.Collections.Generic;
using System.Windows.Controls;

namespace simulation
{
    static class Layout
    {
        static public List<Block> Blocks = new List<Block>();
        static public Switch LeftSwitch;
        static public Switch RightSwitch;
        static public bool DirectionCW = true;
        static private Canvas Canvas;
        static public bool Initialized = false;


        static public void Initialize(Canvas C)
        {
            Canvas = C;
            int x = 0, y = 0;

            // ***************** Block#0 *****************
            Block B_0 = new Block(0, 2000);
            x = Field.SquareSize * 4; y = Field.SquareSize * 2;
            for (int i = 0; i < 4; i++)
            {
                B_0.Tracks.Add(new StraightTrack(x, y, TrackOrientation.HorizontalCenter));
                x += Field.SquareSize;
            }
            B_0.AddCWSignal(new Signal(480, 290, SignalOrientation.CW, false, Canvas)); // signals of block#0 are not settable,
            B_0.AddCCWSignal(new Signal(960, 310, SignalOrientation.CCW, false, Canvas)); // because switches are pointing straight 
            Blocks.Add(B_0);

            // ***************** Block#1 *****************
            Block B_1 = new Block(1, 2000);
            x = Field.SquareSize * 4; y = Field.SquareSize * 3;
            for (int i = 0; i < 4; i++)
            {
                B_1.Tracks.Add(new StraightTrack(x, y, TrackOrientation.HorizontalCenter));
                x += Field.SquareSize;
            }
            B_1.AddCWSignal(new Signal(480, 410, SignalOrientation.CW, true, Canvas)); // the only signal that is settable in the beginning
            B_1.AddCCWSignal(new Signal(960, 430, SignalOrientation.CCW, false, Canvas));
            Blocks.Add(B_1);

            // ***************** Block#2 *****************
            SwitchBlock B_2 = new SwitchBlock(2, 1000);
            x = Field.SquareSize * 3; y = Field.SquareSize * 2;
            B_2.Bottom = new StraightTrack(x, y, TrackOrientation.BottomRight);
            y += Field.SquareSize;
            LeftSwitch = new Switch(x, y, SwitchOrientation.CCW);
            B_2.S = LeftSwitch;
            x -= Field.SquareSize;
            B_2.Horizontal = new StraightTrack(x, y, TrackOrientation.HorizontalCenter);
            x -= Field.SquareSize;
            Blocks.Add(B_2);

            // ***************** Block#3 *****************
            Block B_3 = new Block(3, 3000);
            B_3.Tracks.Add(new StraightTrack(x, y, TrackOrientation.HorizontalCenter));
            x -= Field.SquareSize;
            B_3.Tracks.Add(new StraightTrack(x, y, TrackOrientation.TopRight));
            x = 0; y = Field.SquareSize * 2;
            B_3.Tracks.Add(new StraightTrack(x, y, TrackOrientation.VerticalCenter));
            y -= Field.SquareSize;
            B_3.Tracks.Add(new StraightTrack(x, y, TrackOrientation.VerticalCenter));
            y -= Field.SquareSize;
            B_3.Tracks.Add(new StraightTrack(x, y, TrackOrientation.BottomRight));
            B_3.AddCCWSignal(new Signal(230, 430, SignalOrientation.CCW, false, Canvas)); // track is occupied, signal is not settable
            Blocks.Add(B_3);

            // ***************** Block#4 *****************
            Block B_4 = new Block(4, 4000);
            x = Field.SquareSize * 1; y = 0;
            for (int i = 0; i < 10; i++)
            {
                B_4.Tracks.Add(new StraightTrack(x, y, TrackOrientation.HorizontalCenter));
                x += Field.SquareSize;
            }
            Blocks.Add(B_4);

            // ***************** Block#5 *****************
            Block B_5 = new Block(5, 3000);
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
            B_5.AddCWSignal(new Signal(1210, 410, SignalOrientation.CW, true, Canvas)); // track is not occupied, signal is settable
            Blocks.Add(B_5);

            // ***************** Block#6 *****************
            SwitchBlock B_6 = new SwitchBlock(6, 1000);
            x = Field.SquareSize * 9; y = Field.SquareSize * 3;
            B_6.Horizontal = new StraightTrack(x, y, TrackOrientation.HorizontalCenter);
            x -= Field.SquareSize;
            RightSwitch = new Switch(x, y, SwitchOrientation.CW);
            B_6.S = RightSwitch;
            y -= Field.SquareSize;
            B_6.Bottom = new StraightTrack(x, y, TrackOrientation.BottomLeft);
            Blocks.Add(B_6);

            Initialized = true;
        }

        static public void Draw()
        {
            DrawBackground(12, 4);
            Blocks.ForEach(b => b.Draw(Canvas));
        }

        static private void DrawBackground(int N, int M) // draws a NxM grid as background
        {
            int x = 0, y = 0;
            for (int i = 0; i < M; i++)
            {
                x = 0;
                for (int j = 0; j < N; j++)
                {
                    (new Field(x, y)).Draw(Canvas);
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

        static public bool DepartureConstraints(int Block)
        {
            if (Layout.Blocks[Block].EOBSpeed == 0) return false;
            else if (Block > 1) return true;
            else if (Layout.DirectionCW == true)
            {
                if (Block == 0)
                {
                    if (Layout.LeftSwitch.Straight == true || Blocks[0].CWSignal.State == SignalState.Red)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    if (Layout.LeftSwitch.Straight == false || Blocks[1].CWSignal.State == SignalState.Red)
                    {
                        return false;
                    }
                    return true;
                }
            }
            else
            {
                if (Block == 0)
                {
                    if (Layout.RightSwitch.Straight == true || Blocks[0].CCWSignal.State == SignalState.Red)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    if (Layout.RightSwitch.Straight == false || Blocks[1].CCWSignal.State == SignalState.Red)
                    {
                        return false;
                    }
                    return true;
                }
            }
        }

        static public void UpdateBlockMaxSpeed(int SignalId, SignalState State)
        {
            switch (SignalId)
            {
                case 0:
                    switch (State)
                    {
                        case SignalState.Red:
                            Blocks[0].EOBSpeed = 0;
                            break;
                        case SignalState.Green:
                            Blocks[0].EOBSpeed = 40;
                            Blocks[3].EOBSpeed = 80;
                            Blocks[4].EOBSpeed = 80;
                            break;
                        case SignalState.Yellow:
                            Blocks[0].EOBSpeed = 40;
                            Blocks[3].EOBSpeed = 40;
                            Blocks[4].EOBSpeed = 40;
                            break;
                    }
                    break;
                case 1:
                    switch (State)
                    {
                        case SignalState.Red:
                            Blocks[0].EOBSpeed = 0;
                            break;
                        case SignalState.Green:
                            Blocks[0].EOBSpeed = 40;
                            Blocks[5].EOBSpeed = 80;
                            Blocks[4].EOBSpeed = 80;
                            break;
                        case SignalState.Yellow:
                            Blocks[0].EOBSpeed = 40;
                            Blocks[5].EOBSpeed = 40;
                            Blocks[4].EOBSpeed = 40;
                            break;
                    }
                    break;
                case 2:
                    switch (State)
                    {
                        case SignalState.Red:
                            Blocks[1].EOBSpeed = 0;
                            break;
                        case SignalState.Green:
                            Blocks[1].EOBSpeed = 40;
                            Blocks[3].EOBSpeed = 80;
                            Blocks[4].EOBSpeed = 80;
                            break;
                        case SignalState.Yellow:
                            Blocks[1].EOBSpeed = 40;
                            Blocks[3].EOBSpeed = 40;
                            Blocks[4].EOBSpeed = 40;
                            break;
                    }
                    break;
                case 3:
                    switch (State)
                    {
                        case SignalState.Red:
                            Blocks[1].EOBSpeed = 0;
                            break;
                        case SignalState.Green:
                            Blocks[1].EOBSpeed = 40;
                            Blocks[5].EOBSpeed = 80;
                            Blocks[4].EOBSpeed = 80;
                            break;
                        case SignalState.Yellow:
                            Blocks[1].EOBSpeed = 40;
                            Blocks[5].EOBSpeed = 40;
                            Blocks[4].EOBSpeed = 40;
                            break;
                    }
                    break;
                case 4:
                    switch (State)
                    {
                        case SignalState.Red:
                            Blocks[3].EOBSpeed = 0;
                            break;
                        case SignalState.Green:
                            Blocks[3].EOBSpeed = 40;
                            break;
                        case SignalState.Yellow:
                            Blocks[3].EOBSpeed = 40;
                            break;
                    }
                    break;
                case 5:
                    switch (State)
                    {
                        case SignalState.Red:
                            Blocks[5].EOBSpeed = 0;
                            break;
                        case SignalState.Green:
                            Blocks[5].EOBSpeed = 40;
                            break;
                        case SignalState.Yellow:
                            Blocks[5].EOBSpeed = 40;
                            break;
                    }
                    break;

            }
        }

    }
}
