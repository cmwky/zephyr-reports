using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZephyrScaleTraceabilityMatrixReport.Contexts;
using ZephyrScaleTraceabilityMatrixReport.Models;

namespace ZephyrScaleTraceabilityMatrixReport.Controllers
{
    internal class JiraController
    {
        private readonly JiraApiContext JiraApiContext = new();

        public List<JiraIssue>? GetJiraIssuesUsingJql(string jql)
        {
            string response = JiraApiContext.GetIssuesUsingJql(jql);
            JiraIssuesUsingJql? deserializedIssues = JsonConvert.DeserializeObject<JiraIssuesUsingJql>(response);

            return deserializedIssues?.issues;
        }

        public List<JiraIssue> GetJiraIssuesFromLinks(Links links)
        {
            List<JiraIssue> jiraIssues = new();

            foreach(LinkedIssue linkedIssue in links.issues)
            {
                string issue = JiraApiContext.GetIssue(linkedIssue.IssueId.ToString());
                string parsedIssue = JObject.Parse(issue).ToString();
                JiraIssue serializedIssue = JsonConvert.DeserializeObject<JiraIssue>(parsedIssue);
                jiraIssues.Add(serializedIssue);
            }

            return jiraIssues;
        }
    }
}
