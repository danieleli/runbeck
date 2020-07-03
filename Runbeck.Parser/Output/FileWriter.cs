#region Usings

using System.Collections.Generic;
using System.IO;
using Runbeck.Parser.Parsing;

#endregion

namespace Runbeck.Parser.Output
{
    public class FileWriter : IOutputWriter
    {
        public const string OUTPUT_DIR     = "./Output";
        public const string CORRECT_FILE   = OUTPUT_DIR + "/CorrectlyFormmattedRecords.txt";
        public const string INCORRECT_FILE = OUTPUT_DIR + "/IncorrectlyFormmattedRecords.txt";

        public void WriteOutput(ParseResult parseResult)
        {
            if (!Directory.Exists(OUTPUT_DIR))
            {
                Directory.CreateDirectory(OUTPUT_DIR);
            }

            WriteFile(CORRECT_FILE,   parseResult.GoodLines);
            WriteFile(INCORRECT_FILE, parseResult.BadLines);
        }

        private static void WriteFile(string filePath, IList<string> fileContent)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            if (fileContent.Count > 0)
            {
                File.WriteAllLines(filePath, fileContent);
            }
        }
    }
}