using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ShellEngineLib.Engine.Math
{
    public class Point4D<T>
    {
        public T X => _x;
        public T Y => _y;
        public T Z => _z;
        public T W => _w;

        private T _x;
        private T _y;
        private T _z;
        private T _w;

        public Point4D() { }
        public Point4D(T x, T y, T z, T w)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
        }

        public static Point4D<T> Copy(Point4D<T> p) => new Point4D<T>(p.X, p.Y, p.Z, p.W);

        public void SetX(T x) => _x = x;
        public void SetY(T y) => _y = y;
        public void SetZ(T z) => _z = z;
        public void SetW(T w) => _w = w;
    }
}
