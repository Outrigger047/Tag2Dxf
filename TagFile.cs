///////////////////////////////////////////////////////////////////////////////
/// <summary>
///
/// Tag2Dxf
///
/// File:    TagFile.cs
/// Created: November 20, 2023
/// Purpose: Class to encapsulate a TAG file and its elements
/// Author:  Andrew Wyshak
///          Surface Creations of Maine
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
        /// Collection of elements in file
        /// </summary>
        public List<TagElement> Elements { get; private set; } = new();

        /// <summary>
        /// Parses TAG file data and populates <see cref="Elements"/>
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

                    try
                    {
                        switch(lineSplit[0])
                        {
                            case "1": // Point
                                Elements.Add(new Point(fileLine));
                                break;
                            case "2": // Line
                                Elements.Add(new Line(fileLine));
                                break;
                            case "3": // Arc
                                Elements.Add(new Arc(fileLine));
                                break;
                            case "4": // Circle
                                Elements.Add(new Circle(fileLine));
                                break;
                            default: // No handled data type
                                break;
                        }   
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error while parsing raw TAG file data");
                        Console.WriteLine($"File: {fullPath}");
                        Console.WriteLine($"Raw data: {fileLine}");

                        throw;
                    }
                }
            }
        }
    }
}