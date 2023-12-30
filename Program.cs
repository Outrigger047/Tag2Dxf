///////////////////////////////////////////////////////////////////////////////
/// <summary>
/// Tag2Dxf
/// (C) Copyright 2023 Surface Creations of Maine
///
/// File:    Program.cs
/// Created: November 20, 2023
/// Purpose: Class containing Main method
/// Author:  Andrew Wyshak
/// </summary>
///////////////////////////////////////////////////////////////////////////////

using System.CommandLine;
using System.Text;

namespace Tag2Dxf
{
    /// <summary>
    /// Class containing Main method
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <remarks>
        /// Leveraging System.CommandLine features in preview
        /// </remarks>
        /// <param name="args">Command line arguments</param>
        public static async Task<int> Main(string[] args)
        {
            // Root command
            var rootCmd = new RootCommand("Tag2Dxf: Converts Taglio TAG files to AutoCAD DXF");

            // Options and arguments for both subcommands
            var outputOption = new Option<string>(
                name: "--outdir",
                description: "Directory to output all files",
                getDefaultValue: () => string.Empty);
            outputOption.AddAlias("-o");
            outputOption.Arity = ArgumentArity.ExactlyOne;
            var logOption = new Option<bool>(
                name: "--log",
                description: "Produce an error log file on the Desktop",
                getDefaultValue: () => false);
            logOption.AddAlias("-l");

            // Directories subcommand
            var dirsSubCmd = new Command(
                name: "dirs",
                description: "Processes a list of directories to search for and convert TAG files to DXF");
            dirsSubCmd.AddAlias("-d");
            var recurseOption = new Option<bool>(
                name: "--recurse",
                description: "Use recursion to find all files in all lower directories",
                getDefaultValue: () => false);
            recurseOption.AddAlias("-r");
            dirsSubCmd.AddOption(recurseOption);
            var dirsArgs = new Argument<List<DirectoryInfo>>(
                name: "directory list",
                description: "List of directories to process");
            dirsSubCmd.AddArgument(dirsArgs);
            rootCmd.Add(dirsSubCmd);

            dirsSubCmd.SetHandler( async(dirsArgs, recurseOption, outputOption, logOption) => 
            {
                await ProcessDirectories(dirsArgs, recurseOption, outputOption, logOption);
            }, 
            dirsArgs, recurseOption, outputOption, logOption);

            // Files subcommand
            var filesSubCmd = new Command(
                name: "files",
                description: "Processes a list of TAG files to convert to DXF");
            filesSubCmd.AddAlias("-f");
            var filesArgs = new Argument<List<FileInfo>>(
                name: "file list",
                description: "List of files to process");
            filesSubCmd.AddArgument(filesArgs);
            rootCmd.Add(filesSubCmd);

            filesSubCmd.SetHandler( async(filesArgs, outputOption, logOption) => 
            {
                await ProcessFiles(filesArgs, outputOption, logOption);
            },
            filesArgs, outputOption, logOption);

            filesSubCmd.AddOption(outputOption);
            dirsSubCmd.AddOption(outputOption);
            filesSubCmd.AddOption(logOption);
            dirsSubCmd.AddOption(logOption);

            // Invocation
            return await rootCmd.InvokeAsync(args);
        }

        /// <summary>
        /// Enumerates all files in specified directories and invokes <see cref="ProcessFiles"/>
        /// </summary>
        /// <param name="directories">Space-delimited list of directories to process</param>
        /// <param name="recurse">Recursively traverse lower directories to find all files</param>
        /// <returns></returns>
        private static async Task ProcessDirectories(List<DirectoryInfo> directories, bool recurse, string outputDirectory, bool logErrors)
        {
            if (directories.Any())
            {
                var filesToProcess = new List<FileInfo>();

                foreach (var dir in directories)
                {
                    if (dir.Exists)
                    {
                        foreach (var file in 
                            dir.GetFiles("*.tag", recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
                        {
                            filesToProcess.Add(file);
                        }
                    }
                }

                await ProcessFiles(filesToProcess, outputDirectory, logErrors);
            }
            else
            {
                Console.WriteLine("No directories specified");
            }
        }

        private static async Task ProcessFiles(List<FileInfo> tagFileInfos, string outputDirectoryIn, bool logErrors)
        { 
            var errorFiles = new List<(FileInfo, Exception)>();

            // Initialize each TAG file and do conversion
            foreach (var tagFileInfo in tagFileInfos)
            {
                try
                {
                    var tagFile = new TagFile(tagFileInfo.FullName);
                    var dxfFile = Converter.ConvertTag2Dxf(tagFile);
                    
                    DirectoryInfo outputDirectory;
                    if (outputDirectoryIn != null && outputDirectoryIn != string.Empty)
                    {
                        outputDirectory = new DirectoryInfo(outputDirectoryIn);

                        if (!outputDirectory.Exists)
                        {
                            Directory.CreateDirectory(outputDirectory.FullName);
                        }
                    }
                    else
                    {
                        outputDirectory = new DirectoryInfo(tagFileInfo.DirectoryName);
                    }

                    dxfFile.Save(Path.Combine(outputDirectory.FullName, tagFileInfo.Name + ".dxf"));
                }
                catch (Exception e)
                {
                    errorFiles.Add((tagFileInfo, e));
                    continue;
                }
            }

            // Log any errors
            if (logErrors && errorFiles.Any())
            {
                var sw = File.CreateText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Tag2Dxf_errors_{DateTime.Now.Ticks}.txt"));
                foreach (var errorFile in errorFiles)
                {
                    var errorStringBuilder = new StringBuilder($"{errorFile.Item1.FullName} -- {errorFile.Item2.Message}");
                    if (errorFile.Item2.InnerException != null)
                    {
                        errorStringBuilder.Append(errorStringBuilder + " -- " + errorFile.Item2.InnerException.Message);
                    }

                    sw.WriteLine(errorStringBuilder.ToString());
                    Console.WriteLine(errorStringBuilder.ToString());
                }

                sw.Close();
            }
        }
    }
}