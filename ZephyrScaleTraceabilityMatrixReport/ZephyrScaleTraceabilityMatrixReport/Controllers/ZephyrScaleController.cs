using Newtonsoft.Json;
using System.Web.Helpers;
using ZephyrScaleTraceabilityMatrixReport.Contexts;
using ZephyrScaleTraceabilityMatrixReport.Models;

namespace ZephyrScaleTraceabilityMatrixReport.Controllers
{
    internal class ZephyrScaleController
    {
        private ZephyrScaleApiContext zephyrScaleContext = new();

        public List<TestCase> GetTestCases(string projectKey)
        {
            string testCases = zephyrScaleContext.GetTestCases(projectKey);

            string serializedTestCases = JsonConvert.SerializeObject(testCases);
            dynamic decodedTestCases = Json.Decode(serializedTestCases);

            var testCasesValues = decodedTestCases.values;

            List<TestCase> testCaseCollection = new();

            foreach(TestCase testCase in testCasesValues)
            {
                string serializedTestCase = JsonConvert.SerializeObject(testCase);
                TestCase testCaseObj = JsonConvert.DeserializeObject<TestCase>(serializedTestCase);  
                testCaseCollection.Add(testCaseObj);
            }

            return testCaseCollection;
        }
    }
}
