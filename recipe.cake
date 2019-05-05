#load nuget:?package=Cake.Recipe&version=1.0.0
#load "docs-prep.cake"

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./src",
                            title: "Cake.TeamCityCoverageReporter",
                            repositoryOwner: "wozzo",
                            repositoryName: "Cake.TeamCityCoverageReporter",
                            appVeyorAccountName: "wozzo",
                            shouldRunGitVersion: true,
                            shouldRunDotNetCorePack: true,
                            shouldRunCodecov: false);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context);

// Task("Report-Coverage-To-TeamCity")
//     .IsDependentOn("DotNetCore-Test")
//     .Does(() => {
//         TeamCityCoverageReporter(BuildParameters.Paths.Files.TestCoverageOutputFilePath);
//     });

// BuildParameters.Tasks.TestTask.IsDependentOn("Report-Coverage-To-TeamCity");

Build.RunDotNetCore();
