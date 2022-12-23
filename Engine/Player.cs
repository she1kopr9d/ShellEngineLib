using ShellEngineLib.Engine.Math;
using System.Runtime.CompilerServices;

namespace ShellEngineLib.Engine
{
    public class Player
    {
        public Math.AngleVector Angles => _angles;
        public Figures.Figure Skin => _playerSkin;
        public Math.Point Position => _position;
        public Camera PlayerCamera => _camera;

        private Point _position;
        private AngleVector _angles;
        private Camera _camera;
        private Figures.Figure _playerSkin;
        private float _playerSpeed;

        private bool[] _keyFlags = new bool[11];
        private KeyEnum[] _keys = new KeyEnum[]
        {
            KeyEnum.W,
            KeyEnum.S,
            KeyEnum.A,
            KeyEnum.D,
            KeyEnum.LEFT,
            KeyEnum.RIGHT,
            KeyEnum.UP,
            KeyEnum.DOWN,
            KeyEnum.SPACE,
            KeyEnum.SHIFT,
            KeyEnum.CONTROL
        };
        
        private bool _isPlayer = false;

        private static bool _isWriteKey = false;
        private Point _windowSize;
        private Point _windowHalfSize;

        public Player(float playerSpeed, Point windowSize, IDrawable brush, bool isPlayer = false)
        {
            _angles = new AngleVector(0, 0, 0);
            _position = new Point(0, 0, 0);
            _camera = new Camera(windowSize, 90, brush, this);
            _isPlayer = isPlayer;
            _windowSize = windowSize;
            _windowHalfSize = new Point(windowSize.x / 2, windowSize.y / 2, 0);
            _playerSpeed = playerSpeed; 
        }

        public void SetAngles(float x, float y, float z)
        {
            _angles.x.Angle = x;
            _angles.y.Angle = y;
            _angles.z.Angle = z;
        }

        public void SetSkin(Figures.Figure skin)
        {
            _playerSkin = skin;
        }

        public void SetPlayerSpeed(float speed)
        {
            _playerSpeed = speed;
        }

        public void SkinMovement()
        {
            _playerSkin.SetSettings(Angles, Position, null);
        }

        public void MouseMove(Point point)
        {
            _angles.y.Angle += (_windowHalfSize.x - point.x) / 1.5f;
            _angles.x.Angle += (_windowHalfSize.y - point.y) / 1.5f;

            if ((_angles.x.Angle + 180) % 360 > 225)
                _angles.x.Angle = 45;
            if ((_angles.x.Angle + 180) % 360 < 135)
                _angles.x.Angle = 315;
        }

        public void KeyAdd(KeyEnum key)
        {
            for (int i = 0; i < _keys.Length; i++)
                _keyFlags[i] = _keyFlags[i] | _keys[i] == key;
        }

        public void KeyRemove(KeyEnum key)
        {
            for (int i = 0; i < _keys.Length; i++)
                _keyFlags[i] = _keyFlags[i] ^ _keys[i] == key;
        }

        public void Movement()
        {
            double sinA = System.Math.Sin(_angles.y.Radian);
            double cosA = System.Math.Cos(_angles.y.Radian);

            if (_keyFlags[10] == _isPlayer)
                return;

            if (_keyFlags[0] == true)
            {
                _position.x += (float)(-_playerSpeed * sinA);
                _position.z += (float)(_playerSpeed * cosA);
            }
            if (_keyFlags[1] == true)
            {
                _position.x += (float)(_playerSpeed * sinA);
                _position.z += (float)(-_playerSpeed * cosA);
            }
            if (_keyFlags[2] == true)
            {
                _position.x += (float)(-_playerSpeed * cosA);
                _position.z += (float)(-_playerSpeed * sinA);
            }
            if (_keyFlags[3] == true)
            {
                _position.x += (float)(_playerSpeed * cosA);
                _position.z += (float)(_playerSpeed * sinA);
            }
            if (_keyFlags[4] == true)
            {
                _angles.y.Angle += 0.1f;
            }
            if (_keyFlags[5] == true)
            {
                _angles.y.Angle -= 0.1f;
            }
            if (_keyFlags[6] == true)
            {
                if (false == false)
                    _angles.x.Angle += 0.1f;
            }
            if (_keyFlags[7] == true)
            {
                if (false == false)
                    _angles.x.Angle -= 0.1f;
            }
            if (_keyFlags[8] == true)
            {
                _position.y += _playerSpeed;
            }
            if (_keyFlags[9] == true)
            {
                _position.y -= _playerSpeed;
            }

            if (_isWriteKey == true)
                PrintEnabledKey();
        }

        private void PrintEnabledKey()
        {
            if (_keyFlags[0] == true)
            {
                Console.WriteLine("W");
            }
            if (_keyFlags[1] == true)
            {
                Console.WriteLine("S");
            }
            if (_keyFlags[2] == true)
            {
                Console.WriteLine("A");
            }
            if (_keyFlags[3] == true)
            {
                Console.WriteLine("D");
            }
            if (_keyFlags[4] == true)
            {
                Console.WriteLine("Left");
            }
            if (_keyFlags[5] == true)
            {
                Console.WriteLine("Right");
            }
            if (_keyFlags[6] == true)
            {
                Console.WriteLine("Up");
            }
            if (_keyFlags[7] == true)
            {
                Console.WriteLine("Down");
            }
            if (_keyFlags[8] == true)
            {
                Console.WriteLine("Space");
            }
            if (_keyFlags[9] == true)
            {
                Console.WriteLine("LShift");
            }
        }
    }
}
