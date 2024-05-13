namespace Markis.Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int UserId { get; set; }
        public UserProfile User { get; set; }

        public List<PostTag> PostTags { get; set; }
    }
}
