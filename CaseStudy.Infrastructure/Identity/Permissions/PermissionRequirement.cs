﻿using Microsoft.AspNetCore.Authorization;

namespace CaseStudy.Infrastructure.Identity.Permissions;

public class PermissionRequirement : IAuthorizationRequirement
{
    public static string ClaimType => "Permission";

    public PermissionOperator PermissionOperator { get; }

    public string[] Permissions { get; }

    public PermissionRequirement(
        PermissionOperator permissionOperator, string[] permissions)
    {
        if (permissions.Length == 0)
            throw new ArgumentException("At least one permission is required.", nameof(permissions));

        PermissionOperator = permissionOperator;
        Permissions = permissions;
    }
}