using System.Collections.Generic;
using System.Windows.Controls;

namespace simulation
{
    public class Block
    {
        public int Length;
        public List<Track> Tracks;
        public Signal CWSignal;
        public Signal CCWSignal; // signals at either end of the block (optional)
        public int Id;

        public int EOBSpeed; // maximum speed at which the train is allowed to reach the end of the current block

        public Block(int Id, int Length)
        {
            this.Length = Length;
            Tracks = new List<Track>();
            this.Id = Id;
            CCWSignal = null;
            CWSignal = null;
            EOBSpeed = 0;
        }

        public void AddCWSignal(Signal S)
        {
            CWSignal = S;
        }

        public void AddCCWSignal(Signal S)
        {
            CCWSignal = S;
        }

        public virtual void Occupy()
        {
            Tracks.ForEach(t => t.SetState(TrackState.Occupied));
        }

        public virtual void Free()
        {
            Tracks.ForEach(t => t.SetState(TrackState.Default));
        }

        public virtual void Draw(Canvas Canvas)
        {
            Tracks.ForEach(t => t.Draw(Canvas));
            if (CWSignal != null) CWSignal.Draw(Canvas);
            if (CCWSignal != null) CCWSignal.Draw(Canvas);
        }

    }
}
