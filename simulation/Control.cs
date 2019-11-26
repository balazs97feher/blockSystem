﻿using System;
using System.ComponentModel;
using System.Windows.Threading;
using System.Windows;
using System.IO.Ports;

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

        private Communication Messenger;

        public Train Fecske;
        private int TrainPreviousBlock;
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

        private Control(Communication M)
        {
            Instance = this;
            Messenger = M;
            Fecske = new Train(0, M);
            TrainPreviousBlock = 0;
        }

        public static Control CreateController(Communication M)
        {
            if (Instance != null) return Instance;
            else return new Control(M);
        }

        public void Initialize()
        {
            SetDeparture(0);
            SetCWDirection(true);
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
            if (SetSpeed > 0 && (Layout.DepartureConstraints(Fecske.Block) == false))
            {
                SetSpeed = 0;
                SetInformation("Indulás megtiltva. Ellenőrizze a váltók és a jelzők állását!");
            }
            else if (SetSpeed > Layout.Blocks[Fecske.Block].EOBSpeed)
            {
                SetSpeed = Layout.Blocks[Fecske.Block].EOBSpeed;
                SetInformation("Maximális sebesség következő szakaszon: " + Layout.Blocks[Fecske.Block].EOBSpeed.ToString() + " km/h");
            }
            else if(Layout.ValidRoute==true)
            {
                SetSpeed= Layout.Blocks[Fecske.Block].EOBSpeed;
            }

            if (Fecske.Block != TrainPreviousBlock)
            {
                // Fecske.ChangeSpeed(Layout.Blocks[TrainPreviousBlock].EOBSpeed);
                SetSpeed = Layout.Blocks[TrainPreviousBlock].EOBSpeed;
                FreeBlock(TrainPreviousBlock);
                OccupyBlock(Fecske.Block);
                SetSignalRed();
                TrainPreviousBlock = Fecske.Block;
            }
            if (SetSpeed < Fecske.Speed)
            {
                Fecske.Speed -= 2;
                Fecske.ChangeSpeed(Fecske.Speed);
            }
            else if (SetSpeed > Fecske.Speed)
            {
                Fecske.Speed += 1;
                Fecske.ChangeSpeed(Fecske.Speed);
            }

        }

        public void SubscribeToOccupationPort()
        {
            Messenger.OccupationPort.DataReceived += OccupationPort_DataReceived;
        }

        private void OccupationPort_DataReceived(object sender, SerialDataReceivedEventArgs e) // receive ID from Hall sensor,set the train's current position
        {
            int HallId = Int32.Parse(Messenger.OccupationLastReceived);
            Fecske.Block = Layout.HallIdToBlockId(HallId);
        }

        /*public void Roll() ***** FUNCTION FOR SIMULATION PURPOSES *****
        {
            int RemainingDistance = Fecske.Roll();
            if (RemainingDistance > 0) // the train crosses a block border
            {
                FreeBlock(Fecske.Block);
                SetSignalRed();
                int NextBlockId = Layout.GetNextBlock(Fecske.Block);
                Fecske.Block = NextBlockId;
                SetInformation("Maximális sebesség: " + Layout.Blocks[Fecske.Block].EOBSpeed.ToString() + " km/h");
                //SecureStation(NextBlockId);
                Fecske.DistanceFromEOB = Layout.Blocks[NextBlockId].Length;
                OccupyBlock(Fecske.Block);
                SetDeceleration();
                Fecske.Roll(RemainingDistance);
            }
        }*/

        /*public void SecureStation(int BlockId) // ***** COMMENTED OUT FOR TEST PURPOSES *****
        {
            if (BlockId == 0)
            {
                if (Layout.DirectionCW == true)
                {
                    if (Layout.RightSwitch.Straight == true) Layout.Blocks[5].CWSignal.Settable = true;
                    else Layout.Blocks[5].CWSignal.Settable = false;
                }
                else
                {
                    if (Layout.LeftSwitch.Straight == true) Layout.Blocks[3].CCWSignal.Settable = true;
                    else Layout.Blocks[3].CCWSignal.Settable = false;
                }
            }
            else if (BlockId == 1)
            {
                if (Layout.DirectionCW == true)
                {
                    if (Layout.RightSwitch.Straight == true) Layout.Blocks[5].CWSignal.Settable = false;
                    else Layout.Blocks[5].CWSignal.Settable = true;
                }
                else
                {
                    if (Layout.LeftSwitch.Straight == true) Layout.Blocks[3].CCWSignal.Settable = false;
                    else Layout.Blocks[3].CCWSignal.Settable = true;
                }
            }
            else if (BlockId == 2 && Layout.DirectionCW == true) Layout.Blocks[5].CWSignal.Settable = true;
            else if (BlockId == 6 && Layout.DirectionCW == false) Layout.Blocks[3].CCWSignal.Settable = true;
        }*/


        public void SetSignalRed() // sets the signal to red that the train has passed
        {
            if (Layout.DirectionCW == true && Layout.Blocks[TrainPreviousBlock].CWSignal != null)
                Layout.Blocks[TrainPreviousBlock].CWSignal.SetState(SignalState.Red);
            else if (Layout.DirectionCW == false && Layout.Blocks[TrainPreviousBlock].CCWSignal != null)
                Layout.Blocks[TrainPreviousBlock].CCWSignal.SetState(SignalState.Red);
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
                //SecureStation(BlockId);
                SetInformation("Induló vágány megváltozott.");
                //SecureStation(Fecske.Block);
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

        public void HighlightBlock(int BlockId)
        {
            Layout.Blocks[BlockId].Highlight();
        }

        public void SwitchRight()
        {
            if ((Layout.Blocks[0].CCWSignal.State == SignalState.Red && Layout.Blocks[1].CCWSignal.State == SignalState.Red)
                || Layout.Blocks[5].CWSignal.State == SignalState.Red)
            {
                if (Layout.RightSwitch.DoSwitch() == true)
                {
                    SetInformation("Váltó sikeresen átállítva.");
                    /*if (Layout.DirectionCW == false)
                    {
                        Layout.Blocks[0].CCWSignal.Settable = !Layout.Blocks[0].CCWSignal.Settable;
                        Layout.Blocks[1].CCWSignal.Settable = !Layout.Blocks[1].CCWSignal.Settable;
                    }
                    SecureStation(Fecske.Block);*/
                }
                else SetInformation("Ez a váltó jelenleg nem állítható át.");
            }
            else SetInformation("Ez a váltó jelenleg nem állítható át.");
        }

        public void SwitchRight(bool Straight)
        {
            if (Layout.RightSwitch.Straight != Straight) SwitchRight();
        }

        public void SwitchLeft()
        {
            if ((Layout.Blocks[0].CWSignal.State == SignalState.Red && Layout.Blocks[1].CWSignal.State == SignalState.Red)
                || Layout.Blocks[3].CCWSignal.State == SignalState.Red)
            {
                if (Layout.LeftSwitch.DoSwitch() == true)
                {
                    SetInformation("Váltó sikeresen átállítva.");
                    /*if (Layout.DirectionCW == true)
                    {
                        Layout.Blocks[0].CWSignal.Settable = !Layout.Blocks[0].CWSignal.Settable;
                        Layout.Blocks[1].CWSignal.Settable = !Layout.Blocks[1].CWSignal.Settable;
                    }
                    SecureStation(Fecske.Block);*/
                }
                else SetInformation("Ez a váltó jelenleg nem állítható át.");
            }
            else SetInformation("Ez a váltó jelenleg nem állítható át.");
        }

        public void SwitchLeft(bool Straight)
        {
            if (Layout.LeftSwitch.Straight != Straight) SwitchLeft();
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
                //SecureStation(Fecske.Block);
            }
            else SetInformation("Menetirány megváltoztatása csak állomáson várakozó vonat esetén lehetséges.");
        }

    }
}
