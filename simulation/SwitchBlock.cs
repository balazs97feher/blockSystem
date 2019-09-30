using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    // a fork shaped block
    // contains exactly 3 Track instances: 1 horizontal, 1 switch, 1 bottomleft/bottomright
    class SwitchBlock : Block
    {
        public Switch S;
        public StraightTrack Bottom;
        public StraightTrack Horizontal;

        public SwitchBlock(int Id) : base(Id) { EOBSpeed = 40; }

        public override void UpdateEOBSpeed()
        {
            EOBSpeed = 40;
        }

        public override void Occupy()
        {
            Horizontal.SetState(TrackState.Occupied);
            S.SetState(TrackState.Occupied);
            if (S.Straight == false) Bottom.SetState(TrackState.Occupied);
        }

        public override void Draw()
        {
            S.Draw();
            Bottom.Draw();
            Horizontal.Draw();
            if (CWSignal != null) CWSignal.Draw();
            if (CCWSignal != null) CCWSignal.Draw();
        }

        public override void Free()
        {
            S.SetState(TrackState.Default);
            Horizontal.SetState(TrackState.Default);
            Bottom.SetState(TrackState.Default);
        }
    }
}
