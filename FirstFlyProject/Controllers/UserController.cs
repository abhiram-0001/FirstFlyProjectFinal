using Microsoft.AspNetCore.Mvc;
using FirstFlyProject.Entities;
using FirstFlyProject.Enum;
using FirstFlyProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;


namespace FirstFlyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> Getall([FromQuery] string? role, [FromQuery] bool? active)
        {
            var q = _db.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(role)&& System.Enum.TryParse<UserRole>(role, true, out var r))
            {
                q = q.Where(u => u.Role == r);
            }
            if (active.HasValue)
            {
                q = q.Where(u => u.IsActive == active);
            }
            var allusers = await q.Select(u => new
            {
                Id = u.UserId,
                Name = u.Name,
                u.Email,
                Role = u.Role.ToString(),
                u.IsActive
            }).ToListAsync();
            return Ok(allusers);
        }
        public record UserDto(string Role);
        [HttpPost("{id:int}/role")]
        public async Task<ActionResult> SetRole(int id, [FromBody] UserDto dto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                return BadRequest("No User Found");
            }
            if (!System.Enum.TryParse<UserRole>(dto.Role, true, out var role))
            {
                return BadRequest("Invalid Role");
            }
            user.Role = role;
            if (role == UserRole.Customer)
            {
                var cust = new Customer();
                user.Customer = cust;
                //_db.PaitentProfiles.Add(patientprofile);
            }
            else if (role == UserRole.TravelAgent)
            {
                var travelagent = new TravelAgent();
                user.TravelAgent = travelagent;
                //_db.DoctorProfiles.Add(doctorprofile);
            }
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost("{id:int}/activate")]
        public async Task<ActionResult> Activate(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                return BadRequest("No User Found");
            }
            user.IsActive = true;
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost("{id:int}/deactivate")]
        public async Task<ActionResult> Deactivate(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                return BadRequest("No User Found");
            }
            user.IsActive = false;
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
