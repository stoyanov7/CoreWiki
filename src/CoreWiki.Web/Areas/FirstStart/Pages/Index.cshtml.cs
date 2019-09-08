namespace CoreWiki.Web.Areas.FirstStart.Pages
{
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Models.Identity;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHostingEnvironment hostingEnvironment;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            IHostingEnvironment hostingEnvironment)
        {
            this.userManager = userManager;
            this.hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public FirstStartConfiguration FirstStartConfiguration { get; set; }

        public IActionResult OnGet() => this.Page();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            var newAdminUser = new ApplicationUser
            {
                UserName = this.FirstStartConfiguration.AdminUsername,
                Email = this.FirstStartConfiguration.AdminEmail
            };

            var userResult = await this.userManager
                .CreateAsync(newAdminUser, this.FirstStartConfiguration.AdminPassword);

            if (userResult.Succeeded)
            {
                var result = await this.userManager
                    .AddToRoleAsync(newAdminUser, "Administrator");
            }

            this.WritingConfigFileToDisk(this.FirstStartConfiguration.Database, this.FirstStartConfiguration.ConnectionString);

            return this.RedirectToPage("./Index", new { Area = "" });
        }

        private void WritingConfigFileToDisk(string provider, string connectionString)
		{
			var settingsFileLocation = Path.Combine(this.hostingEnvironment.ContentRootPath, "appsettings.json");

			if (!System.IO.File.Exists(settingsFileLocation))
            {
				var fileStream = System.IO.File.Create(settingsFileLocation);
				var bytes = Encoding.ASCII.GetBytes("{}");
				fileStream.Write(bytes, 0, bytes.Length);
				fileStream.Close();
				fileStream.Dispose();
			}

			var fileContents = System.IO.File.ReadAllText(settingsFileLocation);

			var jsonFile = JsonConvert.DeserializeObject<JObject>(fileContents);
            //jsonFile["foo"] = "bar";
            //jsonFile["DatabaseProvider"] = provider;
            //jsonFile["ConnectionString"] = connectionString;

            System.IO.File.WriteAllText(settingsFileLocation, JsonConvert.SerializeObject(jsonFile, Formatting.Indented));
		}
    }
}