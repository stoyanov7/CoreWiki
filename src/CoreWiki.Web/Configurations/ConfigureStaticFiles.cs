namespace CoreWiki.Web.Configurations
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Net.Http.Headers;

    public static partial class ConfigurationExtensions
    {
        public static IApplicationBuilder UseStaticFilesConfiguration(this IApplicationBuilder app)
        {
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

            return app;
        }
    }
}