using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellEngineLib.Engine.Math
{
    public class Vector
    {
        public Point Start => _start;
        public Point Finish => _finish;

        private Point _start;
        private Point _finish;

        public static Vector Zero = new Vector(
                Point.Zero, Point.Zero
            );

        public Vector(Point p1, Point p2)
        {
            _start = p1;
            _finish = p2;
        }
        public Vector(Point[] points)
        {
            _start = points[0];
            _finish = points[1];
        }

        public float Lenght()
        {
            return (float)System.Math.Sqrt(
                System.Math.Pow(_finish.x - _start.x, 2) +
                System.Math.Pow(_finish.y - _start.y, 2) +
                System.Math.Pow(_finish.z - _start.z, 2)
                );
        }

        public Matrix ConvertInMatrix()
        {
            return new Matrix(3, 1, new float[,]
            {
                {_start.x },
                {_start.y },
                {_start.z }
            });
        }

        public string Print()
        {
            return _start.Print() + "\n" + _finish.Print() + "\n\n";
        }

        public void SetFinish(Point p)
        {
            _finish = p;
        }
        public void SetStart(Point p)
        {
            _start = p;
        }

        public static Vector ScaleMultiply(Vector vector, float start, float finish) =>
            ScaleMultiply(vector, 
                new Point(start, start, start), 
                new Point(finish, finish, finish));

        public static Vector ScaleMultiply(Vector vector, Point start, Point finish)
        {

            Point p1 = vector.Start;
            Point p2 = vector.Finish;

            float koef = 100;

            p1 = (Matrix.Scale(koef / start.x, koef / start.y, koef / start.z) * p1.ConvertInMatrix()).ConvertInPoint();
            p2 = (Matrix.Scale(koef / finish.x, koef / finish.y, koef / finish.z) * p2.ConvertInMatrix()).ConvertInPoint();

            return new Vector(p1, p2);
        }

        public static Vector[] RenderVectors(Figures.Figure figure, Player player)
        {

            return null;
        }

        public static Vector operator -(Vector vector, Point point)
        {
            return new Vector(
                vector._start - point,
                vector._finish - point);
        }
        public static Vector operator +(Vector vector, Point point)
        {
            return new Vector(
                vector._start + point,
                vector._finish + point);
        }
        public static Vector operator *(Vector vector, Matrix matrix)
        {
            return new Vector(
                    (matrix * vector._start.
                        ConvertInMatrix()).
                            ConvertInPoint(),
                    (matrix * vector._finish.
                        ConvertInMatrix()).
                            ConvertInPoint());
        }
    }
}
