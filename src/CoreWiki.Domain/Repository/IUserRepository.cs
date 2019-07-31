namespace CoreWiki.Domain.Repository
{
    using System.Collections.Generic;
    using Models.Identity;

    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> Get();
    }
}