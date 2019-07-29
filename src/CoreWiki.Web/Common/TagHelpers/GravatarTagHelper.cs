namespace CoreWiki.Web.Common.TagHelpers
{
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Cryptography;
    using System.Text;

    /// <inheritdoc />
    /// <summary>
    /// Tag helper for displaying "Gravatar" images based on the email address. 
    /// </summary>
    [HtmlTargetElement("img", Attributes = "gravatar-email")]
    public class GravatarTagHelper : TagHelper
    {
        [HtmlAttributeName("gravatar-email")]
        public string Email { get; set; }

        [HtmlAttributeName("gravatar-mode")]
        public Mode Mode { get; set; } = Mode.Mm;

        [HtmlAttributeName("gravatar-rating")]
        public Rating Rating { get; set; } = Rating.g;

        [HtmlAttributeName("gravatar-size")]
        public int Size { get; set; } = 50;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(this.Email));
                var hash = BitConverter.ToString(result).Replace("-", "").ToLower();
                var url = $"http://gravatar.com/avatar/{hash}";

                var queryBuilder = new QueryBuilder
                {
                    { "s", this.Size.ToString() },
                    { "d", GetModeValue(this.Mode) },
                    { "r", this.Rating.ToString() }
                };

                url = url + queryBuilder.ToQueryString();

                output.Attributes.SetAttribute("src", url);
            }
        }

        private static string GetModeValue(Mode mode)
        {
            return mode == Mode.NotFound
                ? "404" 
                : mode.ToString().ToLower();
        }
    }

    #region Utilities

    /// <summary>
    /// In addition to allowing you to use your own image, "Gravatar" has a number of built in options which
    /// you can also use as defaults. Most of these work by taking the requested email hash and using it to
    /// generate a themed image that is unique to that email address.
    /// </summary>
    public enum Mode
    {
        /// <summary>
        /// Do not load any image if none is associated with the email hash,
        /// instead return an HTTP 404 (File Not Found) response.
        /// </summary>
        [Display(Name = "404")]
        NotFound,
        /// <summary>
        /// Mystery-Man - a simple, cartoon-style silhouetted outline of a person
        /// (does not vary by email hash).
        /// </summary>
        [Display(Name = "Mm")]
        Mm,
        /// <summary>
        /// A geometric pattern based on an email hash.
        /// </summary>
        [Display(Name = "Identicon")]
        Identicon,
        /// <summary>
        /// Generate 'monster' with different colors, faces, etc.
        /// </summary>
        [Display(Name = "MonsterId")]
        Monsterid,
        /// <summary>
        /// Generate faces with differing features and backgrounds
        /// </summary>
        [Display(Name = "Wavatar")]
        Wavatar,
        /// <summary>
        /// Generate 8-bit arcade-style pixelated faces
        /// </summary>
        [Display(Name = "Retro")]
        Retro,
        [Display(Name = "Blank")]
        Blank
    }

    /// <summary>
    /// "Gravatar" allows users to self-rate their images so that they can indicate if an image is
    /// appropriate for a certain audience. By default, only 'G' rated images are displayed unless you
    /// indicate that you would like to see higher ratings.
    /// </summary>
    public enum Rating
    {
        /// <summary>
        /// Suitable for display on all websites with any audience type.
        /// </summary>
        g,
        /// <summary>
        /// May contain rude gestures, provocatively dressed individuals, the lesser swear words, or mild violence.
        /// </summary>
        pg,
        /// <summary>May contain such things as harsh profanity, intense violence, nudity, or hard drug use</summary>
        r,
        /// <summary>
        /// May contain hardcore sexual imagery or extremely disturbing violence
        /// </summary>
        x
    }

    #endregion
}