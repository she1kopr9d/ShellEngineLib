using System.Diagnostics;
using ShellEngineLib.Engine.IO;
using ShellEngineLib.Engine.Figures;
using ShellEngineLib.Engine;
using ShellEngineLib.Engine.Math;
using ShellEngineLib.Engine.IO.MFFP;
using ShellEngineLib.Engine.IO.OBJReader;
using ShellEngineLib.Engine.IO.MFFP2;

namespace ShellEngineLib.Engine
{
    public class GameLoop
    {
        private Player _player;
        private IDrawable _brush;
        private IScreen _screen;

        private FigureTemplate _playerModel;
        private FigureTemplate _triangle;
        private FigureTemplate _animal;
        private FigureTemplate _sphere;
        private FigureTemplate _polyCube;
        private FigureTemplate _line;

        private Figure _pCube;

        private Point _windowMiddle;
        private Point _windowSize;

        private bool _isExit = false;
        private int _limitFPS = 60;

        public void Run(IScreen screen, IPrimitiveDraw brush, Point windowSize, int limitFPS = 60)
        {
            _limitFPS = limitFPS;
            Init(screen, brush, windowSize);


            Stopwatch timer = new Stopwatch();
            while (_isExit == false)
            {
                timer.Start();
                _screen.Update();
                _screen.ClearScreen();
                _screen.SetPositionMouse(_windowMiddle);
                Update();
                _screen.Render();

                timer.Stop();
                screen.SetTitle(Convert.ToString(1 / timer.Elapsed.TotalSeconds));
                _player.SetPlayerSpeed((float)(timer.Elapsed.TotalSeconds * 300));
                timer.Reset();
            }
        }

        #region Init
        private void Init(IScreen screen, IPrimitiveDraw brush, Math.Point windowSize)
        {
            screen.SetLimitFPS((uint)_limitFPS);
            ScreenInit(screen, windowSize);
            FigureTemplateInit();

            
            _brush = new Draw(brush, _windowSize);
            _player = new Player(5f, _windowSize, _brush, true);

            FigureInit();

            /*for (int i = 0; i < 4; i++)
                _windows[i] = new ShellEngineLib.Engine.Window(
                    (int)_windowSize.x,
                    (int)_windowSize.y, i, _brush);*/

            //_player.SetSkin(FiguresFabric.CreateFigure(_playerModel));
            //_player.Skin.SetSettings(null, null, new Math.Point(0.2f, 0.2f, 0.2f));
            //_render.AddFigure(_player.Skin);
        }

        private void ScreenInit(IScreen screen, Math.Point windowSize)
        {
            _screen = screen;
            _screen.VisibleMouse(false);
            _windowSize = windowSize;
            _windowMiddle = new Point(windowSize.x / 2, windowSize.y / 2, 0);
        }
        private void FigureTemplateInit()
        {
            IFiguresLoader reader = new Reader();
            IFiguresLoader loader3 = new MFFP2Loader();

            string directory = Directory.GetCurrentDirectory();

            _polyCube = loader3.Load(directory + @"/Source/cubePolyVertex.mffp2");
            _sphere = loader3.Load(directory + @"/Source/sphere.mffp2");
            _line = loader3.Load(directory + @"/Source/line.mffp2");
            //_animal = reader.Load(directory + @"/Source/animal.obj");
        }
        private void FigureInit()
        {
            Figure sphere = FiguresFabric.CreateFigure(_sphere);
            Figure line = FiguresFabric.CreateFigure(_line);
            //Figure animal = FiguresFabric.CreateFigure(_animal);
            _pCube = FiguresFabric.CreateFigure(_polyCube);

            sphere.SetSettings(null, new Point(300, 0, 300), new Point(0.15f, 0.15f, 0.15f));
            line.SetSettings(null, null, new Point(3f, 3f, 3f));
            //animal.SetSettings(null, new Point(-1000, 0, 0), null);
            _pCube.SetSettings(null, new Point(300, 0, -300), null);

            World.Instance.AddFigure(sphere);
            World.Instance.AddFigure(line);
            //World.Instance.AddFigure(animal);
            World.Instance.AddFigure(_pCube);
        }
        #endregion

        private float _ii = 0; 

        private void Update()
        {
            _pCube.SetSettings(
                new AngleVector(0, _ii += 0.1f, 0), 
                new Point(
                    (float)System.Math.Cos(_pCube.Angles.y.Radian) * 150, 
                    30, (float)System.Math.Sin(_pCube.Angles.y.Radian) * 150), new Point(0.1f, 0.1f, 0.1f));

            _player.Movement();
            _player.PlayerCamera.Render();
        }

        #region Input
        public void Window_KeyPressed(KeyEnum key)
        {
            if (key == KeyEnum.ESCAPE)
                _isExit = true;
            _player.KeyAdd(key);
        }
        public void Window_KeyReleased(KeyEnum key)
        {
            _player.KeyRemove(key);
        }
        public void Window_MouseMove(Math.Point position)
        {
            _player.MouseMove(position);
        }
        #endregion
    }
}


