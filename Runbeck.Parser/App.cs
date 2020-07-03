#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Runbeck.Parser.Output;
using Runbeck.Parser.Parsing;
using Runbeck.Parser.UI;

#endregion

namespace Runbeck.Parser
{
    public interface IApp
    {
        void Run();
        IEnumerable<string> GetFileLines();
    }

    public class App : IApp
    {
        public const string WHERE_IS_FILE_MSG               = "Where is the file located?";
        public const string FILE_NOT_FOUND_MSG              = "File not found.  Try again? (y/n)";
        public const string FILE_EMPTY_MSG                  = "File empty.  Try again? (y/n)";
        public const string UNEXPECTED_EXCEPTION_MSG        = "Unexpected exception occurred.  Program is exiting.";
        public const string EXITING_APPLICATION_MSG         = "Exiting applicion.";
        public const string GET_FILE_TYPE_MSG               = "Is the file format CSV (comma seperated values) or TSV (tab seperated values)? (c/t)";
        public const string FILE_TYPE_INVALID_SELECTION_MSG = "Invalid selection.Please try again.";
        public const string HOW_MANY_FIELDS_MSG             = "How many fields should each record contain?";
        public const string FIELD_COUNT_INVALID_INPUT_MSG   = "Invalid input.  Please try again.";
        public const string GOOD_RECORD_COUNT_MSG           = "Good record count: ";
        public const string BAD_RECORD_COUNT_MSG            = "Bad record count: ";

        private readonly IOutputWriter  _outputWriter;
        private readonly IUserInterface _userInterface;

        public App(IUserInterface userInterface, IOutputWriter outputWriter)
        {
            _userInterface = userInterface;
            _outputWriter  = outputWriter;
        }

        public void Run()
        {
            var lines = GetFileLines();
            if (lines == null)
            {
                _userInterface.WriteLine(EXITING_APPLICATION_MSG);
                return;
            }

            var fileType    = GetFileType();
            var fieldCount  = GetFieldCount();
            var parseResult = ContentParser.Parse(fileType, fieldCount, lines);

            _userInterface.WriteLine(GOOD_RECORD_COUNT_MSG + parseResult.GoodLines.Count);
            _userInterface.WriteLine(BAD_RECORD_COUNT_MSG  + parseResult.BadLines.Count);

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

                    // empty file
                    _userInterface.WriteLine(FILE_EMPTY_MSG);
                    var response = _userInterface.ReadLine();
                    if (!response.ToUpper().Equals("Y"))
                    {
                        return null;
                    }

                    fileLines = null;
                }
                catch (FileNotFoundException)
                {
                    _userInterface.WriteLine(FILE_NOT_FOUND_MSG);
                    var response = _userInterface.ReadLine();
                    if (!response.ToUpper().Equals("Y"))
                    {
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

        public int GetFieldCount()
        {
            while (true)
            {
                _userInterface.WriteLine(HOW_MANY_FIELDS_MSG);

                var response = _userInterface.ReadLine();
                var success  = int.TryParse(response, out var result);
                if (success)
                {
                    return result;
                }

                _userInterface.WriteLine(FIELD_COUNT_INVALID_INPUT_MSG);
            }
        }

        public FileType GetFileType()
        {
            while (true)
            {
                _userInterface.WriteLine(GET_FILE_TYPE_MSG);
                var response = Console.ReadKey().Key;
                _userInterface.WriteLine("\n");
                if (response == ConsoleKey.C)
                {
                    return FileType.Csv;
                }

                if (response == ConsoleKey.T)
                {
                    return FileType.Tsv;
                }

                _userInterface.WriteLine(FILE_TYPE_INVALID_SELECTION_MSG);
            }
        }
    }
}