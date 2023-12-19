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
        /// <summary>
        /// Provides a lookup to find a data type for a corresponding TAG file element type
        /// </summary>
        public static Dictionary<string, Type> ElemTypeLookup = new Dictionary<string, Type>()
        {
            { "1", typeof(Point) },
            { "2", typeof(Line) },
            { "3", typeof(Arc) },
            { "4", typeof(Circle) }
        };

        /// <summary>
        /// Full path of TAG file
        /// </summary>
        private string fullPath;

        /// <summary>
        /// Raw TAG file data read from disk
        /// </summary>
        private string[] rawFileData;

        /// <summary>
        /// Collection of elements in file
        /// </summary>
        private List<TagElement> elements;

        /// <summary>
        /// Parameterized ctor
        /// </summary>
        /// <param name="fullPath">File path of TAG file to read</param>
        public TagFile(string fullPath)
        {
            this.fullPath = fullPath;
        }

        /// <summary>
        /// Returns just the file name portion of the full path
        /// </summary>
        public string FileName => Path.GetFileName(fullPath);
    }
}