using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellEngineLib.Engine.Math
{
    public class Polygon
    {
        private Point[] _points = new Point[3];
        public Polygon(Point[] points)
        {
            _points = points;
        }

        public string Print()
        {
            return _points[0].Print() + "\n" + _points[1].Print() + "\n" + _points[2].Print() + "\n\n";
        }
    }
}
