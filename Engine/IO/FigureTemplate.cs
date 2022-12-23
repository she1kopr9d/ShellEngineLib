using ShellEngineLib.Engine.Math;

namespace ShellEngineLib.Engine.IO
{
    public class FigureTemplate
    {
        public Point[] Vertex;
        public Point[] Triangles;
        public Point[] Vectors;
        public Point4D<int>[] Rects;
        public Point[] SupportVertex;

        public FigureTemplate(Point[] vertex, Point[] triangles, Point[] vectors, Point4D<int>[] rects)
        {
            Vertex = vertex;
            Triangles = triangles;
            Vectors = vectors;
            Rects = rects;
            CreateSupportDots();
        }

        private void CreateSupportDots()
        {
            float minX = 0, minY = 0, minZ = 0, maxX = 0, maxY = 0, maxZ = 0;
            foreach (Point point in Vertex)
            {
                if (point.x > maxX)
                    maxX = point.x;
                else if (point.x < minX)
                    minX = point.x;

                if (point.y > maxY)
                    maxY = point.y;
                else if (point.y < minY)
                    minY = point.y;

                if (point.z > maxZ)
                    maxZ = point.z;
                else if (point.z < minZ)
                    minZ = point.z;
            }
            SupportVertex = new Point[8];

            SupportVertex[0] = new Point(minX, minY, minZ);
            SupportVertex[1] = new Point(maxX, minY, minZ);
            SupportVertex[2] = new Point(maxX, minY, maxZ);
            SupportVertex[3] = new Point(minX, minY, maxZ);

            SupportVertex[4] = new Point(minX, maxY, minZ);
            SupportVertex[5] = new Point(maxX, maxY, minZ);
            SupportVertex[6] = new Point(maxX, maxY, maxZ);
            SupportVertex[7] = new Point(minX, maxY, maxZ);
        }
    }
}
