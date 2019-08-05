namespace CoreWiki.Web.Configurations
{
    using Common;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;

    public static partial class ConfigurationExtensions
    {
        public static IApplicationBuilder UseExceptionsConfiguration(this IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.SeedDatabase();

                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            return app;
        }
    }
}