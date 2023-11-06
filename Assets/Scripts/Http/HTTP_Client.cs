using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;

namespace TarkovServerU19.Http
{
    internal class HTTP_Client
    {
        public static string GET(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(url).GetAwaiter().GetResult();
                if (response.StatusCode != HttpStatusCode.OK)
                    return null;
                var ret = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return ret;
            }
        }

        public static string POSTString(string url, string topost)
        {
            using (var httpClient = new HttpClient())
            {
                var stringContent = new StringContent(topost);
                var response = httpClient.PostAsync(url, stringContent).GetAwaiter().GetResult();
                if (response.StatusCode != HttpStatusCode.OK)
                    return null;
                var ret = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return ret;
            }
        }

        public static string POSTAsJson(string url, object topost)
        {
            using (var httpClient = new HttpClient())
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(topost), Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(url, stringContent).GetAwaiter().GetResult();
                if (response.StatusCode != HttpStatusCode.OK)
                    return null;
                var ret = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return ret;
            }
        }

        public static void POSTAsJsonNoRSP(string url, object topost)
        {
            using (var httpClient = new HttpClient())
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(topost), Encoding.UTF8, "application/json");
                httpClient.PostAsync(url, stringContent).GetAwaiter().GetResult();
            }
        }
    }
}
