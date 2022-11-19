using Newtonsoft.Json;
using System.Web.Helpers;
using ZephyrScaleTraceabilityMatrixReport.Contexts;
using ZephyrScaleTraceabilityMatrixReport.Models;

namespace ZephyrScaleTraceabilityMatrixReport.Controllers
{
    internal class ZephyrScaleController
    {
        private ZephyrScaleApiContext zephyrScaleContext = new ZephyrScaleApiContext();

        public List<TestCase> GetTestCases(string projectKey)
        {
            string testCases = zephyrScaleContext.GetTestCases(projectKey);

            dynamic decodedTestCases = Json.Decode(testCases);

            var testCasesValues = decodedTestCases.values;

            List<TestCase> testCaseCollection = new List<TestCase>();

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
