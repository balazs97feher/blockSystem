using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;

namespace simulation
{
    public class Control : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged; // need to implement the above interface for data binding
        private void NotifyPropertyChanged(string Property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Property));
        }

        private string information; // information, instructions and warnings for the user
        public string Information
        {
            get { return information; }
            set
            {
                information = value;
                NotifyPropertyChanged("Information");
            }
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

        public static bool Initialized = false; //do we have a controller
        private DispatcherTimer Timer;

        public Control()
        {
            Fecske = new Train(0);
            Initialized = true;
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
            if (DepartureConstraints() == false)
            {
                SetSpeed = 0;
            }
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
            if (Layout.DirectionCW == true && Layout.Blocks[Fecske.Block].CWSignal != null)
                Layout.Blocks[Fecske.Block].CWSignal.SetState(SignalState.Red);
            else if (Layout.DirectionCW == false && Layout.Blocks[Fecske.Block].CCWSignal != null)
                Layout.Blocks[Fecske.Block].CCWSignal.SetState(SignalState.Red);
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

        public void SwitchRight()
        {
            if (Layout.Blocks[0].CCWSignal.State == SignalState.Red && Layout.Blocks[1].CCWSignal.State == SignalState.Red
                && Layout.Blocks[5].CWSignal.State == SignalState.Red)
            {
                Layout.RightSwitch.DoSwitch();
                Layout.Blocks[0].CCWSignal.Settable = !Layout.Blocks[0].CCWSignal.Settable;
                Layout.Blocks[1].CCWSignal.Settable = !Layout.Blocks[1].CCWSignal.Settable;
            }
            else MessageBox.Show("Switch cannot be done, because signals permit passing.", "Warning",
                MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void SwitchLeft()
        {
            if (Layout.Blocks[0].CWSignal.State == SignalState.Red && Layout.Blocks[1].CWSignal.State == SignalState.Red
                && Layout.Blocks[3].CCWSignal.State == SignalState.Red)
            {
                Layout.LeftSwitch.DoSwitch();
                Layout.Blocks[0].CWSignal.Settable = !Layout.Blocks[0].CWSignal.Settable;
                Layout.Blocks[1].CWSignal.Settable = !Layout.Blocks[1].CWSignal.Settable;
            }
            else MessageBox.Show("Switch cannot be done, because signals permit passing.", "Warning",
                MessageBoxButton.OK, MessageBoxImage.Warning);
        }

    }
}
