using System.Configuration;
using ZephyrScaleTraceabilityMatrixReport.Controllers;
using ZephyrScaleTraceabilityMatrixReport.Exporters;
using ZephyrScaleTraceabilityMatrixReport.Helpers;
using ZephyrScaleTraceabilityMatrixReport.Models;

ExcelExport.FormatReportWithPowerShell();

HelperClass.ValidateAppConfig();

//controllers
JiraController jira = new();
ZephyrScaleController zephyr = new();

//1. get all test cases belonging to a single project
List<TestCase> testCases = zephyr.GetTestCases(ConfigurationManager.AppSettings["ZephyrScaleProjectKey"]);

//2. for each test case that has a linked jira issue,
//   create JiraIssue objects and save the keys in a list.
List<string> issueKeysFromTestCases = new();

for (int i = 0; i < testCases.Count; i++)
{
    if (testCases[i].links.issues.Count > 0)
    {
        testCases[i].jiraIssues = jira.GetJiraIssuesFromLinks(testCases[i].links);

        foreach(JiraIssue issue in testCases[i].jiraIssues)
        {
            issueKeysFromTestCases.Add(issue.key);
        }
    }
}

issueKeysFromTestCases = issueKeysFromTestCases.Distinct().ToList();

//3. get jira issue keys using jql filter
List<string> issueKeys = jira.GetJiraIssueKeysUsingJql(ConfigurationManager.AppSettings["JiraJqlFilter"]);

List<string> issuesWithNoTestCoverage = issueKeys.Except(issueKeysFromTestCases).ToList();

ExcelExport.ExportToExcel(testCases, issueKeysFromTestCases);
//ExcelExport.FormatReportWithPowerShell();