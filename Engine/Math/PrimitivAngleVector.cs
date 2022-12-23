using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellEngineLib.Engine.Math
{
    public class PrimitivAngleVector
    {
        public float x;
        public float y;
        public float z;

        public static PrimitivAngleVector Zero = new PrimitivAngleVector(0, 0, 0);

        public PrimitivAngleVector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
