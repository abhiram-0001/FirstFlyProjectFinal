using FirstFlyProject.Models;
using Microsoft.AspNetCore.Mvc;
using FirstFlyProject.Services;

namespace FirstFlyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class PaymentController : BaseApiController
    {

        private readonly IPaymentServices _paymentService;

        public PaymentController(IPaymentServices paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpGet("checkout")]
        public async Task<ActionResult> CheckOut([FromQuery]int userId)
        {
            if (CurrentUserRole == "Customer" && CurrentUserId != userId) return Forbid();
            var cards = await _paymentService.GetSavedCardsAsync(userId);
            return Ok(cards);
        }
        [HttpPost("card")]
        public async Task<ActionResult> PayCard([FromBody] CardPaymentDto dto)
        {
            var result=await _paymentService.ProcessCardPaymentDtoAsync(dto);
            
            return Ok(result);
        }
        [HttpPost("upi")]
        public async Task<ActionResult> PayUpi([FromBody]upiPaymentDto dto)
        {
            var result=await _paymentService.ProcessUpiPaymentDtoAsync(dto);
            return Ok(result);
        }
        [HttpGet("Success")]
        public ActionResult Success()
        {
            return Ok("Payment done");
                
        }
    }
}

