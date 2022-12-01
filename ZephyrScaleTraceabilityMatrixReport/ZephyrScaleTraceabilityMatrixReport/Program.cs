using System.Configuration;
using ZephyrScaleTraceabilityMatrixReport.Controllers;
using ZephyrScaleTraceabilityMatrixReport.Exporters;
using ZephyrScaleTraceabilityMatrixReport.Helpers;
using ZephyrScaleTraceabilityMatrixReport.Models;

/*
 * Confirms that all values in App.config are provided.
 * If not, program will terminate immediately and notify user of what's missing.
 */
HelperClass.ValidateAppConfig();

/*
 * Setup
 */ 
JiraController jira = new();
ZephyrScaleController zephyr = new();


/*
 * Get all test cases and test case execution statuses that belong to a project.
 */
//List<TestCaseExecutionStatus> testCaseExecutionStatuses = zephyr.GetTestCaseExecutionStatuses();
List<TestCase> testCases = zephyr.GetTestCases(ConfigurationManager.AppSettings["ZephyrScaleProjectKey"]);


/*
 * For each Jira issue that's linked to a test case,
 * create a JiraIssue and save the keys to a distinct list.
 */
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

/*
 * Get jira issue keys using jql filter
 */
List<string> issueKeys = jira.GetJiraIssueKeysUsingJql(ConfigurationManager.AppSettings["JiraJqlFilter"]);


/*
 * Export data to Excel file and fill out traceability matrix
 */
ExcelExport.ExportToExcel(testCases, issueKeysFromTestCases);
ExcelExport.FormatReportWithPowerShell();