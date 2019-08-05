namespace CoreWiki.Web.Common
{
    using Constants;
    using Microsoft.AspNetCore.Authorization;

    public class AuthPolicy
    {
        public static void Execute(AuthorizationOptions options)
        {
            options.AddPolicy(PolicyConstants.CanDeleteArticle, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole(PolicyConstants.Administrator);
            });

            options.AddPolicy(PolicyConstants.RequireAdministratorRole, policy =>
            {
                policy.RequireRole(PolicyConstants.Administrator).RequireAuthenticatedUser();
            });
        }
    }
}