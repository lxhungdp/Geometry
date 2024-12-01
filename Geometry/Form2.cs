using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Geometry
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        TheModel m;
        private void Form2_Load(object sender, EventArgs e)
        {
            List<Point> L1 = new List<Point>();
            L1.Add(new Point("A'", -3500, 0, 0));
            L1.Add(new Point("B'", 0, 0, 0));
            L1.Add(new Point("C'", 3500, 0, 0));

            L1.Add(new Point("A", -3500, 2500, 0));
            L1.Add(new Point("B", 0, 2500, 0));
            L1.Add(new Point("C", 3500, 2500, 0));

            L1.Add(new Point("D", -3500, 5000, 0));
            L1.Add(new Point("E", 0, 5000, 0));
            L1.Add(new Point("F", 3500, 5000, 0));


            m = new TheModel();
            panel1.Controls.Add(m);
            

            m.SettingOutPoint = L1;
            m.MatchSeg = 5;
            m.CastingSeg = 4;
            m.InitializeModel();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Point> L1 = new List<Point>();
            L1.Add(new Point("A'", -3500, 0, 0));
            L1.Add(new Point("B'", 0, 0, 0));
            L1.Add(new Point("C'", 3500, 0, 0));

            L1.Add(new Point("A", -3500, 2500, 0));
            L1.Add(new Point("B", 0, 2400, 0));
            L1.Add(new Point("C", 3500, 2500, 0));

            L1.Add(new Point("D", -3500, 5000, 0));
            L1.Add(new Point("E", 0, 5100, 0));
            L1.Add(new Point("F", 3500, 5000, 0));


            
            m.SettingOutPoint = L1;
            m.MatchSeg = 7;
            m.CastingSeg = 2;

            m.AddSegmentEntity();
        }
    }
}
