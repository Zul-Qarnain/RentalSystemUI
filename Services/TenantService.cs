using System.Collections.Generic;
using System.Linq;
using RentalSystemUI.Data;
using RentalSystemUI.Models;

namespace RentalSystemUI.Services
{
    public class TenantService
    {
        private readonly TenantRepository _repo = new TenantRepository();

        public List<TenantRental> GetRentals(int tenantId)
        {
            return _repo.GetRentalsByTenant(tenantId);
        }

        public bool CancelRental(int bookingId, int tenantId)
        {
            return _repo.CancelRental(bookingId, tenantId);
        }

        public int CreatePayment(int tenantId, int propertyId, decimal amount, DateTime dueDate, string paymentMethod, string transactionId)
        {
            return _repo.CreatePayment(tenantId, propertyId, amount, dueDate, paymentMethod, transactionId);
        }

        public int CreateBooking(int tenantId, int propertyId, DateTime startDate, DateTime endDate)
        {
            return _repo.CreateBooking(tenantId, propertyId, startDate, endDate);
        }

        public List<BookingWithProperty> GetBookingsByTenant(int tenantId)
        {
            return _repo.GetBookingsByTenant(tenantId);
        }

        public List<BookingWithProperty> GetApprovedUnpaidBookings(int tenantId)
        {
            return _repo.GetApprovedUnpaidBookings(tenantId);
        }

        public int CreatePaymentForBooking(int bookingId, decimal amount, string paymentMethod, string transactionId)
        {
            return _repo.CreatePaymentForBooking(bookingId, amount, paymentMethod, transactionId);
        }

        public List<Payment> GetPayments(int tenantId)
        {
            return _repo.GetPaymentsByTenant(tenantId);
        }

        public (int PaidCount, int UnpaidCount, decimal PaidTotal, decimal UnpaidTotal) GetPaymentSummary(int tenantId)
        {
            var list = GetPayments(tenantId);
            var paid = list.Where(p => p.Status == "Verified" || p.Status == "Paid").ToList();
            var unpaid = list.Where(p => p.Status == "Pending" || p.Status == "Overdue").ToList();

            return (
                paid.Count,
                unpaid.Count,
                paid.Sum(p => p.Amount),
                unpaid.Sum(p => p.Amount)
            );
        }
    }
}
