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
        public float? Radius { get; private set; }

        /// <inheritdoc/>
        protected override void ParseRawElementData()
        {
            var splitElementData = rawElementData.Split(',');
            X = Convert.ToSingle(splitElementData[1]);
            Y = Convert.ToSingle(splitElementData[2]);
            Radius = Convert.ToSingle(splitElementData[3]);
        }
    }
}