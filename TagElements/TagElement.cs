///////////////////////////////////////////////////////////////////////////////
/// <summary>
/// Tag2Dxf
/// (C) Copyright 2023 Surface Creations of Maine
///
/// File:    TagElement.cs
/// Created: November 20, 2023
/// Purpose: Base class for other TAG elements
/// Author:  Andrew Wyshak
/// </summary>
///////////////////////////////////////////////////////////////////////////////

namespace Tag2Dxf.TagElements
{
    /// <summary>
    /// Base class for other TAG elements
    /// </summary>
    public abstract class TagElement
    {
        /// <summary>
        /// X coordinate of the start or center of the element
        /// </summary>
        public float X { get; private set; }

        /// <summary>
        /// Y coordinate of the start or center of the element
        /// </summary>
        public float Y { get; private set; }

        /// <summary>
        /// Color value
        /// </summary>
        public short Color { get; private set; }

        /// <summary>
        /// Level value
        /// </summary>
        public short Level { get; private set; }
    }
}