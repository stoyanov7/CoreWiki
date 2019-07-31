namespace CoreWiki.Application.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Repository;
    using MediatR;

    public class IncrementArticleViewCountCommandHandler : IRequestHandler<IncrementArticleViewCountCommand>
    {
        private readonly IArticleRepository articleRepository;

        public IncrementArticleViewCountCommandHandler(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }


        public async Task<Unit> Handle(IncrementArticleViewCountCommand request, CancellationToken cancellationToken)
        {
            await this.articleRepository.IncrementViewCount(request.Slug);

            return Unit.Value;
        }
    }
}