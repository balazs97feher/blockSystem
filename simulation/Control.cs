using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace simulation
{
    public static class Control
    {
        public static bool DirectionCW = true;

        public static Train Fecske;
        public static int SetSpeed;

        public static bool Initialized = false; //do we have a train instance
        public static DispatcherTimer Timer;

        public static void Initialize()
        {
            Fecske = new Train(0);
            Initialized = true;
            SetDeparture(0);
            Timer = new System.Windows.Threading.DispatcherTimer();
            Timer.Tick += new EventHandler(Tick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            Timer.Start();
        }

        private static void Tick(object sender, EventArgs e)
        {
            int RemainingDistance = Fecske.Roll();
            if(RemainingDistance>0)
            {
                FreeBlock(Fecske.Block);
                int NextBlock = Layout.GetNextBlock(Fecske.Block);
                Fecske.Block = NextBlock;
                Fecske.DistanceFromEOB = Layout.Blocks[NextBlock].Length;
                OccupyBlock(Fecske.Block);
                Fecske.Roll(RemainingDistance);
            }
            if (SetSpeed > Fecske.Speed) Fecske.Accelerate();
            else if (SetSpeed < Fecske.Speed) Fecske.Break();
        }



        static public void SetDeparture(int BlockId)
        {
            if (Initialized)
            {
                FreeBlock(Control.Fecske.Block);
                Fecske.Block = BlockId;
                Fecske.Speed = 0;
                Fecske.DistanceFromEOB = Layout.Blocks[BlockId].Length / 2;
                OccupyBlock(BlockId);
            }
        }

        static public void OccupyBlock(int BlockId)
        {
            Layout.Blocks[BlockId].Occupy();
        }

        static public void FreeBlock(int BlockId)
        {
            Layout.Blocks[BlockId].Free();
        }

    }
}
