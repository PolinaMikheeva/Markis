namespace Markis.Application.DTOs
{
    public class AddProductDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ItemFeatures { get; set; }

        public string FileIncluded { get; set; }

        public string Version { get; set; }

        public string Framework { get; set; }

        public string IdentityUserId { get; set; }

        public int UserId { get; set; }
    }
}
