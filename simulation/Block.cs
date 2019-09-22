using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{
    class Block
    {
        public List<Track> Tracks;
        public Signal CWSignal;
        public Signal CCWSignal;
        public int Id;

        public Block(int Id)
        {
            Tracks = new List<Track>();
            this.Id = Id;
            CCWSignal = null;
            CWSignal = null;
        }

        public void Draw()
        {
            Tracks.ForEach(t => t.Draw());
            if (CWSignal != null) CWSignal.Draw();
            if (CCWSignal != null) CCWSignal.Draw();
        }

    }
}
