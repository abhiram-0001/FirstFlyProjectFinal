using FirstFlyProject.Data;
using FirstFlyProject.Entities;
using FirstFlyProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstFlyProject.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly ApplicationDbContext _db;
        public InsuranceService(ApplicationDbContext db)=>_db=db;

        public async Task<InsuranceOptionDto> GetInsuranceOptionAsync(int packageId, int numofpeople)
        {
            var option = new InsuranceOptionDto
            {
                Provider="DemoInsurance",
                CoverageDetails= "Medical ₹5L, Baggage ₹50K, Delay ₹10K",
                PremiumPerPerson=50.00m
            };
            return option;
        }
        public async Task<InsuranceResultDto> AttachInsuranceToBookingAsync(InsuranceSelectionRequest request)
        {
            var booking=await _db.Bookings.FirstOrDefaultAsync(b=>
                                           b.BookingId==request.BookingId&&b.UserId==request.UserId);
            if (booking == null)
            {
                return new InsuranceResultDto { Success=false,Message="booking or user not found"};
            }
            var option=await GetInsuranceOptionAsync(booking.PackageId,request.NumberOfPeople);

            var premiumtotal = request.IncludeInsurance ? 
                                option.PremiumPerPerson * request.NumberOfPeople : 0m;

            var existing = await _db.Insurances.FirstOrDefaultAsync(i=>i.BookingId==request.BookingId);
            if (!request.IncludeInsurance)
            {
                if (existing != null)
                {
                    existing.Status = "Cancelled";
                    await _db.SaveChangesAsync();
                }
                return new InsuranceResultDto
                {
                    Success=true,
                    InsuranceId=existing?.InsuranceId,
                    Status=existing?.Status,
                    Premium=0m,
                    Message="Insurance removed"
                };
            }
            if (existing==null)
            {
                var insurance = new Insurance
                {
                    UserId=request.UserId,
                    BookingId=request.BookingId,
                    Provider=option.Provider,
                    CoverageDetails=option.CoverageDetails,
                    Status=booking.Status=="Paid"?"Active":"Pending Payment",
                    Premium=premiumtotal
                };
                _db.Insurances.Add(insurance);
                await _db.SaveChangesAsync();
                return new InsuranceResultDto
                {
                    Success = true,
                    InsuranceId = insurance.InsuranceId,
                    Status = insurance.Status,
                    Premium = premiumtotal,
                    Message = "Insurance added to booking"
                };
            }
            else
            {
                existing.Provider = option.Provider;
                existing.CoverageDetails= option.CoverageDetails;
                existing.Premium = option.PremiumPerPerson;
                if (booking.Status == "Paid" && existing.Status != "Active")
                {
                    existing.Status = "Active";
                    await _db.SaveChangesAsync();
                }

                return new InsuranceResultDto
                {
                    Success = true,
                    InsuranceId = existing.InsuranceId,
                    Status = existing.Status,
                    Premium = premiumtotal,
                    Message = "Insurance updated."
                };

            }
        }

        public async Task<Insurance> GetInsuranceByBookingAsync(int bookingid)
        {
            return await _db.Insurances.FirstOrDefaultAsync(i=>i.BookingId==bookingid);
        }

        
    }
}
