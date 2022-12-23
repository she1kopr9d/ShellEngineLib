using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ShellEngineLib.Engine.IO;
using System.ComponentModel;
using System.Reflection;
using System.Diagnostics.Contracts;
using ShellEngineLib.Engine.Math;

namespace ShellEngineLib.Engine.IO.OBJReader
{
    public class Reader : IFiguresLoader
    {
        #region Static Constant for Format
        private static string _ignoreCommand_Inst = "#";
        private static string _vertexCommand_Inst = "v";
        private static string _polygonCommand_Inst = "f";
        private static string _triangleCommand_Inst = "pt";
        private static string _vectorCommand_Inst = "pv";
        private static char _spaceArgumentsSymbol = ' ';
        private static char _spacePointArgumentsSymbol = '/';
        private static char _endLineSymbol = '\n';
        #endregion

        public FigureTemplate Load(string directory)
        {
            string file = "";
            using (StreamReader fileObj = new StreamReader(directory))
            {
                file = fileObj.ReadToEnd();
            }
            string[] lines = file.Split(_endLineSymbol);

            List<Point> vertex = new List<Point>();
            List<Point4D<int>> rects = new List<Point4D<int>>();
            List<Point> triangles = new List<Point>();
            List<Point> vectors = new List<Point>();

            for (int i = 0; i < lines.Length; i++)
            {
                if (i >= lines.Length ||
                    lines[i] == null)
                    continue;
                try
                {
                    string[] args = lines[i].Substring(0, lines[i].Length - 2).Split(_spaceArgumentsSymbol, StringSplitOptions.RemoveEmptyEntries);
                    string arguments = "";
                    if (args[0].GetHashCode() == _vertexCommand_Inst.GetHashCode())
                    {
                        vertex.Add(PointCutter(args));
                    }
                    else if (args[0].GetHashCode() == _polygonCommand_Inst.GetHashCode())
                    {
                        if (args.Length == 4)
                            triangles.Add(PointCutterForTriangle(args));
                        else
                            rects.Add(Point4DCutter(args));
                    }
                    else if (args[0].GetHashCode() == _triangleCommand_Inst.GetHashCode())
                    {
                        //triangles.Add(PointCutter(args));
                        foreach (string arg in args)
                            Console.Write(arg);
                        Console.WriteLine();
                    }
                    else if (args[0].GetHashCode() == _vectorCommand_Inst.GetHashCode())
                    {
                        //vectors.Add(PointCutter(args));
                        foreach (string arg in args)
                            Console.Write(arg);
                        Console.WriteLine();
                    }
                }

                catch
                {
                    Console.WriteLine(i);
                }
            }

            Point[] vertexA;
            Point4D<int>[] rectsA;
            Point[] trianglesA;
            Point[] vectorsA;

            {

                if (vertex.Count == 0)
                    vertexA = new Point[0];
                else
                    vertexA = vertex.ToArray();

                if (rects.Count == 0)
                    rectsA = new Point4D<int>[0];
                else
                    rectsA = rects.ToArray();

                if (triangles.Count == 0)
                    trianglesA = new Point[0];
                else
                    trianglesA = triangles.ToArray();

                if (vectors.Count == 0)
                    vectorsA = new Point[0];
                else
                    vectorsA = vectors.ToArray();

            }

            Console.WriteLine(vertexA.Length);

            return new FigureTemplate(vertexA, trianglesA, null, rectsA);
        }

        private Point PointCutter(string[] args)
        {
            return new Point(
                Convert.ToSingle(args[1].Replace('.', ',')),
                Convert.ToSingle(args[2].Replace('.', ',')),
                Convert.ToSingle(args[3].Replace('.', ',')));
        }

        private Point PointCutterForTriangle(string[] args)
        {
            return new Point(
                Convert.ToInt32(args[1].Split(_spacePointArgumentsSymbol)[0]) - 1,
                Convert.ToInt32(args[2].Split(_spacePointArgumentsSymbol)[0]) - 1,
                Convert.ToInt32(args[3].Split(_spacePointArgumentsSymbol)[0]) - 1);
        }

        private Point4D<int> Point4DCutter(string[] args)
        {
            return new Point4D<int>(
                Convert.ToInt32(args[1].Split(_spacePointArgumentsSymbol)[0]) - 1,
                Convert.ToInt32(args[2].Split(_spacePointArgumentsSymbol)[0]) - 1,
                Convert.ToInt32(args[3].Split(_spacePointArgumentsSymbol)[0]) - 1,
                Convert.ToInt32(args[4].Split(_spacePointArgumentsSymbol)[0]) - 1);
        }
    }
}
