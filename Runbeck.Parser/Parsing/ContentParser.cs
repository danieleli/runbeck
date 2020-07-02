namespace Runbeck.Parser.Parsing
{
    using System;
    using System.Collections.Generic;

    public class ContentParser
    {
        public static ParseResult Parse(FileType fileType, int fieldCount, IEnumerable<string> content)
        {
            var seperator = GetSeperator(fileType);
            var rtn = new ParseResult();
            var isHeaderRow = true;

            foreach (var line in content)
            {
                if (isHeaderRow)
                {
                    isHeaderRow = false;
                    continue;
                }

                var fields = line.Split(seperator);
                if (fields.Length == fieldCount)
                {
                    rtn.GoodLines.Add(line);
                }
                else
                {
                    rtn.BadLines.Add(line);
                }
            }

            return rtn;
        }

        private static char[] GetSeperator(FileType fileType)
        {
            if (fileType == FileType.Csv)
            {
                return ",".ToCharArray();
            }
            else
            {
                return new char[] { Convert.ToChar(9) };
            }
        }
    }
}