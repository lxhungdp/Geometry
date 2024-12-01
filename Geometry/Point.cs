using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class Point
    {
        public Point()
        { }
        public Point ShallowCopy()
        {
            return (Point)this.MemberwiseClone();
        }


        public Point(double x, double y)
        { this.X = x; this.Y = y; }

        public Point(double x, double y, double z)
        { this.X = x; this.Y = y; this.Z = z; }

        public Point(string label, double x, double y)
        {
            Label = label;
            X = x;
            Y = y;
        }


        public Point(string label, double x, double y, double z)
        {
            this.Label = label;
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public string Label { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }


        public Point toGlobal(Point LocalOrgigin, Line X_Axis)
        {
            // Step 1: Calculate direction vector for X-axis in world coordinates
            double dx = X_Axis.jPoint.X - X_Axis.iPoint.X;
            double dy = X_Axis.jPoint.Y - X_Axis.iPoint.Y;
            double length_X = Math.Sqrt(dx * dx + dy * dy);

            // Step 2: Normalize direction vector to get unit X axis
            double unitX_x = dx / length_X;
            double unitX_y = dy / length_X;

            // Step 3: Calculate unit vector for Y-axis by rotating 90 degrees
            double unitY_x = -unitX_y;
            double unitY_y = unitX_x;

            // Step 4: Define the transformation to map local A to world coordinates
            double xA_world = LocalOrgigin.X + (unitX_x * X) + (unitY_x * Y);
            double yA_world = LocalOrgigin.Y + (unitX_y * X) + (unitY_y * Y);

            Point p = new Point();
            p.Label = Label;
            p.X = xA_world;
            p.Y = yA_world;
            p.Z = Z;

            return p;

        }

        public Point toGlobal(Point LocalOrgigin, Point X_Axis)
        {
            // Step 1: Calculate direction vector for X-axis in world coordinates
            double dx = X_Axis.X - LocalOrgigin.X;
            double dy = X_Axis.Y - LocalOrgigin.Y;
            double length_X = Math.Sqrt(dx * dx + dy * dy);

            // Step 2: Normalize direction vector to get unit X axis
            double unitX_x = dx / length_X;
            double unitX_y = dy / length_X;

            // Step 3: Calculate unit vector for Y-axis by rotating 90 degrees
            double unitY_x = -unitX_y;
            double unitY_y = unitX_x;

            // Step 4: Define the transformation to map local A to world coordinates
            double xA_world = LocalOrgigin.X + (unitX_x * X) + (unitY_x * Y);
            double yA_world = LocalOrgigin.Y + (unitX_y * X) + (unitY_y * Y);

            Point p = new Point();
            p.Label = Label;
            p.X = xA_world;
            p.Y = yA_world;
            p.Z = Z;

            return p;

        }

        public Point toLocal(Point LocalOrigin, Line X_Axis)
        {
            // Step 1: Translate point A to the local origin
            double translated_X = X - LocalOrigin.X;
            double translated_Y = Y - LocalOrigin.Y;

            // Step 2: Calculate the X-axis unit vector of the local coordinate system in world coordinates
            double dx = X_Axis.jPoint.X - X_Axis.iPoint.X;
            double dy = X_Axis.jPoint.Y - X_Axis.iPoint.Y;

            double length_X = Math.Sqrt(dx * dx + dy * dy);

            // Normalize to get unit vector for X axis
            double unitX_x = dx / length_X;
            double unitX_y = dy / length_X;

            // Step 3: Calculate the Y-axis unit vector (perpendicular to X)
            double unitY_x = -unitX_y;
            double unitY_y = unitX_x;

            // Step 4: Project the translated point onto the local axes
            double xA_local = translated_X * unitX_x + translated_Y * unitX_y;
            double yA_local = translated_X * unitY_x + translated_Y * unitY_y;

            Point p = new Point();
            p.Label = Label;
            p.X = xA_local;
            p.Y = yA_local;
            p.Z = Z;

            return p;

        }

        public Point toLocal(Point LocalOrigin, Point X_Axis)
        {
            // Step 1: Translate point A to the local origin
            double translated_X = X - LocalOrigin.X;
            double translated_Y = Y - LocalOrigin.Y;

            // Step 2: Calculate the X-axis unit vector of the local coordinate system in world coordinates
            double dx = X_Axis.X - LocalOrigin.X;
            double dy = X_Axis.Y - LocalOrigin.Y;

            double length_X = Math.Sqrt(dx * dx + dy * dy);

            // Normalize to get unit vector for X axis
            double unitX_x = dx / length_X;
            double unitX_y = dy / length_X;

            // Step 3: Calculate the Y-axis unit vector (perpendicular to X)
            double unitY_x = -unitX_y;
            double unitY_y = unitX_x;

            // Step 4: Project the translated point onto the local axes
            double xA_local = translated_X * unitX_x + translated_Y * unitX_y;
            double yA_local = translated_X * unitY_x + translated_Y * unitY_y;

            Point p = new Point();
            p.Label = Label;
            p.X = xA_local;
            p.Y = yA_local;
            p.Z = Z;

            return p;

        }

        public Point ToGlobalYZTrigonometric(Point localOrigin, Line localxAxis)
        {
            // Step 1: Calculate the direction of the local X-axis in the global system
            double deltaX = localxAxis.jPoint.X - localxAxis.iPoint.X;
            double deltaY = localxAxis.jPoint.Y - localxAxis.iPoint.Y;
            double deltaZ = localxAxis.jPoint.Z - localxAxis.iPoint.Z;

            // Step 2: Calculate the rotation angles (reverse of ToLocalZYTrigonometric)
            double alpha = -Math.Atan2(deltaY, deltaX); // Rotation around Z-axis
            double beta = Math.Atan2(deltaZ, Math.Sqrt(deltaX * deltaX + deltaY * deltaY)); // Rotation around Y-axis

            // Step 3: Reverse the rotation
            double global_X = this.X * Math.Cos(alpha) * Math.Cos(beta)
                            + this.Y * Math.Sin(alpha)
                            - this.Z * Math.Cos(alpha) * Math.Sin(beta);

            double global_Y = -this.X * Math.Sin(alpha) * Math.Cos(beta)
                             + this.Y * Math.Cos(alpha)
                             + this.Z * Math.Sin(alpha) * Math.Sin(beta);

            double global_Z = this.X * Math.Sin(beta)
                            + this.Z * Math.Cos(beta);

            // Step 4: Reverse the translation
            Point globalPoint = new Point();
            globalPoint.X = global_X + localOrigin.X;
            globalPoint.Y = global_Y + localOrigin.Y;
            globalPoint.Z = global_Z + localOrigin.Z;

            return globalPoint;
        }

        public Point ToLocalZYTrigonometric(Point localOrigin, Line xAxis)
        {
            // Calculate the direction of the X-axis line
            double deltaX = xAxis.jPoint.X - xAxis.iPoint.X;
            double deltaY = xAxis.jPoint.Y - xAxis.iPoint.Y;
            double deltaZ = xAxis.jPoint.Z - xAxis.iPoint.Z;

            // Calculate the rotation angle in the XOY plane
            double alpha = -Math.Atan2(deltaY, deltaX);

            double beta = Math.Atan2(deltaZ, Math.Sqrt(deltaX * deltaX + deltaY * deltaY));

            double dx = this.X - localOrigin.X;
            double dy = this.Y - localOrigin.Y;
            double dz = this.Z - localOrigin.Z;
            // Perform the rotation and translation

            Point newPoint = new Point();
            newPoint.X = dx * Math.Cos(alpha) * Math.Cos(beta) - dy * Math.Sin(alpha) * Math.Cos(beta) + dz * Math.Sin(beta);
            newPoint.Y = dx * Math.Sin(alpha) + dy * Math.Cos(alpha);
            newPoint.Z = -dx * Math.Cos(alpha) * Math.Sin(beta) + dy * Math.Sin(alpha) * Math.Sin(beta) + dz * Math.Cos(beta);

            return newPoint;
        }




        public Point ToLocalZY(Point localOrigin, Line xAxis)
        {
            // Step 1: Translate the point to the local origin
            double translated_X = this.X - localOrigin.X;
            double translated_Y = this.Y - localOrigin.Y;
            double translated_Z = this.Z - localOrigin.Z;

            // Step 2: Calculate the X-axis unit vector (defined by the line xAxis)
            double dx = xAxis.jPoint.X - xAxis.iPoint.X;
            double dy = xAxis.jPoint.Y - xAxis.iPoint.Y;
            double dz = xAxis.jPoint.Z - xAxis.iPoint.Z;

            double length_X = Math.Sqrt(dx * dx + dy * dy + dz * dz);

            // Normalize to get the unit vector for the X-axis
            double unitX_x = dx / length_X;
            double unitX_y = dy / length_X;
            double unitX_z = dz / length_X;

            // Step 3: Calculate the Y-axis unit vector (perpendicular to X and in the XOY plane)
            double length_Y = Math.Sqrt(unitX_x * unitX_x + unitX_y * unitX_y);
            double unitY_x = -unitX_y / length_Y;
            double unitY_y = unitX_x / length_Y;
            double unitY_z = 0; // Y-axis lies in the XOY plane, so Z is zero

            // Step 4: Calculate the Z-axis unit vector (perpendicular to both X and Y)
            double unitZ_x = unitX_y * unitY_z - unitX_z * unitY_y;
            double unitZ_y = unitX_z * unitY_x - unitX_x * unitY_z;
            double unitZ_z = unitX_x * unitY_y - unitX_y * unitY_x;

            // Step 5: Project the translated point onto the local axes
            double xA_local = translated_X * unitX_x + translated_Y * unitX_y + translated_Z * unitX_z;
            double yA_local = translated_X * unitY_x + translated_Y * unitY_y + translated_Z * unitY_z;
            double zA_local = translated_X * unitZ_x + translated_Y * unitZ_y + translated_Z * unitZ_z;

            // Step 6: Return the transformed point
            Point newPoint = new Point
            {
                Label = Label, // Preserve the label
                X = xA_local,
                Y = yA_local,
                Z = zA_local
            };

            return newPoint;
        }

        public Point ToLocalZY(Point localOrigin, Point xAxis)
        {
            // Step 1: Translate the point to the local origin
            double translated_X = this.X - localOrigin.X;
            double translated_Y = this.Y - localOrigin.Y;
            double translated_Z = this.Z - localOrigin.Z;

            // Step 2: Calculate the X-axis unit vector (defined by the line xAxis)
            double dx = xAxis.X - localOrigin.X;
            double dy = xAxis.Y - localOrigin.Y;
            double dz = xAxis.Z - localOrigin.Z;

            double length_X = Math.Sqrt(dx * dx + dy * dy + dz * dz);

            // Normalize to get the unit vector for the X-axis
            double unitX_x = dx / length_X;
            double unitX_y = dy / length_X;
            double unitX_z = dz / length_X;

            // Step 3: Calculate the Y-axis unit vector (perpendicular to X and in the XOY plane)
            double length_Y = Math.Sqrt(unitX_x * unitX_x + unitX_y * unitX_y);
            double unitY_x = -unitX_y / length_Y;
            double unitY_y = unitX_x / length_Y;
            double unitY_z = 0; // Y-axis lies in the XOY plane, so Z is zero

            // Step 4: Calculate the Z-axis unit vector (perpendicular to both X and Y)
            double unitZ_x = unitX_y * unitY_z - unitX_z * unitY_y;
            double unitZ_y = unitX_z * unitY_x - unitX_x * unitY_z;
            double unitZ_z = unitX_x * unitY_y - unitX_y * unitY_x;

            // Step 5: Project the translated point onto the local axes
            double xA_local = translated_X * unitX_x + translated_Y * unitX_y + translated_Z * unitX_z;
            double yA_local = translated_X * unitY_x + translated_Y * unitY_y + translated_Z * unitY_z;
            double zA_local = translated_X * unitZ_x + translated_Y * unitZ_y + translated_Z * unitZ_z;

            // Step 6: Return the transformed point
            Point newPoint = new Point
            {
                Label = Label, // Preserve the label
                X = xA_local,
                Y = yA_local,
                Z = zA_local
            };

            return newPoint;
        }
        public Point LocaltoLocal(Point OriginalOrigin, Point OriginalX, Point NewOrigin, Point NewX)
        {
            //Local to Golobal
            Point p1 = this.ToGlobalYZ(OriginalOrigin, OriginalX);
            return p1.ToLocalZY(NewOrigin, NewX);
        }

        public Point ToGlobalYZ(Point localOrigin, Line localxAxis)
        {
            // Step 1: Calculate the direction of the local X-axis in the global system
            double dx = localxAxis.jPoint.X - localxAxis.iPoint.X;
            double dy = localxAxis.jPoint.Y - localxAxis.iPoint.Y;
            double dz = localxAxis.jPoint.Z - localxAxis.iPoint.Z;

            double length_X = Math.Sqrt(dx * dx + dy * dy + dz * dz);

            // Normalize to get the unit vector for the X-axis
            double unitX_x = dx / length_X;
            double unitX_y = dy / length_X;
            double unitX_z = dz / length_X;

            // Step 2: Calculate the local Y-axis unit vector
            double length_Y = Math.Sqrt(unitX_x * unitX_x + unitX_y * unitX_y);
            double unitY_x = -unitX_y / length_Y;
            double unitY_y = unitX_x / length_Y;
            double unitY_z = 0; // Y-axis lies in the XOY plane, so Z is zero

            // Step 3: Calculate the local Z-axis unit vector (cross product of X and Y)
            double unitZ_x = unitX_y * unitY_z - unitX_z * unitY_y;
            double unitZ_y = unitX_z * unitY_x - unitX_x * unitY_z;
            double unitZ_z = unitX_x * unitY_y - unitX_y * unitY_x;

            // Step 4: Transform the local point back to the global system
            double global_X = this.X * unitX_x + this.Y * unitY_x + this.Z * unitZ_x;
            double global_Y = this.X * unitX_y + this.Y * unitY_y + this.Z * unitZ_y;
            double global_Z = this.X * unitX_z + this.Y * unitY_z + this.Z * unitZ_z;

            // Step 5: Apply the reverse translation to move back to the global origin
            Point globalPoint = new Point
            {
                Label = Label, // Preserve the label
                X = global_X + localOrigin.X,
                Y = global_Y + localOrigin.Y,
                Z = global_Z + localOrigin.Z
            };

            return globalPoint;
        }
        public Point ToGlobalYZ(Point localOrigin, Point localxAxis)
        {
            // Step 1: Calculate the direction of the local X-axis in the global system
            double dx = localxAxis.X - localOrigin.X;
            double dy = localxAxis.Y - localOrigin.Y;
            double dz = localxAxis.Z - localOrigin.Z;

            double length_X = Math.Sqrt(dx * dx + dy * dy + dz * dz);

            // Normalize to get the unit vector for the X-axis
            double unitX_x = dx / length_X;
            double unitX_y = dy / length_X;
            double unitX_z = dz / length_X;

            // Step 2: Calculate the local Y-axis unit vector
            double length_Y = Math.Sqrt(unitX_x * unitX_x + unitX_y * unitX_y);
            double unitY_x = -unitX_y / length_Y;
            double unitY_y = unitX_x / length_Y;
            double unitY_z = 0; // Y-axis lies in the XOY plane, so Z is zero

            // Step 3: Calculate the local Z-axis unit vector (cross product of X and Y)
            double unitZ_x = unitX_y * unitY_z - unitX_z * unitY_y;
            double unitZ_y = unitX_z * unitY_x - unitX_x * unitY_z;
            double unitZ_z = unitX_x * unitY_y - unitX_y * unitY_x;

            // Step 4: Transform the local point back to the global system
            double global_X = this.X * unitX_x + this.Y * unitY_x + this.Z * unitZ_x;
            double global_Y = this.X * unitX_y + this.Y * unitY_y + this.Z * unitZ_y;
            double global_Z = this.X * unitX_z + this.Y * unitY_z + this.Z * unitZ_z;

            // Step 5: Apply the reverse translation to move back to the global origin
            Point globalPoint = new Point
            {
                Label = Label, // Preserve the label
                X = global_X + localOrigin.X,
                Y = global_Y + localOrigin.Y,
                Z = global_Z + localOrigin.Z
            };

            return globalPoint;
        }

        public double DistanceTo(Point p)
        {
            return Math.Sqrt(Math.Pow(p.X - this.X, 2) + Math.Pow(p.Y - this.Y, 2));
        }

        public double DistanceTo(Line L, bool isExtend, out Point p)
        {
            // Line segment endpoints
            double x1 = L.iPoint.X;
            double y1 = L.iPoint.Y;
            double x2 = L.jPoint.X;
            double y2 = L.jPoint.Y;

            // Direction vector of the line segment
            double dx = x2 - x1;
            double dy = y2 - y1;

            // Calculate the projection factor t for the perpendicular projection of the point onto the line
            double t = ((this.X - x1) * dx + (this.Y - y1) * dy) / (dx * dx + dy * dy);

            if (isExtend)
            {
                // If extending, allow t to be any value, as it’s an infinite line
                double closestX = x1 + t * dx;
                double closestY = y1 + t * dy;
                p = new Point(closestX, closestY);
            }
            else
            {
                // If not extending, restrict t to [0, 1] range, treating L as a segment
                if (t >= 0 && t <= 1)
                {
                    // Projection falls within the line segment, calculate intersection point
                    double closestX = x1 + t * dx;
                    double closestY = y1 + t * dy;
                    p = new Point(closestX, closestY);  // Closest point on the line segment
                }
                else
                {
                    // Projection is outside the segment, choose the closest endpoint
                    double distanceToI = Math.Sqrt(Math.Pow(x1 - this.X, 2) + Math.Pow(y1 - this.Y, 2));
                    double distanceToJ = Math.Sqrt(Math.Pow(x2 - this.X, 2) + Math.Pow(y2 - this.Y, 2));

                    if (distanceToI < distanceToJ)
                    {
                        p = new Point(x1, y1);  // Closest point is iPoint
                    }
                    else
                    {
                        p = new Point(x2, y2);  // Closest point is jPoint
                    }
                }
            }



            // Return the distance between the original point and the closest point p
            return Math.Sqrt(Math.Pow(p.X - this.X, 2) + Math.Pow(p.Y - this.Y, 2));
        }

        public Point FindPointWidthDistanceandPerpendicular(double distance, Point C, Point ComparedPoint)
        {
            //Find Point B, so that distance to this point (A) is Distance
            //And Make the angle AB is perpendicular to BC

            double xA = this.X;
            double yA = this.Y;
            double xC = C.X;
            double yC = C.Y;

            //Establish euation ax + b*sqrt(c - x2) + d = 0
            double a = (xC - xA) * (xC - xA) + (yC - yA) * (yC - yA);
            double b = 2 * (xC - xA) * (xA * xA - distance * distance - xA * xC) - 2 * xA * (yC - yA) * (yC - yA);
            double c = Math.Pow(xA * xA - distance * distance - xA * xC, 2) - (yC - yA) * (yC - yA) * (distance * distance - xA * xA);

            double Delta = b * b - 4 * a * c;

            Point p = new Point(this.X, this.Y - distance);


            if (Delta >= 0)
            {
                double x1 = (-b - Math.Sqrt(Delta)) / (2 * a);
                double x2 = (-b + Math.Sqrt(Delta)) / (2 * a);

                double y11 = (-Math.Sqrt(distance * distance - (x1 - xA) * (x1 - xA)) + yA);
                double y12 = (Math.Sqrt(distance * distance - (x1 - xA) * (x1 - xA)) + yA);

                double y21 = (-Math.Sqrt(distance * distance - (x2 - xA) * (x2 - xA)) + yA);

                double y22 = (Math.Sqrt(distance * distance - (x2 - xA) * (x2 - xA)) + yA);

                List<Point> points = new List<Point>();
                List<Point> p1 = new List<Point>();
                p1.Add(new Point(x1, y11));
                p1.Add(new Point(x1, y12));
                p1.Add(new Point(x2, y21));
                p1.Add(new Point(x2, y22));

                for (int i = 0; i < p1.Count; i++)
                {
                    Line L1 = new Line(this.X, this.Y, p1[i].X, p1[i].Y);
                    Line L2 = new Line(p1[i].X, p1[i].Y, C.X, C.Y);
                    Line L3 = new Line(this.X, this.Y, C.X, C.Y);

                    //Check Again if Perpendicular
                    if (L1.isPerpendicularWith(L2) && !L3.isSameSide(p1[i], ComparedPoint))
                    {
                        points.Add(p1[i]);
                    }
                }

                if (points.Count > 0)
                    p = points.FirstOrDefault();
            }


            return p;

        }

    }
}