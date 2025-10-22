using FirstFlyProject.Entities;
using FirstFlyProject.Models;
using FirstFlyProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstFlyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
       private readonly IAuthServices _authServices;
        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }
        [HttpPost("register")] 
        public async Task<ActionResult<User>> Register(RegDto request)

        {
            var user=await _authServices.RegisterCustomerAsync(request);
            if (user == null)
            {
                return BadRequest("User name already exists");
            }
            return Ok(user);
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var result=await _authServices.LoginAsync(request); 
            if (result == null)
            {
                return BadRequest("Invalid username or Password");
            }
            return Ok(result);
        }
        [HttpGet("protected")]
        [Authorize]
        public ActionResult get()
        {
            return Ok("you are signed in");
        }
        [HttpGet("admin")]
        [Authorize(Roles ="Admin")]
        public ActionResult getAdmin()
        {
            return Ok("you are admin");
        }
    }
}
