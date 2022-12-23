

namespace ShellEngineLib.Engine.Math
{
    public class Matrix
    {
        public int I { get; private set; }
        public int J { get; private set; }
        public float[,] Array => _matrixCells;

        private float[,] _matrixCells;

        public Matrix(int i, int j)
        {
            I = i;
            J = j;
            _matrixCells = new float[I, J];
        }

        public Matrix(int i, int j, float[,] array)
        {
            I = i;
            J = j;
            _matrixCells = array;
        }

        #region Triganomitry patterns 
        public static Matrix XY(double angle)
        {
            return 
            (
                new Matrix
                (
                    3, 3,
                    new float[,] 
                    {
                        { (float)System.Math.Cos(angle), -1 * (float)System.Math.Sin(angle), 0 },
                        { (float)System.Math.Sin(angle), (float)System.Math.Cos(angle), 0 },
                        { 0, 0, 1}
                    }
                )
            );
        }
        public static Matrix XZ(double angle)
        {
            return
            (
                new Matrix
                (
                    3, 3,
                    new float[,]
                    {
                        { (float)System.Math.Cos(angle), 0, (float)System.Math.Sin(angle) },
                        { 0, 1, 0 },
                        { -1 * (float)System.Math.Sin(angle), 0, (float)System.Math.Cos(angle)}
                    }
                )
            );
        }
        public static Matrix YZ(double angle)
        {
            return
            (
                new Matrix
                (
                    3, 3,
                    new float[,]
                    {
                        { 1, 0, 0},
                        { 0, (float)System.Math.Cos(angle), -1 * (float)System.Math.Sin(angle) },
                        { 0, (float)System.Math.Sin(angle), (float)System.Math.Cos(angle) }
                    }
                )
            );
        }
        public static Matrix Scale(float xScale, float yScale, float zScale)
        {
            return new Matrix(3, 1, new float[,] 
            {
                { xScale, 0, 0 },
                { 0, yScale, 0 },
                { 0, 0, zScale }
            });
        }
        public static Matrix Scale(Math.Point point) =>
            Scale(point.x, point.y, point.z);
        #endregion


        public static Matrix MultiplyMatrix(Matrix m1, Matrix m2)
        {
            int i1, j1;
            float[,] matrix1, matrix2, answerArray, preAnswerArray;
            matrix1 = m1.Array;
            matrix2 = m2.Array;
            preAnswerArray = new float[3, 3];
            answerArray = new float[3, 1];

            for (int i = 0; i < 3; i++)
            {
                if (matrix2[i, 0] == 0)
                {
                    for(int j = 0; j < 3; j++)
                        preAnswerArray[i, j] = 0;
                    continue;
                }
                for (int j = 0; j < 3; j++)
                    preAnswerArray[i, j] = matrix1[j, i] * matrix2[i, 0];
            }

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    answerArray[i, 0] += preAnswerArray[j, i];

            return new Matrix(3, 1, answerArray);
        }

        public Point ConvertInPoint()
        {
            return new Point(
                new float[,]
                {
                    { Array[0, 0] },
                    { Array[1, 0] },
                    { Array[2, 0] }
                });
        }

        public static Matrix operator *(Matrix m1, Matrix m2) =>
            MultiplyMatrix(m1, m2);
    }
}
