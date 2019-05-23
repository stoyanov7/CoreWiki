namespace CoreWiki.Web.TagHelpers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.AspNetCore.Mvc.TagHelpers;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Razor.TagHelpers;


    [HtmlTargetElement("pager")]
    public class PagerTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory urlHelperFactory;

        public PagerTagHelper(IUrlHelperFactory urlFactory)
        {
            this.urlHelperFactory = urlFactory;
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
        /// Gets or sets the <see cref="ActionContext"/> for the current request.
        /// </summary>
        [ViewContext]
        public ActionContext ActionContext { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = this.urlHelperFactory.GetUrlHelper(this.ActionContext);

            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "ul";

            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");
            output.MergeAttributes(ul);
            
            for (var pageNum = 1; pageNum <= this.TotalPages; pageNum++)
            {
                var li = new TagBuilder("li");
                li.AddCssClass("page-item");
                
                if (pageNum == this.CurrentPage)
                {
                    li.AddCssClass("active");
                }

                var a = new TagBuilder("a");
                a.AddCssClass("page-link");
                a.InnerHtml.Append($"{pageNum}");

                if (pageNum == this.CurrentPage)
                {
                    a.Attributes.Add("href", "#");
                }
                else
                {
                    var routes = new { PageNumber = pageNum };

                    a.Attributes.Add("href", $"{urlHelper.Page(this.AspPage, routes)}");
                }

                li.InnerHtml.AppendHtml(a);
                output.Content.AppendHtml(li);
            }

            await base.ProcessAsync(context, output);
            return;
        }
    }
}
