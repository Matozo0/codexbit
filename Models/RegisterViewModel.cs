using System;
using System.ComponentModel.DataAnnotations;

namespace CodexBit.Models;

public class RegisterViewModel
{   
    [Required]
    public string Username { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set;}
    
    [Required]
    public string ConfirmPassword { get; set; }
}