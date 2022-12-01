using System.Configuration;

namespace ZephyrScaleTraceabilityMatrixReport.Contexts
{
    internal class ZephyrScaleApiContext : BaseContext
    {
        private readonly string baseUrl = ConfigurationManager.AppSettings["ZephyrScaleApiBaseUrl"];
        private readonly string apiKey = ConfigurationManager.AppSettings["ZephyrScaleApiKey"];

        //public string GetTestCase(string testCaseKey)
        //{
        //    string resource = $"{this.baseUrl}/testcases/{testCaseKey}";
        //    HttpMethod method = HttpMethod.Get;

        //    base.GenerateHttpRequestMessage(resource, method);
        //    this.AddAuthToHttpRequestMessage();

        //    string json = base.SendHttpRequest();
        //    return json;
        //}

        public string GetTestCases(string projectKey, string maxResults = "100", string startAt = "0")
        {
            string resource = $"{this.baseUrl}/testcases?projectKey={projectKey}&maxResults={maxResults}&startAt={startAt}";
            HttpMethod method = HttpMethod.Get;

            base.GenerateHttpRequestMessage(resource, method);
            this.AddAuthToHttpRequestMessage();

            string json = base.SendHttpRequest();
            return json;
        }

        public override void AddAuthToHttpRequestMessage()
        {
            base.Request.Headers.Add("Authorization", "Bearer " + this.apiKey);
        }
    }
}
