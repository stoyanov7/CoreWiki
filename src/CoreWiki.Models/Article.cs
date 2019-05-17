namespace CoreWiki.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Topic { get; set; }

        public string Content { get; set; }

        public DateTime PublishedOn { get; set; } 

        
    }
}
