namespace CoreWiki.Web.Configurations
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;

    public static partial class ConfigurationExtensions
    {
        public static IApplicationBuilder UseFirstStart(this IApplicationBuilder app)
        {
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
            return true;
        }
    }
}