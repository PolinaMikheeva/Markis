using Microsoft.AspNetCore.Identity;

namespace Markis.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        public int? UserId { get; set; }
        public string UserName { get; set; }

        public UserProfile User { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
