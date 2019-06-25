﻿namespace CoreWiki.Repository.Contracts
{
    using System.Threading.Tasks;

    public interface IRepository<T>
        where T : class
    {
        Task AddAsync(T entity);

        Task SaveChangesAsync();
    }
}