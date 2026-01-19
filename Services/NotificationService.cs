using System.Collections.Generic;
using RentalSystemUI.Data;
using RentalSystemUI.Models;

namespace RentalSystemUI.Services
{
    public class NotificationService
    {
        private readonly NotificationRepository _repo = new NotificationRepository();

        public int Notify(int userId, string title, string message)
        {
            return _repo.Insert(userId, title, message);
        }

        public List<Notification> GetLatest(int userId, int take = 20)
        {
            return _repo.GetLatest(userId, take);
        }

        public int GetUnreadCount(int userId)
        {
            return _repo.GetUnreadCount(userId);
        }

        public void MarkAllRead(int userId)
        {
            _repo.MarkAllRead(userId);
        }
    }
}
