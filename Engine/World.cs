using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShellEngineLib.Engine.Figures;

namespace ShellEngineLib.Engine
{
    public class World
    {
        public static World Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new World();
                return _instance;
            }
            private set { _instance = value; }
        }

        private static World _instance;

        public List<Figure> WorldObjects = new List<Figure>();

        public void AddFigure(Figure figure)
        {
            WorldObjects.Add(figure);
        }
    }
}
