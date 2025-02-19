using System;
using System.ComponentModel.DataAnnotations;

namespace CodexBit.Models;

public class PostModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    [Required]
    [MaxLength(500)]
    public string Description { get; set; }
    
    [Required]
    public string ImageURL { get; set; }

    [Required]
    public string ContentMarkdown { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; }     

    public List<CommentModel> Comments { get; set; } = new();
}