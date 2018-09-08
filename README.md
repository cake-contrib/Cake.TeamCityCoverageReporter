# Cake.TeamCityCoverageReporter

## Information

### Build Status

Branch | Status
--- | ---
Master | 
Develop | 

### Nuget
[![NuGet](https://img.shields.io/nuget/v/Cake.TeamCityCoverageReporter.svg)](https://www.nuget.org/packages/Cake.TeamCityCoverageReporter/) 

### Licence
[![License](http://img.shields.io/:license-mit-blue.svg)](http://cake-contrib.mit-license.org)

## Usage

```CSharp
Task("Report-Coverage-To-TeamCity")
    .IsDependentOn("DotNetCore-Test")
    .Does(() => {
        TeamCityCoverageReporter(BuildParameters.Paths.Files.TestCoverageOutputFilePath);
    });
```

Then the output should be as follows with appropriate values replaced

```
========================================
Report-Coverage-To-TeamCity
========================================
Executing task: Report-Coverage-To-TeamCity
##teamcity[buildStatisticValue key='CodeCoverageAbsCCovered' value='1']
##teamcity[buildStatisticValue key='CodeCoverageAbsCTotal' value='1']
##teamcity[buildStatisticValue key='CodeCoverageC' value='100']
##teamcity[buildStatisticValue key='CodeCoverageAbsMCovered' value='3']
##teamcity[buildStatisticValue key='CodeCoverageAbsMTotal' value='3']
##teamcity[buildStatisticValue key='CodeCoverageM' value='100']
##teamcity[buildStatisticValue key='CodeCoverageAbsSCovered' value='16']
##teamcity[buildStatisticValue key='CodeCoverageAbsSTotal' value='16']
##teamcity[buildStatisticValue key='CodeCoverageS' value='100']
##teamcity[buildStatisticValue key='CodeCoverageAbsBCovered' value='3']
##teamcity[buildStatisticValue key='CodeCoverageAbsBTotal' value='3']
##teamcity[buildStatisticValue key='CodeCoverageB' value='100']
Finished executing task: Report-Coverage-To-TeamCity
```

## Scope
The purpose of this project is to enable code coverage reporting from an OpenCover result xml file to TeamCity using the Cake build tool.

## Roadmap

Just started so suggestions welcome.