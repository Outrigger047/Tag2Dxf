/// <summary>
/// TagElement.cs
/// Created 11/18/2023
/// Andrew Wyshak
/// Surface Creations of Maine
/// </summary>

namespace Tag2Dxf.TagElements
{
    /// <summary>
    /// Base class for other Tag elements
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