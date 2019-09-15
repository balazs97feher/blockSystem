using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    public enum Orientation
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
        public Orientation TrackOrientation;
        public override void Draw()
        {
            base.Draw();
            
            switch(TrackOrientation)
            {
                case Orientation.HorizontalCenter:

                    break;
                case Orientation.VerticalCenter:

                    break;
                case Orientation.TopRight:

                    break;
                case Orientation.TopLeft:

                    break;
                case Orientation.BottomRight:

                    break;
                case Orientation.BottomLeft:

                    break;
            }
        }

    }
}
