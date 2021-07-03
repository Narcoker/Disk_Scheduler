using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disk_IO
{
    class Disk
    {
        List<int> DiskQueue;

        public Disk()
        {
            this.DiskQueue = new List<int>();

        }

        public void EnqueueDiskQ(string args)
        {
            foreach (string tmp in args.Split(','))
            {
                this.DiskQueue.Add(int.Parse(tmp));
            }
        }

        public void Clean()
        {
            this.DiskQueue.Clear();
            Tracer.counter = 0;
        }

        public List<Tracer> SchedulingWithFCFS(int intialPoint)
        {
            List<Tracer> tr = new List<Tracer>();
            int acc = 0;
            int currentCur = intialPoint;
            int movement = 0;

            int num = this.DiskQueue.Count;
            int responseSum = 0;

            tr.Add(new Tracer(currentCur, 0, 0, 0));

            while (this.DiskQueue.Count > 0)
            {
                int nextCur = this.DiskQueue[0];
                this.DiskQueue.RemoveAt(0);

                movement = nextCur - currentCur;
                acc += Math.Abs(movement);
                responseSum += acc;

                currentCur = nextCur;
                tr.Add(new Tracer(currentCur, movement, acc, 0));

            }

            for (int i = 1; i < tr.Count; i++)
            {
                Tracer temp = tr[i];
                temp.deviation = temp.accumulateTime - (responseSum / (double)num);
                tr[i] = temp;
            }

            return tr;
        }


        public List<Tracer> SchedulingWithSSTF(int intialPoint)
        {
            List<Tracer> tr = new List<Tracer>();
            int acc = 0;
            int currentCur = intialPoint;
            int index = -1;
            int movement = 0;

            int num = this.DiskQueue.Count;
            int responseSum = 0;

            tr.Add(new Tracer(currentCur, 0, 0, 0));

            DiskQueue.Sort();

            for (int i = 0; i < this.DiskQueue.Count; i++)
            {
                if (this.DiskQueue[i] <= currentCur) index++;
            }

            if (index == -1) return tr;



            while (this.DiskQueue.Count > 0)
            {
                int nextCur;

                if (index == 0 || index == DiskQueue.Count - 1)
                {
                    nextCur = DiskQueue[index];
                    this.DiskQueue.RemoveAt(index);
                    if (index == DiskQueue.Count) index--;


                }

                else if (Math.Abs(currentCur - DiskQueue[index]) <= Math.Abs(currentCur - DiskQueue[index + 1])) //index가 더 작을경우
                {
                    nextCur = DiskQueue[index];
                    this.DiskQueue.RemoveAt(index);
                    if (index != 0) index--;

                }

                else
                {
                    nextCur = DiskQueue[index + 1];
                    this.DiskQueue.RemoveAt(index + 1);
                }

                movement = nextCur - currentCur;
                acc = acc + Math.Abs(movement);
                responseSum += acc;

                currentCur = nextCur;
                tr.Add(new Tracer(currentCur, movement, acc, 0));
            }

            for (int i = 1; i < tr.Count; i++)
            {
                Tracer temp = tr[i];
                temp.deviation = temp.accumulateTime - (responseSum / (double)num);
                tr[i] = temp;
            }

            return tr;
        }

        public List<Tracer> SchedulingWithSCAN(int intialPoint)
        {
            List<Tracer> tr = new List<Tracer>();
            int acc = 0;
            int currentCur = intialPoint;
            int index = -1;
            bool direction = true; // true == 왼쪽으로 false == 오른쪽으로
            int movement = 0;

            int num = this.DiskQueue.Count;
            int responseSum = 0;

            tr.Add(new Tracer(currentCur, 0, 0, 0));

            DiskQueue.Sort();

            for (int i = 0; i < this.DiskQueue.Count; i++)
            {
                if (this.DiskQueue[i] <= currentCur) index++;
            }


            while (this.DiskQueue.Count > 0)
            {
                int nextCur;

                if (index < 0)
                {
                    index = 0;
                    direction = false;
                    nextCur = 0; 
                }

                else if (index > this.DiskQueue.Count - 1)
                {
                    index = this.DiskQueue.Count - 1;
                    direction = true;
                    nextCur = 400;
                }

                else
                {
                    nextCur = this.DiskQueue[index];

                    if (direction == true) { this.DiskQueue.RemoveAt(index--); }
                    else { this.DiskQueue.RemoveAt(0); }

                }

                movement = nextCur - currentCur;
                acc = acc + Math.Abs(movement);
                responseSum += acc;

                currentCur = nextCur;
                tr.Add(new Tracer(currentCur, movement, acc, 0));

            }

            for (int i = 1; i < tr.Count; i++)
            {
                Tracer temp = tr[i];
                temp.deviation = temp.accumulateTime - (responseSum / (double)num);
                tr[i] = temp;
            }

            return tr;
        }


        public List<Tracer> SchedulingWithLOOK(int intialPoint)
        {
            List<Tracer> tr = new List<Tracer>();
            int acc = 0;
            int currentCur = intialPoint;
            int index = -1;
            bool direction = true; // true == 왼쪽으로 false == 오른쪽으로
            int movement = 0;

            int num = this.DiskQueue.Count;
            int responseSum = 0;

            tr.Add(new Tracer(currentCur, 0, 0, 0));

            DiskQueue.Sort();

            for (int i = 0; i < this.DiskQueue.Count; i++)
            {
                if (this.DiskQueue[i] <= currentCur) index++;
            }

            while (this.DiskQueue.Count > 0)
            {
                int nextCur;

                if (index < 0)
                {
                    index = 0;
                    direction = false;
                    nextCur = this.DiskQueue[index];
                    DiskQueue.RemoveAt(index);

                }

                else if (index > this.DiskQueue.Count - 1)
                {
                    index = this.DiskQueue.Count - 1;
                    direction = true;
                    nextCur = this.DiskQueue[index];
                    DiskQueue.RemoveAt(index);
                }

                else
                {
                    nextCur = this.DiskQueue[index];

                    if (direction == true) { this.DiskQueue.RemoveAt(index--); }
                    else { this.DiskQueue.RemoveAt(0); }
                }

                movement = nextCur - currentCur;
                acc = acc + Math.Abs(movement);
                responseSum += acc;

                currentCur = nextCur;
                tr.Add(new Tracer(currentCur, movement, acc, 0));
            }

            for (int i = 1; i < tr.Count; i++)
            {
                Tracer temp = tr[i];
                temp.deviation = temp.accumulateTime - (responseSum / (double)num);
                tr[i] = temp;
            }

            return tr;
        }

        public List<Tracer> SchedulingWithSPTF(int intialPoint)
        {
            List<Tracer> tr = new List<Tracer>();
            int acc = 0;
            int currentCur = intialPoint;
            int movement = 0;

            int num = this.DiskQueue.Count;
            int responseSum = 0;

            int intialSec = 0;
            int currentSec = intialSec;

            tr.Add(new Tracer(currentCur, 0, 0, 0));

            List<int> sector = new List<int>();
            List<int> mvPLUSrL = new List<int>(); // 회전지연시간 계산 리스트

            for (int i = 0; i < this.DiskQueue.Count; i++)
            {
                int sec = this.DiskQueue[i] % 8;
                sector.Add(sec);
            }

            while (this.DiskQueue.Count > 0)
            {
                int RL = 0;

                for (int i = 0; i < this.DiskQueue.Count; i++)
                {
                    RL = sector[i] - currentSec;

                    if (RL < 0)
                    {
                        RL = RL + 8;
                        mvPLUSrL.Add(Math.Abs(currentCur - this.DiskQueue[i]) + RL);
                    }
                    else
                    {
                        mvPLUSrL.Add(Math.Abs(currentCur - this.DiskQueue[i]) + RL);
                    }
                }

                int min = mvPLUSrL.IndexOf(mvPLUSrL.Min());

                int nextCur = this.DiskQueue[min];
                this.DiskQueue.RemoveAt(min);

                movement = nextCur - currentCur;
                acc = acc + mvPLUSrL[min];
                responseSum += acc;

                mvPLUSrL.Clear();

                currentCur = nextCur;
                currentSec = sector[min];
                sector.RemoveAt(min);


                tr.Add(new Tracer(currentCur, movement, acc, 0));

            }

            for (int i = 1; i < tr.Count; i++)
            {
                Tracer temp = tr[i];
                temp.deviation = temp.accumulateTime - (responseSum / (double)num);
                tr[i] = temp;
            }

            return tr;
        }
    }
}
