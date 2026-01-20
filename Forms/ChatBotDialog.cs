using System;
using System.Drawing;
using System.Windows.Forms;
using RentalSystemUI.Services;
using AntdUI;
using Panel = System.Windows.Forms.Panel;

namespace RentalSystemUI.Forms
{
    public class ChatBotDialog : Window
    {
        private FlowLayoutPanel flowMessages;
        private AntdUI.Input txtInput;
        private AntdUI.Button btnSend;
        private AIService _aiService;
        private bool _isThinking = false;

        public ChatBotDialog()
        {
            _aiService = new AIService();

            // Form Settings
            Text = "AI Assistant";
            Size = new Size(400, 600);
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            
            // Header
            var header = new System.Windows.Forms.Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.White };
            var lblTitle = new AntdUI.Label { Text = "Ask AI Assistant", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 12, FontStyle.Bold), TextAlign = ContentAlignment.MiddleCenter };
            
            var btnClose = new AntdUI.Button
            {
                Text = "X",
                Dock = DockStyle.Right,
                Width = 50,
                Type = TTypeMini.Default,
                ForeColor = Color.Red,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnClose.Click += (s, e) => this.Close();

            header.Controls.Add(lblTitle);
            header.Controls.Add(btnClose);
            Controls.Add(header);

            // Drag Support
            bool dragging = false;
            Point dragCursorPoint = Point.Empty;
            Point dragFormPoint = Point.Empty;
            lblTitle.MouseDown += (s, e) => { dragging = true; dragCursorPoint = Cursor.Position; dragFormPoint = Location; };
            lblTitle.MouseMove += (s, e) => { if (dragging) { Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint)); Location = Point.Add(dragFormPoint, new Size(dif)); } };
            lblTitle.MouseUp += (s, e) => dragging = false;

            // Input Area
            var pnlInput = new System.Windows.Forms.Panel { Dock = DockStyle.Bottom, Height = 60, Padding = new Padding(10), BackColor = Color.WhiteSmoke };
            btnSend = new AntdUI.Button { Text = "Send", Dock = DockStyle.Right, Width = 80, Type = TTypeMini.Primary };
            txtInput = new AntdUI.Input { PlaceholderText = "Type your question...", Dock = DockStyle.Fill }; // Removed Margins
            
            // Dock order: Add Button first to separate if needed, or stick to Frame logic.
            // Simplest: Add Button (Right), then Input (Fill). 
            // In WinForms, the one added first *to Controls* (index 0) has priority? 
            // Actually, Dock=Fill takes remaining space. 
            // I'll add btnSend first, then txtInput.
            pnlInput.Controls.Add(txtInput);
            pnlInput.Controls.Add(btnSend);
            Controls.Add(pnlInput);

            // Messages Area
            flowMessages = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.White,
                Padding = new Padding(10)
            };
            Controls.Add(flowMessages);

            // Events
            btnSend.Click += BtnSend_Click;
            txtInput.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) BtnSend_Click(s, e); };

            // Initial Message
            AppendMessage("AI", "Hello! How can I help you today?");
        }

        private async void BtnSend_Click(object? sender, EventArgs e)
        {
            string msg = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(msg) || _isThinking) return;

            // User Message
            AppendMessage("User", msg);
            txtInput.Text = "";
            _isThinking = true;
            btnSend.Loading = true;

            // AI Response
            string response = await _aiService.GetResponse(msg);
            
            _isThinking = false;
            btnSend.Loading = false;
            AppendMessage("AI", response);
        }

        private void AppendMessage(string sender, string text)
        {
            bool isUser = sender == "User";
            var bubble = new Panel
            {
                AutoSize = true,
                MaximumSize = new Size(flowMessages.Width - 40, 0),
                Padding = new Padding(10),
                BackColor = isUser ? Color.FromArgb(22, 119, 255) : Color.FromArgb(240, 240, 240),
                Margin = new Padding(3, 3, 3, 10) // Space between messages
            };

            // Round corners (simulated)
            // AntdUI doesn't have a simple Panel with Radius, using Paint event or just default Panel for now.
            // Using a Label inside Panel
            
            var lblParams = new AntdUI.Label
            {
                Text = text,
                Dock = DockStyle.None, // AutoSize label
                AutoSize = true,
                MaximumSize = new Size(flowMessages.Width - 60, 0),
                ForeColor = isUser ? Color.White : Color.Black,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.Transparent
            };
            
            bubble.Controls.Add(lblParams);

            // Alignment
            // FlowLayoutPanel TopDown doesn't support easy Left/Right align without stretching.
            // But we can create a container panel full width
            
            var container = new Panel { Width = flowMessages.ClientSize.Width - 25, Height = 0, AutoSize = true };
            bubble.Location = isUser ? new Point(container.Width - bubble.PreferredSize.Width, 0) : new Point(0,0);
            
            // Re-calculate bubble size
            lblParams.Location = new Point(5, 5);
            bubble.Size = new Size(lblParams.Width + 20, lblParams.Height + 20); // Padding

            // Adjust Bubble Location again after size known
             if (isUser) 
                bubble.Location = new Point(container.Width - bubble.Width, 0);
            
            container.Controls.Add(bubble);
            container.Height = bubble.Height + 5;

            flowMessages.Controls.Add(container);
            flowMessages.ScrollControlIntoView(container);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (flowMessages != null)
            {
                foreach(Control c in flowMessages.Controls)
                    c.Width = flowMessages.ClientSize.Width - 25;
            }
        }
    }
}
