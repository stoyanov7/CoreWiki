namespace CoreWiki.Application
{
    using System;

    [Serializable]
    public class DeleteArticleException : Exception
    {
        public DeleteArticleException()
        {
            
        }

        public DeleteArticleException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}