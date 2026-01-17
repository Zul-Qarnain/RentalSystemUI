using RentalSystemUI.Models;

namespace RentalSystemUI.Services
{
    public static class AppSession
    {
        public static User? CurrentUser { get; private set; }

        public static void SetUser(User? user)
        {
            CurrentUser = user;
        }

        public static void Clear()
        {
            CurrentUser = null;
        }
    }
}
