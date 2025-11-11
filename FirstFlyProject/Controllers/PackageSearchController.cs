using FirstFlyProject.Data;
using FirstFlyProject.Entities;
using FirstFlyProject.Models;
using FirstFlyProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstFlyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Customer, TravelAgent")]
    public class PackageSearchController : BaseApiController
    {
        private readonly IPackageService _packageService;
        private readonly ApplicationDbContext _context;
        public PackageSearchController(IPackageService packageService,ApplicationDbContext context)
        {
            _packageService = packageService;
            _context = context;
        }
        

 

        [HttpGet("search")]
        public async Task<ActionResult> Search([FromQuery]string? Destination, [FromQuery]int? NoOfAdult, [FromQuery]double? maxprice, [FromQuery] double? minprice,[FromQuery]int?duration)
        {
            var results = await _packageService.SearchPackages(Destination,duration);
            return Ok(results);
        }

        [HttpPost("book")]

        public async Task<IActionResult> Book([FromBody] CreateBookingRequest request)
        {
            // Retrieve UserID from the authenticated user's JWT token
            int userId = CurrentUserId; 

            var booking = await _packageService.CreateBooking(request, userId);


            return Ok(booking);
        }
        [HttpGet("getall/{userId}")]
        public async Task<IActionResult> BookingsDone(int userId)
        {
            var bookings = await _context.Bookings.Where(b => b.UserId == userId).Select(b => new
            {
                b.BookingId,
                b.PackageId,
                b.PackageName,
                b.TotalPrice,
                b.StartDate,
                b.EndDate,
                b.Status
            }).ToListAsync();
            if (bookings == null)
                return NotFound();
            

            return Ok(bookings);
        }
    }
}