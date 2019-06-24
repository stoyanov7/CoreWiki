namespace CoreWiki.Web
{
    using System;
    using Configurations;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Net.Http.Headers;
    using NodaTime;
    using Services;
    using Snickler.RSSCore.Extensions;
    using Snickler.RSSCore.Models;
    using Utilities;
    using Utilities.RssFeed;
    using Westwind.AspNetCore.Markdown;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDatabase(this.Configuration);
            services.ConfigureCookiePolicy();

            services.AddRouting(options => options.LowercaseUrls = true);

            services.ConfigureAuthentication(this.Configuration);
            services.ConfigureResponseCompression();

            services.AddMarkdown();
            services.AddRSSFeed<RssProvider>();

            services.AddSingleton<IClock>(SystemClock.Instance);
            services.AddSingleton<IEmailSender, EmailNotifier>();

            services.ConfigureIdentity();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.SeedDatabase();

                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();
            app.UseNWebSec();
            app.UseResponseCompression();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    const int durationInSeconds = 60 * 60 * 24;
                    ctx.Context
                        .Response
                        .Headers[HeaderNames.CacheControl] = $"public,max-age={durationInSeconds}";
                }
            });
            app.UseCookiePolicy();

            app.UseMarkdown();

            app.UseRSSFeed("/feed", new RSSFeedOptions
            {
                Title = "CoreWiki RSS Feed",
                Copyright = DateTime.UtcNow.Year.ToString(),
                Description = "RSS Feed for CoreWiki",
                Url = new Uri(this.Configuration["Url"], UriKind.Absolute)
            });

            app.UseAuthentication();

            app.UseStatusCodePages();
            app.UseStatusCodePagesWithReExecute("/{0}");

            app.UseMvc();
        }
    }
}
