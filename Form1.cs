using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Stitching;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace TestCmeraWin
{
    public partial class Form1 : Form
    {
        Capture c1, c2, c3;
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Mat cap = new Mat();
            Mat[] imgs = new Mat[3];
            Mat result = new Mat();
            cap = c1.QueryFrame();
            imageBox1.Image = cap;
            imgs[0] = cap;
            cap = c2.QueryFrame();
            imageBox2.Image = cap;
            imgs[1] = cap;
            cap = c3.QueryFrame();
            imageBox3.Image = cap;
            imgs[2] = cap;
            Emgu.CV.Stitching.Stitcher stitcher = new Emgu.CV.Stitching.Stitcher(true);
            using (VectorOfMat vms = new VectorOfMat())
            {
                vms.Push(imgs);
                bool stitchStatus = stitcher.Stitch(vms, result);
                if (stitchStatus)
                {
                    imageBox4.Image = result;
                    toolStripStatusLabel1.Text = "Stitch OK";
                }
                else
                    toolStripStatusLabel1.Text = "Stitch error";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                c1 = new Capture(0);
                c2 = new Capture(2);
                c3 = new Capture(3);
                timer1.Start();
            } else
            {
                timer1.Stop();
                c1.Dispose();
                c2.Dispose();
                c3.Dispose();
            }
        }
    }
}
