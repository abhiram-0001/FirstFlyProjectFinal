using FirstFlyProject.Models;
using FirstFlyProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstFlyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsuranceController : BaseApiController
    {
        private readonly IInsuranceService _insuranceService;
        public InsuranceController(IInsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }
        [HttpGet("option")]
        public async Task<ActionResult> GetOption([FromQuery]int packageId, [FromQuery] int people)
        {
            if (people == null) people = 1;
            var option=await _insuranceService.GetInsuranceOptionAsync(packageId, people);
            return Ok(option);
        }
        [HttpPost("attach")]
        public async Task<ActionResult> Attach([FromBody]InsuranceSelectionRequest req)
        {
            var res= await _insuranceService.AttachInsuranceToBookingAsync(req);
            if(!res.Success) return BadRequest(res);
            return Ok(res);
        }
        [HttpGet("bybooking/{bookingId:int}")]
        public async Task<IActionResult> ByBooking(int bookingId)
        {
            var insurance = await _insuranceService.GetInsuranceByBookingAsync(bookingId);
            if (insurance == null) return NotFound();
            return Ok(insurance);
        }

    }
}
