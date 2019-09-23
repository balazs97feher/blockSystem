using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation
{



    public class Train : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged; // need to implement the above interface to display speed
        private void NotifyPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Speed"));
        }

        private int speed;
        public int Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                NotifyPropertyChanged();
            }
        }
        public int Block; // which block the train is currently in
        public int DistanceFromEOB; // distance from the end of the block
        public const int Acceleration = 1;
        public const int Deceleration = 1;

        public Train(int Block)
        {
            this.Block = Block;
            Speed = 0;
        }

        public void Roll()
        {
            DistanceFromEOB -= Speed;
        }

        public void Accelerate()
        {
            Speed += Acceleration;
        }

        public void Break()
        {
            Speed -= Deceleration;
        }


    }
}
