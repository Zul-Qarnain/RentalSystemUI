using System;
using System.Drawing;
using System.Windows.Forms;
using AntdUI;
using RentalSystemUI.Data;
using BCrypt.Net;

namespace RentalSystemUI.Forms
{
    public class DebugAdminLogin : Form
    {
        public DebugAdminLogin()
        {
            Text = "Debug Admin Login";
            Size = new Size(800, 520);
            StartPosition = FormStartPosition.CenterScreen;

            var lbl = new System.Windows.Forms.Label
            {
                Text = "Password to hash:",
                Location = new Point(30, 20),
                AutoSize = true
            };

            var input = new TextBox
            {
                Text = "Admin@1234",
                Size = new Size(420, 30),
                Location = new Point(150, 16)
            };

            var btnHash = new AntdUI.Button
            {
                Text = "Generate BCrypt Hash",
                Type = TTypeMini.Primary,
                Size = new Size(220, 40),
                Location = new Point(30, 60)
            };

            var btnCheck = new AntdUI.Button
            {
                Text = "Run Admin Hash Check",
                Type = TTypeMini.Default,
                Size = new Size(220, 40),
                Location = new Point(260, 60)
            };

            var box = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Size = new Size(720, 360),
                Location = new Point(30, 120)
            };

            btnHash.Click += (s, e) =>
            {
                box.Clear();
                var pw = input.Text ?? "";
                try
                {
                    var hash = BCrypt.Net.BCrypt.HashPassword(pw);
                    box.AppendText("BCrypt Hash:\r\n");
                    box.AppendText(hash + "\r\n\r\n");
                    box.AppendText("SQL snippet:\r\n");
                    box.AppendText("UPDATE USERS SET PasswordHash='" + hash.Replace("'", "''") + "' WHERE Email='admin@rental.com';\r\n");
                }
                catch (Exception ex)
                {
                    box.AppendText("ERROR: " + ex.Message);
                }
            };

            btnCheck.Click += (s, e) =>
            {
                box.Clear();
                try
                {
                    using var conn = new Database().GetConnection();
                    conn.Open();

                    using var cmd = new Microsoft.Data.SqlClient.SqlCommand(
                        "SELECT TOP 1 PasswordHash, IsActive, UserType FROM USERS WHERE Email=@e", conn);
                    cmd.Parameters.AddWithValue("@e", "admin@rental.com");
                    using var r = cmd.ExecuteReader();
                    if (!r.Read())
                    {
                        box.AppendText("No row found for admin@rental.com\r\n");
                        return;
                    }

                    var hash = r["PasswordHash"].ToString() ?? "";
                    var active = r["IsActive"].ToString() ?? "";
                    var role = r["UserType"].ToString() ?? "";

                    box.AppendText($"UserType={role}, IsActive={active}\r\n");
                    box.AppendText($"Hash={hash}\r\n\r\n");

                    foreach (var pw in new[] { input.Text ?? "", "admin123", "Admin123" })
                    {
                        if (string.IsNullOrWhiteSpace(pw)) continue;
                        bool ok = false;
                        try { ok = BCrypt.Net.BCrypt.Verify(pw, hash); } catch { }
                        box.AppendText($"Verify('{pw}') = {ok}\r\n");
                    }
                }
                catch (Exception ex)
                {
                    box.AppendText("ERROR: " + ex.Message + "\r\n" + ex.StackTrace);
                }
            };

            Controls.Add(lbl);
            Controls.Add(input);
            Controls.Add(btnHash);
            Controls.Add(btnCheck);
            Controls.Add(box);
        }
    }
}
