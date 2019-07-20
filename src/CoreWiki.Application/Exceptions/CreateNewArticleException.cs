namespace CoreWiki.Application.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    internal class CreateNewArticleException : Exception
    {
        public CreateNewArticleException()
        {
        }

        public CreateNewArticleException(string message)
            : base(message)
        {
        }

        public CreateNewArticleException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected CreateNewArticleException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}