using System;
using System.IO;

public class Point3D
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }


    public Point3D() : this(0, 0, 0) 
    {
    }

    public Point3D(int x, int y) : this(x, y, 0) 
    {
    }

    
    public Point3D(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    
    public override string ToString()
    {
        return $"Point3D: X={X}, Y={Y}, Z={Z}";
    }

    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Point3D other = (Point3D)obj;
        return X == other.X && Y == other.Y && Z == other.Z;
    }

    
    public override int GetHashCode()
    {
        return (X, Y, Z).GetHashCode();
    }

    
    public static bool operator ==(Point3D p1, Point3D p2)
    {
        if (ReferenceEquals(p1, null))
            return ReferenceEquals(p2, null);

        return p1.Equals(p2);
    }

    
    public static bool operator !=(Point3D p1, Point3D p2)
    {
        return !(p1 == p2);
    }


    public static Point3D ReadPointFromUser(string pointName)
    {
        int x, y, z;

        Console.WriteLine($"Enter the coordinates for {pointName}:");

        
        Console.Write("X: ");
        while (!int.TryParse(Console.ReadLine(), out x))
        {
            Console.WriteLine("Invalid input for X, please enter an integer.");
            Console.Write("X: ");
        }

        Console.Write("Y: ");
        while (!int.TryParse(Console.ReadLine(), out y))
        {
            Console.WriteLine("Invalid input for Y, please enter an integer.");
            Console.Write("Y: ");
        }

       
        Console.Write("Z: ");
        while (!int.TryParse(Console.ReadLine(), out z))
        {
            Console.WriteLine("Invalid input for Z, please enter an integer.");
            Console.Write("Z: ");
        }

        return new Point3D(x, y, z);
    }
}


public class Program
{
    public static void Main(string[] args)
    {
        SafeReadPointFromUser();
    }


    public static void LogError(Exception ex)
    {
        using (StreamWriter writer = new StreamWriter("error_log.txt", true))
        {
            writer.WriteLine($"[{DateTime.Now}] Error: {ex.Message}");
            writer.WriteLine($"Stack Trace: {ex.StackTrace}");
        }
    }

    public static void SafeReadPointFromUser()
    {
        try
        {
            Point3D p1 = Point3D.ReadPointFromUser("P1");
            Point3D p2 = Point3D.ReadPointFromUser("P2");

            Console.WriteLine($"P1: {p1}");
            Console.WriteLine($"P2: {p2}");

            if (p1 == p2)
            {
                Console.WriteLine("The points are equal.");
            }
            else
            {
                Console.WriteLine("The points are not equal.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred. Check the log for details.");
            LogError(ex); 
        }
    }
}
