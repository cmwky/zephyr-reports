
namespace ZephyrScaleTraceabilityMatrixReport.Contexts
{
    internal class BaseContext
    {
        protected HttpClient HttpClient { get; } = new ();
        protected HttpRequestMessage Request = new();

        public void GenerateHttpRequestMessage(string resource, HttpMethod method, StringContent? body = null)
        {
            this.Request = new HttpRequestMessage(method, new Uri(resource));
            
            if(body is not null)
            {
                this.Request.Content = body;
            }

            this.Request.Headers.Accept.Add(new("application/json"));

            return;
        }
         
        public string SendHttpRequest()
        {
            string json;

            using (HttpResponseMessage response = HttpClient.SendAsync(Request).GetAwaiter().GetResult())
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
