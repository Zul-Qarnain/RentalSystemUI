using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using RentalSystemUI.Models;

namespace RentalSystemUI.Data
{
    public class MessageRepository : Database
    {
        /// <summary>
        /// Get all conversations for a user (grouped by property + other user)
        /// </summary>
        public List<Conversation> GetConversations(int userId)
        {
            var list = new List<Conversation>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"
                    WITH LatestMessages AS (
                        SELECT 
                            m.PropertyID,
                            CASE WHEN m.SenderID = @uid THEN m.ReceiverID ELSE m.SenderID END AS OtherUserID,
                            m.Content,
                            m.CreatedAt,
                            m.IsRead,
                            ROW_NUMBER() OVER (
                                PARTITION BY m.PropertyID, 
                                CASE WHEN m.SenderID = @uid THEN m.ReceiverID ELSE m.SenderID END 
                                ORDER BY m.CreatedAt DESC
                            ) AS rn
                        FROM MESSAGES m
                        WHERE m.SenderID = @uid OR m.ReceiverID = @uid
                    )
                    SELECT 
                        lm.PropertyID,
                        p.Title AS PropertyTitle,
                        lm.OtherUserID,
                        u.FullName AS OtherUserName,
                        lm.Content AS LastMessage,
                        lm.CreatedAt AS LastMessageTime,
                        (SELECT COUNT(*) FROM MESSAGES m2 
                         WHERE m2.ReceiverID = @uid 
                           AND m2.PropertyID = lm.PropertyID 
                           AND (m2.SenderID = lm.OtherUserID)
                           AND m2.IsRead = 0) AS UnreadCount
                    FROM LatestMessages lm
                    JOIN PROPERTIES p ON lm.PropertyID = p.PropertyID
                    JOIN USERS u ON lm.OtherUserID = u.UserID
                    WHERE lm.rn = 1
                    ORDER BY lm.CreatedAt DESC";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Conversation
                            {
                                PropertyID = (int)reader["PropertyID"],
                                PropertyTitle = reader["PropertyTitle"].ToString() ?? "",
                                OtherUserID = (int)reader["OtherUserID"],
                                OtherUserName = reader["OtherUserName"].ToString() ?? "",
                                LastMessage = reader["LastMessage"].ToString() ?? "",
                                LastMessageTime = (DateTime)reader["LastMessageTime"],
                                UnreadCount = (int)reader["UnreadCount"]
                            });
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Get all messages between current user and another user for a specific property
        /// </summary>
        public List<Models.Message> GetMessages(int currentUserId, int otherUserId, int propertyId)
        {
            var list = new List<Models.Message>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT m.*, 
                           s.FullName AS SenderName, 
                           r.FullName AS ReceiverName,
                           p.Title AS PropertyTitle
                    FROM MESSAGES m
                    JOIN USERS s ON m.SenderID = s.UserID
                    JOIN USERS r ON m.ReceiverID = r.UserID
                    JOIN PROPERTIES p ON m.PropertyID = p.PropertyID
                    WHERE m.PropertyID = @pid
                      AND ((m.SenderID = @uid AND m.ReceiverID = @oid) 
                           OR (m.SenderID = @oid AND m.ReceiverID = @uid))
                    ORDER BY m.CreatedAt ASC";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@uid", currentUserId);
                    cmd.Parameters.AddWithValue("@oid", otherUserId);
                    cmd.Parameters.AddWithValue("@pid", propertyId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Models.Message
                            {
                                MessageID = (int)reader["MessageID"],
                                SenderID = (int)reader["SenderID"],
                                ReceiverID = (int)reader["ReceiverID"],
                                PropertyID = (int)reader["PropertyID"],
                                BookingID = reader["BookingID"] as int?,
                                Content = reader["Content"].ToString() ?? "",
                                IsRead = (bool)reader["IsRead"],
                                CreatedAt = (DateTime)reader["CreatedAt"],
                                SenderName = reader["SenderName"].ToString() ?? "",
                                ReceiverName = reader["ReceiverName"].ToString() ?? "",
                                PropertyTitle = reader["PropertyTitle"].ToString() ?? ""
                            });
                        }
                    }
                }

                // Mark messages as read
                MarkMessagesAsRead(conn, currentUserId, otherUserId, propertyId);
            }
            return list;
        }

        private void MarkMessagesAsRead(SqlConnection conn, int currentUserId, int otherUserId, int propertyId)
        {
            string update = @"
                UPDATE MESSAGES 
                SET IsRead = 1 
                WHERE ReceiverID = @uid 
                  AND SenderID = @oid 
                  AND PropertyID = @pid 
                  AND IsRead = 0";
            using (var cmd = new SqlCommand(update, conn))
            {
                cmd.Parameters.AddWithValue("@uid", currentUserId);
                cmd.Parameters.AddWithValue("@oid", otherUserId);
                cmd.Parameters.AddWithValue("@pid", propertyId);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Send a new message
        /// </summary>
        public int SendMessage(int senderId, int receiverId, int propertyId, string content, int? bookingId = null)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"
                    INSERT INTO MESSAGES (SenderID, ReceiverID, PropertyID, BookingID, Content)
                    OUTPUT INSERTED.MessageID
                    VALUES (@sid, @rid, @pid, @bid, @content)";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@sid", senderId);
                    cmd.Parameters.AddWithValue("@rid", receiverId);
                    cmd.Parameters.AddWithValue("@pid", propertyId);
                    cmd.Parameters.AddWithValue("@bid", (object?)bookingId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@content", content);
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// Check if user can message about a property (must have approved booking)
        /// </summary>
        public bool CanMessage(int tenantId, int propertyId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT COUNT(*) FROM BOOKINGS 
                    WHERE TenantID = @tid 
                      AND PropertyID = @pid 
                      AND Status = 'Approved'";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tid", tenantId);
                    cmd.Parameters.AddWithValue("@pid", propertyId);
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
        }

        /// <summary>
        /// Get landlord ID for a property
        /// </summary>
        public int? GetLandlordId(int propertyId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT LandlordID FROM PROPERTIES WHERE PropertyID = @pid", conn))
                {
                    cmd.Parameters.AddWithValue("@pid", propertyId);
                    var result = cmd.ExecuteScalar();
                    return result == null ? null : (int)result;
                }
            }
        }

        /// <summary>
        /// Get properties where user can start a conversation (has approved booking)
        /// </summary>
        public List<(int PropertyID, string Title, int LandlordID, string LandlordName)> GetMessageableProperties(int tenantId)
        {
            var list = new List<(int, string, int, string)>();
            using (var conn = GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT DISTINCT p.PropertyID, p.Title, p.LandlordID, u.FullName AS LandlordName
                    FROM BOOKINGS b
                    JOIN PROPERTIES p ON b.PropertyID = p.PropertyID
                    JOIN USERS u ON p.LandlordID = u.UserID
                    WHERE b.TenantID = @tid AND b.Status = 'Approved'";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tid", tenantId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add((
                                (int)reader["PropertyID"],
                                reader["Title"].ToString() ?? "",
                                (int)reader["LandlordID"],
                                reader["LandlordName"].ToString() ?? ""
                            ));
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Get unread message count for a user
        /// </summary>
        public int GetUnreadCount(int userId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand("SELECT COUNT(*) FROM MESSAGES WHERE ReceiverID = @uid AND IsRead = 0", conn))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    return (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
