using ShellEngineLib.Engine.IO;
using ShellEngineLib.Engine.Math;

namespace ShellEngineLib.Engine.Figures
{
    public class RealtimeFigure
    {
        public static Point[] supportVertexVertexPlan = new Point[]
        {
            new Point(0, 1, 0),
            new Point(1, 2, 0),
            new Point(2, 3, 0),
            new Point(3, 0, 0),
            new Point(0, 4, 0),
            new Point(1, 5, 0),
            new Point(2, 6, 0),
            new Point(3, 7, 0),
            new Point(4, 5, 0),
            new Point(5, 6, 0),
            new Point(6, 7, 0),
            new Point(7, 4, 0)
        };

        public Point[] vertex;
        public FigureTemplate template;
        public Point[] supportVertex;

        public RealtimeFigure(Point[] vertex, FigureTemplate template, Point[] supportVertex)
        {
            this.vertex = vertex;
            this.template = template;
            this.supportVertex = supportVertex;
        }
    }
}
