using ShellEngineLib.Engine.Figures;
using ShellEngineLib.Engine.Math;
using System.Collections;

namespace ShellEngineLib.Engine
{
    public class Render
    {
        private IDrawable _brush;
        private List<Figures.Figure> _figureBuffer => World.Instance.WorldObjects;

        public Render(IDrawable brush)
        {
            _brush = brush;
        }

        public void Update(Point position, AngleVector angles, Camera camera)
        {
            _figureBuffer.ForEach((figure) => {
                RenderFigure(position, angles, camera, figure);
            });
        }

        private void RenderFigure(Point position, AngleVector angles, Camera camera, Figures.Figure figure)
        {
            if (FigureInFov(figure.NowFigure.supportVertex, camera) == false)
                return;

            Point[] vertex = CreateVertex(position, angles, camera, figure.NowFigure.vertex);
            

            Hashtable vertexHash = new Hashtable(vertex.Length);
            for (int i = 0; i < vertex.Length; i++)
            {
                vertexHash[i] = vertex[i];
            }

            Point[] suportVertex = CreateVertex(position, angles, camera, figure.NowFigure.supportVertex);
            RenderVectors(suportVertex, RealtimeFigure.supportVertexVertexPlan);

            if (figure.NowFigure.template.Vectors?.Length > 0)
                RenderVectors(vertex, figure.NowFigure.template.Vectors);
            if (figure.NowFigure.template.Triangles?.Length > 0)
                RenderTriangles(vertex, figure.NowFigure.template.Triangles);
            if (figure.NowFigure.template.Rects?.Length > 0)
                RenderRects(vertex, figure.NowFigure.template.Rects);
        }

        private bool FigureInFov(Point[] supportVertex, Camera camera)
        {
            bool inFov = false;

            foreach (Point point in supportVertex)
                inFov = inFov | camera.InFov(point);

            return inFov;
        }

        private Point[] CreateVertex(Point position, AngleVector angles, Camera camera, Point[] vertex)
        {
            Point[] vertexToCamera = new Point[vertex.Length];
            for (int i = 0; i < vertexToCamera.Length; i++)
            {
                vertexToCamera[i] = vertex[i].Copy();
                vertexToCamera[i] -= position;
                vertexToCamera[i] *= Matrix.XZ(angles.y.Radian);
                vertexToCamera[i] *= Matrix.YZ(angles.x.Radian);
                vertexToCamera[i] = camera.CameraViewPoint(vertexToCamera[i]);
                if (vertexToCamera[i] == null)
                    continue;
                vertexToCamera[i].SetY(1080 - vertexToCamera[i].y);
            }
            return vertexToCamera;
        }

        private void RenderTriangles(Point[] vertex, Point[] triangles)
        {
            foreach(Point triangle in triangles)
            {
                try
                {
                    _brush.Poly(new Triangle(
                        new Point[]
                        {
                            vertex[(int)triangle.x],
                            vertex[(int)triangle.y],
                            vertex[(int)triangle.z]}));
                }
                catch { }
            }   
        }

        private void RenderRects(Point[] vertex, Point4D<int>[] rects)
        {
            foreach (Point4D<int> rect in rects)
            {
                try
                {
                    _brush.Rect(new Rect(
                    new Point[]{
                        vertex[rect.X],
                        vertex[rect.Y],
                        vertex[rect.Z],
                        vertex[rect.W]}));
                }
                catch
                {

                }
            } 
        }

        private void RenderVectors(Point[] vertex, Point[] vectors)
        {
            foreach (Point vector in vectors)
                _brush.Line(
                    new Math.Vector(
                        vertex[(int)vector.x],
                        vertex[(int)vector.y]
                    ));
        }
    }
}
