///////////////////////////////////////////////////////////////////////////////
/// <summary>
///
/// Tag2Dxf
///
/// File:    Line.cs
/// Created: November 20, 2023
/// Purpose: Class to encapsulate a TAG Line
/// Author:  Andrew Wyshak
///          Surface Creations of Maine
/// </summary>
///////////////////////////////////////////////////////////////////////////////

namespace Tag2Dxf.TagElements
{
    /// <summary>
    /// Class to encapsulate a TAG Line
    /// </summary>
    public sealed class Line : TagElement
    {
        /// <inheritdoc/>
        public Line(string rawElementData) : base(rawElementData)
        {
        }

        /// <summary>
        /// X coordinate of line endpoint
        /// </summary>
        public double X2 { get; private set; }

        /// <summary>
        /// Y coordinate of line endpoint
        /// </summary>
        public double Y2 { get; private set; }

        /// <inheritdoc/>
        protected override void ParseRawElementData()
        {
            var splitElementData = rawElementData.Split(',');
            X = Convert.ToDouble(splitElementData[1]);
            Y = Convert.ToDouble(splitElementData[2]);
            X2 = Convert.ToDouble(splitElementData[3]);
            Y2 = Convert.ToDouble(splitElementData[4]);
            Color = Convert.ToInt16(splitElementData[5]);
            Style = Convert.ToInt16(splitElementData[6]);
            Weight = Convert.ToInt16(splitElementData[7]);
            Level = Convert.ToInt16(splitElementData[8]);
        }
    }
}