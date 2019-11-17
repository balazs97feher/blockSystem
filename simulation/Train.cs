﻿using System.ComponentModel;

namespace simulation
{
    public class Train : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged; // need to implement the above interface to display speed
        private void NotifyPropertyChanged(string Property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Property));
        }

        private int speed;
        public int Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                NotifyPropertyChanged("Speed");
            }
        }
        public int Block; // which block the train is currently in
        public int DistanceFromEOB; // distance from the end of the block

        public int Acceleration;
        public int Deceleration;

        private Communication Messenger;
        public Train(int Block, Communication M)
        {
            this.Block = Block;
            Speed = 0;
            Messenger = M;
            /*Acceleration = 1;
            Deceleration = 1;*/
        }

        /*public int Roll() //when crossing block border, returns the remaining distance that the train goes after crossing
        {
            if (DistanceFromEOB >= Speed) ***** FUNCTION FOR SIMULATION PURPOSES *****
            {
                DistanceFromEOB -= Speed;
                return 0;
            }
            else return Speed - DistanceFromEOB;
        }*/

        /*public void Roll(int D) // the train rolls for D distance ***** FUNCTION FOR SIMULATION PURPOSES *****
        {
            DistanceFromEOB -= D;
        }*/

        public void ChangeSpeed(int SetSpeed)
        {
            Speed = SetSpeed;
            Messenger.SetSpeed(SetSpeed);
        }

        /*public void Break(int SetSpeed) ***** FUNCTION FOR SIMULATION PURPOSES *****
        {
            if (Speed > Deceleration) Speed -= Deceleration;
            else Speed = 0;
        }*/

    }
}
