using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Disk_IO
{
    public partial class Form1 : Form
    {
        Disk disk;

        public Form1()
        {
            this.disk = new Disk();
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();
            disk.Clean();
            this.disk.EnqueueDiskQ(this.tbQueueList.Text);
            this.chtSchedule.Series[0].Points.Clear();
            this.chtSchedule.Series[1].Points.Clear();


            String type = comboBox1.SelectedItem.ToString();


            if (type == "FCFS")
            {
                var t = this.disk.SchedulingWithFCFS(int.Parse(this.tbInital.Text));
                TracerRead(t);
                t.Clear();
            }

            else if (type == "SSTF")
            {
                var t = this.disk.SchedulingWithSSTF(int.Parse(this.tbInital.Text));
                TracerRead(t);
                t.Clear();
            }
            else if (type == "SCAN")
            {
                var t = this.disk.SchedulingWithSCAN(int.Parse(this.tbInital.Text));
                TracerRead(t);
                t.Clear();
            }

            else if (type == "LOOK")
            {
                var t = this.disk.SchedulingWithLOOK(int.Parse(this.tbInital.Text));
                TracerRead(t);
                t.Clear();
            }

            else if(type == "SPTF")
            {
                var t = this.disk.SchedulingWithSPTF(int.Parse(this.tbInital.Text));
                TracerRead(t);
                t.Clear();
            }
          

            

        }

        private void TracerRead(List<Tracer> tr)
        {
            int total = 0;
            int num = 0;
            double dispersion = 0;
            

            foreach ( var tmp in tr )
            {
                var listViewItem = new ListViewItem(tmp.jobNumber.ToString());
                listViewItem.SubItems.Add(tmp.jobPosition.ToString());
                listViewItem.SubItems.Add(tmp.jobMovement.ToString());
                listViewItem.SubItems.Add(tmp.accumulateTime.ToString());
                listViewItem.SubItems.Add(tmp.deviation.ToString());
                listView1.Items.Add(listViewItem);

                this.chtSchedule.Series[0].Points.AddXY(tmp.jobPosition, tmp.jobNumber);
                this.chtSchedule.Series[1].Points.AddXY(tmp.jobPosition, tmp.jobNumber);
                total = tmp.accumulateTime;
                dispersion += tmp.deviation * tmp.deviation;
                num = tmp.jobNumber;
            }
            this.lbDebugText.Text = "총 이동거리: " + total;
            this.label4.Text = "표준편차: " + Math.Round(Math.Sqrt(dispersion / num) , 2);
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            Random rd = new Random();

            int count = rd.Next(8, 20);
            StringBuilder sb = new StringBuilder();

            this.tbInital.Text = rd.Next(0, 400).ToString();
            for ( int i = 0; i < count; i++ )
            {
                sb.Append(rd.Next(0, 400));
                sb.Append(",");
            }
            sb[sb.Length-1] = '\0';
            this.tbQueueList.Text = sb.ToString();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chtSchedule_Click(object sender, EventArgs e)
        {

        }

        private void tbQueueList_TextChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
