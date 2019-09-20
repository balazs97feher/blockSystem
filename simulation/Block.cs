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
        public int Id;

        public Block(int Id)
        {
            Tracks = new List<Track>();
            this.Id = Id;
        }

        public void Draw()
        {
            Tracks.ForEach(t => t.Draw());
        }

    }
}
