namespace CoreWiki.Web.Test
{
    using System;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public static class CoreWikiContextMock
    {
        public static CoreWikiContext GetContext()
        {
            var options = new DbContextOptionsBuilder<CoreWikiContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new CoreWikiContext(options);
        }
    }
}