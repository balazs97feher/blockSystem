using System;
using System.ComponentModel;
using System.Windows.Threading;
using System.Windows;

namespace simulation
{
    public class Control : INotifyPropertyChanged
    {
        private static Control Instance;
        public static void SetInformation(string Text) { Instance.Information = Text; }
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

        private string information;
        public string Information // info displayed for the user
        {
            get { return information; }
            set
            {
                information = value;
                NotifyPropertyChanged("Information");
            }
        }

        private DispatcherTimer Timer;

        private Control()
        {
            Instance = this;
            Fecske = new Train(0);
        }

        public static Control CreateController()
        {
            if (Instance != null) return Instance;
            else return new Control();
        }

        public void Initialize()
        {
            SetDeparture(0);
            SetCWDirection(true);
            StartTimer();
        }

        private void StartTimer()
        {
            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(Tick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            Timer.Start();
        }

        private void Tick(object sender, EventArgs e)
        {
            if (SetSpeed > 0 && (Layout.DepartureConstraints(Fecske.Block) == false || Layout.Blocks[Fecske.Block].EOBSpeed == 0))
            {
                SetSpeed = 0;
                SetInformation("Indulás megtiltva. Ellenőrizze a váltók és a jelzők állását!");
            }
            else if (SetSpeed > Layout.Blocks[Fecske.Block].EOBSpeed)
            {
                SetSpeed = Layout.Blocks[Fecske.Block].EOBSpeed;
                SetInformation("Maximális sebesség: " + Layout.Blocks[Fecske.Block].EOBSpeed.ToString() + " km/h");
            }
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
                SetSignalRed();
                int NextBlockId = Layout.GetNextBlock(Fecske.Block);
                Fecske.Block = NextBlockId;
                SetInformation("Maximális sebesség: " + Layout.Blocks[Fecske.Block].EOBSpeed.ToString() + " km/h");
                SecureStation(NextBlockId);
                Fecske.DistanceFromEOB = Layout.Blocks[NextBlockId].Length;
                OccupyBlock(Fecske.Block);
                SetDeceleration();
                Fecske.Roll(RemainingDistance);
            }
        }

        public void SecureStation(int BlockId)
        {
            if (BlockId < 2)
            {
                Layout.Blocks[3].CCWSignal.Settable = false;
                Layout.Blocks[5].CWSignal.Settable = false;
            }
            else if (BlockId == 2 && Layout.DirectionCW == true) Layout.Blocks[5].CWSignal.Settable = true;
            else if (BlockId == 6 && Layout.DirectionCW == false) Layout.Blocks[3].CCWSignal.Settable = true;
        }


        public void SetSignalRed() // sets the signal to red that the train has passed
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
            if (Fecske.Block < 2 && Fecske.Speed == 0)
            {
                FreeBlock(Fecske.Block);
                Fecske.Block = BlockId;
                Fecske.Speed = 0;
                Fecske.DistanceFromEOB = Layout.Blocks[BlockId].Length / 2;
                OccupyBlock(BlockId);
                SetInformation("Induló vágány megváltozott.");
            }
            else SetInformation("Induló vágány megváltoztatása jelenleg nem lehetséges.");
        }

        public void OccupyBlock(int BlockId)
        {
            Layout.Blocks[BlockId].Occupy();
        }

        public void FreeBlock(int BlockId)
        {
            Layout.Blocks[BlockId].Free();
        }

        public void SwitchRight()
        {
            if ((Layout.Blocks[0].CCWSignal.State == SignalState.Red && Layout.Blocks[1].CCWSignal.State == SignalState.Red)
                || Layout.Blocks[5].CWSignal.State == SignalState.Red)
            {
                if (Layout.RightSwitch.DoSwitch() == true)
                {
                    SetInformation("Váltó sikeresen átállítva.");
                    if (Layout.DirectionCW == false)
                    {
                        Layout.Blocks[0].CCWSignal.Settable = !Layout.Blocks[0].CCWSignal.Settable;
                        Layout.Blocks[1].CCWSignal.Settable = !Layout.Blocks[1].CCWSignal.Settable;
                    }
                }
                else SetInformation("Ez a váltó jelenleg nem állítható át.");
            }
            else SetInformation("Ez a váltó jelenleg nem állítható át.");
        }

        public void SwitchLeft()
        {
            if ((Layout.Blocks[0].CWSignal.State == SignalState.Red && Layout.Blocks[1].CWSignal.State == SignalState.Red)
                || Layout.Blocks[3].CCWSignal.State == SignalState.Red)
            {
                if (Layout.LeftSwitch.DoSwitch() == true)
                {
                    SetInformation("Váltó sikeresen átállítva.");
                    if (Layout.DirectionCW == true)
                    {
                        Layout.Blocks[0].CWSignal.Settable = !Layout.Blocks[0].CWSignal.Settable;
                        Layout.Blocks[1].CWSignal.Settable = !Layout.Blocks[1].CWSignal.Settable;
                    }
                }
                else SetInformation("Ez a váltó jelenleg nem állítható át.");
            }
            else SetInformation("Ez a váltó jelenleg nem állítható át.");
        }

        public void SetCWDirection(bool IsCW)
        {
            if (Fecske.Block < 2 && Fecske.Speed == 0)
            {
                Layout.DirectionCW = IsCW;
                if (Layout.DirectionCW == true)
                {
                    Layout.Blocks[0].CCWSignal.SetState(SignalState.Blank);
                    Layout.Blocks[1].CCWSignal.SetState(SignalState.Blank);
                    Layout.Blocks[3].CCWSignal.SetState(SignalState.Blank);
                    Layout.Blocks[0].CWSignal.SetState(SignalState.Red);
                    Layout.Blocks[1].CWSignal.SetState(SignalState.Red);
                    Layout.Blocks[5].CWSignal.SetState(SignalState.Red);
                    if (Layout.LeftSwitch.Straight == true) Layout.Blocks[1].CWSignal.Settable = true;
                    else Layout.Blocks[0].CWSignal.Settable = true;
                }
                else
                {
                    Layout.Blocks[0].CWSignal.SetState(SignalState.Blank);
                    Layout.Blocks[1].CWSignal.SetState(SignalState.Blank);
                    Layout.Blocks[5].CWSignal.SetState(SignalState.Blank);
                    Layout.Blocks[0].CCWSignal.SetState(SignalState.Red);
                    Layout.Blocks[1].CCWSignal.SetState(SignalState.Red);
                    Layout.Blocks[3].CCWSignal.SetState(SignalState.Red);
                    if (Layout.RightSwitch.Straight == true) Layout.Blocks[1].CCWSignal.Settable = true;
                    else Layout.Blocks[0].CCWSignal.Settable = true;
                }
                SetInformation("Menetirány megváltozott.");
            }
            else SetInformation("Menetirány megváltoztatása jelenleg nem lehetséges.");
        }

    }
}
