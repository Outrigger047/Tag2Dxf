/// <summary>
/// TagFile.cs
/// Created 11/18/2023
/// Andrew Wyshak
/// Surface Creations of Maine
/// </summary>

using Tag2Dxf.TagElements;

namespace Tag2Dxf
{
    /// <summary>
    /// Encapsulates a Tag file and its elements
    /// </summary>
    public class TagFile
    {
        private List<Arc> arcs;
        private List<Circle> circles;
        private List<Line> lines;
        private List<Point> points;
    }
}