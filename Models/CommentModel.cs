using System;
using System.ComponentModel.DataAnnotations;

namespace CodexBit.Models;

public class CommentModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Username { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Content { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public int PostId { get; set; }

    [Required]
    public PostModel Post { get; set; }
}