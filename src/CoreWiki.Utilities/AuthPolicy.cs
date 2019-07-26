namespace CoreWiki.Utilities
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
                policy.RequireRole("Administrator");
            });

            options.AddPolicy("RequireAdministratorRole", policy =>
            {
                policy.RequireRole("Administrator").RequireAuthenticatedUser();
            });
        }
    }
}