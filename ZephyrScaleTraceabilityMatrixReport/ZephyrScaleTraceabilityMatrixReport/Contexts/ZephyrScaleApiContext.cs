using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZephyrScaleTraceabilityMatrixReport.Contexts
{
    internal class ZephyrScaleApiContext : BaseContext
    {
        private readonly string baseUrl = "https://api.zephyrsacle.smartbear.com/v2";
        private readonly string? apiKey;

        public ZephyrScaleApiContext()
        {
            if (ConfigurationManager.AppSettings["ZephyrScaleApiKey"] != null)
            {
                this.apiKey = ConfigurationManager.AppSettings["ZephyrScaleApiKey"];
            }
            else 
            {
                throw new ConfigurationErrorsException("ZephyrScaleApiKey must be provided in App.config file. Aborting execution.");
            }
        }

        public string GetTestCase(string testCaseKey)
        {
            string resource = $"{this.baseUrl}/testcases/{testCaseKey}";
            HttpMethod method = HttpMethod.Get;

            base.GenerateHttpRequestMessage(resource, method);
            this.AddAuthToHttpRequestMessage();

            string json = base.SendHttpRequest();
            return json;
        }

        public string GetAllTestCases(string projectKey, string maxResults = "maxResults=200", string startAt = "startAt=0")
        {
            string resource = $"{this.baseUrl}/testcases?{projectKey}&{maxResults}&{startAt}";
            HttpMethod method = HttpMethod.Get;

            base.GenerateHttpRequestMessage(resource, method);
            this.AddAuthToHttpRequestMessage();

            string json = base.SendHttpRequest();
            return json;
        }

        public override void AddAuthToHttpRequestMessage()
        {
            base.request.Headers.Add("Authorization", "Bearer " + this.apiKey);
        }
    }
}
