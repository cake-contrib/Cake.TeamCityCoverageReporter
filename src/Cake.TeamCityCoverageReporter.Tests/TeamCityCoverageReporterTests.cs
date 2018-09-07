using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.Diagnostics;
using Moq;
using Shouldly;
using Xunit;

namespace Cake.TeamCityCoverageReporter.Tests
{
    public class TeamCityCoverageReporterTests
    {
        [Fact]
        public async Task GIVEN_SampleFiles_WHEN_ReporterCalled_THEN_OutputIsAsExpected()
        {
            // Given
            const string sampleFilePath = @".\Sample1.xml";
            const string sampleExpectedResultsPath = @".\Sample1-Expected.txt";

            var mockCakeContext = new Mock<ICakeContext>();
            var mockCakeLog = new Mock<ICakeLog>();
            mockCakeContext
                .SetupGet(x => x.Log)
                .Returns(mockCakeLog.Object);
            var loggedRows = new List<string>();
            mockCakeLog
                .Setup(x => x.Write(It.IsAny<Verbosity>(), It.IsAny<LogLevel>(), It.IsAny<string>(), It.IsAny<object[]>()))
                .Callback<Verbosity, LogLevel, string, object[]>((verbosity, logLevel, format, objects) 
                    => loggedRows.Add(string.Format(format, objects)));

            // When
            mockCakeContext.Object.TeamCityCoverageReporter(sampleFilePath);

            // Then
            var result = string.Join("\r\n", loggedRows);
            var expected = await File.ReadAllTextAsync(sampleExpectedResultsPath).ConfigureAwait(false);
            result.ShouldBe(expected);
        }
    }
}
