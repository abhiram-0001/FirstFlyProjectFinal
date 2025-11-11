using FirstFlyProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstFlyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TravelAgentController : BaseApiController
    {
        private readonly ApplicationDbContext _context;
        public TravelAgentController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("get/{id:int}")]
        public async Task<ActionResult> Get(int id)
        {

            if (id != CurrentUserId) return Forbid();
            var x = await _context.Users.FirstAsync(x => x.UserId == id); 
            /*if (x.Age <= 0) { return BadRequest("Age must be a positive integer."); }*/
            var p = await _context.Users.Where(x => x.UserId == id)
                    .Select(x => new { x.Name, x.Email, x.Age, x.Gender, x.ContactNumber }).FirstOrDefaultAsync();
            return p == null ? NotFound() : Ok(p);
        }
        public record UpdateDto(string? Name, string? ContactNumber, int Age, string? Gender);
        [HttpPost("update/{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateDto dto)
        {
            if (id != CurrentUserId) return Forbid();
            var agent = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (agent == null) return NotFound();
            if (dto.Name != null) agent.Name = dto.Name;
            if (dto.ContactNumber != null) agent.ContactNumber = dto.ContactNumber;
            if (dto.Age != null) agent.Age = dto.Age;
            if (dto.Gender != null) agent.Gender = dto.Gender;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
