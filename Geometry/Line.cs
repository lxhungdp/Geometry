using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{

    public enum IntersectionExtendType
    {
        NoExtend,
        L1Extend,
        L2Extend,
        BothExtend
    }

    public class Line
    {
        const double tolerance = 1e-4;

        public Point iPoint { get; set; }

        public Point jPoint { get; set; }


        public Line() { }

        public Line(Point p1, Point p2) { this.iPoint = p1; this.jPoint = p2; }

        public Line(double x1, double y1, double x2, double y2)
        {
            this.iPoint = new Point(x1, y1);
            this.jPoint = new Point(x2, y2);
        }

        public Line(LineParameters p)
        {
            if (p.B != 0)
            {
                this.iPoint = new Point(0, -p.C / p.B);
                this.jPoint = new Point(1, (-p.A - p.C) / p.B);
            }
            else
            {
                this.iPoint = new Point(-p.C / p.A, 0);
                this.jPoint = new Point(-p.C / p.A, 1);
            }

        }


        


        public Line Offset(double valueOffset)
        {
            //Postive value: Left side (look from iPoint to jPoint)
            Point p1 = new Point(0, valueOffset).toGlobal(iPoint, new Line(iPoint, jPoint));
            Point p2 = new Point(0, valueOffset).toGlobal(jPoint, new Line(iPoint, jPoint));

            return new Line(p1, p2);
        }

        public Line Offset(double valueOffset, bool RightSide)
        {
            return Offset(RightSide ? -valueOffset : valueOffset);

        }

        public Line Invert()
        {
            return new Line(this.jPoint, this.iPoint);
        }


        public Point OffsetIntersecWith(Line L1)
        {
            // Parameters for this line (extending from jPoint)
            double dx1 = jPoint.X - iPoint.X;  // Direction vector of this line
            double dy1 = jPoint.Y - iPoint.Y;
            double x1 = jPoint.X;  // Starting point (jPoint)
            double y1 = jPoint.Y;

            // Parameters for L1 (extending from iPoint)
            double dx2 = L1.jPoint.X - L1.iPoint.X;  // Direction vector of L1
            double dy2 = L1.jPoint.Y - L1.iPoint.Y;
            double x2 = L1.iPoint.X;  // Starting point (iPoint)
            double y2 = L1.iPoint.Y;

            // Calculate determinant
            double determinant = dx1 * dy2 - dy1 * dx2;

            // Check if lines are parallel (determinant = 0)
            if (Math.Abs(determinant) < tolerance)
            {
                return null; // Lines are parallel or coincident, no intersection
            }

            // Calculate parameters for intersection point
            double t = ((x2 - x1) * dy2 - (y2 - y1) * dx2) / determinant;
            double u = ((x2 - x1) * dy1 - (y2 - y1) * dx1) / determinant;

            // Calculate intersection point
            double intersectX = x1 + t * dx1;
            double intersectY = y1 + t * dy1;

            // First check if intersection occurs within the original line segments
            if (IsPointOnSegment(new Point(intersectX, intersectY), this) &&
                IsPointOnSegment(new Point(intersectX, intersectY), L1))
            {
                return new Point(intersectX, intersectY);
            }

            // If not on segments, check if intersection is in the correct direction for extended lines
            bool isValidForThisLine = ((intersectX - jPoint.X) * dx1 + (intersectY - jPoint.Y) * dy1) >= 0;
            bool isValidForL1 = ((intersectX - L1.iPoint.X) * dx2 + (intersectY - L1.iPoint.Y) * dy2) >= 0;

            if (isValidForThisLine && isValidForL1)
            {
                return new Point(intersectX, intersectY);
            }

            return null;
        }

        // Helper method to check if a point lies on a line segment
        private bool IsPointOnSegment(Point p, Line line)
        {
            // Calculate the distances
            double d1 = p.DistanceTo(line.iPoint);
            double d2 = p.DistanceTo(line.jPoint);
            double lineLength = line.iPoint.DistanceTo(line.jPoint);

            // Check if point lies on segment (allowing for small numerical errors)
            
            return Math.Abs(d1 + d2 - lineLength) < tolerance;
        }


        public Point ExtendIntersecWith(Line L1)
        {
            // Parameters for this line
            double a1 = jPoint.Y - iPoint.Y;
            double b1 = iPoint.X - jPoint.X;
            double c1 = -(a1 * iPoint.X + b1 * iPoint.Y);

            // Parameters for line L1
            double a2 = L1.jPoint.Y - L1.iPoint.Y;
            double b2 = L1.iPoint.X - L1.jPoint.X;
            double c2 = -(a2 * L1.iPoint.X + b2 * L1.iPoint.Y);

            // Calculate determinant
            double determinant = a1 * b2 - a2 * b1;

            // Check if lines are parallel (determinant = 0)
            if (Math.Abs(determinant) < tolerance)
            {
                return null; // Lines are parallel or coincident, no intersection
            }

            // Calculate intersection point
            double x = (b1 * c2 - b2 * c1) / determinant;
            double y = (a2 * c1 - a1 * c2) / determinant;

            return new Point(x, y);
        }

        public List<Point> IntersecWith(Line L1)
        {
            List<Point> intersections = new List<Point>();
            IntersectionExtendType type;

            // Use existing IntersecWith method
            Point intersectionPoint = this.IntersecWith(L1, out type);

            if (intersectionPoint != null && type == IntersectionExtendType.NoExtend)
            {
                intersections.Add(intersectionPoint);
            }

            return intersections;
        }

        public List<Point> IntersecWith(PLine PL1)
        {
            List<Point> intersections = new List<Point>();
            if (PL1 == null || PL1.Lines.Count == 0) return intersections;

            // Check intersection with each line in the PLine
            foreach (var line in PL1.Lines)
            {
                var points = this.IntersecWith(line);
                intersections.AddRange(points);
            }

            return intersections;
        }

        public Point IntersecWith(Line L2, out IntersectionExtendType type)
        {


            // Step 1: Calculate direction vectors
            double dx1 = jPoint.X - iPoint.X;
            double dy1 = jPoint.Y - iPoint.Y;
            double dx2 = L2.jPoint.X - L2.iPoint.X;
            double dy2 = L2.jPoint.Y - L2.iPoint.Y;

            // Step 2: Calculate the determinant
            double det = dx1 * dy2 - dy1 * dx2;

            if (Math.Abs(det) < tolerance)
            {
                type = IntersectionExtendType.NoExtend;
                return null;

            }
            else
            {
                // Step 3: Calculate the parameters t and u for the intersection point
                double t = ((L2.iPoint.X - iPoint.X) * dy2 - (L2.iPoint.Y - iPoint.Y) * dx2) / det;
                double u = ((L2.iPoint.X - iPoint.X) * dy1 - (L2.iPoint.Y - iPoint.Y) * dx1) / det;

                // Calculate the intersection point using t for L1
                double intersectX = iPoint.X + t * dx1;
                double intersectY = iPoint.Y + t * dy1;

                // Step 4: Determine the intersection case based on t and u
                if (t >= 0 && t <= 1 && u >= 0 && u <= 1)
                {
                    type = IntersectionExtendType.NoExtend;
                }
                else if (t < 0 || t > 1) // Check if L1 needs to be extended
                {
                    if (u >= 0 && u <= 1) // L2 within bounds
                    {
                        type = IntersectionExtendType.L1Extend;
                    }
                    else // Both segments need extension
                    {
                        type = IntersectionExtendType.BothExtend;
                    }
                }
                else if (u < 0 || u > 1) // Check if L2 needs to be extended
                {
                    if (t >= 0 && t <= 1) // L1 within bounds
                    {
                        type = IntersectionExtendType.L2Extend;
                    }
                    else
                    {
                        type = IntersectionExtendType.BothExtend;
                    }
                }
                else
                {
                    type = IntersectionExtendType.BothExtend;
                }

                return new Point(intersectX, intersectY);


            }

        }


        public double AngleWith(Line L2, bool inRadian)
        {

            double dx1 = jPoint.X - iPoint.X;
            double dy1 = jPoint.Y - iPoint.Y;
            double dx2 = L2.jPoint.X - L2.iPoint.X;
            double dy2 = L2.jPoint.Y - L2.iPoint.Y;

            if ((dx1 == 0 && dy1 == 0) || (dx2 == 0 && dy2 == 0))
            {
                return 0; // Indicate an undefined angle
            }

            // Step 3: Calculate the angle of each line relative to the X-axis using atan2
            double theta1 = Math.Atan2(dy1, dx1);
            double theta2 = Math.Atan2(dy2, dx2);

            // Step 4: Calculate the angle between the two lines
            double angleBetweenLines = theta1 - theta2;


            // Convert the angle to degrees
            double angleBetweenLinesDegrees = angleBetweenLines * (180.0 / Math.PI);


            return inRadian ? angleBetweenLines : angleBetweenLinesDegrees;

        }

        public double AngleWithX(bool inRadian)
        {

            double dx1 = jPoint.X - iPoint.X;
            double dy1 = jPoint.Y - iPoint.Y;

            // Step 3: Calculate the angle of each line relative to the X-axis using atan2
            double theta1 = Math.Atan2(dy1, dx1);

            // Convert the angle to degrees
            double angleBetweenLinesDegrees = theta1 * (180.0 / Math.PI);


            return inRadian ? theta1 : angleBetweenLinesDegrees;

        }

        public LineParameters Parameters()
        {
            double A = jPoint.Y - iPoint.Y;  // A = yB - yA
            double B = iPoint.X - jPoint.X;  // B = xA - xB
            double C = -(A * iPoint.X + B * iPoint.Y); // C = -(A*xA + B*yA)

            return new LineParameters(A, B, C);
        }

        public Point PointWithDistance(double station, double distance, bool RightSide)
        {
            // Step 1: Calculate the coordinates of point C at the given station
            double xC = iPoint.X + station * (jPoint.X - iPoint.X);
            double yC = iPoint.Y + station * (jPoint.Y - iPoint.Y);

            // Step 2: Calculate the direction vector of the line
            double dx = jPoint.X - iPoint.X;
            double dy = jPoint.Y - iPoint.Y;

            // Step 3: Determine the perpendicular direction based on RightSide
            double perpX, perpY;

            if (RightSide)
            {
                perpX = dy;      // Right side: (dy, -dx)
                perpY = -dx;
            }
            else
            {
                perpX = -dy;     // Left side: (-dy, dx)
                perpY = dx;
            }

            // Step 4: Normalize the perpendicular direction
            double length = Math.Sqrt(perpX * perpX + perpY * perpY);
            double unitPerpX = perpX / length;
            double unitPerpY = perpY / length;

            // Step 5: Calculate the point at the given distance from C
            double xP = xC + unitPerpX * distance;
            double yP = yC + unitPerpY * distance;

            // Return the resulting point
            return new Point(xP, yP);
        }


        public bool isPerpendicularWith(Line L1)
        {
            // Direction vector of this line
            double dx1 = jPoint.X - iPoint.X;
            double dy1 = jPoint.Y - iPoint.Y;

            // Direction vector of line L1
            double dx2 = L1.jPoint.X - L1.iPoint.X;
            double dy2 = L1.jPoint.Y - L1.iPoint.Y;

            // Check if either line is degenerate (zero-length), which cannot be perpendicular
            if ((dx1 == 0 && dy1 == 0) || (dx2 == 0 && dy2 == 0))
            {
                return false; // A point (zero-length line) cannot be perpendicular to another line
            }

            // Calculate the dot product of the two direction vectors
            double dotProduct = dx1 * dx2 + dy1 * dy2;

            // Use a small tolerance for floating-point comparison
            //const double tolerance = 1e-4;
            return Math.Abs(dotProduct) < tolerance;
        }

        public bool isSameSide(Point p1, Point p2)
        {
            // Line parameters for this line (ax + by + c = 0)
            double a = jPoint.Y - iPoint.Y;
            double b = iPoint.X - jPoint.X;
            double c = -(a * iPoint.X + b * iPoint.Y);

            // Evaluate the line equation for both points
            double val1 = a * p1.X + b * p1.Y + c;
            double val2 = a * p2.X + b * p2.Y + c;

            // Check if both values have the same sign (both positive or both negative)
            return val1 * val2 > 0;
        }

        public LineParameters PerpendicularLineParameters(double s)
        {
            // Step 1: Calculate direction vector of L1
            double dx1 = jPoint.X - iPoint.X;
            double dy1 = jPoint.Y - iPoint.Y;

            // Step 2: Calculate point C on L1 at relative distance s
            double xC = iPoint.X + s * dx1;
            double yC = iPoint.Y + s * dy1;

            var p = Parameters();

            if (p.A != 0 && p.B != 0)
            {
                double a1 = -1 / (-p.A / p.B);
                double b1 = yC - a1 * xC;

                return new LineParameters(a1, -1, b1);
            }
            else if (p.A == 0 && p.B != 0)
            {
                return new LineParameters(1, 0, -xC);
            }

            else if (p.A != 0 && p.B == 0)
            {
                return new LineParameters(0, 1, -yC);

            }
            else
            {
                return new LineParameters();
            }
        }

        public Line PerpendicularLine(double s)
        {
            return new Line(PerpendicularLineParameters(s));
        }

        public double? FindX(double Y, bool Inside)
        {
            // Check if the line is horizontal
            if (iPoint.Y == jPoint.Y)
            {
                return null;
            }

            // Calculate the slope
            double slope = (jPoint.X - iPoint.X) / (jPoint.Y - iPoint.Y);
            double X = iPoint.X + slope * (Y - iPoint.Y);

            // If Inside is true, restrict Y to within segment bounds on the X-axis
            if (Inside)
            {
                double minY = Math.Min(iPoint.Y, jPoint.Y);
                double maxY = Math.Max(iPoint.Y, jPoint.Y);

                if (Y < minY || Y > maxY)
                {
                    return null; // X is outside the segment bounds
                }
            }

            return X;
        }

        public double? FindY(double X, bool Inside)
        {
            // Check if the line is vertical
            if (iPoint.X == jPoint.X)
            {
                return null;
            }

            // Calculate the slope
            double slope = (jPoint.Y - iPoint.Y) / (jPoint.X - iPoint.X);
            double Y = iPoint.Y + slope * (X - iPoint.X);

            // If Inside is true, restrict Y to within segment bounds on the X-axis
            if (Inside)
            {
                double minX = Math.Min(iPoint.X, jPoint.X);
                double maxX = Math.Max(iPoint.X, jPoint.X);

                if (X < minX || X > maxX)
                {
                    return null; // X is outside the segment bounds
                }
            }

            return Y;
        }

        public Point PointAt(double station)
        {
            // Calculate the coordinates of the point at the given station
            double xP = iPoint.X + station * (jPoint.X - iPoint.X);
            double yP = iPoint.Y + station * (jPoint.Y - iPoint.Y);

            return new Point(xP, yP);
        }

        public Point FindPointMakePerpendicularTo2Points(Point p1, Point p2)
        {
            List<Point> result = new List<Point>();

            double xA = p1.X;
            double yA = p1.Y;
            double xB = p2.X;
            double yB = p2.Y;

            LineParameters p = Parameters();

            if (p.B != 0)
            {
                double a1 = -p.A / p.B;
                double b1 = -p.C / p.B;

                double a = -1 * a1 * a1 - 1;
                double b = a1 * yA - 2 * a1 * b1 + a1 * yB + xA + xB;
                double c = yA * b1 - yA * yB - b1 * b1 + b1 * yB - xA * xB;

                double Delta = b * b - 4 * a * c;

                if (Delta >= 0)
                {
                    double x1 = (-b - Math.Sqrt(Delta)) / (2 * a);
                    double x2 = (-b + Math.Sqrt(Delta)) / (2 * a);

                    // Find corresponding y values using FindY method
                    double? y1 = FindY(x1, false);
                    if (y1 != null) result.Add(new Point(x1, y1.Value));

                    if (Delta > 0)
                    {
                        double? y2 = FindY(x2, false);
                        if (y2 != null) result.Add(new Point(x2, y2.Value));
                    }
                }
            }
            else
            {
                // p.B = 0
                double xC = -p.C / p.A;
                double a = -1;
                double b = yA + yB;
                double c = -yA * yB + xA * xC - xA * xB - xC * xC + xC * xB;

                double Delta = b * b - 4 * a * c;

                if (Delta >= 0)
                {
                    double y1 = (-b - Math.Sqrt(Delta)) / (2 * a);
                    double y2 = (-b + Math.Sqrt(Delta)) / (2 * a);

                    // Find corresponding y values using FindY method
                    result.Add(new Point(xC, y1));

                    if (Delta > 0)
                    {
                        result.Add(new Point(xC, y2));
                    }
                }
            }

            Point PP = p1;
            if (result.Count > 0)
            {
                PP = result.FirstOrDefault();
                if (PP.DistanceTo(p1) > PP.DistanceTo(p2))
                {
                    PP = result.LastOrDefault();
                }
            }


            return PP;
        }

    }
}