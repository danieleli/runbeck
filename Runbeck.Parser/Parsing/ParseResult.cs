#region Usings

using System.Collections.Generic;

#endregion

namespace Runbeck.Parser.Parsing
{
    public class ParseResult
    {
        public ParseResult()
        {
            GoodLines = new List<string>();
            BadLines  = new List<string>();
        }

        public IList<string> BadLines { get; set; }
        public IList<string> GoodLines { get; set; }
    }
}