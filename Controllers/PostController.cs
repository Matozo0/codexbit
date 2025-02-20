using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CodexBit.Models;
using CodexBit.Context;
using Microsoft.EntityFrameworkCore;

namespace CodexBit.Controllers;

public class PostController : Controller
{
    private readonly ILogger<PostController> _logger;
    private readonly BlogContext _context;

    public PostController(ILogger<PostController> logger, BlogContext context)
    {
        _logger = logger;
        _context = context;
    }

    // Página inicial
    [HttpGet("")]
    public IActionResult Index()
    {   
        // Pega todos os posts em ordem do mais recente
        var posts = _context.Posts
            .OrderByDescending(post => post.UpdatedAt)
            .ToList();
        return View(posts);
    }

    // Post id
    [HttpGet("{id?}")]
    public IActionResult Details(int id)
    {   
        // Pega o post com id fornecido
        var post = _context.Posts
            .FirstOrDefault(post => post.Id == id);
            
        // Caso não exista o post retorna NotFound
        if (post == null)
            return NotFound();

        return View(post);
    }
}
