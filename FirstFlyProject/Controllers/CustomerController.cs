using FirstFlyProject.Data;
using FirstFlyProject.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstFlyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Customer,Admin")]
    public class CustomerController : BaseApiController
    {
        private readonly ApplicationDbContext _context;
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("get/{id:int}")]
        public async Task<ActionResult> Get(int id)
        {

            if (id != CurrentUserId) return Forbid();

            var p = await _context.Customers
                    .Where(x => x.CustomerID == id)
                    .Select(x => new { x.CustomerID, x.season, x.EmergencyContact }).FirstOrDefaultAsync();
            return p == null ? NotFound() : Ok(p);
        }
        public record UpdateDto(string season, string EmergencyContact);
        [HttpPut("update/{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateDto dto)
        {
            if (id != CurrentUserId) return Forbid();
            var cust = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerID == id);
            if (cust == null) return NotFound();
            if (System.Enum.TryParse<Season>(dto.season, true, out var season))
            {
                cust.season = season;
            }


            cust.EmergencyContact = dto.EmergencyContact;
            await _context.SaveChangesAsync();
            return Ok(cust);
        }
    }
}