using ShellEngineLib.Engine.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellEngineLib.Engine
{
    public interface IPrimitiveDraw
    {
        public void Line(Engine.Math.Vector vector);
        public void Poly(Triangle triangle);
        public void Rect(Rect rect);
    }
}
