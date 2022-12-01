using Newtonsoft.Json;
using System.Configuration;
using System.Text;

namespace ZephyrScaleTraceabilityMatrixReport.Contexts
{
    internal class JiraApiContext : BaseContext
    {
        private static readonly string baseUrl = ConfigurationManager.AppSettings["JiraApiBaseUrl"];
        private static readonly string userEmail = ConfigurationManager.AppSettings["JiraUserEmail"];
        private static readonly string apiToken = ConfigurationManager.AppSettings["JiraApiToken"];
        private static readonly string encodedBasicAuth = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(userEmail + ":" + apiToken));

        public string GetIssue(string issueId)
        {
            string resource = $"{baseUrl}/issue/{issueId}";
            HttpMethod method = HttpMethod.Get; 

            base.GenerateHttpRequestMessage(resource, method);
            this.AddAuthToHttpRequestMessage();

            string json = base.SendHttpRequest();
            return json;
        }

        public string GetIssuesUsingJql(string jql)
        {
            string resource = $"{baseUrl}/search";

            HttpMethod method = HttpMethod.Post;
            Dictionary<string, string> requestBodies = new()
            {
                { "jql", jql },
                { "maxResults", "100" },
                { "fieldsByKeys", "false"},
                { "startAt", "0" }
            };

            string jsonBody = JsonConvert.SerializeObject(requestBodies);
            StringContent requestbody = new(jsonBody, Encoding.UTF8, "application/json");

            base.GenerateHttpRequestMessage(resource, method, requestbody);
            this.AddAuthToHttpRequestMessage();

            string json = base.SendHttpRequest();

            return json;
        }

        public override void AddAuthToHttpRequestMessage()
        {
            base.Request.Headers.Add("Authorization", "Basic " + encodedBasicAuth);
        }
    }
}
