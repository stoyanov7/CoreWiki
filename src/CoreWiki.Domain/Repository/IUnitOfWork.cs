namespace CoreWiki.Domain.Repository
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