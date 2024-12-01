using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Geometry
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Point P1Local = new Point(1618.3759, 1268.0757);
            Point P2Lcoal = new Point(1824.3904, 1447.7064);



            Point x1i = new Point(1773.8763, 933.0388);
            Point x1j = new Point(2236.4195, 1426.1507);

            Point x2i = new Point(1212.7329, 764.3459);
            Point x2j = new Point(1812.4128, 580.689);


            Point p1 = new Point(137.976, 342.6263);
            Point p2 = new Point(409.9325, 315.2614);


            var p11 = p1.LocaltoLocal(x1i, x1j, x2i, x2j);
            var p22 = p2.LocaltoLocal(x1i, x1j, x2i, x2j);


            double a = 1;

        }
    }
}
