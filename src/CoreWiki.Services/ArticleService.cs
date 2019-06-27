namespace CoreWiki.Services
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts;
    using Repository.Contracts;

    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository articleRepository;
        private readonly IMapper mapper;

        public ArticleService(IArticleRepository articleRepository, IMapper mapper)
        {
            this.articleRepository = articleRepository;
            this.mapper = mapper;
        }

        public async Task<TModel> FindBySlugAsync<TModel>(string slug)
        {
            var articles = await this.articleRepository.FindByAsync(slug);
            var model = this.mapper.Map<TModel>(articles);

            return model;
        }

        public async Task Delete(string slug)
        {
            this.articleRepository.Delete(slug);
            await this.articleRepository.SaveChangesAsync();
        }
    }
}