using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Point3d
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Point3d(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    public Point3d()
    {
        X = 0;
        Y = 0;
        Z = 0;
    }
    public Point3d(Vector3d vec)
    {
        X = vec.X;
        Y = vec.Y;
        Z = vec.Z;
    }
    public Point3d(Point3d p1)
    {
        X = p1.X;
        Y = p1.Y;
        Z = p1.Z;
    }
    public Point3d(Point3d p1, Vector3d vec)
    {
        X = p1.X + vec.X;
        Y = p1.Y + vec.Y;
        Z = p1.Z + vec.Z;
    }
    public void Move(Vector3d vec)
    {
        X += vec.X;
        Y += vec.Y;
        Z += vec.Z;
    }
    public void Move(double x, double y, double z)
    {
        X += x;
        Y += y;
        Z += z;
    }

    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }

    public static Point3d operator + (Point3d a, Point3d b)
    {
        return new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }
    public static Point3d operator + (Point3d p, Vector3d vec)
    {
        return new(p, vec);
    }
}


