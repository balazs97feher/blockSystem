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
        public static Signal SelectedSignal = null;

        public static Train Fecske;
        public static int SetSpeed;

        public static DispatcherTimer Timer;

        public static void Initialize()
        {
            Fecske = new Train(0);
            SetDeparture(0);
            Timer = new System.Windows.Threading.DispatcherTimer();
            Timer.Tick += new EventHandler(Tick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            Timer.Start();
        }

        private static void Tick(object sender, EventArgs e)
        {
            if (Fecske.DistanceFromEOB<=0)
            {
                FreeBlock(Fecske.Block);
                int NextBlock = Layout.GetNextBlock(Fecske.Block);
                Fecske.Block = NextBlock;
                Fecske.DistanceFromEOB = Layout.Blocks[NextBlock].Length;
                OccupyBlock(Fecske.Block);
            }
            Fecske.Roll();
            if (SetSpeed > Fecske.Speed) Fecske.Accelerate();
            else if (SetSpeed < Fecske.Speed) Fecske.Break();
        }



        static public void SetDeparture(int BlockId)
        {
            FreeBlock(Control.Fecske.Block);
            Fecske.Block = BlockId;
            Fecske.DistanceFromEOB = Layout.Blocks[BlockId].Length / 2;
            OccupyBlock(BlockId);
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
