using System.ComponentModel.DataAnnotations;

namespace Markis.Application.DTOs
{
    public class PostDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string UserName { get; set; }

        public int UserId { get; set; }

        public string IdentityUserId { get; set; } 

        public List<CommentDto> Comments { get; set; } = new List<CommentDto>();

        public List<PostTagDto> PostTags { get; set; } = new List<PostTagDto>();
    }
}
