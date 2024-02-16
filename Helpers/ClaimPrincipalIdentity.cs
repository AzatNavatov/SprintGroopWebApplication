using System.Security.Claims;

namespace SprintGroopWebApplication.Helpers
{
    public static class ClaimPrincipalIdentity
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
