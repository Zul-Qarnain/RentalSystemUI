using System.Collections.Generic;
using System.Data;
using RentalSystemUI.Data;

namespace RentalSystemUI.Services
{
    public class AdminService
    {
        private readonly AdminRepository _repository;

        public AdminService()
        {
            _repository = new AdminRepository();
        }

        public Dictionary<string, object> GetStats()
        {
            return _repository.GetDashboardStats();
        }

        public DataTable GetAllUsers()
        {
            return _repository.GetAllUsers();
        }

        public bool DeleteUser(int userId)
        {
            return _repository.DeleteUser(userId);
        }

        public DataTable GetAllTransactions()
        {
            return _repository.GetAllTransactions();
        }
    }
}
