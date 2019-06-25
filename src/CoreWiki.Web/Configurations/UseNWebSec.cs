namespace CoreWiki.Web.Configurations
{
    using Microsoft.AspNetCore.Builder;

    public static partial class ConfigurationExtensions
    {
        public static IApplicationBuilder UseNWebSec(this IApplicationBuilder app)
        {
            // HTTP Strict Transport Security Header
            app.UseHsts(options => options.MaxAge(days: 365).IncludeSubdomains());

            // This response header prevents pages from loading in modern browsers
            // when reflected cross-site scription is detected.
            // This is often unnecessary if a site implements a strong Content-Security-Policy
            app.UseXXssProtection(options => options.EnabledWithBlockMode());

            // Ensure that site content is not being embedded in an iframe on other sites
            // - used to avoid clickjacking attacks.
            app.UseXfo(options => options.SameOrigin());

            // Blocks any content sniffing that could happen that might change an innocent MIME type
            // (e.g. text/css) into something executable that could do some real damage.
            app.UseXContentTypeOptions();

            app.UseReferrerPolicy(opts => opts.NoReferrer());

            return app;
        }
    }
}