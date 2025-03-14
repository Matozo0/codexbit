using System.Diagnostics.CodeAnalysis;
using CodexBit.Context;
using CodexBit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace CodexBit.Controllers;

[Route("account")]
public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;
    private readonly SignInManager<UserModel> _signInManager;
    private readonly UserManager<UserModel> _userManager;

    public AuthController(ILogger<AuthController> logger, SignInManager<UserModel> signInManager, UserManager<UserModel> userManager)
    {
        _logger = logger;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View(new LoginViewModel());
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Dados inválidos.");
            return View(model);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            ModelState.AddModelError("", "Usuário não encontrado.");
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Login inválido.");
            return View(model);
        }

        _logger.LogInformation($"Usuario logado. Id:{_userManager.GetUserId}");
        return RedirectToAction(nameof(Index), "Post");
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View(new RegisterViewModel());
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Dados inválidos.");
            Console.WriteLine("Dados inválidos.");
            return View(model);
        }
        if (model.Password != model.ConfirmPassword)
        {
            ModelState.AddModelError("", "Senhas não são identicas.");
            Console.WriteLine("Senhas não são identicas.");
            return View(model);
        }

        var emailExists = await _userManager.FindByEmailAsync(model.Email);
        Console.WriteLine(emailExists);
        if (emailExists != null)
        {
            ModelState.AddModelError("", "Email já cadastrado.");
            Console.WriteLine("Email já cadastrado.");
            return View(model);
        }

        var newUser = new UserModel { UserName = model.Username, Email = model.Email };
        var result = await _userManager.CreateAsync(newUser, model.Password);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Registro inválido");
            Console.WriteLine("Registro inválido");
            return View();
        }

        await _signInManager.SignInAsync(newUser, isPersistent: false);
        _logger.LogInformation($"Novo usuario registrado. Id:{newUser.Id}");
        return RedirectToAction(nameof(Index), "Post");
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        _logger.LogInformation($"Usuario deslogado. Id:{_userManager.GetUserId}");
        return RedirectToAction("Index", "Post");
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound("Usuário não encontrado.");
        }
        return View(user);
    }
}