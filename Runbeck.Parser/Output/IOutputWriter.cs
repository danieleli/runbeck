#region Usings

using Runbeck.Parser.Parsing;

#endregion

namespace Runbeck.Parser.Output
{
    public interface IOutputWriter
    {
        void WriteOutput(ParseResult parseResult);
    }
}