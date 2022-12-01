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
 * Create JiraIssues from testCase[].links.issues
 */
zephyr.PopulateJiraIssuesFromLinks(ref testCases);


var t = 1;

/*
 * Export data to Excel file and fill out traceability matrix
 */
//ExcelExport.ExportToExcel(testCases/*, issueKeysFromTestCases*/);
//ExcelExport.FormatReportWithPowerShell();