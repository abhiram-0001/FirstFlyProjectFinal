using FirstFlyProject.Entities;
using FirstFlyProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace FirstFlyProject.Services
{
    

    // Define the contract for all package-related business operations.
    public interface IPackageService
    {
        Task<List<PackageSearchRequest>> SearchPackages(string? Destination, int? duration);

        Task<BookingDto> CreateBooking(CreateBookingRequest request, int userId);
    }
}
