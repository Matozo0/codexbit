using System;
using CodexBit.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodexBit.Context;

public class AppDBContext : IdentityDbContext<UserModel>
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

    // Representação da tabela de Posts
    public DbSet<PostModel> Posts { get; set; }
    // Representação da tabela de Comentários
    public DbSet<CommentModel> Comments { get; set; }
}