namespace CoreWiki.Web
{
    using System;
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Net.Http.Headers;
    using NodaTime;
    using Snickler.RSSCore.Extensions;
    using Snickler.RSSCore.Models;
    using Utilities.RssFeed;
    using Westwind.AspNetCore.Markdown;
    using SameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<CoreWikiContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddResponseCompression(options => options.EnableForHttps = true);
            services.AddMarkdown();
            services.AddRSSFeed<RssProvider>();

            services.AddSingleton<IClock>(SystemClock.Instance);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
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
            app.UseResponseCompression();

            app.UseMarkdown();

            app.UseRSSFeed("/feed", new RSSFeedOptions
            {
                Title = "CoreWiki RSS Feed",
                Copyright = DateTime.UtcNow.Year.ToString(),
                Description = "RSS Feed for CoreWiki",
                Url = new Uri(this.Configuration["Url"], UriKind.Absolute)
            });

            app.UseMvc();
        }
    }
}
