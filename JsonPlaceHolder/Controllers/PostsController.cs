using System;
using System.Net;
using System.Threading.Tasks;
using JsonPlaceHolder.DTO.Requests;
using JsonPlaceHolder.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JsonPlaceHolder.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly ILogger<PostsController> _logger;
        private readonly IPostsService _postsService;

        public PostsController(IPostsService postsService, ILogger<PostsController> logger)
        {
            _postsService = postsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _postsService.GetAllPostsAsync().ConfigureAwait(false);
                return Ok(response);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Cannot retrieve the posts");
            }

            return StatusCode((int) HttpStatusCode.InternalServerError);
        }

        [HttpGet("{PostId}")]
        public async Task<IActionResult> Get([FromRoute] GetPostRequest request)
        {
            //
            // TODO: Validate the request
            //
            try
            {
                var response = await _postsService.GetPostAsync(request).ConfigureAwait(false);
                return Ok(response);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Cannot retrieve the post");
            }

            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}