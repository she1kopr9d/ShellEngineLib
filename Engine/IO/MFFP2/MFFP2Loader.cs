using ShellEngineLib.Engine.Math;

namespace ShellEngineLib.Engine.IO.MFFP2
{
    public class MFFP2Loader : IFiguresLoader
    {
        #region Static Constant for Format
        private static string _ignoreCommand_Inst = "#";
        private static string _vertexCommand_Inst = "v";
        private static string _polygonCommand_Inst = "pr";
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
                if (lines[i] == "" | lines[i][0] == _endLineSymbol)
                    continue;
                string[] args = lines[i].Split(_spaceArgumentsSymbol);
                string arguments = "";
                if (args[0].GetHashCode() == _vertexCommand_Inst.GetHashCode())
                {
                    vertex.Add(PointCutter(args));
                }
                else if (args[0].GetHashCode() == _polygonCommand_Inst.GetHashCode())
                {
                    rects.Add(Point4DCutter(args));
                }
                else if (args[0].GetHashCode() == _triangleCommand_Inst.GetHashCode())
                {
                    triangles.Add(PointCutter(args));
                }
                else if (args[0].GetHashCode() == _vectorCommand_Inst.GetHashCode())
                {
                    vectors.Add(PointCutter(args));
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

            return new FigureTemplate(vertexA, trianglesA, vectorsA, rectsA);
        }

        private Point PointCutter(string[] args)
        {
            return new Point(
                Convert.ToSingle(args[1]), 
                Convert.ToSingle(args[2]),
                Convert.ToSingle(args[3]));
        }

        private Point4D<int> Point4DCutter(string[] args)
        {
            return new Point4D<int>(
                Convert.ToInt32(args[1]),
                Convert.ToInt32(args[2]),
                Convert.ToInt32(args[3]),
                Convert.ToInt32(args[4]));
        }
    }
}
