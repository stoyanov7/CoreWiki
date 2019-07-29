namespace CoreWiki.Web.Common.TagHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement("pager")]
    public class PagerTagHelper : TagHelper
    {
        private readonly IHtmlGenerator generator;

        public PagerTagHelper(IHtmlGenerator generator)
        {
            this.generator = generator;
        }

        /// <summary>
        /// Gets or sets the number of the current page.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the number of page links to show.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the name of the page.
        /// </summary>
        public string AspPage { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ViewContext"/> for the current request.
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        /// <summary>
        /// Optional. Enables adding url parameters (e.g '?query=test') to the link URL
        /// </summary>
        public Dictionary<string, string> UrlParams { get; set; }

        /// <summary>
        /// Show up to this number of page links in the paginator.
        /// </summary>
        /// <remarks>
        /// If not specified this will default to <c>10</c>.
        /// </remarks>
        public int MaxPagesDisplayed { get; set; } = 10;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "ul";

            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");
            output.MergeAttributes(ul);

            this.AppendPreNavigationButtons(output);
            this.AppendNavigationButtons(output);
            this.AppendPostNavigationButtons(output);

            await base.ProcessAsync(context, output);
            return;
        }

        private Dictionary<string, string> MakeRouteValues(int pageNumber)
        {
            var route = new Dictionary<string, string>
            {
                {"PageNumber", pageNumber.ToString()}
            };

            if (this.UrlParams == null)
            {
                return route;
            }

            foreach (var key in this.UrlParams.Keys)
            {
                // We don't want to override existing values such as PageNumber
                if (route.ContainsKey(key))
                {
                    continue;
                }

                route.Add(key, this.UrlParams[key]);
            }

            return route;
        }

        public TagBuilder CreatePageItem()
        {
            var tag = new TagBuilder("li");
            tag.AddCssClass("page-item");

            return tag;
        }

        private TagBuilder CreatePaginatorButton(string textForScreenReaders, string textToDisplay, int pageNum, bool clickable)
        {
            var tag = clickable
                ? this.generator.GeneratePageLink(this.ViewContext,
                    linkText: "",
                    pageName: this.AspPage,
                    pageHandler: string.Empty,
                    protocol: string.Empty,
                    hostname: string.Empty,
                    fragment: string.Empty,
                    routeValues: this.MakeRouteValues(pageNum),
                    htmlAttributes: null
                )
                : new TagBuilder("span");
            tag.AddCssClass("page-link");
            tag.AddAriaSpans(textForScreenReaders, textToDisplay);

            return tag;
        }

        private void AppendPreNavigationButtons(TagHelperOutput output)
        {
            var first = this.CreatePageItem();
            var previous = this.CreatePageItem();
            var clickable = this.CurrentPage != 1;

            first.InnerHtml.AppendHtml(this.CreatePaginatorButton("First Page", "<<", 1, clickable));
            previous.InnerHtml.AppendHtml(this.CreatePaginatorButton("Previous Page", "<", this.CurrentPage - 1, clickable));

            if (!clickable)
            {
                first.AddCssClass("disabled");
                previous.AddCssClass("disabled");
            }

            output.Content.AppendHtml(first);
            output.Content.AppendHtml(previous);
        }

        private void AppendPostNavigationButtons(TagHelperOutput output)
        {
            var next = this.CreatePageItem();
            var last = this.CreatePageItem();
            var clickable = this.CurrentPage != this.TotalPages;

            next.InnerHtml.AppendHtml(this.CreatePaginatorButton("Next Page", ">", this.CurrentPage + 1, clickable));
            last.InnerHtml.AppendHtml(this.CreatePaginatorButton("Last Page", ">>", this.TotalPages, clickable));

            if (!clickable)
            {
                next.AddCssClass("disabled");
                last.AddCssClass("disabled");
            }

            output.Content.AppendHtml(next);
            output.Content.AppendHtml(last);
        }

        private void AppendNavigationButtons(TagHelperOutput output)
        {
            var (start, end) = this.CalculatePaginatorDisplayRange(this.CurrentPage, this.TotalPages, this.MaxPagesDisplayed);

            for (var pageNum = start; pageNum <= end; pageNum++)
            {
                var li = this.CreatePageItem();

                if (pageNum == this.CurrentPage)
                {
                    li.AddCssClass("active");
                    li.InnerHtml.AppendHtml(this.CreatePaginatorButton("Current Page", $"{pageNum}", pageNum, false));
                }
                else
                {
                    li.InnerHtml.AppendHtml(this.CreatePaginatorButton($"Page {pageNum}", $"{pageNum}", pageNum, true));
                }

                output.Content.AppendHtml(li);
            }
        }

        private (int start, int end) CalculatePaginatorDisplayRange(int currentPage, int totalPages, int maxPagesDisplayed)
        {
            var start = 0;
            var end = 0;

            var midPoint = (int)Math.Floor(this.MaxPagesDisplayed / 2.0);
            var pagesToShowBeforeMidpoint = this.MaxPagesDisplayed - midPoint - 1;
            var pagesToShowAfterMidpoint = this.MaxPagesDisplayed - pagesToShowBeforeMidpoint - 1;

            if (this.CurrentPage <= pagesToShowBeforeMidpoint)
            {
                start = 1;
                end = Math.Min(this.MaxPagesDisplayed, this.TotalPages);
            }
            else if (this.CurrentPage >= this.TotalPages - pagesToShowBeforeMidpoint)
            {
                start = this.MaxPagesDisplayed > this.TotalPages ? 1 : this.TotalPages - this.MaxPagesDisplayed + 1;
                end = this.TotalPages;
            }
            else
            {
                start = this.CurrentPage - pagesToShowBeforeMidpoint;
                end = this.CurrentPage + pagesToShowAfterMidpoint;
            }

            return (start, end);
        }
    }
}
