#region Usings

using FluentAssertions;
using Runbeck.Parser.Parsing;
using Xunit;
using Xunit.Abstractions;

#endregion

namespace Runbeck.Parser._Test
{
    public class ContentParserTests
    {
        private readonly ITestOutputHelper _output;

        public ContentParserTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void OneGoodOneBad()
        {
            var fileType   = FileType.Csv;
            var content    = new[] {"head1,head2,head3", "line1-1,line1-2,line1-3", ""};
            var fieldCount = 3;

            var results = ContentParser.Parse(fileType, fieldCount, content);

            _output.WriteLine("Good lines:");
            foreach (var line in results.GoodLines)
            {
                _output.WriteLine(line);
            }

            _output.WriteLine("\nBad lines:");
            foreach (var line in results.BadLines)
            {
                _output.WriteLine(line);
            }

            results.BadLines.Count.Should().Be(1);
            results.GoodLines.Count.Should().Be(1);
        }
    }
}