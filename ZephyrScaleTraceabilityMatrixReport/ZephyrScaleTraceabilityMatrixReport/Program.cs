using ZephyrScaleTraceabilityMatrixReport.Contexts;
using ZephyrScaleTraceabilityMatrixReport.Helpers;

HelperClass.ValidateAppConfig();

ZephyrScaleApiContext zephyrScaleApiContext = new ZephyrScaleApiContext();
zephyrScaleApiContext.GetTestCases("CMW");

JiraApiContext jiraApi = new JiraApiContext();
jiraApi.GetIssue("AUTO-2");