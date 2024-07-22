namespace Markis.Application.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Text { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int PostId { get; set; }

        public string IdentityUserId { get; set; }
    }

}
