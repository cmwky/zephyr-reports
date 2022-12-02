using Newtonsoft.Json;
using System.Configuration;

namespace ZephyrScaleTraceabilityMatrixReport.Models.Jira
{
    internal class JiraIssue
    {
        internal string Id { get; set; }
        internal string Key { get; set; }
        internal string Url { get; set; }
        internal JiraIssueFields Fields { get; set; } = new();

        [JsonConstructor]
        public JiraIssue(string id, string key)
        {
            Id = id;
            Key = key;
            Url = $"{ConfigurationManager.AppSettings["JiraBaseUrl"]}/browse/{Key}";
        }
    }
}
