namespace CoreWiki.Application.Queries
{
    using System.Collections.Generic;
    using Dto;
    using MediatR;

    public class GetAllArticlesQuery : IRequest<IEnumerable<AllArticlesDto>>
    {
        public GetAllArticlesQuery(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}