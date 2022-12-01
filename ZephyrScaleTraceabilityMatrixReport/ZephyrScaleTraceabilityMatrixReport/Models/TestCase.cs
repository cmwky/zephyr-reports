
namespace ZephyrScaleTraceabilityMatrixReport.Models
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
            this.JiraIssues = new List<JiraIssue>();
        }
    }
}
