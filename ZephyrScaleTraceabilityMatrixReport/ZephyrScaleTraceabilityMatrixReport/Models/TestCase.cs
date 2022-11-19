
namespace ZephyrScaleTraceabilityMatrixReport.Models
{
    internal class TestCase
    {
        public int id;
        public string key;
        public Links links;
        public string name;
        public string objective;
        public List<JiraIssue> jiraIssues;
    }
}
