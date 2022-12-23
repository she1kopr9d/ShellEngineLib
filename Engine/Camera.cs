using ShellEngineLib.Engine.Math;

namespace ShellEngineLib.Engine
{
    public class Camera
    {
        private FAngle _FOV = new FAngle();
        private float _h => (float)(System.Math.Abs(System.Math.Tan(_angleUnHalfFOV_Radian) * (_windowSize.x / 2)));
        private float _maxTangens => (float)System.Math.Abs(System.Math.Tan((180 / System.Math.PI) * (90 - (_FOV.Angle / 2))));
        private float _angleUnHalfFOV_Radian => (float)((System.Math.PI / 180) * 90 - (_FOV.Radian / 2));

        private bool _nonReverse = false;
        private float _maxDepth = 100;
        private Point _windowSize;
        private Render _renderCamera;
        private Player _player;

        public Camera(Point windowSize, float FOV, IDrawable brush, Player player)
        {
            _windowSize = windowSize;
            _FOV.Angle = FOV;
            _player = player;
            _renderCamera = new Render(brush);
        }

        public Point CameraViewPoint(Point point) 
            => Formater2D(point);

        public void Render()
        {
            _renderCamera.Update(_player.Position, _player.Angles, this);
        }

        private Point Formater2D(Point point)
        {
            float x = point.x, y = point.y, z = point.z;

            if (InFov(point) == false)
                return null;

            (x, z) = LineFormater2D(x, z, _windowSize.x);
            (y, z) = LineFormater2D(y, z, _windowSize.y);

            return new Point(x, y, z); 
        }

        private (float, float) LineFormater2D(float arg0, float arg1, float size)
        {
            if ((arg0 > (size / 2)) == _nonReverse)
                return (PrePointFormater(size, arg0, arg1, 1), arg1);
            else if ((arg0 < (-1 * (size / 2))) == _nonReverse)
                return (PrePointFormater(size, arg0, arg1, -1), arg1);
            else
                return (0, arg1);
        }

        private float PrePointFormater(float size, float prePoint_main, float prePoint_support, float koef)
            => ((size / 2) + (_h * prePoint_main / prePoint_support)) * koef;

        public bool InFov(Point point)
        {
            return (_maxTangens < System.Math.Abs(point.x / point.z) |
                _maxTangens < System.Math.Abs(point.y / point.z) |
                point.z > _maxDepth + _h |
                point.z < 0) == false;
        }
    }
}
