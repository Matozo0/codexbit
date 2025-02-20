using System;
using CodexBit.Models;
using Microsoft.EntityFrameworkCore;

namespace CodexBit.Context;

public class BlogContext : DbContext
{
    public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

    // Representação da tabela de Posts
    public DbSet<PostModel> Posts { get; set; }
    // Representação da tabela de Comentários
    public DbSet<CommentModel> Comments { get; set; }    
}