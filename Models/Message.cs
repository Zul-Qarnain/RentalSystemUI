namespace RentalSystemUI.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
        public int PropertyID { get; set; }
        public int? BookingID { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public System.DateTime CreatedAt { get; set; }

        // Display properties (populated via joins)
        public string SenderName { get; set; } = string.Empty;
        public string ReceiverName { get; set; } = string.Empty;
        public string PropertyTitle { get; set; } = string.Empty;
    }

    /// <summary>
    /// Represents a conversation thread between a tenant and landlord about a property
    /// </summary>
    public class Conversation
    {
        public int PropertyID { get; set; }
        public string PropertyTitle { get; set; } = string.Empty;
        public int OtherUserID { get; set; }
        public string OtherUserName { get; set; } = string.Empty;
        public string LastMessage { get; set; } = string.Empty;
        public System.DateTime LastMessageTime { get; set; }
        public int UnreadCount { get; set; }
    }
}
