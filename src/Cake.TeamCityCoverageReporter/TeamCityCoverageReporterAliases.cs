using System;
using System.Xml.Linq;
using System.Xml.XPath;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.Diagnostics;
using Cake.Core.IO;

namespace Cake.TeamCityCoverageReporter
{
    public static class TeamCityCoverageReporterAliases {
        [CakeMethodAlias]
        public static void TeamCityCoverageReporter(this ICakeContext context, FilePath openCoverResultsFilePath) {
            context.TeamCityCoverageReporter(openCoverResultsFilePath.FullPath);
        }

        [CakeMethodAlias]
        public static void TeamCityCoverageReporter(this ICakeContext context, string openCoverResultsFilePath) {
            var doc = XDocument.Load(openCoverResultsFilePath);
            var summary = doc.XPathSelectElement("/CoverageSession/Summary");
        
            // Classes.
            ReportCoverageMetric(
                context,
                summary, 
                "visitedClasses", "CodeCoverageAbsCCovered", 
                "numClasses", "CodeCoverageAbsCTotal", 
                "CodeCoverageC");
        
            // Methods.
            ReportCoverageMetric(
                context,
                summary, 
                "visitedMethods", "CodeCoverageAbsMCovered", 
                "numMethods", "CodeCoverageAbsMTotal", 
                "CodeCoverageM");
        
            // Sequence points / statements.
            ReportCoverageMetric(
                context,
                summary, 
                "visitedSequencePoints", "CodeCoverageAbsSCovered", 
                "numSequencePoints", "CodeCoverageAbsSTotal", 
                "CodeCoverageS");
        
            // Branches.
            ReportCoverageMetric(
                context,
                summary, 
                "visitedBranchPoints", "CodeCoverageAbsBCovered", 
                "numBranchPoints", "CodeCoverageAbsBTotal", 
                "CodeCoverageB");
        }

        private static void ReportCoverageMetric(
            ICakeContext context,
            XElement summary, 
            string ocVisitedAttr, 
            string tcVisitedKey, 
            string ocTotalAttr, 
            string tcTotalKey,
            string tcCoverageKey) 
        {
            double visited = Convert.ToDouble(summary.Attribute(ocVisitedAttr)?.Value);
            double total = Convert.ToDouble(summary.Attribute(ocTotalAttr)?.Value);
            double coverage = (visited / total) * 100;
            if (double.IsNaN(coverage))
            {
                coverage = 0.0d;
            }
        
            context.Log.Information(FormattableString.Invariant($"##teamcity[buildStatisticValue key='{tcVisitedKey}' value='{visited}']"));
            context.Log.Information(FormattableString.Invariant($"##teamcity[buildStatisticValue key='{tcTotalKey}' value='{total}']"));
            context.Log.Information(FormattableString.Invariant($"##teamcity[buildStatisticValue key='{tcCoverageKey}' value='{coverage}']"));
        }
    }
}
