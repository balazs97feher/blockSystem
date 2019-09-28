using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace simulation
{
    public class Control : INotifyPropertyChanged
    { // need to implement the above interface for data binding
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string Property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Property));
        }

        public static bool DirectionCW = true;

        public Train Fecske;
        private int setSpeed;
        public int SetSpeed
        {
            get { return setSpeed; }
            set
            {
                setSpeed = value;
                NotifyPropertyChanged("SetSpeed");
            }
        }


        public static bool Initialized = false; //do we have a controller
        public DispatcherTimer Timer;

        public Control()
        {
            Fecske = new Train(0);
            Initialized = true;
            SetDeparture(0);
            Timer = new System.Windows.Threading.DispatcherTimer();
            Timer.Tick += new EventHandler(Tick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            Timer.Start();
        }

        private void Tick(object sender, EventArgs e)
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
            if (setSpeed > Fecske.Speed) Fecske.Accelerate();
            else if (setSpeed < Fecske.Speed) Fecske.Break();
        }



        public void SetDeparture(int BlockId)
        {
            if (Initialized)
            {
                FreeBlock(Fecske.Block);
                Fecske.Block = BlockId;
                Fecske.Speed = 0;
                Fecske.DistanceFromEOB = Layout.Blocks[BlockId].Length / 2;
                OccupyBlock(BlockId);
            }
        }

        public void OccupyBlock(int BlockId)
        {
            Layout.Blocks[BlockId].Occupy();
        }

        public void FreeBlock(int BlockId)
        {
            Layout.Blocks[BlockId].Free();
        }

    }
}
