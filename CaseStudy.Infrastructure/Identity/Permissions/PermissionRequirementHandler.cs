using Microsoft.AspNetCore.Authorization;

namespace CaseStudy.Infrastructure.Identity.Permissions;

public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (requirement.PermissionOperator == PermissionOperator.And)
        {
            foreach (var permission in requirement.Permissions)
            {
                /*
                 * ClaimType: Permission, ClaimValue: Read
                 * ClaimType: Permission, ClaimValue: Create
                 * ClaimType: Permission, ClaimValue: Update
                 * ClaimType: Permission, ClaimValue: Delete
                 */
                if (!context.User.
                        HasClaim(PermissionRequirement.ClaimType, permission))
                {

                    context.Fail();
                    return Task.CompletedTask;
                }
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        foreach (var permission in requirement.Permissions)
        {
            if (context.User.HasClaim(PermissionRequirement.ClaimType, permission))
            {

                context.Succeed(requirement);
                return Task.CompletedTask;
            }
        }

        context.Fail();
        return Task.CompletedTask;
    }
}