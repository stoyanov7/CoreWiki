namespace CoreWiki.Web.Configurations
{
    using Data;
    using Domain.Logger;
    using Domain.Repository;
    using Domain.Services;
    using Infrastructure.Logger;
    using Infrastructure.Repository;
    using Infrastructure.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NodaTime;

    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection AddServicesConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpClient<IHaveIBeenPawnedClient, HaveIBeenPawnedClient>();

            services.AddScoped<DbContext, CoreWikiContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IArticleSearchService, ArticleSearchService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();

            services.AddSingleton<IClock>(SystemClock.Instance);
            services.AddSingleton<IEmailSender, EmailNotifier>();
            services.AddScoped(typeof(IMyLogger<>), typeof(MyLogger<>));

            return services;
        }
    }
}