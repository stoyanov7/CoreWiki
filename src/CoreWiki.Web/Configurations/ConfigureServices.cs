namespace CoreWiki.Web.Configurations
{
    using Data;
    using Domain.Repository;
    using Domain.Services;
    using Infrastructure.Repository;
    using Infrastructure.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NodaTime;

    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<DbContext, CoreWikiContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IArticleRepository, ArticleRepository>();

            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IArticleSearchService, ArticleSearchService>();
            services.AddTransient<ICommentService, CommentService>();

            services.AddSingleton<IClock>(SystemClock.Instance);
            services.AddSingleton<IEmailSender, EmailNotifier>();

            return services;
        }
    }
}