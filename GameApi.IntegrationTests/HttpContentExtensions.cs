using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using System.IO;

namespace GameApi.IntegrationTests
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsAsync<T>(this HttpContent content)
        {
            return await JsonSerializer.DeserializeAsync<T>(await content.ReadAsStreamAsync());
        }
           
    }
}