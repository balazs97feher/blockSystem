using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    class Block
    {
        public int Length;
        public List<Track> Tracks;
        public Signal CWSignal;
        public Signal CCWSignal; // signals at either end of the block
        public int Id;

        public Block(int Id)
        {
            Length = 1000;
            Tracks = new List<Track>();
            this.Id = Id;
            CCWSignal = null;
            CWSignal = null;
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
