namespace CoreWiki.Repository.Contracts
{
    using System.Collections.Generic;
    using Models.Identity;

    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> Get();
    }
}