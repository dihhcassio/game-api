using System.Threading.Tasks;
using Xunit;
using GameAPI;
using System.Net;
using FluentAssertions;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;


namespace GameApi.IntegrationTests
{
    public class GameIntegrationTest: IClassFixture<GameWebApplicationFactory<Startup>>
    {
        protected readonly HttpClient _httpclient;

        public GameIntegrationTest(GameWebApplicationFactory<Startup> factory)
        {
            _httpclient = factory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await _httpclient.PostAsync("/api/auth/", CreateStringContent(
                new 
                {
                    Email = "teste1@teste.com",
                    Password = "12345"
                })
            );

            var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return registrationResponse.token;
        }

        protected StringContent CreateStringContent(object json){
            return new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json");
        }
    }
    public class AuthSuccessResponse
    {
        public string token {get; set;}
    }

    public class ReturnApi
    {
        public bool sucess {get; set;}
    }

}