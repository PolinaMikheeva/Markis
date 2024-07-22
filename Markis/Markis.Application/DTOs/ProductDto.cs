namespace Markis.Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public decimal Price { get; set; }

        public string ItemFeatures { get; set; }

        public string FileIncluded { get; set; }

        public string Version { get; set; }

        public string Framework { get; set; }

        public string IdentityUserId { get; set; }

        public int UserId { get; set; }
        public AuthorDto User { get; set; }
    }
}
