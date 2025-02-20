using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CodexBit.Models;
using CodexBit.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Sqlite.Diagnostics.Internal;

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

    // Pagina de criação do post
    [HttpGet("create")]
    public IActionResult Create()
    {
        return View(new PostModel());
    }

    // Endpoint do formulário de criação do post
    [HttpPost("create")]
    public IActionResult Create(PostModel post)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        post.UpdatedAt = post.CreatedAt;

        _context.Add(post);
        _context.SaveChanges();
        _logger.LogInformation($"Novo post de id: {post.Id} criado");

        return RedirectToAction("Index");
    }
}