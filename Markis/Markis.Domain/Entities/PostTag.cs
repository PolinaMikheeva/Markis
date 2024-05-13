namespace Markis.Domain.Entities
{
    public class PostTag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Post> Posts { get; set; }
    }
}
