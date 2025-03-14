using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CodexBit.Models;
using CodexBit.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Sqlite.Diagnostics.Internal;
using Microsoft.AspNetCore.Authorization;

namespace CodexBit.Controllers;

public class PostController : Controller
{
    private readonly ILogger<PostController> _logger;
    private readonly AppDBContext _context;

    public PostController(ILogger<PostController> logger, AppDBContext context)
    {
        _logger = logger;
        _context = context;
    }

    // Página inicial
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        // Pega todos os posts em ordem do mais recente
        var posts = await _context.Posts
            .OrderByDescending(post => post.UpdatedAt)
            .AsNoTracking()
            .ToListAsync();
        return View(posts);
    }

    // Post id
    [HttpGet("{id?}")]
    public async Task<IActionResult> Details(int id)
    {
        // Pega o post com id fornecido
        var post = await _context.Posts
            .Include(post => post.Comments) // Carrega os comentários numa só consulta
            .FirstOrDefaultAsync(post => post.Id == id);

        // Caso não exista o post retorna NotFound
        if (post == null)
            return NotFound();

        var viewModel = new DetailsViewModel
        {
            Post = post,
            Comments = post.Comments.OrderByDescending(comment => comment.CreatedAt).ToList(),
            NewComment = new CommentModel()
        };

        return View(viewModel);
    }

    [Authorize]
    [HttpPost("create-comment")]
    public async Task<IActionResult> CreateComment(CommentCreateDTO commentVm)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var post = await _context.Posts
            .FirstOrDefaultAsync(p => p.Id == commentVm.PostId);

        if (post == null)
            return NotFound("O post especificado não existe.");

        var comment = new CommentModel
        {
            Username = commentVm.Username,
            Content = commentVm.Content,
            PostId = commentVm.PostId,
            CreatedAt = DateTime.UtcNow
        };

        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", new { id = commentVm.PostId });
    }

    // Pagina de criação do post
    [Authorize]
    [HttpGet("create")]
    public IActionResult Create()
    {
        return View(new PostModel());
    }

    // Endpoint do formulário de criação do post
    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> Create(PostModel post)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        post.UpdatedAt = post.CreatedAt;

        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        _logger.LogInformation($"Novo post de id: {post.Id} criado");

        return RedirectToAction("Index");
    }
}