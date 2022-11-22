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
            List<string> jiraIssues = JiraApiContext.GetIssuesUsingJql(jql);
            List<string> keys = new();

            foreach(string issue in jiraIssues)
            {
                dynamic decodedIssue = JObject.Parse(issue);
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
                string parsedIssue = JObject.Parse(issue).ToString();
                JiraIssue serializedIssue = JsonConvert.DeserializeObject<JiraIssue>(parsedIssue);
                jiraIssues.Add(serializedIssue);
            }

            return jiraIssues;
        }
    }
}
