using FirstFlyProject.Entities;
using FirstFlyProject.Models;

namespace FirstFlyProject.Services
{
    public interface IPaymentServices
    {
        Task<PaymentResultDto> ProcessCardPaymentDtoAsync(CardPaymentDto req);
        Task<PaymentResultDto> ProcessUpiPaymentDtoAsync(upiPaymentDto req);
        Task<CardDetail> SaveCardAsync(int userId,
            string cardnumber,string holdername,string expirymonth,string expiryyear);

        Task<IEnumerable<CardDetail>> GetSavedCardsAsync(int userId);
    }
}
