///////////////////////////////////////////////////////////////////////////////
/// <summary>
/// Tag2Dxf
/// (C) Copyright 2023 Surface Creations of Maine
///
/// File:    Converter.cs
/// Created: December 22, 2023
/// Purpose: Static converter to consume a TAG file and output a DXF
/// Author:  Andrew Wyshak
/// </summary>
///////////////////////////////////////////////////////////////////////////////

using netDxf;
using DxfEntities = netDxf.Entities;

namespace Tag2Dxf
{
    /// <summary>
    /// Contains methods for doing TAG to DXF conversion
    /// </summary>
    public static class Converter
    {
        /// <summary>
        /// Lookup for converting TAG color values to ACI color
        /// </summary>
        /// <remarks>
        /// Color names are predefined in Taglio LogoTag/Intercad
        /// </remarks>
        private static Dictionary<int, AciColor> tagDxfColorPairs = new Dictionary<int, AciColor>()
        {
            // Note in AutoCAD land, black and white have the same index value
            { 1, new AciColor(7) },         // White 
            { 2, new AciColor(2) },         // Yellow
            { 3, new AciColor(6) },         // Fuchsia
            { 4, new AciColor(1) },         // Red
            { 5, new AciColor(4) },         // Cyan
            { 6, new AciColor(3) },         // "Lemon" green -- is this an Italian thing?
            { 7, new AciColor(5) },         // Blue
            { 8, new AciColor(8) },         // Gray
            { 9, new AciColor(9) },         // Light gray
            { 10, new AciColor(64) },       // Olive green
            { 11, new AciColor(190) },      // Purple
            { 12, new AciColor(236) },      // Bordeaux
            { 13, new AciColor(120) },      // Sea green
            { 14, new AciColor(84) },       // Green
            { 15, new AciColor(166) },      // Dark blue
            { 16, new AciColor(0, 0, 0) }   // Black must be initialized with rgb
        };

        /// <summary>
        /// Converts TAG file data to DXF
        /// </summary>
        /// <param name="tagFile">TAG file data</param>
        /// <returns>DXF document</returns>
        /// 
        /// TODO Consider rewriting to reduce code reuse for transferring over color/style/weight/level attributes
        public static DxfDocument ConvertTag2Dxf(TagFile tagFile)
        {           
            var dxfFile = new DxfDocument();

            // Points
            var tagPoints = tagFile.Elements.OfType<TagElements.Point>().ToList();
            foreach (var tagPoint in tagPoints)
            {
                var dxfPoint = new DxfEntities.Point(new Vector2(tagPoint.X, tagPoint.Y));

                tagDxfColorPairs.TryGetValue(tagPoint.Color, out var aciColor);
                dxfPoint.Color = aciColor ?? AciColor.Default;

                dxfFile.Entities.Add(dxfPoint);
            }

            // Lines
            var tagLines = tagFile.Elements.OfType<TagElements.Line>().ToList();
            foreach (var tagLine in tagLines)
            {
                var dxfLine = new DxfEntities.Line(new Vector2(tagLine.X, tagLine.Y), new Vector2(tagLine.X2, tagLine.Y2));

                tagDxfColorPairs.TryGetValue(tagLine.Color, out var aciColor);
                dxfLine.Color = aciColor ?? AciColor.Default;

                dxfFile.Entities.Add(dxfLine);
            }

            // Arcs
            var tagArcs = tagFile.Elements.OfType<TagElements.Arc>().ToList();
            foreach (var tagArc in tagArcs)
            {
                var dxfArc = new DxfEntities.Arc(new Vector2(tagArc.X, tagArc.Y), tagArc.Radius, tagArc.AngleStart, tagArc.AngleEnd);

                tagDxfColorPairs.TryGetValue(tagArc.Color, out var aciColor);
                dxfArc.Color = aciColor ?? AciColor.Default;

                dxfFile.Entities.Add(dxfArc);
            }

            // Circles
            var tagCircles = tagFile.Elements.OfType<TagElements.Circle>().ToList();
            foreach (var tagCircle in tagCircles)
            {
                var dxfCircle = new DxfEntities.Circle(new Vector2(tagCircle.X, tagCircle.Y), tagCircle.Radius);

                tagDxfColorPairs.TryGetValue(tagCircle.Color, out var aciColor);
                dxfCircle.Color = aciColor ?? AciColor.Default;

                dxfFile.Entities.Add(dxfCircle);
            }

            return dxfFile;
        }
    }
}