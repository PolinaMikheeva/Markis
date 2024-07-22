namespace Markis.Application.DTOs
{
    public class AddPostDto
    {
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string IdentityUserId { get; set; }

        public int UserId { get; set; }
    }
}
