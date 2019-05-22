namespace CoreWiki.Web.TagHelpers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Routing;
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

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        [ViewContext]
        public ActionContext ActionContext { get; set; }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = this.urlHelperFactory.GetUrlHelper(this.ActionContext);

            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "ul";

            if (output.Attributes.ContainsName("class"))
            {
                output.Attributes.SetAttribute("class", output.Attributes["class"].Value + " pagination");
            }
            else
            {
                output.Attributes.Add("class", "pagination");
            }

            for (var pageNum = 1; pageNum <= this.TotalPages; pageNum++)
            {
                // LI tag
                output.Content.AppendHtml($"<li class=\"page-item");
                if (pageNum == this.CurrentPage)
                {
                    output.Content.AppendHtml(" active");
                }
                output.Content.AppendHtml("\">");

                // A tag
                if (pageNum == this.CurrentPage)
                {
                    output.Content.AppendHtml($"<a class=\"page-link active\" href=\"#\">{pageNum}</a>");
                }
                else
                {
                    var routes = new { PageNumber = pageNum };

                    output.Content.AppendHtml($"<a class=\"page-link\" href=\"{urlHelper.Page("./All", routes)}\">{pageNum}</a>");
                }
            }

            output.Content.AppendHtml("</ul>");
            return Task.CompletedTask;

        }
    }
}
