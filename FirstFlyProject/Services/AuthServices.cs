using FirstFlyProject.Data;
using FirstFlyProject.Entities;
using FirstFlyProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FirstFlyProject.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;
        public AuthServices(ApplicationDbContext context,IConfiguration config)
        {
            _config = config;
            _context = context; 
        }

        public async Task<string?> LoginAsync(UserDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Email == request.Email);
            if (user == null)
            {
                return null;
            }
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, request.Password)
                == PasswordVerificationResult.Failed)
            {
                return null;
            }
            var result = CreateToken(user);
            return result;
        }

        public async Task<User?> RegisterCustomerAsync(RegDto request)
        {
            if (await _context.Users.AnyAsync(e => e.Email == request.Email))
            {
                return null;
            }
            User user = new User();
            user.Name = request.Name;
            
            user.Email = request.Email;
            var hashed = new PasswordHasher<User>().HashPassword(user, request.Password);
            user.Password = hashed;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        private string CreateToken(User user)

        {

            var claims = new List<Claim>

            {

                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),

                new Claim(ClaimTypes.Name,user.Email),

                new Claim(ClaimTypes.Role,user.Role.ToString())

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("AppSettings:Token")));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokendiscriptor = new JwtSecurityToken(

                    issuer: _config.GetValue<string>("AppSettings:Issuer"),

                    audience: _config.GetValue<string>("AppSettings:Audience"),

                    claims: claims,

                    expires: DateTime.UtcNow.AddDays(1),

                    signingCredentials: creds

                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokendiscriptor);

            return token;

        }

    }
}
