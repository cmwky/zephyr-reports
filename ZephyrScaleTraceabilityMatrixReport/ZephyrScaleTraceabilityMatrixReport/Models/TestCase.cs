
namespace ZephyrScaleTraceabilityMatrixReport.Models
{
    internal class TestCase
    {
        public int id { get; set; }
        public string key { get; set; }
        public Links links { get; set; }
        public string name { get; set; }
        public string objective { get; set; }
        public List<JiraIssue> jiraIssues { get; set; }
        public List<TestCaseExecutionStatus> executionStatuses { get; set; }

        public TestCase()
        {
            this.jiraIssues = new List<JiraIssue>();
        }
    }
}
