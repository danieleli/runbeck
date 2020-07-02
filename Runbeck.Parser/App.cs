﻿namespace Runbeck.Parser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Runbeck.Parser.Output;
    using Runbeck.Parser.Parsing;
    using Runbeck.Parser.UI;

    public interface IApp
    {
        void Start();
        IEnumerable<string> GetFileLines();
    }

    public class App : IApp
    {
        private readonly IUserInterface _userInterface;
        private readonly IOutputWriter _outputWriter;
        public const string WHERE_IS_FILE_MSG = "Where is the file located?";
        public const string FILE_NOT_FOUND_MSG = "File not found.  Try again? (y/n)";
        public const string FILE_EMPTY_MSG = "File empty.  Try again? (y/n)";
        public const string UNEXPECTED_EXCEPTION_MSG = "Unexpected exception occurred.  Program is exiting.";
        public const string EXITING_APPLICATION_MSG = "Exiting applicion.";

        public App(IUserInterface userInterface, IOutputWriter outputWriter)
        {
            _userInterface = userInterface;
            _outputWriter = outputWriter;
        }

        public void Start()
        {
            var lines = GetFileLines();
            if (lines == null)
            {
                _userInterface.WriteLine(EXITING_APPLICATION_MSG);
            }

            var parseResult = ContentParser.Parse(FileType.Csv, 3, lines);

            _outputWriter.WriteOutput(parseResult);
        }

        

        /// <returns>Contents of file or null to signal to end appliation.</returns>
        public IEnumerable<string> GetFileLines()
        {
            IEnumerable<string> fileLines = null;
            while (fileLines == null)
            {
                _userInterface.WriteLine(WHERE_IS_FILE_MSG);
                var filename = _userInterface.ReadLine();
                try
                {
                    fileLines = File.ReadLines(filename);
                    var fileHasContent = fileLines.Count() > 0;
                    if (fileHasContent)
                    {
                        return fileLines;
                    }
                    else
                    {
                        // empty file
                        _userInterface.WriteLine(FILE_EMPTY_MSG);
                        var response = _userInterface.ReadLine();
                        if (!response.ToUpper().Equals("Y"))
                        {
                            return null;
                        }

                        fileLines = null;
                    }
                }
                catch (FileNotFoundException)
                {
                    _userInterface.WriteLine(FILE_NOT_FOUND_MSG);
                    var response = _userInterface.ReadLine();
                    if (!response.ToUpper().Equals("Y"))
                    {
                        _userInterface.WriteLine(EXITING_APPLICATION_MSG);
                        return null;
                    }
                }
                catch (Exception e)
                {
                    _userInterface.WriteLine(UNEXPECTED_EXCEPTION_MSG);
                    _userInterface.WriteLine(e.Message);
                    return null;
                }
            }

            return null;
        }
    }
}