﻿namespace CoreWiki.Infrastructure.Services
{
    using System.Threading.Tasks;
    using Domain.Repository;
    using Domain.Services;
    using Models;
    using NodaTime;

    public class CommentService : ICommentService
    {
        private readonly IClock clock;
        private readonly IRepository<Comment> commentRepository;

        public CommentService(IClock clock, IRepository<Comment> commentRepository)
        {
            this.clock = clock;
            this.commentRepository = commentRepository;
        }

        public async Task SetCommentToArticleAsync(Comment comment, Article article)
        {
            comment.Article = article;
            comment.Submitted = this.clock.GetCurrentInstant();

            await this.commentRepository.AddAsync(comment);
            await this.commentRepository.SaveChangesAsync();
        }
    }
}