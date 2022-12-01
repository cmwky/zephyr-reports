using System.Configuration;
using ZephyrScaleTraceabilityMatrixReport.Controllers;
//using ZephyrScaleTraceabilityMatrixReport.Exporters;
using ZephyrScaleTraceabilityMatrixReport.Helpers;
using ZephyrScaleTraceabilityMatrixReport.Models;


/*
 * Verifies that all values in App.config have been provided.
 * If not, user will be notified of what's missing and the program will exit entirely.
 */
HelperClass.ValidateAppConfig();


/*
 * Setup
 */
string? zephyrScaleProjectKey = ConfigurationManager.AppSettings["ZephyrScaleProjectKey"];
JiraController jira = new();
ZephyrScaleController zephyr = new();


/*
 * Get all test cases that belong to a project.
 */
List<TestCase> testCases = zephyr.GetTestCases(projectKey: zephyrScaleProjectKey);


/*
 * For each linked issue to a test case, create a JiraIssue
 */
for (int i = 0; i < testCases.Count; i++)
{
    if (testCases[i].Links.issues.Count > 0)
    {
        testCases[i].JiraIssues = jira.GetJiraIssuesFromLinks(testCases[i].Links);
    }
}

var t = 1;

/*
 * Export data to Excel file and fill out traceability matrix
 */
//ExcelExport.ExportToExcel(testCases/*, issueKeysFromTestCases*/);
//ExcelExport.FormatReportWithPowerShell();