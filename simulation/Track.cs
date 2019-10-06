using System;

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
        public TrackState State;

        protected Track(int x, int y) : base(x,y)
        {
            State = TrackState.Default;
        }

        public abstract void SetState(TrackState S);

        public System.Windows.Media.SolidColorBrush GetStateColor(TrackState s) //milyen szinnel rajzoljuk ki a szakaszt
        {
            switch(s)
            {
                case TrackState.Default:
                    return System.Windows.Media.Brushes.Gray;
                case TrackState.Occupied:
                    return System.Windows.Media.Brushes.Red;
                case TrackState.Highlighted:
                    return System.Windows.Media.Brushes.White;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}
