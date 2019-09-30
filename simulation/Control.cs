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
    {
        public event PropertyChangedEventHandler PropertyChanged; // need to implement the above interface for data binding
        private void NotifyPropertyChanged(string Property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Property));
        }

        public Train Fecske;
        private int setSpeed;
        public int SetSpeed // the speed that's been set
        {
            get { return setSpeed; }
            set
            {
                setSpeed = value;
                NotifyPropertyChanged("SetSpeed");
            }
        }

        private Signal PrevSignal;
        private SignalState PrevSignalState;

        public static bool Initialized = false; //do we have a controller
        private DispatcherTimer Timer;

        public Control()
        {
            Fecske = new Train(0);
            Initialized = true;
            PrevSignal = null;
            SetDeparture(0);
            StartTimer();
        }

        private void StartTimer()
        {
            Timer = new System.Windows.Threading.DispatcherTimer();
            Timer.Tick += new EventHandler(Tick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            Timer.Start();
        }

        private void Tick(object sender, EventArgs e)
        {
            if (DepartureConstraints() == false) SetSpeed = 0;
            else if (SetSpeed > Layout.Blocks[Fecske.Block].EOBSpeed) SetSpeed = Layout.Blocks[Fecske.Block].EOBSpeed;
            Roll();
            if (SetSpeed > Fecske.Speed) Fecske.Accelerate();
            else if (SetSpeed < Fecske.Speed) Fecske.Break();
        }

        public void Roll()
        {
            int RemainingDistance = Fecske.Roll();
            if (RemainingDistance > 0) // the train crosses a block border
            {
                FreeBlock(Fecske.Block);
                SetSignal();
                int NextBlock = Layout.GetNextBlock(Fecske.Block);
                Fecske.Block = NextBlock;
                Fecske.DistanceFromEOB = Layout.Blocks[NextBlock].Length;
                OccupyBlock(Fecske.Block);
                SetDeceleration();
                Fecske.Roll(RemainingDistance);
            }
        }


        public void SetSignal() // sets the signal to red that the train has passed
        {
            ResetSignal();
            if (Layout.DirectionCW == true && Layout.Blocks[Fecske.Block].CWSignal != null)
            {
                PrevSignal = Layout.Blocks[Fecske.Block].CWSignal;
                PrevSignalState = PrevSignal.State;
                PrevSignal.SetState(SignalState.Red);
            }
            else if (Layout.DirectionCW == false && Layout.Blocks[Fecske.Block].CCWSignal != null)
            {
                PrevSignal = Layout.Blocks[Fecske.Block].CCWSignal;
                PrevSignalState = PrevSignal.State;
                PrevSignal.SetState(SignalState.Red);
            }
        }

        public void ResetSignal() // once the train is out of a block, it resets the previous block's signal to it's original state
        {
            if (PrevSignal != null) PrevSignal.SetState(PrevSignalState);
        }

        public void SetDeceleration() // sets the deceleration of the train according to its speed and the current block's speedlimit
        {
            if (Fecske.Speed != 0)
            {
                int T = (2 * Fecske.DistanceFromEOB) / (Fecske.Speed + Layout.Blocks[Fecske.Block].EOBSpeed); // suppose that the train's speed is greater than the EOBSpeed
                int D = (Fecske.Speed - Layout.Blocks[Fecske.Block].EOBSpeed) / T + 1;
                if (D < 1) Fecske.Deceleration = 1;
                else Fecske.Deceleration = D;
            }
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

        public bool DepartureConstraints()
        {
            if (Fecske.Block > 1) return true;
            if (Layout.DirectionCW == true)
            {
                if (Fecske.Block == 0)
                {
                    if (Layout.LeftSwitch.Straight == true)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    if (Layout.LeftSwitch.Straight == false)
                    {
                        return false;
                    }
                    return true;
                }
            }
            else
            {
                if (Fecske.Block == 0)
                {
                    if (Layout.RightSwitch.Straight == true)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    if (Layout.RightSwitch.Straight == false)
                    {
                        return false;
                    }
                    return true;
                }
            }
        }

    }
}
