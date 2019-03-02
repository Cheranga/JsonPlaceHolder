using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JsonPlaceHolder.DTO.Assets;
using JsonPlaceHolder.DTO.Responses;
using JsonPlaceHolder.Services.Configs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace JsonPlaceHolder.Services
{
    public class PostsService : IPostsService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PostsService> _logger;

        public PostsService(HttpClient httpClient, PostServiceConfig config, ILogger<PostsService> logger)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(config.Url);

            _logger = logger;
        }

        public async Task<GetAllPostsResponse> GetAllPostsAsync()
        {
            _logger.LogInformation("Getting all posts");

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, "/posts");
            var httpResponse = await _httpClient.SendAsync(httpRequest).ConfigureAwait(false);
            if (!httpResponse.IsSuccessStatusCode)
            {
                _logger.LogError($"Cannot get posts from the service : {httpResponse.ReasonPhrase}");
                throw new Exception(httpResponse.ReasonPhrase);
            }

            _logger.LogInformation("Posts received.");
            var posts = JsonConvert.DeserializeObject<List<Post>>(await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false));
            return new GetAllPostsResponse { Posts = posts };
        }
    }
}