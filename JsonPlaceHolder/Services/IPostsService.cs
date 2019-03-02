using System.Threading.Tasks;
using JsonPlaceHolder.DTO.Responses;

namespace JsonPlaceHolder.Services
{
    public interface IPostsService
    {
        Task<GetAllPostsResponse> GetAllPostsAsync();
    }
}