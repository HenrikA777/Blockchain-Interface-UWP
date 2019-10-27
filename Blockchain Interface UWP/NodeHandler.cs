using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.UI.Xaml.Documents;
using Newtonsoft.Json;

namespace Blockchain_Interface_UWP
{
    class NodeHandler
    {
        private static HttpClient _httpClient = new HttpClient();
        
        public HttpClient Client
        {
            get
            {
                if (_httpClient == null) _httpClient = new HttpClient();
                return _httpClient;
            }
        }

        public async Task<Chain> GetChain(string uri)
        {
            uri = uri + "/chain";
            try
            {
                HttpResponseMessage response = Client.GetAsync(uri).Result;
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
                Chain chain = JsonConvert.DeserializeObject<Chain>(responseBody);
                return chain;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                throw e;
            }
        }
        public async Task<Nodes> GetNodes(string uri)
        {
            uri = uri + "/nodes";
            try
            {
                HttpResponseMessage response = Client.GetAsync(uri).Result;
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
                Nodes nodes = JsonConvert.DeserializeObject<Nodes>(responseBody);
                return nodes;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                throw e;
            }
        }

        public void PostMessage(string uri, Message message)
        {
            uri = uri + "/transactions/new";
            var jsonString = JsonConvert.SerializeObject(message);
            Console.WriteLine("json string: " + jsonString);

            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            Console.WriteLine("content: : " + content.ToString());

            HttpResponseMessage response = Client.PostAsync(uri, content).Result;
            Console.WriteLine(response.StatusCode);
        }
    }
}
