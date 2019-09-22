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
        public static DispatcherTimer Timer;

        public static void Initialize()
        {
            Timer = new System.Windows.Threading.DispatcherTimer();
            Timer.Tick += new EventHandler(Tick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            Timer.Start();
        }

        private static void Tick(object sender, EventArgs e)
        {
            
        }



        static public int GetNextBlock(int CurrentBlock)
        {
            if (DirectionCW)
            {
                if (CurrentBlock == 0) return 2;
                if (CurrentBlock > 0 && CurrentBlock < 6) return CurrentBlock + 1;
                if (Layout.RightSwitch.Straight == true) return 1; // Block#6
                else return 0;
            }
            else
            {
                if (CurrentBlock <= 1) return 6;
                if (CurrentBlock > 2 && CurrentBlock <= 6) return CurrentBlock - 1;
                if (Layout.LeftSwitch.Straight == true) return 1; // Block#3
                else return 0;
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
