namespace CoreWiki.Web.Configurations
{
    using Application.Commands;
    using MediatR;
    using MediatR.Pipeline;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class ConfigureExtensions
    {
        public static IServiceCollection AddMediatorConfiguration(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));

            services.AddMediatR(typeof(CreateNewArticleCommandHandler));

            return services;
        }
    }
}