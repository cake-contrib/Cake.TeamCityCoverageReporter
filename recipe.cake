#load nuget:https://www.myget.org/F/cake-contrib/api/v2?package=Cake.Recipe&prerelease
#load "docs-prep.cake"
#addin nuget:?package=Cake.TeamCityCoverageReporter&PreRelease

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./src",
                            title: "Cake.TeamCityCoverageReporter",
                            repositoryOwner: "cake-contrib",
                            repositoryName: "Cake.TeamCityCoverageReporter",
                            appVeyorAccountName: "cakecontrib",
                            shouldRunGitVersion: true,
                            shouldRunDotNetCorePack: true);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context,
                            dupFinderExcludePattern: new string[] {
                                BuildParameters.RootDirectoryPath + "/src/Cake.TeamCityCoverageReporter.Tests/*.cs"
                                },
                            testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* -[Moq*]* -[AutoFixture*]* ",
                            testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
                            testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

Task("Report-Coverage-To-TeamCity")
    .IsDependentOn("DotNetCore-Test")
    .Does(() => {
        TeamCityCoverageReporter(BuildParameters.Paths.Files.TestCoverageOutputFilePath);
    });

BuildParameters.Tasks.TestTask.IsDependentOn("Report-Coverage-To-TeamCity");

#break
Build.RunDotNetCore();
