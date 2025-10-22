using FirstFlyProject.Entities;
using FirstFlyProject.Models;
using System.Data;

namespace FirstFlyProject.Services
{
    public interface IAuthServices
    {
        Task<User?> RegisterCustomerAsync(RegDto request);
        Task<string?> LoginAsync(UserDto request);
        
    }
}
