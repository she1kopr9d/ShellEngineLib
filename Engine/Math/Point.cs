using System.Numerics;

namespace ShellEngineLib.Engine.Math
{
    public class Point
    {
        public float x = 0;
        public float y = 0;
        public float z = 0;

        public static Point Zero = new Point(0, 0, 0);
        public static Point One = new Point(1, 1, 1);

        public Point(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Point(float[] points)
        {
            x = points[0];
            y = points[1];
            z = points[2];
        }

        public Point(float[,] p)
        {
            x = p[0, 0];
            y = p[1, 0];
            z = p[2, 0];
        }

        public Matrix ConvertInMatrix()
        {
            return new Matrix(3, 1,
                new float[,]
                {
                    { x },
                    { y },
                    { z }
                });
        }

        public string Print() => "|" + x + "|" + y + "|" + z + "|";

        public void SetX(float x) => this.x = x;
        public void SetY(float y) => this.y = y;
        public void SetZ(float z) => this.z = z;

        public static float Distance(float num1, float num2)
        {
            return (float)System.Math.Sqrt(
                    System.Math.Pow(num1, 2) +
                    System.Math.Pow(num2, 2)
                );
        }
        public static float Distance3D(float num1, float num2, float num3)
        {
            return (float)System.Math.Sqrt(
                    System.Math.Pow(num1, 2) +
                    System.Math.Pow(num2, 2) +
                    System.Math.Pow(num3, 2)
                );
        }
        public static float Distance3D(Point point)
        {
            return (float)System.Math.Sqrt(
                    System.Math.Pow(point.x, 2) +
                    System.Math.Pow(point.y, 2) +
                    System.Math.Pow(point.z, 2)
                );
        }

        public Point Copy() => new Point( x, y, z );

        public static Point operator +(Point p1, Point p2)
        {
            if (p1 == null | p2 == null)
                return null;
            return new Point(p1.x + p2.x, p1.y + p2.y, p1.z + p2.z);
        }
        public static Point operator -(Point p1, Point p2)
        {
            if (p1 == null | p2 == null)
                return null;
            return new Point(p1.x - p2.x, p1.y - p2.y, p1.z - p2.z);
        }
        public static Point operator *(Point p, float multiply)
        {
            return new Point(
                    p.x * multiply,
                    p.y * multiply,
                    p.z * multiply
                );
        }

        public static Point operator *(Point left, Point right)
        {
            return new Point(
                    left.x * right.x,
                    left.y * right.y,
                    left.z * right.z
                );
        }

        public static Point operator *(Point p, Matrix matrix)
        {
            if (p == null)
                return null;
            return (matrix * p.
                        ConvertInMatrix()).
                            ConvertInPoint();
        }
    }
}
