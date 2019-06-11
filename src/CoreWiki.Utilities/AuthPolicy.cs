namespace CoreWiki.Utilities
{
    using Microsoft.AspNetCore.Authorization;

    public class AuthPolicy
    {
        public static void Execute(AuthorizationOptions options)
        {
            options.AddPolicy("CanDeleteArticle", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole("Administrator");
            });
        }
    }
}