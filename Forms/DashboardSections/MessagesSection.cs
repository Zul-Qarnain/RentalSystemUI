using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Data;
using RentalSystemUI.Models;
using RentalSystemUI.Services;

namespace RentalSystemUI.Forms.DashboardSections
{
    public class MessagesSection : Form
    {
        private readonly int _userId;
        private readonly string _userType;
        private readonly MessageService _messageService = new MessageService();
        private readonly TenantService _tenantService = new TenantService();
        private readonly LandlordService _landlordService = new LandlordService();

        private AntdUI.Tabs _tabs = null!;
        private FlowLayoutPanel _flowConversations = null!;
        private FlowLayoutPanel _flowReviews = null!;

        public MessagesSection(int userId = 0)
        {
            _userId = userId > 0 ? userId : (AppSession.CurrentUser?.UserID ?? 0);
            _userType = AppSession.CurrentUser?.UserType ?? "Tenant";

            BackColor = ColorTranslator.FromHtml("#f6f7f8");
            FormBorderStyle = FormBorderStyle.None;
            Dock = DockStyle.Fill;

            InitializeUI();
            LoadData();
        }

        private void InitializeUI()
        {
            // Header
            var lblTitle = new AntdUI.Label
            {
                Text = "Messages",
                Font = Styles.Header,
                ForeColor = Styles.Blue,
                Location = new Point(25, 25),
                AutoSize = true
            };

            var lblSubtitle = new AntdUI.Label
            {
                Text = "Communicate with " + (_userType == "Tenant" ? "homeowners" : "tenants") + " about your properties",
                Font = Styles.SubHeader,
                ForeColor = Styles.TextGray,
                Location = new Point(30, 70),
                AutoSize = true
            };

            // Tabs container
            _tabs = new AntdUI.Tabs
            {
                Location = new Point(25, 110),
                Size = new Size(1100, 650),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                Type = TabType.Card
            };

            // Conversations tab content
            _flowConversations = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            // Reviews tab content
            _flowReviews = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            // Create tab pages
            var tabConversations = new AntdUI.TabPage { Text = "Conversations" };
            tabConversations.Controls.Add(_flowConversations);

            var tabReviews = new AntdUI.TabPage { Text = _userType == "Tenant" ? "My Reviews" : "Property Reviews" };
            tabReviews.Controls.Add(_flowReviews);

            _tabs.Pages.Add(tabConversations);
            _tabs.Pages.Add(tabReviews);

            Controls.Add(lblTitle);
            Controls.Add(lblSubtitle);
            Controls.Add(_tabs);
        }

        private void LoadData()
        {
            LoadConversations();
            LoadReviews();
        }

        #region Conversations

        private void LoadConversations()
        {
            _flowConversations.Controls.Clear();

            // Business rule hint
            var hint = new AntdUI.Label
            {
                Text = _userType == "Tenant" 
                    ? "💡 You can only message homeowners of properties you have rented."
                    : "💡 You can reply to messages from tenants who have rented your properties.",
                ForeColor = Styles.TextGray,
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 15)
            };
            _flowConversations.Controls.Add(hint);

            // New conversation button (for tenants only)
            if (_userType == "Tenant")
            {
                var btnNewConversation = new AntdUI.Button
                {
                    Text = "+ New Message",
                    Type = TTypeMini.Primary,
                    Size = new Size(150, 40),
                    Radius = 8,
                    Margin = new Padding(0, 0, 0, 15)
                };
                btnNewConversation.Click += OnNewConversationClick;
                _flowConversations.Controls.Add(btnNewConversation);
            }

            var conversations = _messageService.GetConversations(_userId);

            if (conversations.Count == 0)
            {
                _flowConversations.Controls.Add(new AntdUI.Label
                {
                    Text = "No conversations yet.",
                    ForeColor = Styles.TextGray,
                    Font = Styles.SubHeader,
                    AutoSize = true,
                    Margin = new Padding(0, 20, 0, 0)
                });
                return;
            }

            foreach (var conv in conversations)
            {
                _flowConversations.Controls.Add(CreateConversationCard(conv));
            }
        }

        private Control CreateConversationCard(Conversation conv)
        {
            var card = new AntdUI.Panel
            {
                Width = 1050,
                Height = 90,
                Radius = 12,
                Shadow = 3,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 10),
                Cursor = Cursors.Hand
            };

            // Unread indicator
            if (conv.UnreadCount > 0)
            {
                card.Controls.Add(new System.Windows.Forms.Panel
                {
                    Dock = DockStyle.Left,
                    Width = 5,
                    BackColor = Styles.Blue
                });
            }

            var lblName = new AntdUI.Label
            {
                Text = conv.OtherUserName,
                Font = Styles.CardTitle,
                ForeColor = Styles.DarkBlue,
                Location = new Point(20, 15),
                AutoSize = true
            };

            var lblProperty = new AntdUI.Label
            {
                Text = $"Re: {conv.PropertyTitle}",
                Font = Styles.Small,
                ForeColor = Styles.Blue,
                Location = new Point(20, 40),
                AutoSize = true
            };

            var lblPreview = new AntdUI.Label
            {
                Text = conv.LastMessage.Length > 60 ? conv.LastMessage.Substring(0, 60) + "..." : conv.LastMessage,
                Font = Styles.Normal,
                ForeColor = Styles.TextGray,
                Location = new Point(20, 60),
                AutoSize = true
            };

            var lblTime = new AntdUI.Label
            {
                Text = FormatTime(conv.LastMessageTime),
                Font = Styles.Small,
                ForeColor = Styles.TextGray,
                Location = new Point(card.Width - 100, 15),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            if (conv.UnreadCount > 0)
            {
                var badge = new AntdUI.Button
                {
                    Text = conv.UnreadCount.ToString(),
                    BackColor = Styles.Blue,
                    ForeColor = Color.White,
                    Size = new Size(28, 28),
                    Radius = 14,
                    Location = new Point(card.Width - 50, 35),
                    BorderWidth = 0,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right
                };
                card.Controls.Add(badge);
            }

            card.Click += (s, e) => OpenConversation(conv.OtherUserID, conv.PropertyID, conv.PropertyTitle, conv.OtherUserName);
            lblName.Click += (s, e) => OpenConversation(conv.OtherUserID, conv.PropertyID, conv.PropertyTitle, conv.OtherUserName);
            lblProperty.Click += (s, e) => OpenConversation(conv.OtherUserID, conv.PropertyID, conv.PropertyTitle, conv.OtherUserName);
            lblPreview.Click += (s, e) => OpenConversation(conv.OtherUserID, conv.PropertyID, conv.PropertyTitle, conv.OtherUserName);

            card.Controls.Add(lblTime);
            card.Controls.Add(lblPreview);
            card.Controls.Add(lblProperty);
            card.Controls.Add(lblName);

            return card;
        }

        private void OnNewConversationClick(object? sender, EventArgs e)
        {
            var properties = _messageService.GetMessageableProperties(_userId);
            if (properties.Count == 0)
            {
                AntdUI.Message.warn(this, "You can only message homeowners of properties you have rented.");
                return;
            }

            // Show property selection dialog
            using (var dialog = new SelectPropertyDialog(properties))
            {
                if (dialog.ShowDialog() == DialogResult.OK && dialog.SelectedProperty != null)
                {
                    var prop = dialog.SelectedProperty.Value;
                    OpenConversation(prop.LandlordID, prop.PropertyID, prop.Title, prop.LandlordName);
                }
            }
        }

        private void OpenConversation(int otherUserId, int propertyId, string propertyTitle, string otherUserName)
        {
            using (var chatDialog = new ChatDialog(_userId, otherUserId, propertyId, propertyTitle, otherUserName))
            {
                chatDialog.ShowDialog(this);
            }
            LoadConversations(); // Refresh after closing
        }

        #endregion

        #region Reviews

        private void LoadReviews()
        {
            _flowReviews.Controls.Clear();

            if (_userType == "Tenant")
            {
                LoadTenantReviews();
            }
            else
            {
                LoadLandlordReviews();
            }
        }

        private void LoadTenantReviews()
        {
            // Hint
            _flowReviews.Controls.Add(new AntdUI.Label
            {
                Text = "💡 You can only review properties you have booked before.",
                ForeColor = Styles.TextGray,
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 15)
            });

            // Get bookable properties for review
            var rentals = _tenantService.GetRentals(_userId);
            var existingReviews = _tenantService.GetReviewsByTenant(_userId);
            var reviewedPropertyIds = new HashSet<int>();
            foreach (var r in existingReviews) reviewedPropertyIds.Add(r.PropertyID);

            // Show existing reviews
            if (existingReviews.Count > 0)
            {
                _flowReviews.Controls.Add(new AntdUI.Label
                {
                    Text = "Your Reviews",
                    Font = Styles.CardTitle,
                    ForeColor = Styles.DarkBlue,
                    AutoSize = true,
                    Margin = new Padding(0, 10, 0, 10)
                });

                foreach (var review in existingReviews)
                {
                    _flowReviews.Controls.Add(CreateTenantReviewCard(review));
                }
            }

            // Show properties that can be reviewed
            var reviewable = new List<TenantRental>();
            foreach (var r in rentals)
            {
                if (!reviewedPropertyIds.Contains(r.PropertyId))
                    reviewable.Add(r);
            }

            if (reviewable.Count > 0)
            {
                _flowReviews.Controls.Add(new AntdUI.Label
                {
                    Text = "Write a Review",
                    Font = Styles.CardTitle,
                    ForeColor = Styles.DarkBlue,
                    AutoSize = true,
                    Margin = new Padding(0, 20, 0, 10)
                });

                foreach (var rental in reviewable)
                {
                    _flowReviews.Controls.Add(CreateReviewablePropertyCard(rental));
                }
            }

            if (existingReviews.Count == 0 && reviewable.Count == 0)
            {
                _flowReviews.Controls.Add(new AntdUI.Label
                {
                    Text = "No reviews yet. Book a property to leave a review!",
                    ForeColor = Styles.TextGray,
                    Font = Styles.SubHeader,
                    AutoSize = true,
                    Margin = new Padding(0, 20, 0, 0)
                });
            }
        }

        private Control CreateTenantReviewCard(Review review)
        {
            var card = new AntdUI.Panel
            {
                Width = 1050,
                Height = 100,
                Radius = 12,
                Shadow = 3,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 10)
            };

            string stars = new string('★', review.Rating) + new string('☆', 5 - review.Rating);

            var lblProperty = new AntdUI.Label
            {
                Text = review.PropertyTitle,
                Font = Styles.CardTitle,
                ForeColor = Styles.DarkBlue,
                Location = new Point(20, 15),
                MaximumSize = new Size(400, 0),
                AutoEllipsis = true,
                AutoSize = true
            };

            var lblStars = new AntdUI.Label
            {
                Text = stars,
                Font = new Font("Segoe UI", 14),
                ForeColor = Color.Gold,
                Location = new Point(20, 45),
                AutoSize = true
            };

            var lblComment = new AntdUI.Label
            {
                Text = review.Comment ?? "",
                Font = Styles.Normal,
                ForeColor = Styles.TextGray,
                Location = new Point(200, 50),
                MaximumSize = new Size(600, 0),
                AutoEllipsis = true,
                AutoSize = true
            };

            var lblDate = new AntdUI.Label
            {
                Text = review.CreatedAt.ToShortDateString(),
                Font = Styles.Small,
                ForeColor = Styles.TextGray,
                Location = new Point(card.Width - 100, 15),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            var btnEdit = new AntdUI.Button
            {
                Text = "Edit",
                Type = TTypeMini.Default,
                Ghost = true,
                Size = new Size(60, 30),
                Location = new Point(card.Width - 90, 45),
                Radius = 6,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnEdit.Click += (s, e) =>
            {
                using (var dlg = new WriteReviewDialog(review, _userId))
                {
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        LoadReviews();
                    }
                }
            };

            card.Controls.Add(btnEdit);
            card.Controls.Add(lblDate);
            card.Controls.Add(lblComment);
            card.Controls.Add(lblStars);
            card.Controls.Add(lblProperty);

            if (!string.IsNullOrEmpty(review.Reply))
            {
                card.Height = 160;
                var lblReply = new AntdUI.Label
                {
                    Text = $"Landlord Reply: {review.Reply}",
                    ForeColor = Styles.Blue,
                    Font = new Font("Segoe UI", 9, FontStyle.Regular),
                    Location = new Point(20, 100),
                    Size = new Size(card.Width - 40, 50),
                    AutoEllipsis = true
                };
                card.Controls.Add(lblReply);
            }

            return card;
        }

        private Control CreateReviewablePropertyCard(TenantRental rental)
        {
            var card = new AntdUI.Panel
            {
                Width = 1050,
                Height = 80,
                Radius = 12,
                Shadow = 3,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 10)
            };

            var lblTitle = new AntdUI.Label
            {
                Text = rental.PropertyTitle,
                Font = Styles.CardTitle,
                ForeColor = Styles.DarkBlue,
                Location = new Point(20, 15),
                MaximumSize = new Size(400, 0),
                AutoEllipsis = true,
                AutoSize = true
            };

            var lblAddress = new AntdUI.Label
            {
                Text = rental.PropertyAddress,
                Font = Styles.Small,
                ForeColor = Styles.TextGray,
                Location = new Point(20, 45),
                MaximumSize = new Size(400, 0),
                AutoEllipsis = true,
                AutoSize = true
            };

            var btnReview = new AntdUI.Button
            {
                Text = "Write Review",
                Type = TTypeMini.Primary,
                Ghost = true,
                Size = new Size(120, 36),
                Location = new Point(card.Width - 150, 22),
                Radius = 8,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnReview.Click += (s, e) => OpenWriteReviewDialog(rental.PropertyId, rental.PropertyTitle);

            card.Controls.Add(btnReview);
            card.Controls.Add(lblAddress);
            card.Controls.Add(lblTitle);

            return card;
        }

        private void OpenWriteReviewDialog(int propertyId, string propertyTitle)
        {
            using (var dialog = new WriteReviewDialog(propertyId, propertyTitle, _userId))
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    AntdUI.Message.success(this, "Review submitted successfully!");
                    LoadReviews();
                }
            }
        }

        private void LoadLandlordReviews()
        {
            var reviews = _landlordService.GetReviews(_userId);

            if (reviews.Count == 0)
            {
                _flowReviews.Controls.Add(new AntdUI.Label
                {
                    Text = "No reviews received yet.",
                    ForeColor = Styles.TextGray,
                    Font = Styles.SubHeader,
                    AutoSize = true,
                    Margin = new Padding(0, 20, 0, 0)
                });
                return;
            }

            foreach (var review in reviews)
            {
                _flowReviews.Controls.Add(CreateLandlordReviewCard(review));
            }
        }

        private Control CreateLandlordReviewCard(Review review)
        {
            var card = new AntdUI.Panel
            {
                Width = 1050,
                Height = 130,
                Radius = 12,
                Shadow = 3,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 10)
            };

            string stars = new string('★', review.Rating) + new string('☆', 5 - review.Rating);

            var lblStars = new AntdUI.Label
            {
                Text = stars,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.Gold,
                Location = new Point(20, 15),
                AutoSize = true
            };

            var lblTenant = new AntdUI.Label
            {
                Text = review.TenantName,
                Font = Styles.Bold,
                ForeColor = Styles.DarkBlue,
                Location = new Point(200, 20),
                AutoSize = true
            };

            var lblProperty = new AntdUI.Label
            {
                Text = "on " + review.PropertyTitle,
                Font = Styles.Small,
                ForeColor = Styles.Blue,
                Location = new Point(200, 45),
                MaximumSize = new Size(400, 0),
                AutoEllipsis = true,
                AutoSize = true
            };

            var lblComment = new AntdUI.Label
            {
                Text = review.Comment ?? "",
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = Styles.TextGray,
                Location = new Point(20, 55),
                MaximumSize = new Size(900, 40),
                AutoEllipsis = true,
                AutoSize = true
            };

            var lblDate = new AntdUI.Label
            {
                Text = review.CreatedAt.ToShortDateString(),
                Font = Styles.Small,
                ForeColor = Styles.TextGray,
                Location = new Point(card.Width - 100, 20),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            card.Controls.Add(lblDate);
            card.Controls.Add(lblComment);
            card.Controls.Add(lblProperty);
            card.Controls.Add(lblTenant);
            card.Controls.Add(lblStars);

            return card;
        }

        #endregion

        private string FormatTime(DateTime time)
        {
            var diff = DateTime.Now - time;
            if (diff.TotalMinutes < 60) return $"{(int)diff.TotalMinutes}m ago";
            if (diff.TotalHours < 24) return $"{(int)diff.TotalHours}h ago";
            if (diff.TotalDays < 7) return $"{(int)diff.TotalDays}d ago";
            return time.ToShortDateString();
        }
    }

    #region Helper Dialogs

    /// <summary>
    /// Dialog for selecting a property to start a new conversation
    /// </summary>
    public class SelectPropertyDialog : Form
    {
        public (int PropertyID, string Title, int LandlordID, string LandlordName)? SelectedProperty { get; private set; }

        public SelectPropertyDialog(List<(int PropertyID, string Title, int LandlordID, string LandlordName)> properties)
        {
            Text = "Select Property";
            Size = new Size(500, 400);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;

            var lblTitle = new System.Windows.Forms.Label
            {
                Text = "Select a property to message about:",
                Font = Styles.CardTitle,
                Location = new Point(20, 20),
                AutoSize = true
            };

            var listBox = new System.Windows.Forms.ListBox
            {
                Location = new Point(20, 60),
                Size = new Size(440, 220),
                Font = Styles.Normal
            };

            foreach (var p in properties)
            {
                listBox.Items.Add(new PropertyListItem(p));
            }

            var btnSelect = new AntdUI.Button
            {
                Text = "Start Conversation",
                Type = TTypeMini.Primary,
                Size = new Size(200, 45),
                Location = new Point(140, 300),
                Radius = 8
            };
            btnSelect.Click += (s, e) =>
            {
                if (listBox.SelectedItem is PropertyListItem item)
                {
                    SelectedProperty = item.Property;
                    DialogResult = DialogResult.OK;
                    Close();
                }
            };

            Controls.Add(lblTitle);
            Controls.Add(listBox);
            Controls.Add(btnSelect);
        }

        private class PropertyListItem
        {
            public (int PropertyID, string Title, int LandlordID, string LandlordName) Property { get; }
            public PropertyListItem((int, string, int, string) prop) => Property = prop;
            public override string ToString() => $"{Property.Title} (Owner: {Property.LandlordName})";
        }
    }

    /// <summary>
    /// Chat dialog for viewing and sending messages
    /// </summary>
    public class ChatDialog : Form
    {
        private readonly int _currentUserId;
        private readonly int _otherUserId;
        private readonly int _propertyId;
        private readonly MessageService _service = new MessageService();
        private FlowLayoutPanel _flowMessages = null!;
        private AntdUI.Input _txtMessage = null!;

        public ChatDialog(int currentUserId, int otherUserId, int propertyId, string propertyTitle, string otherUserName)
        {
            _currentUserId = currentUserId;
            _otherUserId = otherUserId;
            _propertyId = propertyId;

            Text = $"Chat with {otherUserName}";
            Size = new Size(600, 550);
            StartPosition = FormStartPosition.CenterParent;
            BackColor = ColorTranslator.FromHtml("#f6f7f8");

            var lblHeader = new AntdUI.Label
            {
                Text = $"Conversation about: {propertyTitle}",
                Font = Styles.CardTitle,
                ForeColor = Styles.Blue,
                Location = new Point(20, 15),
                AutoSize = true
            };

            _flowMessages = new FlowLayoutPanel
            {
                Location = new Point(20, 50),
                Size = new Size(540, 380),
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.White,
                Padding = new Padding(10)
            };

            _txtMessage = new AntdUI.Input
            {
                Location = new Point(20, 445),
                Size = new Size(430, 45),
                PlaceholderText = "Type your message...",
                Radius = 8
            };

            var btnSend = new AntdUI.Button
            {
                Text = "Send",
                Type = TTypeMini.Primary,
                Size = new Size(90, 45),
                Location = new Point(460, 445),
                Radius = 8
            };
            btnSend.Click += OnSendClick;

            _txtMessage.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter && !e.Shift)
                {
                    e.Handled = true;
                    OnSendClick(null, EventArgs.Empty);
                }
            };

            Controls.Add(lblHeader);
            Controls.Add(_flowMessages);
            Controls.Add(_txtMessage);
            Controls.Add(btnSend);

            Load += (s, e) => LoadMessages();
        }

        private void LoadMessages()
        {
            _flowMessages.Controls.Clear();
            var messages = _service.GetMessages(_currentUserId, _otherUserId, _propertyId);

            foreach (var msg in messages)
            {
                bool isMe = msg.SenderID == _currentUserId;
                _flowMessages.Controls.Add(CreateMessageBubble(msg, isMe));
            }

            // Scroll to bottom only if there are messages
            if (_flowMessages.Controls.Count > 0)
            {
                _flowMessages.ScrollControlIntoView(_flowMessages.Controls[^1]);
            }
            else
            {
                // Show empty state message
                _flowMessages.Controls.Add(new AntdUI.Label
                {
                    Text = "No messages yet. Start the conversation!",
                    ForeColor = Styles.TextGray,
                    Font = Styles.SubHeader,
                    AutoSize = true,
                    Margin = new Padding(10, 20, 0, 0)
                });
            }
        }

        private Control CreateMessageBubble(Models.Message msg, bool isMe)
        {
            var panel = new System.Windows.Forms.Panel
            {
                Width = 520,
                Height = 60,
                Margin = new Padding(0, 5, 0, 5)
            };

            var bubble = new AntdUI.Panel
            {
                BackColor = isMe ? Styles.Blue : Color.White,
                Radius = 12,
                Shadow = 2,
                Size = new Size(Math.Min(400, msg.Content.Length * 8 + 40), 45),
                Location = isMe ? new Point(520 - Math.Min(400, msg.Content.Length * 8 + 40), 0) : new Point(0, 0)
            };

            var lblText = new AntdUI.Label
            {
                Text = msg.Content,
                ForeColor = isMe ? Color.White : Styles.DarkBlue,
                Font = Styles.Normal,
                Location = new Point(10, 12),
                AutoSize = true,
                MaximumSize = new Size(380, 0)
            };

            var lblTime = new System.Windows.Forms.Label
            {
                Text = msg.CreatedAt.ToString("HH:mm"),
                ForeColor = Styles.TextGray,
                Font = Styles.Small,
                Location = new Point(isMe ? bubble.Left - 50 : bubble.Right + 10, 15),
                AutoSize = true
            };

            bubble.Controls.Add(lblText);
            panel.Controls.Add(bubble);
            panel.Controls.Add(lblTime);

            return panel;
        }

        private void OnSendClick(object? sender, EventArgs e)
        {
            var content = _txtMessage.Text?.Trim();
            if (string.IsNullOrEmpty(content)) return;

            _service.SendMessage(_currentUserId, _otherUserId, _propertyId, content);
            _txtMessage.Text = "";
            LoadMessages();
        }
    }

    /// <summary>
    /// Dialog for writing a review
    /// </summary>
    public class WriteReviewDialog : Form
    {
        private readonly int _propertyId;
        private readonly int _tenantId;
        private readonly int? _reviewId;
        private readonly TenantService _service = new TenantService();
        private int _selectedRating = 5;
        private AntdUI.Input _txtComment = null!;
        private System.Windows.Forms.Label[] _stars = new System.Windows.Forms.Label[5];

        // Constructor for NEW review
        public WriteReviewDialog(int propertyId, string propertyTitle, int tenantId)
        {
            _propertyId = propertyId;
            _tenantId = tenantId;
            _reviewId = null;
            InitializeUI(propertyTitle, 5, "");
        }

        // Constructor for EDITing review
        public WriteReviewDialog(Review review, int tenantId)
        {
            _propertyId = review.PropertyID;
            _tenantId = tenantId;
            _reviewId = review.ReviewID;
            InitializeUI(review.PropertyTitle, review.Rating, review.Comment);
        }

        private void InitializeUI(string propertyTitle, int initialRating, string initialComment)
        {
            Text = _reviewId.HasValue ? "Edit Review" : "Write a Review";
            Size = new Size(500, 400);
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            var lblTitle = new AntdUI.Label
            {
                Text = $"{( _reviewId.HasValue ? "Edit Review for" : "Review for" )}: {propertyTitle}",
                Font = Styles.CardTitle,
                ForeColor = Styles.Blue,
                Location = new Point(20, 20),
                AutoSize = true
            };

            var lblRating = new System.Windows.Forms.Label
            {
                Text = "Your Rating:",
                Font = Styles.Bold,
                Location = new Point(20, 70),
                AutoSize = true
            };

            // Star rating
            for (int i = 0; i < 5; i++)
            {
                int starIndex = i;
                _stars[i] = new System.Windows.Forms.Label
                {
                    Text = "★",
                    Font = new Font("Segoe UI", 24),
                    ForeColor = Color.Gold,
                    Location = new Point(20 + i * 40, 95),
                    AutoSize = true,
                    Cursor = Cursors.Hand
                };
                _stars[i].Click += (s, e) => SetRating(starIndex + 1);
                Controls.Add(_stars[i]);
            }

            var lblComment = new System.Windows.Forms.Label
            {
                Text = "Your Review (optional):",
                Font = Styles.Bold,
                Location = new Point(20, 150),
                AutoSize = true
            };

            _txtComment = new AntdUI.Input
            {
                Location = new Point(20, 175),
                Size = new Size(440, 120),
                PlaceholderText = "Share your experience with this property...",
                Multiline = true,
                Text = initialComment
            };

            var btnSubmit = new AntdUI.Button
            {
                Text = _reviewId.HasValue ? "Update Review" : "Submit Review",
                Type = TTypeMini.Primary,
                Size = new Size(150, 45),
                Location = new Point(160, 310),
                Radius = 8
            };
            btnSubmit.Click += OnSubmitClick;

            Controls.Add(lblTitle);
            Controls.Add(lblRating);
            Controls.Add(lblComment);
            Controls.Add(_txtComment);
            Controls.Add(btnSubmit);

            SetRating(initialRating);
        }

        private void SetRating(int rating)
        {
            _selectedRating = rating;
            for (int i = 0; i < 5; i++)
            {
                _stars[i].ForeColor = i < rating ? Color.Gold : Color.LightGray;
            }
        }

        private void OnSubmitClick(object? sender, EventArgs e)
        {
            var comment = _txtComment.Text?.Trim() ?? "";
            
            bool success;
            if (_reviewId.HasValue)
            {
                success = _service.UpdateReview(_reviewId.Value, _selectedRating, comment);
            }
            else
            {
                success = _service.CreateReview(_propertyId, _tenantId, _selectedRating, comment);
            }

            if (success)
            {
                AntdUI.Message.success(this, _reviewId.HasValue ? "Review updated!" : "Review submitted!");
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                AntdUI.Message.error(this, "Operation failed. Please try again.");
            }
        }
    }

    #endregion
}
