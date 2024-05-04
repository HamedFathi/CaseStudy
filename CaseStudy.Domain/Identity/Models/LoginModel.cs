using System.ComponentModel.DataAnnotations;

namespace CaseStudy.Domain.Identity.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Password is required")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "UserName is required")]
    public required string UserName { get; set; }
}