namespace Markis.Areas.Admin.Models.AdminPost
{
    public class AdminPostFetchVM
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string UserName { get; set; }

        public int UserId { get; set; }
    }
}
