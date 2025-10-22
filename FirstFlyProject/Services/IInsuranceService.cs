using FirstFlyProject.Entities;
using FirstFlyProject.Models;

namespace FirstFlyProject.Services
{
    public interface IInsuranceService
    {
        Task<InsuranceOptionDto> GetInsuranceOptionAsync(int packageId,int numofpeople);
        Task<InsuranceResultDto> AttachInsuranceToBookingAsync(InsuranceSelectionRequest request);
        Task<Insurance> GetInsuranceByBookingAsync(int bookingid);
    }
}
