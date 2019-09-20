using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    static class Layout
    {
        static public List<Block> Blocks=new List<Block>();
        static public Switch LeftSwitch;
        static public Switch RighSwitch;

        static public void Initialize()
        {
            for (int i = 1; i < 8; i++) Blocks.Add(new Block(i));
            int x, y;
            // ***************** Block#1 *****************
            x = Field.SquareSize * 2; y = Field.SquareSize * 2;
            Blocks[0].Tracks.Add(new StraightTrack(x, y, Orientation.BottomRight));
            x += Field.SquareSize;
            for (int i = 0; i < 4; i++)
            {
                Blocks[0].Tracks.Add(new StraightTrack(x, y, Orientation.HorizontalCenter));
                x += Field.SquareSize;
            }
            Blocks[0].Tracks.Add(new StraightTrack(x, y, Orientation.BottomLeft));
            // ***************** Block#2 *****************
            x = Field.SquareSize * 3; y = Field.SquareSize * 3;
            for (int i = 0; i < 4; i++)
            {
                Blocks[1].Tracks.Add(new StraightTrack(x, y, Orientation.HorizontalCenter));
                x += Field.SquareSize;
            }
            // ***************** Block#3 *****************
            x = Field.SquareSize * 2; y = Field.SquareSize * 3;
            LeftSwitch = new Switch(x, y, SwitchOrientation.CCW);
            Blocks[2].Tracks.Add(LeftSwitch);
            x -= Field.SquareSize;
            Blocks[2].Tracks.Add(new StraightTrack(x, y, Orientation.HorizontalCenter));
            x -= Field.SquareSize;
            Blocks[2].Tracks.Add(new StraightTrack(x, y, Orientation.TopRight));
            // ***************** Block#4 *****************
            x = 0; y = Field.SquareSize * 2;
            Blocks[3].Tracks.Add(new StraightTrack(x, y, Orientation.VerticalCenter));
            y -= Field.SquareSize;
            Blocks[3].Tracks.Add(new StraightTrack(x, y, Orientation.VerticalCenter));
            y -= Field.SquareSize;
            Blocks[3].Tracks.Add(new StraightTrack(x, y, Orientation.BottomRight));
            // ***************** Block#5 *****************
            x = Field.SquareSize * 1; y = 0;
            for (int i = 0; i < 8; i++)
            {
                Blocks[4].Tracks.Add(new StraightTrack(x, y, Orientation.HorizontalCenter));
                x += Field.SquareSize;
            }
            // ***************** Block#6 *****************
            x = Field.SquareSize * 9; y = 0;
            Blocks[5].Tracks.Add(new StraightTrack(x, y, Orientation.BottomLeft));
            y += Field.SquareSize;
            Blocks[5].Tracks.Add(new StraightTrack(x, y, Orientation.VerticalCenter));
            y += Field.SquareSize;
            Blocks[5].Tracks.Add(new StraightTrack(x, y, Orientation.VerticalCenter));
            y += Field.SquareSize;
            // ***************** Block#7 *****************
            x = Field.SquareSize * 9; y = Field.SquareSize * 3;
            Blocks[6].Tracks.Add(new StraightTrack(x, y, Orientation.TopLeft));
            x -= Field.SquareSize;
            Blocks[6].Tracks.Add(new StraightTrack(x, y, Orientation.HorizontalCenter));
            x -= Field.SquareSize;
            RighSwitch = new Switch(x, y, SwitchOrientation.CW);
            Blocks[6].Tracks.Add(RighSwitch);
            x -= Field.SquareSize;

        }

        static public void Draw()
        {
            Blocks.ForEach(b => b.Draw());
        }

    }
}
