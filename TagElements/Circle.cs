///////////////////////////////////////////////////////////////////////////////
/// <summary>
/// Tag2Dxf
/// (C) Copyright 2023 Surface Creations of Maine
///
/// File:    Circle.cs
/// Created: November 20, 2023
/// Purpose: Class to encapsulate a TAG Circle
/// Author:  Andrew Wyshak
/// </summary>
///////////////////////////////////////////////////////////////////////////////

namespace Tag2Dxf.TagElements
{
    /// <summary>
    /// Class to encapsulate a TAG Circle
    /// </summary>
    public sealed class Circle : TagElement
    {
        /// <inheritdoc/>
        public Circle(string rawElementData) : base(rawElementData)
        {
        }

        /// <summary>
        /// Radius of circle
        /// </summary>
        public double Radius { get; private set; }

        /// <inheritdoc/>
        protected override void ParseRawElementData()
        {
            var splitElementData = rawElementData.Split(',');
            X = Convert.ToDouble(splitElementData[1]);
            Y = Convert.ToDouble(splitElementData[2]);
            Radius = Convert.ToDouble(splitElementData[3]);
        }
    }
}