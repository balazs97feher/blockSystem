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
        public static bool Clockwise = true; //default direction is clockwise
        public Track NextStraightTrack;
        public Track PrevStraightTrack;
        public TrackState State;

        protected Track(int x, int y) : base(x,y)
        {
            State = TrackState.Default;
        }
        public virtual Track GetNextTrack() //merre haladjon tovabb a szerelveny
        {
            if (Clockwise) return NextStraightTrack;
            else return PrevStraightTrack;
        }
        


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
                    return System.Windows.Media.Brushes.White;
                default:
                    return System.Windows.Media.Brushes.DarkGray;
            }
        }


    }
}
