namespace CoreWiki.Infrastructure.Repository
{
    using System;
    using Domain.Repository;
    using Microsoft.EntityFrameworkCore;

    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;

        public UnitOfWork(DbContext context) => this.Context = context;

        public DbContext Context { get; }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                // Free any other managed objects here.
                this.Context.Dispose();
            }

            // Free any unmanaged objects here.
            this.disposed = true;
        }

        /// <inheritdoc />
        /// <summary>
        /// Releases the allocated resources for this context. 
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}