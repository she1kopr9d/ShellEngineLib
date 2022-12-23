using ShellEngineLib.Engine.Math;
using ShellEngineLib.Engine;

namespace ShellEngineLib.Engine
{
    public class Draw : IDrawable
    {
        private int _multiplyKoef = 1;

        private IPrimitiveDraw _brush;
        private Point _windowSize;
        private Point _windowSizeHalf;

        private static int _depth = 1000;

        public Draw(IPrimitiveDraw brush, Point windowSize)
        {
            _windowSize = windowSize;
            _windowSizeHalf = new Point(windowSize.x / 2, windowSize.y / 2, 0);
            _brush = brush;
        }
        public void Line(Vector vector)
        {
            if (vector == null)
                return;
            if (vector.Finish == null | vector.Start == null)
                return;
            /*if (vector.Finish.z <= 0 & vector.Start.z <= 0)
                return;*/

            //if (vector.Finish.z < _depth | vector.Start.z < _depth)
                _brush.Line(vector);
        }

        public void LineCenter(Vector vector)
        {
            Vector line = new Vector(
                new Point(_windowSizeHalf.x + vector.Start.x, _windowSizeHalf.y + vector.Start.y, vector.Start.z),
                new Point(_windowSizeHalf.x + vector.Finish.x, _windowSizeHalf.y + vector.Finish.y, vector.Finish.z));
            Line(line);
        }

        public void LineEnd(Vector vector)
        {
            Vector line = new Vector(
                new Point(vector.Start.x, _windowSize.y - vector.Start.y, vector.Start.z),
                new Point(vector.Finish.x, _windowSize.y - vector.Finish.y, vector.Finish.z));
            Line(line);
        }

        public void LineCenterEnd(Vector vector)
        {
            Vector line = new Vector(
                new Point(_windowSizeHalf.x + vector.Start.x, _windowSizeHalf.y - vector.Start.y, vector.Start.z),
                new Point(_windowSizeHalf.x + vector.Finish.x, _windowSizeHalf.y - vector.Finish.y, vector.Finish.z));
            Line(line);
        }

        public void Poly(Triangle triangle)
        {
            foreach (Point point in triangle._vertex)
                if (point == null)
                    return;

            bool isCreated = true;
            /*foreach (Point point in triangle._vertex)
                isCreated = isCreated | (point.z < _depth & point.z >= 0);*/

            if (isCreated == true)
                _brush.Poly(triangle);
        }

        public void Rect(Rect rect)
        {
            foreach (Point point in rect.points)
                if (point == null)
                    return;

            bool isCreated = true;
            /*foreach (Point point in rect.points)
                isCreated = isCreated | (point.z < _depth & point.z >= 0);*/


            if (isCreated == true)
                _brush.Rect(rect);
        }
    }
}
