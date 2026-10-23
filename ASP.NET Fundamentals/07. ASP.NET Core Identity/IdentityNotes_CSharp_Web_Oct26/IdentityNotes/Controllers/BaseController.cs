using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IdentityNotes.Controllers
{
    public abstract class BaseController : Controller
    {
        protected bool IsUserAuthenticated
            => this.User?.Identity?.IsAuthenticated ?? false;

        protected string? GetuserId()
            => this.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}
