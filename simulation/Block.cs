using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    public class Block
    {
        public int Length;
        public List<Track> Tracks;
        public Signal CWSignal;
        public Signal CCWSignal; // signals at either end of the block
        public int Id;

        public int EOBSpeed; // maximum speed at which the train is allowed to reach the end of the current block

        public Block(int Id)
        {
            Length = 3000;
            Tracks = new List<Track>();
            this.Id = Id;
            CCWSignal = null;
            CWSignal = null;
            EOBSpeed = 120;
        }

        public void AddCWSignal(Signal S)
        {
            CWSignal = S;
            UpdateEOBSpeed();
        }

        public void AddCCWSignal(Signal S)
        {
            CCWSignal = S;
            UpdateEOBSpeed();
        }

        public virtual void UpdateEOBSpeed()
        {
            if (Layout.DirectionCW == true && CWSignal != null) EOBSpeed = CWSignal.MaxSpeed;
            else if (Layout.DirectionCW == false && CCWSignal != null) EOBSpeed = CCWSignal.MaxSpeed;
            else EOBSpeed = 120;
        }

        public virtual void Occupy()
        {
            Tracks.ForEach(t => t.SetState(TrackState.Occupied));
        }

        public virtual void Free()
        {
            Tracks.ForEach(t => t.SetState(TrackState.Default));
        }

        public virtual void Draw()
        {
            Tracks.ForEach(t => t.Draw());
            if (CWSignal != null) CWSignal.Draw();
            if (CCWSignal != null) CCWSignal.Draw();
        }

    }
}
