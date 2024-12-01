using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class LineParameters
    {
        public LineParameters() { }

        public LineParameters(double A1, double B1, double C1) { this.A = A1; this.B = B1; this.C = C1; }
        public double A
        { get; set; }
        public double B
        { get; set; }

        public double C
        { get; set; }

    }
}