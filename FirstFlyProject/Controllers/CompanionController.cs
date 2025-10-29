using FirstFlyProject.Entities;
using FirstFlyProject.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FirstFlyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanionController : BaseApiController
    {
        private readonly Data.ApplicationDbContext _context;

        public CompanionController(Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public record companionDto(int companionid, int userid, string? name, int age, string? gender);
        [HttpPost]
        public async Task<IActionResult> CreateCompanion([FromBody] companionDto dto)
        {
            Companion newcompanion = new Companion();
            newcompanion.CompanionId = dto.companionid;
            newcompanion.UserId = CurrentUserId;
            newcompanion.Name = dto.name;
            newcompanion.Gender = dto.gender;

            newcompanion.Age = dto.age;
            _context.Companions.Add(newcompanion);
            await _context.SaveChangesAsync();
            return Ok("Companion added");
        }
        [HttpGet("{Userid}")]
        public async Task<IActionResult> Travellers(int id)
        {
            var q = _context.Companions.AsQueryable();
            if(id!=0)
            {
                q = q.Where(q => q.UserId == id);
            }
            var allcomps = await q.Select(u => new
            {
                u.CompanionId,
                UserId = u.UserId,
                Name = u.Name,
                u.Age,
                u.Gender
            }).ToListAsync();
            return Ok(allcomps);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var comp = await _context.Companions.FindAsync(id);
            if (comp == null)
                return NotFound();
            _context.Companions.Remove(comp);
            await _context.SaveChangesAsync();
            return Ok("Companion deleted successfully");

        }
        [HttpPut("{Companionid:int}")]
        public async Task<ActionResult> SpecificUpdate(int Companionid, [FromBody] companionDto dto)
        {
            var comp = await _context.Companions.FindAsync(Companionid);
            if(comp==null) return NotFound();
            if(dto.age!=null) comp.Age = dto.age;
            if (dto.gender != null) comp.Gender = dto.gender;
            if (dto.name != null) comp.Name = dto.name;
            await _context.SaveChangesAsync();
            return Ok("Updated Successfully");
        }
    }
}
