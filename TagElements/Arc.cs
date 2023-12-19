///////////////////////////////////////////////////////////////////////////////
/// <summary>
/// Tag2Dxf
/// (C) Copyright 2023 Surface Creations of Maine
///
/// File:    Arc.cs
/// Created: November 20, 2023
/// Purpose: Class to encapsulate a TAG Arc
/// Author:  Andrew Wyshak
/// </summary>
///////////////////////////////////////////////////////////////////////////////

namespace Tag2Dxf.TagElements
{
    /// <summary>
    /// Class to encapsulate a TAG Arc
    /// </summary>
    public sealed class Arc : TagElement
    {
        /// <inheritdoc/>
        public Arc(string rawElementData) : base(rawElementData)
        {
        }

        /// <summary>
        /// Arc radius
        /// </summary>
        public float Radius { get; private set; }

        /// <summary>
        /// Angle of arc start
        /// </summary>
        public float AngleStart { get; private set; }

        /// <summary>
        /// Angle of arc end
        /// </summary>
        public float AngleEnd { get; private set; }

        /// <inheritdoc/>
        protected override void ParseRawData()
        {
            var splitElementData = rawElementData.Split(',');
            X = Convert.ToSingle(splitElementData[1]);
            Y = Convert.ToSingle(splitElementData[2]);
            Radius = Convert.ToSingle(splitElementData[3]);
            AngleStart = Convert.ToSingle(splitElementData[4]);
            AngleEnd = Convert.ToSingle(splitElementData[5]);
            Color = Convert.ToInt16(splitElementData[6]);
            Style = Convert.ToInt16(splitElementData[7]);
            Weight = Convert.ToInt16(splitElementData[8]);
            Level = Convert.ToInt16(splitElementData[9]);
        }
    }
}