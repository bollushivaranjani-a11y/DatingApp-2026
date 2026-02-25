using System;
using System.Security.Claims;

namespace API.Extensions;

public static class ClaimsPrincipleExtensions
{
    public static string GetMemberId(this ClaimsPrincipal claimsPrincipal)
    {
        return  claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new Exception("Cannot get member ID from token");
    }

}
