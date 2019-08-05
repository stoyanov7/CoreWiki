namespace CoreWiki.Web.Configurations
{
    using Microsoft.AspNetCore.Builder;

    public static partial class ConfigurationExtensions
    {
        public static IApplicationBuilder UseStatusCodePagesConfiguration(this IApplicationBuilder app)
        {
            app.UseStatusCodePages();
            app.UseStatusCodePagesWithReExecute("/{0}");

            return app;
        }
    }
}