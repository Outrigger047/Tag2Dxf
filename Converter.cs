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
using netDxf.Tables;
using Tag2Dxf.TagElements;
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
        /// Lookup for converting TAG style values to AutoCAD linetype values
        /// </summary>
        private static Dictionary<int, Linetype> tagDxfStylePairs = new Dictionary<int, Linetype>()
        {
            { 1, Linetype.Continuous },     // ------------------
            { 2, Linetype.Dashed },         // ---  ---  ---  ---
            { 3, Linetype.Dot },            // .  .  .  .  .  .  
            { 4, Linetype.DashDot },        // --- . --- . --- .
            { 5, Linetype.DashDot }         // No equivalent or similar
        };

        /// <summary>
        /// Converts TAG file data to DXF
        /// </summary>
        /// <param name="tagFile">TAG file data</param>
        /// <returns>DXF document</returns>
        public static DxfDocument ConvertTag2Dxf(TagFile tagFile)
        {
            var dxfFile = new DxfDocument
            {
                Name = tagFile.FileName
            };

            var elementPairs = new List<Tuple<TagElement, DxfEntities.EntityObject>>();

            // Points
            foreach (var tagPoint in tagFile.Elements.OfType<TagElements.Point>())
            {
                var dxfPoint = new DxfEntities.Point(new Vector2(tagPoint.X, tagPoint.Y));
                elementPairs.Add(new Tuple<TagElement, DxfEntities.EntityObject>(tagPoint, dxfPoint));
            }

            // Lines
            foreach (var tagLine in tagFile.Elements.OfType<TagElements.Line>())
            {
                var dxfLine = new DxfEntities.Line(new Vector2(tagLine.X, tagLine.Y), new Vector2(tagLine.X2, tagLine.Y2));           
                elementPairs.Add(new Tuple<TagElement, DxfEntities.EntityObject>(tagLine, dxfLine));
            }

            // Arcs
            foreach (var tagArc in tagFile.Elements.OfType<TagElements.Arc>())
            {
                var dxfArc = new DxfEntities.Arc(new Vector2(tagArc.X, tagArc.Y), tagArc.Radius, tagArc.AngleStart, tagArc.AngleEnd);
                elementPairs.Add(new Tuple<TagElement, DxfEntities.EntityObject>(tagArc, dxfArc));
            }

            // Circles
            foreach (var tagCircle in tagFile.Elements.OfType<TagElements.Circle>())
            {
                var dxfCircle = new DxfEntities.Circle(new Vector2(tagCircle.X, tagCircle.Y), tagCircle.Radius);
                elementPairs.Add(new Tuple<TagElement, DxfEntities.EntityObject>(tagCircle, dxfCircle));
            }

            // Iterate over collection of all element pairs and convert color, style, weight, level
            foreach (var elementPair in elementPairs)
            {
                // Color
                tagDxfColorPairs.TryGetValue(elementPair.Item1.Color, out var aciColor);
                elementPair.Item2.Color = aciColor ?? AciColor.Default;

                // Style
                tagDxfStylePairs.TryGetValue(elementPair.Item1.Style, out var linetype);
                elementPair.Item2.Linetype = linetype ?? Linetype.Continuous;

                // Add entity to DXF document
                dxfFile.Entities.Add(elementPair.Item2);
            }

            return dxfFile;
        }
    }
}