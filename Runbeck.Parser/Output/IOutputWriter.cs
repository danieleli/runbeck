namespace Runbeck.Parser.Output
{
    using Runbeck.Parser.Parsing;

    public interface IOutputWriter
    {
        void WriteOutput(ParseResult parseResult);
    }
}