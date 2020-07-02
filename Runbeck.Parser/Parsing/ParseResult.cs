namespace Runbeck.Parser.Parsing
{
    using System.Collections.Generic;

    public class ParseResult
    {
        public IList<string> GoodLines { get; set; }
        public IList<string> BadLines { get; set; }

        public ParseResult()
        {
            GoodLines = new List<string>();
            BadLines = new List<string>();
        }
    }
}