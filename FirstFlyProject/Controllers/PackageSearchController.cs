using FirstFlyProject.Entities;
using FirstFlyProject.Models;
using FirstFlyProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstFlyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Customer, TravelAgent")]
    public class PackageSearchController : BaseApiController
    {
        private readonly IPackageService _packageService;
        public PackageSearchController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        [HttpGet("search")]
        public async Task<ActionResult> Search([FromQuery]string Destination, [FromQuery]int NoOfAdult, [FromQuery]double? maxprice)
        {
            var results = await _packageService.SearchPackages(Destination,NoOfAdult,maxprice);
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
    }
}