using System.Threading.Tasks;
using JsonPlaceHolder.DTO.Requests;
using JsonPlaceHolder.DTO.Responses;

namespace JsonPlaceHolder.Services
{
    public interface IPostsService
    {
        Task<GetAllPostsResponse> GetAllPostsAsync();
        Task<GetPostResponse> GetPostAsync(GetPostRequest request);
    }
}