using ShellEngineLib.Engine.IO;
using ShellEngineLib.Engine.Math;
using System.Text;

namespace ShellEngineLib.Engine.IO.MFFP
{
    public class MFFPLoader : IFiguresLoader
    {
        #region Static Constant for Format
        private static string _ignoreCommand_Inst = "#";
        private static string _polygonCommand_Inst = "p";
        private static char _spaceArgumentsSymbol = ' ';
        private static char _spacePointArgumentsSymbol = '/';
        private static char _endLineSymbol = '\n';
        #endregion

        public FigureTemplate Load(string directory)
        {
            string file = "";
            string line = "";
            int index = 0;
            List<Triangle> triangles = new List<Triangle>();
            using (StreamReader fileObj = new StreamReader(directory))
            {
                file = fileObj.ReadToEnd();
            }
            while (index < file.Length)
            {
                (line, index) = CutterLine(file, index);
                string[] args = ArgumentsCutter(line);
                string arguments = "";
                if (args[0].GetHashCode() == _polygonCommand_Inst.GetHashCode())
                {
                    Triangle triangle = new Triangle();
                    for(int i = 0; i < 3; i++)
                        triangle._vertex[i] = PointCutter(args[i+1], _spacePointArgumentsSymbol);
                    triangles.Add(triangle);
                }
            }
            return null;
        }

        private Math.Point PointCutter(string line, char space)
        {
            int[] nums = new int[3];
            string[] args = line.Split(space);
            for (int i = 0; i < 3; i++)
                nums[i] = Convert.ToInt32(args[i]);

            return new Math.Point(nums[0], nums[1], nums[2]);
        }

        private string[] ArgumentsCutter(string line) =>
            line.Split(_spaceArgumentsSymbol, StringSplitOptions.TrimEntries);

        private (string, int) CutterLine(string file, int index)
        {
            StringBuilder line = new StringBuilder();
            while (file[index] != _endLineSymbol)
                line.Append(file[index++]);
            return (line.ToString(), ++index);
        }
    }
}
