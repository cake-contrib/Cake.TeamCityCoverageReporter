using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Moq;
using Shouldly;
using Xunit;

namespace Cake.TeamCityCoverageReporter.Tests
{
    public class TeamCityCoverageReporterTests
    {
        private static async Task WhenReceiveThenExpectTeamCityCoverageReport(string sampleFile, string sampleExpectedResultsPath)
        {
            // Given
            var sampleFilePath = new FilePath(sampleFile);
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

        [Fact]
        public async Task GIVEN_SampleFiles_WHEN_ReporterCalled_THEN_OutputIsAsExpected()
        {
            const string sampleFile = @".\Sample1.xml";
            const string sampleExpectedResultsPath = @".\Sample1-Expected.txt";
            await WhenReceiveThenExpectTeamCityCoverageReport(sampleFile, sampleExpectedResultsPath);
        }

        [Fact]
        public async Task GIVEN_EmptySampleFiles_WHEN_ReporterCalled_THEN_OutputIsAsExpected()
        {
            // Given
            const string sampleFile = @".\Sample2-Empty.xml";
            const string sampleExpectedResultsPath = @".\Sample2-Empty-Expected.txt";
            await WhenReceiveThenExpectTeamCityCoverageReport(sampleFile, sampleExpectedResultsPath);
        }
    }
}
