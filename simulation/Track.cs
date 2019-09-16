using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    public enum TrackState
    {
        Default,
        Occupied,
        Highlighted
    }
    public abstract class Track : Field
    {
        public Track StraightTrack;
        public virtual Track NextTrack() //merre haladjon tovabb a szerelveny
        {
            return StraightTrack; //alapesetben az egyenes szomszedot adja vissza (valto eseten lehet kitero is)
        }
        public TrackState State;


        public override void Draw()
        {
            base.Draw();
        }
        public System.Windows.Media.SolidColorBrush GetStateColor(TrackState s) //milyen szinnel rajzoljuk ki a szakaszt
        {
            switch(s)
            {
                case TrackState.Default:
                    return System.Windows.Media.Brushes.DarkGray;
                case TrackState.Occupied:
                    return System.Windows.Media.Brushes.Red;
                case TrackState.Highlighted:
                    return System.Windows.Media.Brushes.Yellow;
                default:
                    return System.Windows.Media.Brushes.DarkGray;
            }
        }


    }
}
