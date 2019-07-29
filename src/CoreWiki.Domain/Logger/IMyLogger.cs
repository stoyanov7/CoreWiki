namespace CoreWiki.Domain.Logger
{
    using System;

    public interface IMyLogger<TModel>
    {
        /// <summary>
        /// Log a message object with the Debug level.
        /// </summary>
        /// <param name="message">The message.</param>
        void Debug(string message);

        /// <summary>
        /// Logs a message object with the Info level.
        /// </summary>
        /// <param name="message">The message.</param>
        void Info(string message);

        /// <summary>
        /// Logs a message object with the Warning level.
        /// </summary>
        /// <param name="message">The message.</param>
        void Warning(string message);

        /// <summary>
        /// Logs a message object with the Error level.
        /// </summary>
        /// <param name="message">The message.</param>
        void Error(string message);

        /// <summary>
        /// Log a message object with the Error level including the
        /// stack trace of the <seealso cref="Exception"/> passed as a parameter.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Error(string message, Exception exception);

        /// <summary>
        /// Log a message object with the Fatal level.
        /// </summary>
        /// <param name="message">The message.</param>
        void Fatal(string message);

        /// <summary>
        /// Log a message object with the Fatal level including the
        /// stack trace of the <seealso cref="Exception"/> passed as a parameter.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Fatal(string message, Exception exception);
    }
}