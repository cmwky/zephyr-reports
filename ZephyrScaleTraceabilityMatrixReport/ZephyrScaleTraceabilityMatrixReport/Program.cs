using ZephyrScaleTraceabilityMatrixReport.Contexts;
using ZephyrScaleTraceabilityMatrixReport.Helpers;

HelperClass.ValidateAppConfig();

JiraApiContext jiraApi = new JiraApiContext();
jiraApi.GetIssue("AUTO-2");