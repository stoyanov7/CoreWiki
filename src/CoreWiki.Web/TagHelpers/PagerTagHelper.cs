namespace CoreWiki.Web.TagHelpers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
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

        public string AspPage { get; set; }

        [ViewContext]
        public ActionContext ActionContext { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
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
