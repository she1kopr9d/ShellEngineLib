namespace ShellEngineLib.Engine.Math
{
    public class Triangle
    {
        public Point[] _vertex = new Point[]
        {
            new Point(0, 0, 0),
            new Point(0, 0, 0),
            new Point(0, 0, 0)
        };

        public bool NULL => _vertex != null & _vertex[0] != null & _vertex[1] != null & _vertex[2] != null;

        public Triangle() { }
        public Triangle(Point[] vertex)
        {
            _vertex[0] = vertex[0];
            _vertex[1] = vertex[1];
            _vertex[2] = vertex[2];
        }

        public void CopyToRect(int offset, Rect rect)
        {
            for (int i = 0; i < 3; i++)
                _vertex[i] = rect.points[i + offset];
        }

        public void Print()
        {
            foreach(Point p in _vertex)
                Console.WriteLine(p?.Print());
        }
    }
}
