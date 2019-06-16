namespace CoreWiki.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using NodaTime;
    using NodaTime.Extensions;

    public class ArticleHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }

        [Required]
        public int Version { get; set; }

        [Required]
        [MaxLength(100)]
        public string Topic { get; set; }

        public string Slug { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [NotMapped]
        public Instant Published { get; set; }

        [Obsolete("This property only exists for EF-serialization purposes")]
        [DataType(DataType.DateTime)]
        [Column("Published")]
        public DateTime PublisheOn
        {
            get => this.Published.ToDateTimeUtc();
            
            set => this.Published = DateTime.SpecifyKind(value, DateTimeKind.Utc).ToInstant();
        }
    }
}