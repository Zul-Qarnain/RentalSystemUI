using System.Collections.Generic;
using System.Linq;
using RentalSystemUI.Data;
using RentalSystemUI.Models;

namespace RentalSystemUI.Services
{
    // BUSINESS LOGIC LAYER: Handles operations related to Tenants, acting as a middleman between UI and Data.
    public class TenantService
    {
        private readonly TenantRepository _repo = new TenantRepository();

        // Retrieves the list of rentals specifically for the logged-in tenant.
        public List<TenantRental> GetRentals(int tenantId)
        {
            return _repo.GetRentalsByTenant(tenantId);
        }

        public bool CancelRental(int bookingId, int tenantId)
        {
            return _repo.CancelRental(bookingId, tenantId);
        }

        // Validates and processes a new payment record.
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

        public List<Review> GetReviewsByTenant(int tenantId)
        {
            return _repo.GetReviewsByTenant(tenantId);
        }

        public bool CreateReview(int propertyId, int tenantId, int rating, string comment)
        {
            return _repo.CreateReview(propertyId, tenantId, rating, comment);
        }

        public bool UpdateReview(int reviewId, int rating, string comment)
        {
            return _repo.UpdateReview(reviewId, rating, comment);
        }

        public bool RequestRefund(int bookingId, string reason)
        {
            return _repo.RequestRefund(bookingId, reason);
        }

        public string GetRefundStatus(int bookingId)
        {
            return _repo.GetRefundStatus(bookingId);
        }
    }
}
