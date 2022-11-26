
namespace ZephyrScaleTraceabilityMatrixReport.Contexts
{
    internal class BaseContext
    {
        public HttpClient httpClient { get; }
        public HttpRequestMessage? request { get; set; }

        public BaseContext()
        {
            this.httpClient = new();
        }

        public void GenerateHttpRequestMessage(string resource, HttpMethod method, StringContent? body = null)
        {
            this.request = new HttpRequestMessage(method, new Uri(resource));
            
            if(body is not null)
            {
                this.request.Content = body;
            }

            this.request.Headers.Accept.Add(new("application/json"));

            return;
        }
         
        public string SendHttpRequest()
        {
            string json;

            using (HttpResponseMessage response = this.httpClient.SendAsync(this.request).GetAwaiter().GetResult())
            {
                using (HttpContent content = response.Content)
                {
                    json = content.ReadAsStringAsync().GetAwaiter().GetResult();
                }
            }

            return json;
        }

        public virtual void AddAuthToHttpRequestMessage() { }
    }
}
