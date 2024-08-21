using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Domain.Entities
{
    [Table("articles")]
    public class Article
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Image { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
