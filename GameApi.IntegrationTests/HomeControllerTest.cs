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
    public class HomeControllerTest: GameIntegrationTest
    {
        public HomeControllerTest(GameWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

                [Fact]
        public async Task Home()
        {
            // Arrange


            // Act
            var response = await _httpclient.GetAsync("/");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        
    }
}