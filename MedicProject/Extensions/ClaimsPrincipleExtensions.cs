using System.Security.Claims;

namespace MedicProject.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetEmail(this ClaimsPrincipal User)
        {
            return User.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}