using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace GeekShopping.Web.Utils
{
    public static class HttpClientExtensions
    {
        private static MediaTypeHeaderValue applicationJson = new MediaTypeHeaderValue("application/json");
        public static async Task<T?> ReadContenteAs<T>(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var dataAsAsString = await response
                .Content
                .ReadAsStringAsync()
                .ConfigureAwait(false);
                return JsonSerializer.Deserialize<T>(dataAsAsString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            }
            throw new ApplicationException($"Error getting {typeof(T).Name}  msg: {response.ReasonPhrase}");
        }

        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, string url, T data)
        {
            var dataAsJson = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsJson);
            content.Headers.ContentType = applicationJson;
            return client.PostAsync(url, content);
        }
    public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, string url, T data)
        {
            var dataAsJson = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsJson);
            content.Headers.ContentType = applicationJson;
            return client.PutAsync(url, content);
        }
        public static Task<HttpResponseMessage> DeleteAsync(this HttpClient client, string url)
        {
            return client.DeleteAsync(url);
        }
        public static Task<HttpResponseMessage> PatchAsJsonAsync<T>(this HttpClient client, string url, T data)
        {
            var dataAsJson = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsJson);
            content.Headers.ContentType = applicationJson;
            return client.PatchAsync(url, content);
        }
    }
}