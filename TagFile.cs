///////////////////////////////////////////////////////////////////////////////
/// <summary>
/// Tag2Dxf
/// (C) Copyright 2023 Surface Creations of Maine
///
/// File:    TagFile.cs
/// Created: November 20, 2023
/// Purpose: Class to encapsulate a TAG file and its elements
/// Author:  Andrew Wyshak
/// </summary>
///////////////////////////////////////////////////////////////////////////////

using Tag2Dxf.TagElements;

namespace Tag2Dxf
{
    /// <summary>
    /// Encapsulates a TAG file and its elements
    /// </summary>
    public class TagFile
    {
        private List<Arc> arcs;
        private List<Circle> circles;
        private List<Line> lines;
        private List<Point> points;
    }
}