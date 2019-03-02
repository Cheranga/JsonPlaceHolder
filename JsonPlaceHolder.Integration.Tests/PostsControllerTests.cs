using System.Net.Http;
using System.Threading.Tasks;
using JsonPlaceHolder.DTO.Responses;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace JsonPlaceHolder.Integration.Tests
{
    public class PostsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        public PostsControllerTests(WebApplicationFactory<Startup> factory)
        {
            _httpClient = factory.CreateClient();
        }

        private readonly HttpClient _httpClient;

        [Fact]
        public async Task There_Must_Be_Posts()
        {
            //
            // Arrange and act
            //
            var httpResponse = await _httpClient.GetAsync(@"/api/posts").ConfigureAwait(false);
            //
            // Assert
            //
            httpResponse.EnsureSuccessStatusCode();
            var response = JsonConvert.DeserializeObject<GetAllPostsResponse>(await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false));

            Assert.Equal(true, response?.IsValid());
        }
    }
}