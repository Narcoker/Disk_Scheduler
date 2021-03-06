using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disk_IO
{
    public struct Tracer
    {
        static public int counter = 0;

        public int jobNumber;
        public int jobPosition;
        public int jobMovement;
        public int accumulateTime;
        public double deviation;

        public Tracer(int jobPosition, int jobMovement, int accumulateTime, double deviation)
        {
            this.jobNumber = counter++;
            this.jobPosition = jobPosition;
            this.jobMovement = jobMovement;
            this.accumulateTime = accumulateTime;
            this.deviation = deviation;
        }
    }
}
