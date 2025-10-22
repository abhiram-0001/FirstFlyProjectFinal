using FirstFlyProject.Entities;
using FirstFlyProject.Models;
using FirstFlyProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstFlyProject.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "TravelAgent")]
    public class PackageController : BaseApiController
    {

        private readonly Data.ApplicationDbContext _context;

        public PackageController(Data.ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpGet("getall")]
        public async Task<ActionResult> GetAll()
        {
            var packages = await _context.TravelPackages.ToListAsync();
            return Json(packages);
        }
        public record PackageDto(int PackageId, int Duration, string? IncludedServices, float? Price, string? Description, string? Title);
        //public record PackageDto(int PackageId, string Title, int TravelAgentId);
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PackageDto Package)
        {
            TravelPackage newPackage = new TravelPackage();
            newPackage.Duration = Package.Duration;
            newPackage.IncludedServices = Package.IncludedServices;
            newPackage.Price = Package.Price;
            newPackage.Description = Package.Description;

            newPackage.Title = Package.Title;
            newPackage.TravelAgentID = CurrentUserId;
            _context.TravelPackages.Add(newPackage);
            await _context.SaveChangesAsync();
            return Ok("Package created successfully");
            // }
            //return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] PackageDto Package)
        {
            if (id != Package.PackageId)
                return BadRequest("ID mismatch");
            TravelPackage newPackage = await _context.TravelPackages.FindAsync(id);

            if(Package.Duration!=null) newPackage.Duration = Package.Duration;

            if(Package.IncludedServices!=null) newPackage.IncludedServices = Package.IncludedServices;

            if(Package.Price!=null) newPackage.Price = Package.Price;

            if (Package.Description != null) newPackage.Description = Package.Description;

            if(Package.Title!=null) newPackage.Title = Package.Title;

            newPackage.TravelAgentID = CurrentUserId;
            _context.Update(newPackage);
            await _context.SaveChangesAsync();
            return Ok("Package updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var package = await _context.TravelPackages.FindAsync(id);
            if (package == null)
                return NotFound();

            _context.TravelPackages.Remove(package);
            await _context.SaveChangesAsync();
            return Ok("Package deleted successfully");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var package = await _context.TravelPackages.FindAsync(id);
            if (package == null)
                return NotFound();

            return Json(package);
        }
    }
}