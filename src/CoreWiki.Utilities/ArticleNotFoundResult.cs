namespace CoreWiki.Utilities
{
    using Microsoft.AspNetCore.Mvc;

    public class ArticleNotFoundResult : ViewResult
    {
        public ArticleNotFoundResult()
        {
            this.ViewName = "ArticleNotFound";
            this.StatusCode = 404;
        }
    }
}