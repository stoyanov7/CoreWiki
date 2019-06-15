namespace CoreWiki.Data.EntityConfiguration
{
    using FluentValidation;
    using Models;

    public class CommentValidator : AbstractValidator<Comment>
    {
        public CommentValidator()
        {
            this.RuleFor(o => o.Name)
                .NotEmpty()
                .MaximumLength(100);

            this.RuleFor(o => o.Email)
                .EmailAddress();
        }
    }
}
