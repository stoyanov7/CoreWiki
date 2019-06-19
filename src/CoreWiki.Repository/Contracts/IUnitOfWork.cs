namespace CoreWiki.Repository.Contracts
{
    using System;
    using Microsoft.EntityFrameworkCore;

    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Getter for context.
        /// </summary>
        DbContext Context { get; }
    }
}