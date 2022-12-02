using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZephyrScaleTraceabilityMatrixReport.Contexts;
using ZephyrScaleTraceabilityMatrixReport.Models.ZephyrScale;

namespace ZephyrScaleTraceabilityMatrixReport.Controllers
{
    internal class ZephyrScaleController
    {
        private readonly ZephyrScaleApiContext zephyrScaleContext = new();

        public List<TestCase> GetTestCases(string projectKey)
        {
            string testCases = zephyrScaleContext.GetTestCases(projectKey);
            dynamic decodedTestCases = JObject.Parse(testCases);

            var testCasesValues = decodedTestCases.values;

            List<TestCase> testCaseCollection = new();

            foreach(var testCase in testCasesValues)
            {
                string serializedTestCase = JsonConvert.SerializeObject(testCase);
                TestCase testCaseObj = JsonConvert.DeserializeObject<TestCase>(serializedTestCase);  
                testCaseCollection.Add(testCaseObj);
            }

            return testCaseCollection;
        }

        public void PopulateJiraIssuesFromLinks(ref List<TestCase> testCases)
        {
            JiraController jiraController = new();

            for (int i = 0; i < testCases.Count; i++)
            {
                if (testCases[i].Links.issues.Count > 0)
                {
                    testCases[i].JiraIssues = jiraController.GetJiraIssuesFromLinks(testCases[i].Links);
                }
            }

            return;
        }

        public List<TestCaseExecution> GetTestCasesExecutions(string projectKey) 
        {
            
        }
    }
}
