namespace CoreWiki.Repository
{
    using System.Threading.Tasks;
    using Contracts;

    public class Repository<T> : IRepository<T>
        where T : class
    {
        public Repository(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// Add asynchronous entity to the database.
        /// </summary>
        /// <param name="entity">Entity for adding.</param>
        /// <returns></returns>
        public virtual async Task AddAsync(T entity)
        {
            await this.UnitOfWork
                .Context
                .Set<T>()
                .AddAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await this.UnitOfWork.Context.SaveChangesAsync();
        }
    }
}
