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

        // --- REQUESTS ---
        public List<RentalSystemUI.Models.Application> GetApplications(int landlordId)
        {
            return _repo.GetApplicationsByLandlord(landlordId);
        }

        public void ApproveApplication(int applicationId)
        {
            // Logic: Approve this one, Reject others for same property? 
            // For now, let's just approve. Complex logic (auto-reject others) would require a transaction or separate calls.
            _repo.UpdateApplicationStatus(applicationId, "Accepted");
        }

        public void RejectApplication(int applicationId)
        {
             _repo.UpdateApplicationStatus(applicationId, "Rejected");
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
