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

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async Task Existing_Posts_Must_Be_Accessible_By_PostId(int postId)
        {
            //
            // Arrange and act
            //
            var httpResponse = await _httpClient.GetAsync($@"/api/posts/{postId}").ConfigureAwait(false);
            //
            // Assert
            //
            httpResponse.EnsureSuccessStatusCode();
            var response = JsonConvert.DeserializeObject<GetPostResponse>(await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false));

            Assert.NotNull(response);
            Assert.Equal(postId, response.Post.Id);
        }
    }
}