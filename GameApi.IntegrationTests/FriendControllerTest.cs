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
    public class FriendControllerTest: GameIntegrationTest
    {
        public FriendControllerTest(GameWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Insert()
        {
            // Arrange
            var route = "/api/friend/";
            await AuthenticateAsync();

            // Act
            var response = await _httpclient.PostAsync(route, CreateStringContent(new 
            {
               name =  "João da Silva", 
               category = "77988776655"
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
            var route = $"/api/friend/";
            await AuthenticateAsync();

            // Act
            var response = await _httpclient.PutAsync(route, CreateStringContent(new 
            {
               id = 1,
               name =  "João da Silva", 
               category = "77988776655"
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
            var idFriend = 1;
            var route = $"/api/friend/{idFriend}";
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
            var idFriend = 1;
            var route = $"/api/friend/{idFriend}";
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
            var route = "/api/friend/all";
            await AuthenticateAsync();

            // Act
            var response = await _httpclient.GetAsync(route);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

    }
}