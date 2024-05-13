namespace Markis.Domain.Entities
{
    public class Product
    {
        public Product()
        {
            ItemFeatures = new List<string>();
            FileIncluded = new List<string>();
            Browsers = new List<string>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public decimal Price { get; set; }

        public List<string> ItemFeatures { get; set; }

        public List<string> FileIncluded { get; set; }

        public string Version { get; set; }

        public string Framework { get; set; }

        public List<string> Browsers { get; set; }

        public int UserId { get; set; }
        public UserProfile User { get; set; }

        public List<ProductTag> ProductTags { get; set; }
    }
}
