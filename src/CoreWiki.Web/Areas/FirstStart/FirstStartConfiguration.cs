namespace CoreWiki.Web.Areas.FirstStart
{
    using System.ComponentModel.DataAnnotations;

    public class FirstStartConfiguration
    {
        [Required]
        [StringLength(200, MinimumLength = 3)]
        [Display(Name = "Administrator Username")]
        public string AdminUsername { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Administrator Email Address")]
        public string AdminEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = " Administrator Password")]
        public string AdminPassword { get; set; }

        [Required]
        public string Database { get; set; }

        [Display(Name = "Connection String")]
        public string ConnectionString { get; set; }
    }
}