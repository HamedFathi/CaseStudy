

using CaseStudy.Domain.Identity.Models;
using CaseStudy.Domain.Identity;
using HamedStack.TheResult;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudy.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public AuthController(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    [HttpPost("register", Name = "RegisterUser")]
    public async Task<Result> RegisterUser(RegisterModel registerModel)
    {
        return await _identityService.Register(registerModel);
    }
    [HttpPost("login", Name = "LoginUser")]
    public async Task<TokenModel?> LoginUser(LoginModel loginModel)
    {
        return await _identityService.Login(loginModel);
    }
}
