using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace velora.api.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : APIBaseController
    {
        [HttpGet("dashboard")]
        public IActionResult AdminOnly()
        {
            return Ok("✅ Hello Admin 👋 — this is your dashboard");
        }
    }
}
