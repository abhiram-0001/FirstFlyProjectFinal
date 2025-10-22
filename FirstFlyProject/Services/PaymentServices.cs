using Azure.Core;
using FirstFlyProject.Data;
using FirstFlyProject.Entities;
using FirstFlyProject.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace FirstFlyProject.Services
{
    public class PaymentServices : IPaymentServices
    {
        private readonly ApplicationDbContext _context;
        public PaymentServices(ApplicationDbContext context)=>_context = context;
        public async Task<PaymentResultDto> ProcessCardPaymentDtoAsync(CardPaymentDto req)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b=>b.BookingId==req.BookingId&&b.UserId==req.UserId);
            if (booking == null) return Fail("Booking not found","Card");
            var normalizedcard = NormalizeCardNumber(req.CardNumber);
            if (!IsValidExpiry(req.ExpiryMonth, req.ExpiryYear))
            {
                return Fail("Card expired", "Card");
            }
            if (normalizedcard.Length < 16)
            {
                return Fail("Invalid card number","Card");
            }
            if (string.IsNullOrEmpty(req.CVV))
            {
                return Fail("Invalid CVV","Card");
            }
            var payment = new Payment
            {
                UserId = req.UserId,
                BookingId = req.BookingId,
                Amount = req.Amount,
                Status="Success",
                PaymentMethod="Card"
            };
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            if (req.SaveCard)
            {
                await SaveCardAsync(req.UserId,
                    normalizedcard,req.CardHolderName,req.ExpiryMonth,req.ExpiryYear);
            }

            var insurance = await _context.Insurances.FirstOrDefaultAsync(i => i.BookingId == req.BookingId);
            if (insurance != null && insurance.Status == "Pending Payment")
            {
                insurance.Status = "Active";
                await _context.SaveChangesAsync();
            }

            return new PaymentResultDto
            {
                success=true,
                paymentId=payment.PaymentId,
                Status=payment.Status,
                message="Payment Success",
                paymentMethod=payment.PaymentMethod
            };
        }

        public async Task<PaymentResultDto> ProcessUpiPaymentDtoAsync(upiPaymentDto req)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == req.BookingId && b.UserId == req.UserId);


            if (booking == null) return Fail("Booking Not Found","UPI");
            var upiId = NormalizeUpiId(req.UpiId);
            if (IsValidUpi(upiId))
                return Fail("Invalid Upi ID","UPI");

            var payment = new Payment
            {
                UserId = req.UserId,
                BookingId = req.BookingId,
                Amount = req.Amount,
                Status = "Success",
                PaymentMethod = "UPI"
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            var insurance = await _context.Insurances.FirstOrDefaultAsync(i => i.BookingId == req.BookingId);
            if (insurance != null && insurance.Status == "Pending Payment")
            {
                insurance.Status = "Active";
                await _context.SaveChangesAsync();
            }

            return new PaymentResultDto
            {
                success=true,
                paymentId = payment.PaymentId,
                Status = payment.Status,
                message="Payment Success",
                paymentMethod=payment.PaymentMethod
            };
        }


        public async Task<CardDetail> SaveCardAsync(int userId, string cardnumber, string holdername, string expirymonth, string expiryyear)
        {
            var card = await _context.CardDetail.FirstOrDefaultAsync(c=>c.UserId==userId&&c.CardNumber==cardnumber);
            if (card == null)
            {
                card = new CardDetail
                {
                    UserId=userId,
                    CardNumber=cardnumber,
                    CardHolderName=holdername,
                    ExpiryMonth=expirymonth,
                    ExpiryYear=expiryyear
                };
                _context.CardDetail.Add(card);
                await _context.SaveChangesAsync();
            }
            return card;
        }
        public async Task<IEnumerable<CardDetail>> GetSavedCardsAsync(int userId)
        {

            var cards = await _context.CardDetail.Where(c => c.UserId == userId).OrderByDescending(c=>c.CardDetailId).ToListAsync();
            return cards;
        }

        private static PaymentResultDto Fail(string message, string method) =>
                new PaymentResultDto { success = false, Status = "Failed", message = message, paymentMethod = method };


        private  string NormalizeCardNumber(string card)
        {
            return new string(card?.Where(char.IsDigit).ToArray() ?? Array.Empty<char>());
        }

        private static bool IsValidExpiry(string month, string year)
        {
            if (!Regex.IsMatch(month ?? "", "^(0[1-9]|1[0-2])$"))
                return false;
            if (!Regex.IsMatch(year ?? "", "^\\d{2}$|^\\d{4}$"))
                return false;

            if (!int.TryParse(month, out int m)) return false;

            if (!int.TryParse(year, out int y)) return false;
            y = (year.Length == 2) ? 2000 + y : y;

            if (y < 2025) return false;
            

            
            var lastDay = DateTime.DaysInMonth(y, m);
            var expiryDate = new DateTime(y, m, lastDay, 23, 59, 59, DateTimeKind.Utc);
            return expiryDate >= DateTime.UtcNow;
        }

        private string NormalizeUpiId(string upi) => upi?.Trim().ToLowerInvariant();

        private bool IsValidUpi(string upi)
        {
            
            return !string.IsNullOrWhiteSpace(upi)
                   && Regex.IsMatch(upi, @"^[a-z0-9.\-_]{2,}@[a-z]$", RegexOptions.IgnoreCase);
        }

    }   
}
