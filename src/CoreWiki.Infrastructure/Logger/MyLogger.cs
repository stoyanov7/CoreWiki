namespace CoreWiki.Infrastructure.Logger
{
    using System;
    using Domain.Logger;
    using Microsoft.Extensions.Logging;

    public class MyLogger<TModel> : IMyLogger<TModel>
    {
        private readonly ILogger<TModel> logger;

        public MyLogger(ILogger<TModel> logger)
        {
            this.logger = logger;
        }

        public void Debug(string message) => this.logger.LogDebug(message);

        public void Info(string message) => this.logger.LogInformation(message);

        public void Warning(string message) => this.logger.LogWarning(message);

        public void Error(string message) => this.logger.LogError(message);

        public void Error(string message, Exception exception)
            => this.logger.LogError(message, exception);

        public void Fatal(string message) => this.logger.LogCritical(message);

        public void Fatal(string message, Exception exception)
            => this.logger.LogCritical(message, exception);
    }
}