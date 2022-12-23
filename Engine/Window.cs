using ShellEngineLib.Engine.Math;

namespace ShellEngineLib.Engine
{
    public class Window : IDrawable
    {
        private int _windowId = 1;
        private int _width = 0;
        private int _height = 0;
        private int HALFWIDTH => _width / 2;
        private int HALFHEIGHT => _height / 2;

        private int _widthId => (_windowId % 2 == 0) ? 0 : 1;
        private int _heightId => (_windowId <= 1) ? 0 : 1;
        private int _minWidth => _width / 2 * _widthId;
        private int _minHeight => _height / 2 * _heightId;

        private IDrawable _brush;


        public Window(int width, int height, int id, IDrawable brush)
        {
            _width = width;
            _height = height;
            _windowId = id;
            _brush = brush;
        }

        public void Line(Math.Vector vector)
        {
            Math.Vector vec = new Math.Vector(
                        MinMaxPoint(vector.Start, _minWidth, _minHeight),
                        MinMaxPoint(vector.Finish, _minWidth, _minHeight));
            _brush.Line(vec);
        }

        public void LineCenter(Math.Vector vector)
        {
            Vector line = new Vector(
                new Point(HALFWIDTH / 2 + vector.Start.x, HALFHEIGHT / 2 + vector.Start.y, vector.Start.z),
                new Point(HALFWIDTH / 2 + vector.Finish.x, HALFHEIGHT / 2 + vector.Finish.y, vector.Finish.z));
            Line(line);
        }

        private Math.Point MinMaxPoint(Math.Point point, int maxX, int maxY) =>
            new Math.Point
            (
                MinMax((int)point.x, _width / 2) + maxX,
                MinMax((int)point.y, _height / 2) + maxY, 0
            );

        private int MinMax(int num, int max)
        {
            if (num < 0)
                return 0;
            else if (num > max)
                return max;
            return num;
        }
        public void LineEnd(Math.Vector vector)
        {
            Vector line = new Vector(
                new Point(vector.Start.x, _height / 2 - vector.Start.y, vector.Start.z),
                new Point(vector.Finish.x, _height / 2 - vector.Finish.y, vector.Finish.z));
            Line(line);
        }

        public void LineCenterEnd(Vector vector)
        {
            Vector line = new Vector(
                new Point(HALFWIDTH / 2 + vector.Start.x, HALFHEIGHT / 2 - vector.Start.y, vector.Start.z),
                new Point(HALFWIDTH / 2 + vector.Finish.x, HALFHEIGHT / 2 - vector.Finish.y, vector.Finish.z));
            Line(line);
        }
        public void Poly(Triangle triangle)
        {
            _brush.Poly(triangle);
        }

        public void Rect(Rect rect)
        {
            throw new NotImplementedException();
        }
    }
}
