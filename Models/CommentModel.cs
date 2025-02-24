using System;
using System.ComponentModel.DataAnnotations;

namespace CodexBit.Models;

public class CommentModel
{
    // Identificação
    [Key]
    public int Id { get; set; }

    // Usuário que comentou
    [Required]
    [MaxLength(200)]
    public string Username { get; set; }
        
    // Conteúdo do comentário    
    [Required]
    [MaxLength(200)]
    public string Content { get; set; }

    // Data de criação do comentário
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Identificação do post pertecente ao comentário
    [Required]
    public int PostId { get; set; }

    // Objeto Post que obtém o post
    public PostModel Post { get; set; }
}