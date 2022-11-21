﻿
namespace ZephyrScaleTraceabilityMatrixReport.Contexts
{
    internal class BaseContext
    {
        public HttpClient httpClient { get; }
        public HttpRequestMessage? request { get; set; }

        public BaseContext()
        {
            this.httpClient = new HttpClient();
        }

        public void GenerateHttpRequestMessage(string resource, HttpMethod method, StringContent? body = null)
        {
            this.request = new HttpRequestMessage(method, new Uri(resource));
            
            if(body is not null)
            {
                this.request.Content = body;
            }

            this.request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

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

        //to be overridden within each api context
        public virtual void AddAuthToHttpRequestMessage() { }
    }
}
