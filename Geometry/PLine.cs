using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class PLine
    {
        public List<Point> Points { get; private set; }
        public List<Line> Lines { get; private set; }

        // Constructor to initialize the PLINE with a list of points
        public PLine()
        {
            Points = new List<Point>();
            Lines = new List<Line>();
        }

        public PLine(List<Point> points)
        {
            

            Points = new List<Point>(points);
            Lines = GenerateLines(points);
        }

        // Generate lines from the list of points
        private List<Line> GenerateLines(List<Point> points)
        {
            var lines = new List<Line>();
            for (int i = 0; i < points.Count - 1; i++)
            {
                lines.Add(new Line(points[i], points[i + 1]));
            }
            return lines;
        }

        // Add a point to the PLINE
        public void AddPoint(Point point)
        {
            if (Points.Count > 0)
            {
                // Add a new line connecting the last point to the new point
                Lines.Add(new Line(Points[Points.Count - 1], point));
            }
            Points.Add(point);
        }

        // Get the total length of the PLINE
        public double Length()
        {
            double totalLength = 0;
            foreach (var line in Lines)
            {
                totalLength += line.iPoint.DistanceTo(line.jPoint);
            }
            return totalLength;
        }

        // Find the closest point on the PLINE to a given point
        public Point ClosestPoint(Point target, out double minDistance)
        {
            Point closestPoint = null;
            minDistance = double.MaxValue;

            foreach (var line in Lines)
            {
                // Find the closest point on the current line segment
                double distance = target.DistanceTo(line, false, out Point closest);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestPoint = closest;
                }
            }

            return closestPoint;
        }

        public PLine Offset(double offsetValue)
        {
            if (Points.Count < 2)
                throw new InvalidOperationException("PLINE must have at least two points to offset.");

            List<Point> newPoints = new List<Point>();
            List<Line> offsetLines = new List<Line>();

            // Step 1: Offset each line in the polyline
            foreach (var line in Lines)
            {
                offsetLines.Add(line.Offset(offsetValue));
            }

            // Step 2: Calculate intersection points for consecutive offset lines

            newPoints.Add(offsetLines.FirstOrDefault().iPoint);
            for (int i = 0; i < offsetLines.Count - 1; i++)
            {
                Line currentLine = offsetLines[i];
                Line nextLine = offsetLines[i + 1];

                // Find intersection of the two offset lines
                Point intersection = currentLine.OffsetIntersecWith(nextLine.Invert());

                if (intersection != null)
                {
                    
                    newPoints.Add(intersection);
                }
               
            }

            newPoints.Add(offsetLines.LastOrDefault().jPoint);

            // Create and return the new PLINE
            return new PLine(newPoints);
        }

        public PLine Offset(double offsetValue, bool RightSide)
        {
            return Offset(RightSide ? -offsetValue : offsetValue);
        }

        public Point PointAt(double station)
        {
            if (Lines.Count == 0) return null;
            if (station < 0) return Lines.First().iPoint;

            double accumulatedLength = 0;

            // Calculate total length and find which line contains the station point
            foreach (var line in Lines)
            {
                double lineLength = line.iPoint.DistanceTo(line.jPoint);
                if (accumulatedLength + lineLength >= station)
                {
                    // Station point is on this line
                    double remainingStation = station - accumulatedLength;
                    double ratio = remainingStation / lineLength;

                    // Calculate point coordinates using linear interpolation
                    double x = line.iPoint.X + ratio * (line.jPoint.X - line.iPoint.X);
                    double y = line.iPoint.Y + ratio * (line.jPoint.Y - line.iPoint.Y);
                    double z = line.iPoint.Z + ratio * (line.jPoint.Z - line.iPoint.Z);

                    return new Point(x, y, z);
                }
                accumulatedLength += lineLength;
            }

            // If station is beyond total length, return last point
            return Lines.Last().jPoint;
        }

        public double StationsAt(Point points, double tolerance = 1e-6)
        {
            
            if (Lines.Count == 0 || points == null)
                return -1;

            double accumulatedLength = 0;

            double stationAtPoint = 0;
            bool foundPoint = false;

            // Check each line segment
            foreach (var line in Lines)
            {
                double lineLength = line.iPoint.DistanceTo(line.jPoint);

                // Check if point is on this line segment
                if (IsPointOnLine(points, line, tolerance))
                {
                    stationAtPoint = accumulatedLength + line.iPoint.DistanceTo(points);
                    foundPoint = true;
                    break;
                }

                accumulatedLength += lineLength;
            }

            return foundPoint ? stationAtPoint : -1; // -1 indicates point not found on polyline

            
        }

        private bool IsPointOnLine(Point point, Line line, double tolerance)
        {
            // Calculate distances
            double d1 = point.DistanceTo(line.iPoint);
            double d2 = point.DistanceTo(line.jPoint);
            double lineLength = line.iPoint.DistanceTo(line.jPoint);

            // Check if point lies on line segment (within tolerance)
            return Math.Abs(d1 + d2 - lineLength) <= tolerance;
        }

        public List<Point> IntersecWith(Line L1)
        {
            List<Point> intersections = new List<Point>();
            if (Lines.Count == 0) return intersections;

            // Check intersection with each line in this PLine
            foreach (var line in Lines)
            {
                var points = line.IntersecWith(L1);
                intersections.AddRange(points);
            }

            return intersections;
        }

        public List<Point> IntersecWith(PLine PL1)
        {
            List<Point> intersections = new List<Point>();
            if (Lines.Count == 0 || PL1 == null || PL1.Lines.Count == 0)
                return intersections;

            // Check intersection of each line in this PLine with each line in PL1
            foreach (var line1 in Lines)
            {
                foreach (var line2 in PL1.Lines)
                {
                    var points = line1.IntersecWith(line2);
                    intersections.AddRange(points);
                }
            }

            // Optional: Remove duplicate intersection points within a small tolerance
            return RemoveDuplicatePoints(intersections);
        }

        // Helper method to remove duplicate points within a tolerance
        private List<Point> RemoveDuplicatePoints(List<Point> points, double tolerance = 1e-6)
        {
            if (points.Count <= 1) return points;

            List<Point> uniquePoints = new List<Point>();
            foreach (var point in points)
            {
                bool isDuplicate = false;
                foreach (var uniquePoint in uniquePoints)
                {
                    if (point.DistanceTo(uniquePoint) <= tolerance)
                    {
                        isDuplicate = true;
                        break;
                    }
                }
                if (!isDuplicate)
                {
                    uniquePoints.Add(point);
                }
            }
            return uniquePoints;
        }

        public PLine Invert()                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
        {
            // Reverse the points and create a new PLine
            return new PLine(Points.AsEnumerable().Reverse().ToList());
        }
    }
}
