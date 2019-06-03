using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(CoreWiki.Web.Areas.Identity.IdentityHostingStartup))]
namespace CoreWiki.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}