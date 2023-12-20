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
        /// Full path of TAG file
        /// </summary>
        private string fullPath;

        /// <summary>
        /// Raw TAG file data read from disk
        /// </summary>
        private readonly string[]? rawFileData;

        /// <summary>
        /// Collection of elements in file
        /// </summary>
        private List<TagElement> elements = new();

        /// <summary>
        /// Parameterized ctor
        /// </summary>
        /// <param name="fullPath">File path of TAG file to read</param>
        public TagFile(string fullPath)
        {
            this.fullPath = fullPath;

            if (File.Exists(Path.GetFullPath(fullPath)))
            {
                rawFileData = File.ReadAllLines(Path.GetFullPath(fullPath));
                ParseRawFileData();
            }
        }

        /// <summary>
        /// Returns just the file name portion of the full path
        /// </summary>
        public string FileName => Path.GetFileName(fullPath);

        /// <summary>
        /// Returns full path of the file
        /// </summary>
        public string FullPath => Path.GetFullPath(fullPath);

        /// <summary>
        /// Parses TAG file data and populates <see cref="elements"/>
        /// </summary>
        private void ParseRawFileData()
        {
            if (rawFileData != null)
            {
                foreach (var fileLine in rawFileData)
                {
                    // Skip TAG file header comments
                    if (fileLine.StartsWith("***"))
                    {
                        continue;
                    }
    
                    var lineSplit = fileLine.Split(',');
    
                    switch(lineSplit[0])
                    {
                        case "1": // Point
                            elements.Add(new Point(fileLine));
                            break;
                        case "2": // Line
                            elements.Add(new Line(fileLine));
                            break;
                        case "3": // Arc
                            elements.Add(new Arc(fileLine));
                            break;
                        case "4": // Circle
                            elements.Add(new Circle(fileLine));
                            break;
                        default: // No handled data type
                            break;
                    }
                }
            }
        }
    }
}