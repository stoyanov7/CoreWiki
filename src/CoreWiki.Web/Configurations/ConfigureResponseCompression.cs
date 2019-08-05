namespace CoreWiki.Web.Configurations
{
    using System.IO.Compression;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.ResponseCompression;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection AddResponseCompressionConfiguration(this IServiceCollection service)
        {
            service.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            service.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });

            return service;
        }

        public static IApplicationBuilder UseResponseCompressionConfiguration(this IApplicationBuilder app)
        {
            app.UseResponseCompression();

            return app;
        }
    }
}