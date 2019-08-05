namespace CoreWiki.Infrastructure.Exceptions
{
    using System;

    [Serializable]
    public class ArticleNotFoundException : Exception
    {
        public ArticleNotFoundException()
        {
            
        }

        public ArticleNotFoundException(string message)
            : base(message)
        {
            
        }
    }
}