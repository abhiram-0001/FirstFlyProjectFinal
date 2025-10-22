using FirstFlyProject.Entities;
using FirstFlyProject.Models;
using FirstFlyProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstFlyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class PackageSearchController : BaseApiController
    {
        private readonly IPackageService _packageService;
        public PackageSearchController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        // 1. SEARCH ENDPOINT: GET /api/Packages/search?Destination=...
        [HttpGet("search")]
        public async Task<ActionResult> Search([FromQuery]string Destination, [FromQuery]int NoOfAdult, [FromQuery]double? maxprice)
        {
            var results = await _packageService.SearchPackages(Destination,NoOfAdult,maxprice);
            return Ok(results);
        }

        // 2. BOOKING ENDPOINT: POST /api/Packages/book
        [HttpPost("book")]

        public async Task<IActionResult> Book([FromBody] CreateBookingRequest request)
        {
            // Retrieve UserID from the authenticated user's JWT token
            int userId = CurrentUserId; // Placeholder: Replace with logic to get user ID from token

            var booking = await _packageService.CreateBooking(request, userId);


            return Ok(booking);
        }
    }
}