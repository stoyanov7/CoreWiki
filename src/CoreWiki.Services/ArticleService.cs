﻿namespace CoreWiki.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Contracts;
    using Models;
    using NodaTime;
    using Repository.Contracts;
    using Utilities;

    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository articleRepository;
        private readonly IMapper mapper;
        private readonly IClock clock;

        public ArticleService(IArticleRepository articleRepository, IMapper mapper, IClock clock)
        {
            this.articleRepository = articleRepository;
            this.mapper = mapper;
            this.clock = clock;
        }

        public bool IsArticleExist(string topic)
            =>this.articleRepository.IsArticleExistByTopic(topic);

        public async Task Create(string topic, string content, string authorId)
        {
            var article = new Article
            {
                Topic = topic,
                Content = content,
                Slug = UrlHelpers.UrlFriendly(topic.ToLower()),
                Published = this.clock.GetCurrentInstant(),
                AuthorId = authorId
            };

            await this.articleRepository
                .AddAsync(article);

            await this.articleRepository.SaveChangesAsync();
        }

        public async Task<TModel> FindBySlugAsync<TModel>(string slug)
        {
            var articles = await this.articleRepository.FindByAsync(slug);
            var model = this.mapper.Map<TModel>(articles);

            return model;
        }

        public async Task<IList<TModel>> GetAllArticlesAsync<TModel>()
        {
            var allArticles = await this.articleRepository.All();

            foreach (var current in allArticles)
            {
                if (current.Content.Length >= 50)
                {
                    current.Content = current.Content.Substring(0, 50) + "...";
                }
            }

            var model = this.mapper.Map<IList<TModel>>(allArticles);

            return model;
        }

        public async Task<IEnumerable<TModel>> GetAllArticlesAsync<TModel>(int pageNumber, int pageSize)
        {
            var allArticles = await this.articleRepository.All(pageNumber, pageSize);
            var model = this.mapper.Map<IEnumerable<TModel>>(allArticles);

            return model;
        }

        public async Task<IEnumerable<TModel>> GetLatestArticle<TModel>()
        {
            var article = await this.articleRepository.LatestArticle();
            var model = this.mapper.Map<IEnumerable<TModel>>(article);

            return model;
        }

        public int GetCount() => this.articleRepository.Count();

        public async Task Delete(string slug)
        {
            this.articleRepository.Delete(slug);
            await this.articleRepository.SaveChangesAsync();
        }
    }
}