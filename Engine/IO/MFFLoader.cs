using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellEngineLib.Engine.IO
{
    public class MFFLoader : IFiguresLoader
    {
        private static char StartSymbol = '{';
        private static char HeaderSymbol = '-';
        private static char VectorSymbol = 'v';
        private static char PolygonSymbol = 'p';
        private static char StartHeaderSymbol = '[';
        private static char StartPointSymbol = '(';
        private static char CutterNumSymbol = ';';
        private static char EndPointSymbol = ')';
        private static char CutterPointSymbol = ',';
        private static char EndHeaderSymbol = ']';
        private static char EndSymbol = '}';

        public FigureTemplate Load(string directory)
        {
            string file;
            List<Math.Polygon> polygons = new List<Math.Polygon>();
            List<Math.Vector> vectors = new List<Math.Vector>();
            MFF.MFFEnum state = MFF.MFFEnum.StartProgram;

            using (StreamReader mffFile = new StreamReader(directory))
            {
                file = mffFile.ReadToEnd();
            }
            string headerBuffer = "";
            char symbol;
            for(int i = 0; i < file.Length; i++)
            {
                symbol = file[i];
                switch (state)
                {
                    case MFF.MFFEnum.StartProgram:
                        if (symbol == StartSymbol)
                            state = MFF.MFFEnum.FindHeader;
                        break;
                    case MFF.MFFEnum.FindHeader:
                        if (symbol == HeaderSymbol)
                        {
                            if (file[i + 1] == VectorSymbol)
                                state = MFF.MFFEnum.VectorCutter;
                            else if (file[i + 1] == PolygonSymbol)
                                state = MFF.MFFEnum.PolygonCutter;
                        }                            
                        break;
                    case MFF.MFFEnum.PolygonCutter:
                    case MFF.MFFEnum.VectorCutter:
                        if(symbol != EndHeaderSymbol)
                            headerBuffer += symbol;
                        else
                        {
                            if (state == MFF.MFFEnum.PolygonCutter)
                                polygons.Add(PolygonCutter(headerBuffer));
                            else if (state == MFF.MFFEnum.VectorCutter)
                                vectors.Add(VectorCutter(headerBuffer));
                            headerBuffer = "";
                            state = MFF.MFFEnum.FindHeader;
                        }
                        break;
                    case MFF.MFFEnum.EndProgram:
                        break;
                }
            }

            return null;
        }

        private Math.Vector VectorCutter(string sliceFile)
        {
            const byte countPoint = 2;
            return new Math.Vector(ArgumentCutter(sliceFile, countPoint));
        }
        private Math.Polygon PolygonCutter(string sliceFile)
        {
            const byte countPoint = 3;
            return new Math.Polygon(ArgumentCutter(sliceFile, countPoint));
        }
        private Math.Point[] ArgumentCutter(string sliceFile, byte countPoint)
        {
            sliceFile = sliceFile.Substring(2);
            string[] pointsStr = PointCutter(sliceFile, countPoint);
            Math.Point[] points = new Math.Point[2];
            for (int i = 0; i < countPoint; i++)
                points[i] = PointCutter(pointsStr[i]);
            return points;
        }
        private string[] PointCutter(string sliceFile, byte countPoint)
        {
            string[] points = new string[countPoint];
            int pointNum = 0;
            for(int i = 0; i < sliceFile.Length; i++)
            {
                if (sliceFile[i] != CutterPointSymbol)
                    points[pointNum] += sliceFile[i];
                else
                    pointNum++;
            }
            return points;
        }
        private Math.Point PointCutter(string sliceFile)
        {
            sliceFile = sliceFile.Substring(1);
            sliceFile = sliceFile.Substring(0, sliceFile.Length - 1);
            float[] nums = new float[3];
            string buffer = "";
            int arrayCell = 0;
            for (int i = 0; i < sliceFile.Length; i++)
            {
                if (sliceFile[i] != CutterNumSymbol && i != sliceFile.Length - 1)
                    buffer += sliceFile[i];
                else if (i == sliceFile.Length - 1)
                    nums[arrayCell] = Convert.ToSingle(buffer + sliceFile[i]);
                else
                {
                    nums[arrayCell++] = Convert.ToSingle(buffer);
                    buffer = "";
                }
            }

            return new Math.Point(nums);
        }
    }
}
