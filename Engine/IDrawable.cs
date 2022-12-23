using ShellEngineLib.Engine.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellEngineLib.Engine
{
    public interface IDrawable
    {
        void Line(Math.Vector vector);
        void LineCenter(Math.Vector vector);
        void LineEnd(Math.Vector vector);
        void LineCenterEnd(Math.Vector vector);
        void Poly(Triangle triangle);
        void Rect(Rect rect);
    }
}
