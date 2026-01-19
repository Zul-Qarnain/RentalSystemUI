using System;
using System.Collections.Generic;
using RentalSystemUI.Data;
using RentalSystemUI.Models;

namespace RentalSystemUI.Services
{
    public class LandlordService
    {
        private LandlordRepository _repo = new LandlordRepository();
        private readonly NotificationService _notifications = new NotificationService();
         
        // --- DASHBOARD ---
        public (int TotalProps, int PendingReqs, decimal MonthlyEarnings, int Unpaid) GetStats(int landlordId)
        {
            return _repo.GetDashboardStats(landlordId);
        }

        /// <summary>
        /// Gets comprehensive dashboard stats for the landlord:
        /// Total properties, total earnings, approved tenants, pending requests, occupancy %
        /// </summary>
        public (int TotalProps, decimal TotalEarnings, int ApprovedTenants, int PendingReqs, int OccupancyPercent) GetComprehensiveStats(int landlordId)
        {
            return _repo.GetComprehensiveStats(landlordId);
        }

        // --- BOOKINGS (Requests) ---
        public List<BookingWithProperty> GetBookings(int landlordId)
        {
            return _repo.GetBookingsByLandlord(landlordId);
        }

        public void ApproveBooking(int bookingId)
        {
            _repo.UpdateBookingStatus(bookingId, "Approved");

// Notify tenant about booking approval
            var info = _repo.GetBookingNotificationInfo(bookingId);
            if (info.HasValue)
            {
                _notifications.Notify(info.Value.TenantId, "Booking Approved", $"Your booking for '{info.Value.PropertyTitle}' has been approved.");
            }
        }

        public void RejectBooking(int bookingId)
        {
            _repo.UpdateBookingStatus(bookingId, "Rejected");

// Notify tenant about booking rejection
            var info = _repo.GetBookingNotificationInfo(bookingId);
            if (info.HasValue)
            {
                _notifications.Notify(info.Value.TenantId, "Booking Rejected", $"Your booking for '{info.Value.PropertyTitle}' has been rejected.");
            }
        }

        public void TerminateBooking(int bookingId)
        {
            _repo.UpdateBookingStatus(bookingId, "Terminated");

// Notify tenant about booking termination
            var info = _repo.GetBookingNotificationInfo(bookingId);
            if (info.HasValue)
            {
                _notifications.Notify(info.Value.TenantId, "Booking Terminated", $"Your booking for '{info.Value.PropertyTitle}' has been terminated by the landlord.");
            }
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

        public void ReplyToReview(int reviewId, string reply)
        {
            _repo.ReplyToReview(reviewId, reply);
        }
    }
}
