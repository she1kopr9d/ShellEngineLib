using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellEngineLib.Engine.Math
{
    public class Rect
    {
        public Point[] points = new Point[]
        {
            new Point(0, 0, 0),
            new Point(0, 0, 0),
            new Point(0, 0, 0),
            new Point(0, 0, 0)
        };

        public Rect(Point[] points)
        {
            if (points == null)
                return;
            this.points = points;
        }

        public Rect() { }
    }
}
