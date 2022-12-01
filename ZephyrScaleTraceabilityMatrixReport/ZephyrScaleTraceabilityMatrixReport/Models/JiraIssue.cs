using Newtonsoft.Json;
using System.Configuration;

namespace ZephyrScaleTraceabilityMatrixReport.Models
{
    internal class JiraIssue
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string Url { get; set; }
        public JiraIssueFields Fields { get; set; } = new();

        [JsonConstructor]
        public JiraIssue(string id, string key)
        {
            this.Id = id;
            this.Key = key;
            this.Url = $"{ConfigurationManager.AppSettings["JiraBaseUrl"]}/browse/{this.Key}";
        }
    }
}
