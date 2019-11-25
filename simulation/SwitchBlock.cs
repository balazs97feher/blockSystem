using System.Windows.Controls;

namespace simulation
{
    // a fork shaped block
    // contains exactly 3 Track instances: 1 horizontal, 1 switch, 1 bottomleft/bottomright
    class SwitchBlock : Block
    {
        public Switch S;
        public StraightTrack Bottom;
        public StraightTrack Horizontal;

        public SwitchBlock(int Id, int Length) : base(Id, Length) { EOBSpeed = 40; }

        public override void Occupy()
        {
            Horizontal.SetState(TrackState.Occupied);
            S.SetState(TrackState.Occupied);
            if (S.Straight == false) Bottom.SetState(TrackState.Occupied);
        }

        public override void Draw(Canvas Canvas)
        {
            S.Draw(Canvas);
            Bottom.Draw(Canvas);
            Horizontal.Draw(Canvas);
            if (CWSignal != null) CWSignal.Draw(Canvas);
            if (CCWSignal != null) CCWSignal.Draw(Canvas);
        }

        public override void Free()
        {
            S.SetState(TrackState.Default);
            Horizontal.SetState(TrackState.Default);
            Bottom.SetState(TrackState.Default);
        }

        public override void Highlight()
        {
            S.SetState(TrackState.Highlighted);
            Horizontal.SetState(TrackState.Highlighted);
            if (S.Straight == false) Bottom.SetState(TrackState.Highlighted);
        }
    }
}
