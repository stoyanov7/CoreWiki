namespace CoreWiki.Web.Configurations
{
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

    public static partial class ConfigurationExtensions
    {
        private static bool firstStartIncomplete = true;
        private static string appConfigurationFilename;

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
            if (firstStartIncomplete && !File.Exists(appConfigurationFilename))
            {
                return firstStartIncomplete;
            }

            firstStartIncomplete = false;
            return false;
        }
    }
}