namespace Markis.Areas.Admin.Models.AdminProduct
{
    public class AdminProductFetchVM
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

        public int UserId { get; set; }
    }
}
