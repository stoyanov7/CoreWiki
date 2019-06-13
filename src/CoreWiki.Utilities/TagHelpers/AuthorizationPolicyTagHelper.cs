namespace CoreWiki.Utilities.TagHelpers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authorization.Policy;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Razor.TagHelpers;

    /// <summary>
    /// The Authorize tag helper allows to render blocks of HTML code
    /// only for users who are authorized based on Authorization Roles or Policies. 
    /// </summary>
    [HtmlTargetElement(Attributes = "asp-authorize")]
    [HtmlTargetElement(Attributes = "asp-authorize,asp-roles")]
    [HtmlTargetElement(Attributes = "asp-policy")]
    public class AuthorizationPolicyTagHelper : TagHelper, IAuthorizeData
    {
        private readonly IAuthorizationPolicyProvider policyProvider;
        private readonly IPolicyEvaluator policyEvaluator;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthorizationPolicyTagHelper(
            IAuthorizationPolicyProvider policyProvider,
            IPolicyEvaluator policyEvaluator, 
            IHttpContextAccessor httpContextAccessor)
        {
            this.policyProvider = policyProvider;
            this.policyEvaluator = policyEvaluator;
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets or sets the policy name that determines access to the HTML block. 
        /// </summary>
        [HtmlAttributeName("asp-policy")]
        public string Policy { get; set; }

        /// <summary>
        /// Gets or sets a comma delimited list of roles that are allowed to assess the HTML block.
        /// </summary>
        [HtmlAttributeName("asp-roles")]
        public string Roles { get; set; }

        /// <summary>
        /// Gets or sets a comma delimited list of schemes from which user information is constructed.
        /// </summary>
        [HtmlAttributeName("asp-authentication-schema")]
        public string AuthenticationSchemes { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var policy = await AuthorizationPolicy.CombineAsync(this.policyProvider, new[] { this });

            var authenticateResult = await this.policyEvaluator
                    .AuthenticateAsync(policy, this.httpContextAccessor.HttpContext);

            var authorizeResult = await this.policyEvaluator
                .AuthorizeAsync(policy, authenticateResult, this.httpContextAccessor.HttpContext, null);

            if (!authorizeResult.Succeeded)
            {
                output.SuppressOutput();
            }
        }
    }
}