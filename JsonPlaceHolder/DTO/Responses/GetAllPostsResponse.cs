using System.Collections.Generic;
using System.Linq;
using JsonPlaceHolder.DTO.Assets;

namespace JsonPlaceHolder.DTO.Responses
{
    public class GetAllPostsResponse
    {
        public GetAllPostsResponse()
        {
            Posts = new List<Post>();
        }

        public List<Post> Posts { get; set; }

        public bool IsValid()
        {
            return Posts != null && Posts.Any();
        }
    }
}