///////////////////////////////////////////////////////////////////////////////
/// <summary>
/// Tag2Dxf
/// (C) Copyright 2023 Surface Creations of Maine
///
/// File:    Point.cs
/// Created: November 20, 2023
/// Purpose: Class to encapsulate a TAG Point
/// Author:  Andrew Wyshak
/// </summary>
///////////////////////////////////////////////////////////////////////////////

namespace Tag2Dxf.TagElements
{
    /// <summary>
    /// Class to encapsulate a TAG Point
    /// </summary>
    public sealed class Point : TagElement
    {
        /// <inheritdoc/>
        public Point(string rawElementData) : base(rawElementData)
        {
        }

        /// <inheritdoc/>
        protected override void ParseRawElementData()
        {
            var splitElementData = rawElementData.Split(',');
            X = Convert.ToSingle(splitElementData[1]);
            Y = Convert.ToSingle(splitElementData[2]);
            Color = Convert.ToInt16(splitElementData[3]);
            Level = Convert.ToInt16(splitElementData[4]);
        }
    }
}