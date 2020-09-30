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
    public class GameControllerTest: GameIntegrationTest
    {
        public GameControllerTest(GameWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Insert()
        {
            // Arrange
            var route = "/api/game/";
            await AuthenticateAsync();

            // Act
            var response = await _httpclient.PostAsync(route, CreateStringContent(new 
            {
               title =  "Dockey kong", 
               category = "Aventura"
            }));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedInsert = await response.Content.ReadAsAsync<ReturnApi>();
            returnedInsert.sucess.Should().Be(true);
        }

        [Fact]
        public async Task Update()
        {
            // Arrange
            var route = $"/api/game/";
            await AuthenticateAsync();

            // Act
            var response = await _httpclient.PutAsync(route, CreateStringContent(new 
            {
               id = 1,
               title =  "Dockey kong", 
               category = "Aventura"
            }));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedUpdate = await response.Content.ReadAsAsync<ReturnApi>();
            returnedUpdate.sucess.Should().Be(true);
        }

        [Fact]
        public async Task Delete()
        {
            // Arrange
            var idGame = 1;
            var route = $"/api/game/{idGame}";
            await AuthenticateAsync();

            // Act
            var response = await _httpclient.DeleteAsync(route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedUpdate = await response.Content.ReadAsAsync<ReturnApi>();
            returnedUpdate.sucess.Should().Be(true);
        }


        [Fact]
        public async Task Get()
        {
            // Arrange
            var idGame = 1;
            var route = $"/api/game/{idGame}";
            await AuthenticateAsync();

            // Act
            var response = await _httpclient.GetAsync(route);

            // Assert
            response.StatusCode.Should().Match<HttpStatusCode>(p=> p==HttpStatusCode.OK || p == HttpStatusCode.NotFound); 
        }


        [Fact]
        public async Task GetAll()
        {
            // Arrange
            var route = "/api/game/all";
            await AuthenticateAsync();

            // Act
            var response = await _httpclient.GetAsync(route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

    }
}