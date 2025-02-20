using System;
using System.ComponentModel.DataAnnotations;

namespace CodexBit.Models;

public class PostModel
{
    // Identificação do post
    [Key]
    public int Id { get; set; }

    // Titulo do post
    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    // Breve descrição do post
    [Required]
    [MaxLength(500)]
    public string Description { get; set; }
    
    // Thumbnail do post
    [Required]
    public string ImageURL { get; set; }

    // Texto do post em formato Markdown
    [Required]
    public string ContentMarkdown { get; set; }

    // Data de criação do post
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Data de atualização do post
    [DataType(DataType.DateTime)]
    public DateTime UpdatedAt { get; set; }     

    // Comentários do post
    public List<CommentModel> Comments { get; set; } = new();
}