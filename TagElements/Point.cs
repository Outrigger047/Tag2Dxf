///////////////////////////////////////////////////////////////////////////////
/// <summary>
///
/// Tag2Dxf
///
/// File:    Point.cs
/// Created: November 20, 2023
/// Purpose: Class to encapsulate a TAG Point
/// Author:  Andrew Wyshak
///          Surface Creations of Maine
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
            X = Convert.ToDouble(splitElementData[1]);
            Y = Convert.ToDouble(splitElementData[2]);
            Color = Convert.ToInt16(splitElementData[3]);
            Level = Convert.ToInt16(splitElementData[4]);
        }
    }
}