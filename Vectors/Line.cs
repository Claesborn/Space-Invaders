using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Line
{
    public Point3d p1;
    public Point3d p2;

    public double Length
    {
        get
        {
            return GetLength();
        }
    }

    public Line(Point3d p1, Point3d p2)
    {
        this.p1 = p1;
        this.p2 = p2;
    }
    public Line(Point3d p1, Vector3d vec)
    {
        this.p1 = p1;
        p2 = new Point3d(p1, vec);
    }

    public Point3d Lerp(double t)
    {
        //Ensure that t is within [0, 1] range.
        t = Math.Max(0, Math.Min(1, t));

        //Calculate interpolated point
        double newX = p1.X + t * (p2.X - p1.X);
        double newY = p1.Y + t * (p2.Y - p1.Y);
        double newZ = p1.Z + t * (p2.Z - p1.Z);
        return new Point3d(newX, newY, newZ);
    }
    public void DrawLine(double deltaTime, ConsoleColor color)
    {
        int x = (int)p1.X;
        int y = (int)p1.Y;

        Console.ForegroundColor = color;
        //Console.SetCursorPosition(x, y);
        //Console.Write('*');

        for (double t = 0; t <= 1; t += 0.02)
        {
            Point3d interpolatedPoint = Lerp(t);
            int consoleX = ((int)interpolatedPoint.X % Console.WindowWidth -1) +1;
            int consoleY = ((int)interpolatedPoint.Y % Console.WindowHeight -1) +1;



            Console.SetCursorPosition(consoleX, consoleY);
            Console.Write('*');
        }
    }
    //public void DrawLine(double deltaTime, ConsoleColor color)
    //{
    //    int x = (int)p1.X;
    //    int y = (int)p1.Y;
    //
    //    Console.ForegroundColor = color;
    //    Console.SetCursorPosition(x, y);
    //    Console.Write('*');
    //
    //    for (double t = 0; t <= 1; t += deltaTime)
    //    {
    //        Point interpolatedPoint = Lerp(t);
    //        int consoleX = (int)interpolatedPoint.X;
    //        int consoleY = (int)interpolatedPoint.Y;
    //        Console.SetCursorPosition(consoleX, consoleY);
    //        Console.Write('*');
    //    }
    //}
    public void DrawLine()
    {
        for (double t = 0; t <= 1; t += 0.02)
        {
            Point3d interpolatedPoint = Lerp(t);
            int consoleX = (int)interpolatedPoint.X;
            int consoleY = (int)interpolatedPoint.Y;
            if(consoleY >= 30)
            {
                consoleY = 0;
                interpolatedPoint.Y = 0;
            }
            if(consoleX >= 120)
            {
                consoleX = 0;
                interpolatedPoint.X = 0;
            }
            Console.SetCursorPosition(consoleX, consoleY);
            Console.Write('*');
        }
    }

    public void MoveLineStart(Vector3d vec)
    {
        p1.Move(vec);
    }
    public void MoveLineStart(double x, double y, double z)
    {
        p1.Move(x, y, z);
    }

    public void MoveLineEnd(Vector3d vec)
    {
        p2.Move(vec);
    }
    public void MoveLineEnd(double x, double y, double z)
    {
        p2.Move(x, y, z);
    }
    public void MoveLine(Vector3d vec)
    {
        p1.Move(vec);
        p2.Move(vec);
    }
    public void MoveLine(double x, double y, double z)
    {
        p1.Move(x, y, z);
        p2.Move(x, y, z);
    }
    public double GetLength()
    {
        double dx = p1.X - p2.X;
        double dy = p1.Y - p2.Y;
        double dz = p1.Z - p2.Z;
        return Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }


    public override string ToString()
    {

        return $"({p1.X}, {p1.Y}, {p1.Z}) - ({p2.X}, {p2.Y}, {p2.Z})";
        
    }
}

