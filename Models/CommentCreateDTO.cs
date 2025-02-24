using System;
using System.ComponentModel.DataAnnotations;

namespace CodexBit.Models;

public class CommentCreateDTO
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Content { get; set; }

    [Required]
    public int PostId { get; set; }
}