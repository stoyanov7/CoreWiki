namespace CoreWiki.Web
{
    using AutoMapper;
    using Configurations;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseConfiguration(this.Configuration);
            services.AddCookiePolicyConfiguration();
            services.AddAuthenticationConfiguration(this.Configuration);
            services.AddResponseCompressionConfiguration();

            services.AddMarkdownConfiguration();
            services.AddRssConfiguration();

            services.AddServicesConfiguration();
            services.AdddentityConfiguration();

            services.AddAutoMapper(cfg => cfg.ValidateInlineMaps = false);

            services.AddFirstStartConfiguration(this.Configuration);

            services.AddMediatorConfiguration();
            services.AddRoutingConfiguration();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseExceptionsConfiguration(env);
            
            app.UseSecurityConfiguration(env);
            app.UseResponseCompressionConfiguration();
            app.UseStaticFilesConfiguration();
            app.UseCookiePolicyConfiguration();

            app.UseFirstStartConfiguration(env);
            app.UseMarkdownConfiguration();
            app.UseRssConfiguration(this.Configuration);
            
            app.UseAuthenticationConfiguration();
            app.UseStatusCodePagesConfiguration();

            app.UseMvc();
        }
    }
}
