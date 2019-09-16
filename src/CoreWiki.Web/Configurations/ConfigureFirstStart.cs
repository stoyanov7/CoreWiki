namespace CoreWiki.Web.Configurations
{
    using System.IO;
    using System.Threading.Tasks;
    using Areas.FirstStart;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class ConfigurationExtensions
    {
        private static bool firstStartIncomplete = true;
        private static string appConfigurationFilename;

        private static IConfiguration _configuration;

        public static IServiceCollection AddFirstStartConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            _configuration = configuration;
            services.Configure<UserAppConfig>(_configuration);

            return services;
        }

        public static IApplicationBuilder UseFirstStartConfiguration(this IApplicationBuilder app, IHostingEnvironment hostingEnvironment)
        {
            appConfigurationFilename = Path.Combine(hostingEnvironment.ContentRootPath, "appsettings.json");

            app.UseWhen(IsFirstStartIncomplete, thisApp =>
            {
                thisApp.MapWhen(
                    context => !context.Request.Path.StartsWithSegments("/FirstStart"),
                    mapApp => mapApp.Run(
                        request =>
                        {
                            request.Response.Redirect("/FirstStart");
                            return Task.CompletedTask;
                        }));

                thisApp.UseMvc();
            });

            return app;
        }

        private static bool IsFirstStartIncomplete(HttpContext context)
        {
            if (firstStartIncomplete && string.IsNullOrEmpty(_configuration["DatabaseProvider"]))
            {
                return firstStartIncomplete;
            }

            firstStartIncomplete = false;
            return false;
        }
    }
}