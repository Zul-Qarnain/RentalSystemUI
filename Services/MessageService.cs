using System.Collections.Generic;
using RentalSystemUI.Data;
using RentalSystemUI.Models;

namespace RentalSystemUI.Services
{
    public class MessageService
    {
        private readonly MessageRepository _repo = new MessageRepository();

        /// <summary>
        /// Get all conversations for a user
        /// </summary>
        public List<Conversation> GetConversations(int userId)
        {
            return _repo.GetConversations(userId);
        }

        /// <summary>
        /// Get all messages in a conversation
        /// </summary>
        public List<Models.Message> GetMessages(int currentUserId, int otherUserId, int propertyId)
        {
            return _repo.GetMessages(currentUserId, otherUserId, propertyId);
        }

        /// <summary>
        /// Send a message (with permission check)
        /// </summary>
        public int SendMessage(int senderId, int receiverId, int propertyId, string content)
        {
            // Validate content
            if (string.IsNullOrWhiteSpace(content)) return -1;

            return _repo.SendMessage(senderId, receiverId, propertyId, content.Trim());
        }

        /// <summary>
        /// Check if tenant can message about a property
        /// </summary>
        public bool CanTenantMessage(int tenantId, int propertyId)
        {
            return _repo.CanMessage(tenantId, propertyId);
        }

        /// <summary>
        /// Get properties the tenant can message about
        /// </summary>
        public List<(int PropertyID, string Title, int LandlordID, string LandlordName)> GetMessageableProperties(int tenantId)
        {
            return _repo.GetMessageableProperties(tenantId);
        }

        /// <summary>
        /// Get landlord ID for a property
        /// </summary>
        public int? GetLandlordId(int propertyId)
        {
            return _repo.GetLandlordId(propertyId);
        }

        /// <summary>
        /// Get unread message count
        /// </summary>
        public int GetUnreadCount(int userId)
        {
            return _repo.GetUnreadCount(userId);
        }
    }
}
