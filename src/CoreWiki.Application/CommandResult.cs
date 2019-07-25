namespace CoreWiki.Application
{
    using System;

    public class CommandResult
    {
        public bool Successful { get; set; }

        public Exception Exception { get; set; }

        public dynamic ObjectId { get; set; }

        public static CommandResult Success()
            => new CommandResult { Successful = true };

        public static CommandResult Success(dynamic objectId)
        {
            return new CommandResult
            {
                Successful = true,
                ObjectId = objectId
            };
        }

        public static CommandResult Error(Exception exception)
        {
            return new CommandResult
            {
                Successful = false,
                Exception = exception
            };
        }
    }
}