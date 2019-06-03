namespace CoreWiki.Utilities
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Models.Identity;

    public static class ApplicationBuilderAuthExtensions
    {
        private const string AdminRole = "Administrator";
        private const string AdminUsername = "admin";
        private const string AdminEmail = "admin@gmail.com";
        private const string DefaultAdminPassword = "123";

        /// <summary>
        /// Middleware for seed data into database.
        /// </summary>
        /// <param name="app"></param>
        public static async void SeedDatabase(this IApplicationBuilder app)
        {
            var serviceFactory = app
                .ApplicationServices
                .GetRequiredService<IServiceScopeFactory>();

            var scope = serviceFactory.CreateScope();

            using (scope)
            {
                var roleManager = scope
                    .ServiceProvider
                    .GetRequiredService<RoleManager<IdentityRole>>();

                var userManager = scope
                    .ServiceProvider
                    .GetRequiredService<UserManager<ApplicationUser>>();

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Name))
                    {
                        await roleManager.CreateAsync(role);
                    }
                }

                var user = await userManager.FindByNameAsync(AdminUsername);

                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = AdminUsername,
                        Email = AdminEmail
                    };

                    await userManager.CreateAsync(user, DefaultAdminPassword);
                    await userManager.AddToRoleAsync(user, roles[0].Name);
                }
            }
        }

        private static readonly IdentityRole[] roles =
        {
            new IdentityRole(AdminRole)
        };
    }
}