namespace CoreWiki.Web.Common
{
    using Microsoft.AspNetCore.Authorization;
    using Utilities.Constants;

    public class AuthPolicy
    {
        public static void Execute(AuthorizationOptions options)
        {
            options.AddPolicy(PolicyConstants.CanDeleteArticle, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole("Administrator");
            });
        }
    }
}