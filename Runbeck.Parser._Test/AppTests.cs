using System;

namespace Runbeck.Parser._Test
{
    using FluentAssertions;
    using Moq;
    using Xunit;
    using Xunit.Abstractions;
    using Runbeck.Parser.UI;
    using Runbeck.Parser.Output;

    public class AppTests
    {
        private readonly ITestOutputHelper _output;

        public AppTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void FileDoesNotExist_Should_PromptForRetry()
        {
            var console = new Mock<IUserInterface>();
            var consoleReadLineCallCount = 0;
            var badFilename = "a non existing file";
            var noRetry = "n";
            var readLineValues = new[] {badFilename, noRetry};  
            console.Setup(c => c.ReadLine()).Returns(() =>
            {
               return readLineValues[consoleReadLineCallCount++];
            });

            var outputWriter = new Mock<IOutputWriter>();
            var app = new App(console.Object, outputWriter.Object);

            var lines = app.GetFileLines();

            lines.Should().BeNull();
            console.Verify(c=>c.WriteLine(App.WHERE_IS_FILE_MSG), Times.Once);
            console.Verify(c => c.WriteLine(App.FILE_NOT_FOUND_MSG), Times.Once);
        }

        [Fact]
        public void FileDoesExist_Should_ReturnFileContent()
        {
            var console = new Mock<IUserInterface>();
            var goodFilename = "./TestFiles/TestFile1.csv";
            console.Setup(c => c.ReadLine()).Returns(goodFilename);

            var outputWriter = new Mock<IOutputWriter>();
            var app = new App(console.Object, outputWriter.Object);

            var lines = app.GetFileLines();

            lines.Should().NotBeNull();
            console.Verify(c => c.WriteLine(App.WHERE_IS_FILE_MSG), Times.Once);

            foreach (var line in lines)
            {
                _output.WriteLine(line);
            }
        }

        [Fact]
        public void EmptyFile_Should_PromptForRetry_ThenSubmitGoodFile()
        {
            var console = new Mock<IUserInterface>();
            var consoleReadLineCallCount = 0;
            var emptyFilename = "./TestFiles/emptyFile.csv";
            var yesRetry = "y";
            var goodFile = "./TestFiles/TestFile1.csv";
            var readLineValues = new[] { emptyFilename, yesRetry, goodFile };
            console.Setup(c => c.ReadLine()).Returns(() =>
            {
                return readLineValues[consoleReadLineCallCount++];
            });

            var outputWriter = new Mock<IOutputWriter>();
            var app = new App(console.Object, outputWriter.Object);

            var lines = app.GetFileLines();

            lines.Should().NotBeNull();
            console.Verify(c => c.WriteLine(App.WHERE_IS_FILE_MSG), Times.Exactly(2));
            console.Verify(c => c.WriteLine(App.FILE_EMPTY_MSG), Times.Once);

            foreach (var line in lines)
            {
                _output.WriteLine(line);
            }
        }
    }
}
