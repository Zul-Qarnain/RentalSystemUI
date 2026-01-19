using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using RentalSystemUI.Models;

namespace RentalSystemUI.Data
{
    public class NotificationRepository : Database
    {
        public int Insert(int userId, string title, string message)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(@"
                    INSERT INTO NOTIFICATIONS (UserID, Title, Message, IsRead, CreatedAt)
                    OUTPUT INSERTED.NotificationID
                    VALUES (@uid, @title, @msg, 0, GETDATE())", conn))
                    {
                        cmd.Parameters.AddWithValue("@uid", userId);
                        cmd.Parameters.AddWithValue("@title", title ?? string.Empty);
                        cmd.Parameters.AddWithValue("@msg", message ?? string.Empty);
                        return (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch
            {
                return -1;
            }
        }

        public List<Notification> GetLatest(int userId, int take = 20)
        {
            var list = new List<Notification>();
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(@"
                    SELECT TOP (@take) NotificationID, UserID, Title, Message, IsRead, CreatedAt
                    FROM NOTIFICATIONS
                    WHERE UserID=@uid
                    ORDER BY CreatedAt DESC", conn))
                    {
                        cmd.Parameters.AddWithValue("@uid", userId);
                        cmd.Parameters.AddWithValue("@take", take);
                        using (var r = cmd.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                list.Add(new Notification
                                {
                                    NotificationID = (int)r["NotificationID"],
                                    UserID = (int)r["UserID"],
                                    Title = r["Title"].ToString() ?? string.Empty,
                                    Message = r["Message"].ToString() ?? string.Empty,
                                    IsRead = r["IsRead"] != DBNull.Value && (bool)r["IsRead"],
                                    CreatedAt = (DateTime)r["CreatedAt"]
                                });
                            }
                        }
                    }
                }
            }
            catch
            {
                // Table missing or DB issue; treat as no notifications.
            }
            return list;
        }

        public int GetUnreadCount(int userId)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT COUNT(*) FROM NOTIFICATIONS WHERE UserID=@uid AND IsRead=0", conn))
                    {
                        cmd.Parameters.AddWithValue("@uid", userId);
                        return (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch
            {
                return 0;
            }
        }

        public void MarkAllRead(int userId)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("UPDATE NOTIFICATIONS SET IsRead=1 WHERE UserID=@uid", conn))
                    {
                        cmd.Parameters.AddWithValue("@uid", userId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
            }
        }
    }
}
