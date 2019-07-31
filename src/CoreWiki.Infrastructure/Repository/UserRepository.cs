namespace CoreWiki.Infrastructure.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Repository;
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