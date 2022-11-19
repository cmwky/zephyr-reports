using Newtonsoft.Json;
using System.Configuration;
using System.Text;

namespace ZephyrScaleTraceabilityMatrixReport.Contexts
{
    internal class JiraApiContext : BaseContext
    {
        internal readonly string? baseUrl;
        internal readonly string? userEmail;
        internal readonly string? apiToken;
        internal readonly string? encodedBasicAuth;

        public JiraApiContext()
        {
            this.baseUrl = ConfigurationManager.AppSettings["JiraApiBaseUrl"];
            this.userEmail = ConfigurationManager.AppSettings["JiraUserEmail"];
            this.apiToken = ConfigurationManager.AppSettings["JiraApiToken"];
            this.encodedBasicAuth = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(this.userEmail + ":" + this.apiToken)); ;
        }

        public string GetIssue(string issueId)
        {
            string resource = $"{this.baseUrl}/issue/{issueId}";
            HttpMethod method = HttpMethod.Get; 

            base.GenerateHttpRequestMessage(resource, method);
            this.AddAuthToHttpRequestMessage();

            string json = base.SendHttpRequest();
            return json;
        }

        public List<string> SearchIssueUsingJqlFilter(string jql)
        {
            string resource = $"{this.baseUrl}/search";

            List<string> response = new List<string>();

            //need to follow up on this
            for (int i = 0; i < 10; i++)
            {
                HttpMethod method = HttpMethod.Post;
                Dictionary<string, string> requestBodies = new Dictionary<string, string>
                {
                    { "jql", jql },
                    { "maxResults", "100" },
                    { "fieldsByKeys", "false"},
                    { "startAt", $"{i*100}" }
                };

                string jsonBody = JsonConvert.SerializeObject(requestBodies);
                StringContent requestbody = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                base.GenerateHttpRequestMessage(resource, method, requestbody);
                this.AddAuthToHttpRequestMessage();

                string json = base.SendHttpRequest();
                response.Add(json);
            }

            return response;
        }

        public override void AddAuthToHttpRequestMessage()
        {
            base.request?.Headers.Add("Authorization", "Basic " + this.encodedBasicAuth);
            base.request?.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        }
    }
}
