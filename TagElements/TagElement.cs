///////////////////////////////////////////////////////////////////////////////
/// <summary>
/// Tag2Dxf
/// (C) Copyright 2023 Surface Creations of Maine
///
/// File:    TagElement.cs
/// Created: November 20, 2023
/// Purpose: Base class for all TAG elements
/// Author:  Andrew Wyshak
/// </summary>
///////////////////////////////////////////////////////////////////////////////

namespace Tag2Dxf.TagElements
{
    /// <summary>
    /// Base class for all TAG elements
    /// </summary>
    public abstract class TagElement
    {
        /// <summary>
        /// Line of raw element data read from file
        /// </summary>
        protected string rawElementData;

        /// <summary>
        /// TagElement parameterized ctor accepting raw data from file
        /// </summary>
        /// <param name="rawElementData">Raw element data in read from TAG file</param>
        protected TagElement(string rawElementData)
        {
            this.rawElementData = rawElementData;
        }

        /// <summary>
        /// X coordinate of the start or center of the element
        /// </summary>
        public float X { get; protected set; }

        /// <summary>
        /// Y coordinate of the start or center of the element
        /// </summary>
        public float Y { get; protected set; }

        /// <summary>
        /// Z coordinate of the start or center of the element
        /// </summary>
        public float Z { get; protected set; }

        /// <summary>
        /// Color value
        /// </summary>
        public short Color { get; protected set; }

        /// <summary>
        /// Style value (dashed, dotted, solid, etc.)
        /// </summary>
        public short Style { get; protected set; }

        /// <summary>
        /// Weight value (line thickness)
        /// </summary>
        public short Weight { get; protected set; }

        /// <summary>
        /// Level value
        /// </summary>
        public short Level { get; protected set; }

        /// <summary>
        /// Parses raw data from file and populates class members. Implementation varies
        /// based on TAG element type.
        /// </summary>
        protected abstract void ParseRawData();
    }
}