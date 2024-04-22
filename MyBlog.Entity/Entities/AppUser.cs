using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entity.Entities
{
    public class AppUser :IdentityUser<Guid>
    {
        public  string FirstName { get; set; }
        public  string LastName { get; set; }
        public Guid ImageId { get; set; } = Guid.Parse("9F6C6C6B-F940-4C39-99D6-873DC98AF56A");
        public Image? Image { get; set; }
        public ICollection<Article> Articles { get; set; }
        public string? LinkedInLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? ContactMail { get; set; }
        public string? GithubLink { get; set; }

    }
}
