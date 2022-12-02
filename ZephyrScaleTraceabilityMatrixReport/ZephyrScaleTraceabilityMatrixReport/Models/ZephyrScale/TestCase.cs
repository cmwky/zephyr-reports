using ZephyrScaleTraceabilityMatrixReport.Models.Jira;

namespace ZephyrScaleTraceabilityMatrixReport.Models.ZephyrScale
{
    internal class TestCase
    {
        public int Id;
        public string Key;
        public Links Links;
        public string Name;
        public List<JiraIssue> JiraIssues;

        public TestCase()
        {
            JiraIssues = new List<JiraIssue>();
        }
    }
}
