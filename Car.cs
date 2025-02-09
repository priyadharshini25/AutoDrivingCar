using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDrivingCarSimulation
{
    public class Car
    {
        public string Name
        {
            get;
        }
        public int X
        {
            get;
            private set;
        }
        public int Y
        {
            get;
            private set;
        }
        public string Facing
        {
            get;
            private set;
        }

        private int Width
        {
            get;
        }
        private int Height
        {
            get;

        }

    }
}
