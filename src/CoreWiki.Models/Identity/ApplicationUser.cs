namespace CoreWiki.Models.Identity
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public bool CanNotify { get; set; }

        public ICollection<Article> Articles { get; set; } = new HashSet<Article>();
    }
}