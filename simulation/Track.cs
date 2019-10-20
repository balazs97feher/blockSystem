using System;
using System.Windows.Media;

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

        public Track(int x, int y) : base(x, y)
        {
            State = TrackState.Default;
        }

        public abstract void SetState(TrackState S);

        public SolidColorBrush GetStateColor(TrackState S) // what color to paint a given section
        {
            switch (S)
            {
                case TrackState.Default:
                    return Brushes.Gray;
                case TrackState.Occupied:
                    return Brushes.Red;
                case TrackState.Highlighted:
                    return Brushes.White;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}
