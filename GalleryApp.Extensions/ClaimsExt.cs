
using System.Security.Claims;

namespace GalleryApp.Extensions
{
    public static class ClaimsExt
    {
        public static string GetCurrentUser(this ClaimsPrincipal user) 
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}