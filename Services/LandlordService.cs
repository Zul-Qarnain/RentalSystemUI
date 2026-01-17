using System;
using System.Collections.Generic;
using RentalSystemUI.Data;
using RentalSystemUI.Models;

namespace RentalSystemUI.Services
{
    public class LandlordService
    {
        private LandlordRepository _repo = new LandlordRepository();
        
        // --- DASHBOARD ---
        public (int TotalProps, int PendingReqs, decimal MonthlyEarnings, int Unpaid) GetStats(int landlordId)
        {
            return _repo.GetDashboardStats(landlordId);
        }

        // --- BOOKINGS (Requests) ---
        public List<BookingWithProperty> GetBookings(int landlordId)
        {
            return _repo.GetBookingsByLandlord(landlordId);
        }

        public void ApproveBooking(int bookingId)
        {
            _repo.UpdateBookingStatus(bookingId, "Approved");
        }

        public void RejectBooking(int bookingId)
        {
            _repo.UpdateBookingStatus(bookingId, "Rejected");
        }

        // --- PAYMENTS ---
        public List<Payment> GetPayments(int landlordId)
        {
            return _repo.GetPaymentsByLandlord(landlordId);
        }

        public void VerifyPayment(int paymentId)
        {
            _repo.UpdatePaymentStatus(paymentId, "Verified");
        }
        
         public void RejectPayment(int paymentId)
        {
            _repo.UpdatePaymentStatus(paymentId, "Rejected");
        }

        // --- REVIEWS ---
        public List<Review> GetReviews(int landlordId)
        {
            return _repo.GetReviewsByLandlord(landlordId);
        }
    }
}
