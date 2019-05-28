namespace CoreWiki.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using NodaTime;
    using NodaTime.Extensions;

    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Topic { get; set; }

        public string Slug { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [NotMapped]
        public Instant Published { get; set; }

        [Obsolete("This property only exists for EF-serialization purposes")]
        [DataType(DataType.Date)]
        [Display(Name = "Published on")]
        public DateTime PublishedOn
        {
            get => this.Published.ToDateTimeUtc();

            set => this.Published = DateTime.SpecifyKind(value, DateTimeKind.Utc).ToInstant();
        }

        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
