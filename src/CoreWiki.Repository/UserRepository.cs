namespace CoreWiki.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Models.Identity;

    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        /// <summary>
        /// Get entity as enumerable.
        /// </summary>
        /// <returns>Enumerable entity.</returns>
        public IEnumerable<ApplicationUser> Get()
            => this.UnitOfWork
                .Context
                .Set<ApplicationUser>()
                .AsEnumerable();
    }
}