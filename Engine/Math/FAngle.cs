using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellEngineLib.Engine.Math
{
    public class FAngle
    {
        public float Angle 
        {
            get
            {
                return _angle;
            }
            set
            {
                if (_angle + value < 0)
                    _angle = 360;
                else
                    _angle = System.Math.Abs(value % 360);
            } 
        }

        private float _angle = 0;

        public float Radian => (float)(System.Math.PI / 180 * _angle);

    }
}
