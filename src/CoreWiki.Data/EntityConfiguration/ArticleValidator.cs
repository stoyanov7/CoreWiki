namespace CoreWiki.Data.EntityConfiguration
{
    using FluentValidation;
    using Models;

    public class ArticleValidator : AbstractValidator<Article>
    {
        public ArticleValidator()
        {
            this.RuleFor(o => o.Topic)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
