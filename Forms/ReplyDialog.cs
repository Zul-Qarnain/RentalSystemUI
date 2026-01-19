using System;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;

namespace RentalSystemUI.Forms
{
    public class ReplyDialog : Form
    {
        public string ReplyText { get; private set; } = "";

        public ReplyDialog(string tenantName, string originalComment)
        {
            Text = "Reply to Review";
            Size = new Size(500, 350);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;

            var lblTitle = new AntdUI.Label
            {
                Text = $"Replying to {tenantName}",
                Font = Styles.CardTitle,
                ForeColor = Styles.DarkBlue,
                Location = new Point(20, 20),
                AutoSize = true
            };

            var lblOriginal = new AntdUI.Label
            {
                Text = $"Original Review: \"{originalComment}\"",
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Styles.TextGray,
                Location = new Point(20, 50),
                MaximumSize = new Size(440, 40),
                AutoEllipsis = true,
                AutoSize = true
            };

            var txtReply = new AntdUI.Input
            {
                PlaceholderText = "Write your reply here...",
                Location = new Point(20, 100),
                Size = new Size(440, 120),
                Multiline = true,
                Radius = 8
            };

            var btnSubmit = new AntdUI.Button
            {
                Text = "Submit Reply",
                Type = TTypeMini.Primary,
                Location = new Point(300, 240),
                Size = new Size(160, 40),
                Radius = 8
            };

            var btnCancel = new AntdUI.Button
            {
                Text = "Cancel",
                Type = TTypeMini.Default,
                Location = new Point(180, 240),
                Size = new Size(100, 40),
                Radius = 8
            };

            btnSubmit.Click += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtReply.Text))
                {
                    AntdUI.Message.warn(this, "Please write a reply.");
                    return;
                }
                ReplyText = txtReply.Text.Trim();
                DialogResult = DialogResult.OK;
                Close();
            };

            btnCancel.Click += (s, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            Controls.Add(lblTitle);
            Controls.Add(lblOriginal);
            Controls.Add(txtReply);
            Controls.Add(btnSubmit);
            Controls.Add(btnCancel);
        }
    }
}
