using devDept.Eyeshot.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class Tools
    {

        public static CompositeCurve CompCurvebyPLine(List<Point> Points)
        {
            List<ICurve> ILast = new List<ICurve>();
            CompositeCurve CLast = new CompositeCurve();


            if (Points.Count > 0)
            {
                for (int i = 0; i < Points.Count - 1; i++)
                {
                    ILast.Add(new devDept.Eyeshot.Entities.Line(Points[i].X, Points[i].Y, Points[i + 1].X, Points[i + 1].Y));
                }

                ILast.Add(new devDept.Eyeshot.Entities.Line(Points.LastOrDefault().X, Points.LastOrDefault().Y, Points.FirstOrDefault().X, Points.FirstOrDefault().Y));

                CLast = new CompositeCurve(ILast);
            }



            return CLast;
        }

    }
}
