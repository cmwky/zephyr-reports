using Newtonsoft.Json;
using System.Web.Helpers;
using ZephyrScaleTraceabilityMatrixReport.Contexts;
using ZephyrScaleTraceabilityMatrixReport.Models;

namespace ZephyrScaleTraceabilityMatrixReport.Controllers
{
    internal class JiraController
    {
        private JiraApiContext JiraApiContext { get; set; }

        public List<string> GetJiraIssueKeysUsingJql(string jql)
        {
            List<string> jiraIssues = JiraApiContext.GetIssuesUsingJql(jql);
            List<string> keys = new();

            foreach(string issue in jiraIssues)
            {
                dynamic decodedIssue = Json.Decode(issue);
                var issues = decodedIssue.issues;

                foreach(var i in issues)
                {
                    keys.Add(i.key);
                }
            }

            return keys;
        }

        public List<JiraIssue> GetJiraIssuesFromLinks(Links links)
        {
            List<JiraIssue> jiraIssues = new();

            foreach(LinkedIssue linkedIssue in links.issues)
            {
                string issue = JiraApiContext.GetIssue(linkedIssue.issueId.ToString());
                dynamic decodedIssue = Json.Decode(issue);
                JiraIssue serializedIssue = JsonConvert.DeserializeObject<JiraIssue>(decodedIssue);
                jiraIssues.Add(serializedIssue);
            }

            return jiraIssues;
        }
    }
}
