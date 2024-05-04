using CaseStudy.Domain.Identity.Models;
using HamedStack.TheResult;

namespace CaseStudy.Domain.Identity;

public interface IIdentityService
{
    Task<TokenModel?> Login(LoginModel model);
    Task<Result> Register(RegisterModel model);
    Task<bool> Revoke(string userName);
    Task Revoke(IEnumerable<string> userNames);
    Task RevokeAll();
}