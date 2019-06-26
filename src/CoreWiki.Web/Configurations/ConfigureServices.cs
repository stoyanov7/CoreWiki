namespace CoreWiki.Web.Configurations
{
    using Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NodaTime;
    using Repository;
    using Repository.Contracts;
    using Services;
    using Services.Contracts;

    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<DbContext, CoreWikiContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IArticleRepository, ArticleRepository>();

            services.AddTransient<IArticleSearchService, ArticleSearchService>();

            services.AddSingleton<IClock>(SystemClock.Instance);
            services.AddSingleton<IEmailSender, EmailNotifier>();

            return services;
        }
    }
}