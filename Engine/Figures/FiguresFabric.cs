using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellEngineLib.Engine.Figures
{
    public class FiguresFabric
    {
        public static Figure CreateFigure(IO.FigureTemplate template)
        {
            return new Figure(template);
        }
    }
}
