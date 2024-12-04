﻿using System.ComponentModel.DataAnnotations;


namespace FashionShop.Models.views;

public class LoginViewModel
{

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }= string.Empty;
    
}