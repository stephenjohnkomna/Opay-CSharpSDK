using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Opay_CSharpSDK.lib.core.Infrastructure;
using RestSharp;

namespace Opay_CSharpSDK.lib.core.modules.Infrastructure
{
    public class ConnectionClient : IConnectionClient
    {
        private String baseUrl;
        private Dictionary<String, String> headers = new Dictionary<String, String>();

        public ConnectionClient(String baseUrl, Dictionary<String, String> headers)
        {
            this.baseUrl = baseUrl;
            this.headers = headers;
        }

        public Task<JObject> makePostRequestAsync(SortedDictionary<string, object> parameters, string endpoint)
        {
            try
            {
                var client = new RestClient(baseUrl);
                var request = new RestRequest(endpoint, Method.POST);
                request.AddHeaders(headers);
                request.RequestFormat = DataFormat.Json;
                
                request.AddJsonBody(Util.mapToJsonString(parameters));
               var response = client.ExecuteAsync(request);
                String result = response.Result.Content;
                JObject.Parse(result);
                return Task.FromResult<JObject>(JObject.Parse(result));
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<JObject> makePostRequestAsync(string endpoint)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(endpoint, Method.POST);
            request.AddHeaders(headers);
            request.RequestFormat = DataFormat.Json;
            var response = client.ExecuteAsync(request);
            String result = response.Result.Content;
            return Task.FromResult<JObject>(JObject.Parse(result));
        }

   
    }
}
