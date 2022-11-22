using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Helpers;
using ZephyrScaleTraceabilityMatrixReport.Contexts;
using ZephyrScaleTraceabilityMatrixReport.Models;

namespace ZephyrScaleTraceabilityMatrixReport.Controllers
{
    internal class JiraController
    {
        private JiraApiContext JiraApiContext;

        public JiraController()
        {
            this.JiraApiContext = new();
        }

        public List<string> GetJiraIssueKeysUsingJql(string jql)
        {
            string jiraIssues = JiraApiContext.GetIssuesUsingJql(jql);
            List<string> keys = new();

            var deserializedJiraIssues = JsonConvert.DeserializeObject<JiraIssuesUsingJql>(jiraIssues);

            foreach(JiraIssue issue in deserializedJiraIssues.issues)
            {
                keys.Add(issue.key);
            }

            return keys;
        }

        public List<JiraIssue> GetJiraIssuesFromLinks(Links links)
        {
            List<JiraIssue> jiraIssues = new();

            foreach(LinkedIssue linkedIssue in links.issues)
            {
                string issue = JiraApiContext.GetIssue(linkedIssue.issueId.ToString());
                string parsedIssue = JObject.Parse(issue).ToString();
                JiraIssue serializedIssue = JsonConvert.DeserializeObject<JiraIssue>(parsedIssue);
                jiraIssues.Add(serializedIssue);
            }

            return jiraIssues;
        }
    }
}
