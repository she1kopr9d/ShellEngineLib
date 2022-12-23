using ShellEngineLib.Engine.Math;
using System.Security.Cryptography;

namespace ShellEngineLib.Engine.Figures
{
    public class Figure
    {
        public RealtimeFigure NowFigure;

        public AngleVector Angles => _angles;
        public Point Scale => _scale;
        public Point Offset => _offset;

        private IO.FigureTemplate _template;

        private Point _offset = new Point(0, 0, 0);
        private AngleVector _angles = new AngleVector(0, 0, 0);
        private Point _scale = new Point(1, 1, 1);
        private Point[] _supportDots;

        public Figure(IO.FigureTemplate template)
        {
            _template = template;
        }
        
        public void SetSettings(AngleVector angles, Point offset, Point scale)
        {
            if(angles != null)
            {
                _angles.x.Angle = angles.x.Angle;
                _angles.y.Angle = angles.y.Angle;
                _angles.z.Angle = angles.z.Angle;
            }
            if(offset != null)
                _offset = offset;
            if(scale != null)
                _scale = scale;
            UpdateFigure();
        }

        private void UpdateFigure()
        {
            Point[] points = new Point[_template.Vertex.Length];
            Point[] supportPoints = new Point[8];

            {
                for (int i = 0; i < points.Length; i++)
                    points[i] = UpdateDot(_template.Vertex[i].Copy());
            }
            {
                for (int i = 0; i < 8; i++)
                    supportPoints[i] = UpdateDot(_template.SupportVertex[i].Copy());
            }

            NowFigure = new RealtimeFigure(points, _template, supportPoints);
        }

        private Point UpdateDot(Point point)
        {
            point *= Matrix.Scale(_scale);
            point *= Matrix.XZ(_angles.y.Radian);
            point *= Matrix.YZ(_angles.x.Radian);
            point += _offset;
            return point;
        }
    }
}
