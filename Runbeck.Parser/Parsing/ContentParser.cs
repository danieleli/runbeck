#region Usings

using System;
using System.Collections.Generic;

#endregion

namespace Runbeck.Parser.Parsing
{
    public class ContentParser
    {
        public static ParseResult Parse(FileType fileType, int fieldCount, IEnumerable<string> content)
        {
            var seperator   = GetSeperator(fileType);
            var rtn         = new ParseResult();
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

            return new[] {Convert.ToChar(9)};
        }
    }
}