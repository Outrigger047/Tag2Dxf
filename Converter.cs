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
        /// Converts TAG file data to DXF
        /// </summary>
        /// <param name="tagFile">TAG file data</param>
        /// <returns>DXF document</returns>
        public static DxfDocument ConvertTag2Dxf(TagFile tagFile)
        {
            var dxfFile = new DxfDocument();

            // Points
            var tagPoints = tagFile.Elements.OfType<TagElements.Point>().ToList();
            foreach (var point in tagPoints)
            {
                dxfFile.Entities.Add(new DxfEntities.Point(new Vector2(point.X, point.Y)));
            }

            // Lines
            var tagLines = tagFile.Elements.OfType<TagElements.Line>().ToList();
            foreach (var line in tagLines)
            {
                dxfFile.Entities.Add(
                    new DxfEntities.Line(new Vector2(line.X, line.Y), new Vector2(line.X2, line.Y2)));
            }

            // Arcs
            var tagArcs = tagFile.Elements.OfType<TagElements.Arc>().ToList();
            foreach (var arc in tagArcs)
            {
                dxfFile.Entities.Add(
                    new DxfEntities.Arc(new Vector2(arc.X, arc.Y), arc.Radius, arc.AngleStart, arc.AngleEnd));
            }

            // Circles
            var tagCircles = tagFile.Elements.OfType<TagElements.Circle>().ToList();
            foreach (var circle in tagCircles)
            {
                dxfFile.Entities.Add(
                    new DxfEntities.Circle(new Vector2(circle.X, circle.Y), circle.Radius));
            }

            return dxfFile;
        }
    }
}