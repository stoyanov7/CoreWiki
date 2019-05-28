namespace CoreWiki.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using NodaTime;
    using NodaTime.Extensions;

    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Required]
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }

        [NotMapped]
        public Instant Submitted { get; set; }

        [Obsolete("This property only exists for EF-serialization purposes")]
        [DataType(DataType.DateTime)]
        [Column("Submitted")]
        public DateTime SubmittedDateTime
        {
            get => this.Submitted.ToDateTimeUtc();
           
            set => this.Submitted = DateTime.SpecifyKind(value, DateTimeKind.Utc).ToInstant();
        }

    }
}
