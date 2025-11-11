using FirstFlyProject.Entities;
using FirstFlyProject.Models;
using FirstFlyProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FirstFlyProject.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
        public record PackageDto(int Duration, string? IncludedServices, float? Price, string? Description, string? Title,string?url,string?destination);
        //public record PackageDto(int PackageId, string Title, int TravelAgentId);
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PackageDto Package)
        {
            TravelPackage newPackage = new TravelPackage();
            
            newPackage.Duration = Package.Duration;
            newPackage.IncludedServices = Package.IncludedServices;
            newPackage.Price = Package.Price;
            newPackage.Description = Package.Description;
            newPackage.ImageUrl=Package.url;
            newPackage.Title = Package.Title;
            newPackage.Destination=Package.destination;
            newPackage.TravelAgentID = CurrentUserId;
            _context.TravelPackages.Add(newPackage);
            await _context.SaveChangesAsync();
            return Ok();
            // }
            //return BadRequest(ModelState);
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] PackageDto Package)
        {
            
            TravelPackage newPackage = await _context.TravelPackages.FindAsync(id);
            if(Package.Title!=null) newPackage.Title = Package.Title;
            if(Package.destination!=null) newPackage.Destination = Package.destination;
            if(Package.url!=null) newPackage.ImageUrl = Package.url;
            if (Package.Duration!=null) newPackage.Duration = Package.Duration;

            if(Package.IncludedServices!=null) newPackage.IncludedServices = Package.IncludedServices;

            if(Package.Price!=null) newPackage.Price = Package.Price;

            if (Package.Description != null) newPackage.Description = Package.Description;

            if(Package.Title!=null) newPackage.Title = Package.Title;

            newPackage.TravelAgentID = CurrentUserId;
            _context.Update(newPackage);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var package = await _context.TravelPackages.FindAsync(id);
            if (package == null)
                return NotFound();

            _context.TravelPackages.Remove(package);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("get/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var package = await _context.TravelPackages.FindAsync(id);
            if (package == null)
                return NotFound();

            return Ok(package);
        }
    }
}