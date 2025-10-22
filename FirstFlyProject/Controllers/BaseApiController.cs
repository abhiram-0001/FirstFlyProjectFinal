
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
 
namespace FirstFlyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BaseApiController : Controller
    {
        
        protected int CurrentUserId => Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)) ;
        protected string CurrentUserRole => User.FindFirstValue(ClaimTypes.Role);
    }
}