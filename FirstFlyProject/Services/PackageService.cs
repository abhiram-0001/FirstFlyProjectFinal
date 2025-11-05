using FirstFlyProject.Data;
using FirstFlyProject.Entities;
using FirstFlyProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstFlyProject.Services
{

    // This assumes you have an ApplicationDbContext to interact with the database.
    public class PackageService : IPackageService
    {
        private readonly ApplicationDbContext _context;

        public PackageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PackageSearchRequest>> SearchPackages(string Destination, int NoOfAdult, double? maxprice)
        {

            var query = _context.TravelPackages.AsQueryable();

            if (!string.IsNullOrEmpty(Destination))
            {
                query = query.Where(p => p.Title.Contains(Destination) || p.Description.Contains(Destination));
            }

            if (maxprice.HasValue)
            {
                query = query.Where(p => (p.Price * NoOfAdult) <= maxprice);
            }
            var isEmpty = !query.Any();
            if (isEmpty)
            {
                return null;
            }
            var result = await query.Select(x => new PackageSearchRequest
            {
                PackageId = x.PackageId,
                TravelAgentID = x.TravelAgentID,
                Duration = x.Duration,
                Description = x.Description,
                Title = x.Title,
                TotalGroupPrice = (decimal)(x.Price) * NoOfAdult,


            }).ToListAsync();
            return result;

        }

        public async Task<BookingDto> CreateBooking(CreateBookingRequest request, int userId)
        {
            var package = await _context.TravelPackages.FindAsync(request.PackageID);
            if (package == null)
                throw new Exception("Selected package not found.");

            // Calculate Total Amount
            decimal basePrice =(decimal)package.Price * request.NumberOfPeople;
            decimal insuranceCost = request.IncludeInsurance ? 50.00m * request.NumberOfPeople : 0; // Conceptual cost
            decimal totalAmount = basePrice + insuranceCost;

            // Create new Booking 
            var newBooking = new Booking
            {
                UserId = userId,
                PackageId = request.PackageID,
                StartDate = request.SelectedStartDate,
                EndDate = request.SelectedStartDate.AddDays(package.Duration),
                Status = "Pending Payment"
            };

            _context.Bookings.Add(newBooking);
            await _context.SaveChangesAsync();

            if (request.IncludeInsurance)
            {
                var insurance = new Insurance
                {
                    UserId = userId,
                    BookingId = newBooking.BookingId,
                    Provider = "DemoInsurance",
                    CoverageDetails= "Medical ₹5L, Baggage ₹50K, Trip Delay ₹10K",
                    Premium=insuranceCost,
                    Status="Pending Payment"
                };
                _context.Insurances.Add(insurance);
                await _context.SaveChangesAsync();
            }
            var result = new BookingDto
            {
                BookingId = newBooking.BookingId,
                PackageId=request.PackageID,
                Status=newBooking.Status,
                price=totalAmount
            };
            return result;
        }
    }
}
