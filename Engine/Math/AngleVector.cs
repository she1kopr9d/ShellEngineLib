using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellEngineLib.Engine.Math
{
    public class AngleVector
    {
        public FAngle x = new FAngle();
        public FAngle y = new FAngle();
        public FAngle z = new FAngle();
        public static AngleVector Zero = new AngleVector(0, 0, 0);

        public AngleVector() { }

        public AngleVector(float xAngle, float yAngle, float zAngle)
        {
            this.x.Angle = xAngle;
            this.y.Angle = yAngle;
            this.z.Angle = zAngle;
        }

        public static AngleVector operator +(AngleVector aV1, AngleVector aV2)
        {
            return new AngleVector(aV1.x.Angle + aV2.x.Angle,
                aV1.y.Angle + aV2.y.Angle,
                aV1.z.Angle + aV2.z.Angle);
        }
        public static AngleVector operator -(AngleVector aV1, AngleVector aV2)
        {
            return new AngleVector(aV1.x.Angle - aV2.x.Angle,
                aV1.y.Angle - aV2.y.Angle,
                aV1.z.Angle - aV2.z.Angle);
        }

        public bool Equals(AngleVector right)
        {
            return x.Angle == right.x.Angle & y.Angle == right.y.Angle & z.Angle == right.z.Angle;
        }
    }
}
