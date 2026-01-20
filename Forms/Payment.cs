using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentalSystemUI.Data;
using RentalSystemUI.Services;

namespace RentalSystemUI.Forms
{
    public partial class Payment : Form
    {
        private readonly int? _bookingId;
        private readonly TenantService _tenantService = new TenantService();

        private decimal _amount;
        private int _months;
        private decimal _monthlyRent;
        private string _propertyTitle = string.Empty;
        private string _ownerPhone = string.Empty;

        public Payment() : this(null)
        {
        }

        public Payment(int? bookingId)
        {
            _bookingId = bookingId;

            InitializeComponent();
            Size = new Size(1100, 800);
            MinimumSize = new Size(1100, 800);
            MaximumSize = new Size(1100, 800);
            StartPosition = FormStartPosition.CenterScreen;

            SetupUIComponents();

            if (btnConfirm != null)
            {
                btnConfirm.Click -= BtnConfirm_Click;
                btnConfirm.Click += BtnConfirm_Click;
            }

            if (btnCancel != null)
            {
                btnCancel.Click += (s, e) => Close();
            }

            if (btnClose != null)
            {
                btnClose.Click += (s, e) => Close();
            }

            // Default method: Mobile Banking
            try
            {
                if (btnMobileBanking != null)
                {
                    btnMobileBanking.Type = AntdUI.TTypeMini.Primary;
                }
                if (btnEpay != null)
                {
                    btnEpay.Click += InitSSLCommerz;
                }
            }
            catch { }
        }

        private void BtnConfirm_Click(object? sender, EventArgs e)
        {
            if (!_bookingId.HasValue)
            {
                AntdUI.Message.error(this, "Missing booking.");
                return;
            }

            var method = (selMobileMethod?.Text ?? string.Empty).Trim();
            var senderPhone = (inputSenderPhone?.Text ?? string.Empty).Trim();
            var trxId = (inputTrxId?.Text ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(method))
            {
                AntdUI.Message.error(this, "Select Bkash or Nagad.");
                return;
            }

            if (string.IsNullOrWhiteSpace(senderPhone))
            {
                AntdUI.Message.error(this, "Sender phone number is required.");
                return;
            }

            if (string.IsNullOrWhiteSpace(trxId))
            {
                AntdUI.Message.error(this, "Transaction ID is required.");
                return;
            }

            // Keep existing payment saving logic: uses TenantService.CreatePaymentForBooking
            // Map mobile method + trx id into existing parameters.
            int paymentId = _tenantService.CreatePaymentForBooking(_bookingId.Value, _amount, method, trxId);
            if (paymentId <= 0)
            {
                AntdUI.Message.error(this, "Payment failed.");
                return;
            }

            AntdUI.Message.success(this, $"Payment submitted (ID: {paymentId}).");
            Close();
        }

        private void SetupUIComponents()
        {
            try { lblLogo.Text = "Payment"; } catch { }

            try
            {
                inputSenderPhone.PlaceholderText = "01XXXXXXXXX";
                inputTrxId.PlaceholderText = "Transaction ID";
            }
            catch { }

            LoadBookingSummary();
        }

        private void LoadBookingSummary()
        {
            if (!_bookingId.HasValue)
            {
                _amount = 0;
                _months = 0;
                _monthlyRent = 0;
                _propertyTitle = "Booking";
                _ownerPhone = "";
                UpdateSummaryUi();
                return;
            }

            try
            {
                using var conn = new Database().GetConnection();
                conn.Open();

                using var cmd = new SqlCommand(@"
                    SELECT b.BookingID, b.TotalAmount, b.DurationMonths,
                           p.Title, p.RentAmount, p.LandlordID,
                           u.Phone as OwnerPhone
                    FROM BOOKINGS b
                    JOIN PROPERTIES p ON b.PropertyID = p.PropertyID
                    JOIN USERS u ON p.LandlordID = u.UserID
                    WHERE b.BookingID=@bid", conn);

                cmd.Parameters.AddWithValue("@bid", _bookingId.Value);

                using var r = cmd.ExecuteReader();
                if (!r.Read())
                {
                    _amount = 0;
                    _months = 0;
                    _monthlyRent = 0;
                    _propertyTitle = $"Booking #{_bookingId.Value}";
                    _ownerPhone = "";
                    UpdateSummaryUi();
                    return;
                }

                _amount = (decimal)r["TotalAmount"];
                _months = r["DurationMonths"] as int? ?? 1;
                _propertyTitle = r["Title"].ToString() ?? $"Booking #{_bookingId.Value}";
                _monthlyRent = (decimal)r["RentAmount"];
                _ownerPhone = r["OwnerPhone"].ToString() ?? string.Empty;

                UpdateSummaryUi();
            }
            catch
            {
                _amount = 0;
                _months = 0;
                _monthlyRent = 0;
                _propertyTitle = $"Booking #{_bookingId.Value}";
                _ownerPhone = "";
                UpdateSummaryUi();
            }
        }

        private void UpdateSummaryUi()
        {
            try { lblPropertyTitle.Text = _propertyTitle; } catch { }
            try { lblMonthlyRent.Text = $"Monthly Rent: ৳{_monthlyRent:N0}"; } catch { }
            try { lblDuration.Text = $"Duration: {_months} month(s)"; } catch { }
            try { lblTotal.Text = $"Total: ৳{_amount:N0}"; } catch { }
            try
            {
                var phone = string.IsNullOrWhiteSpace(_ownerPhone) ? "01XXXXXXXXX" : _ownerPhone;
                lblOwnerPhone.Text = $"Send money to this number:\n{phone}";
            }
            catch { }

            try { btnConfirm.Text = $"Confirm Payment (৳{_amount:N0})"; } catch { }
        }

        // Draggable window logic
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                Capture = false;
                System.Windows.Forms.Message m = System.Windows.Forms.Message.Create(Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
                WndProc(ref m);
            }
        }

        private async void InitSSLCommerz(object? sender, EventArgs e)
        {
            if (_amount <= 0) 
            {
                AntdUI.Message.warn(this, "Amount is zero.");
                return;
            }

            try 
            {
                AntdUI.Message.info(this, "Initiating SSLCommerz..."); 
                
                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(30); // 30s timeout

                string storeId = Environment.GetEnvironmentVariable("SSLCOMMERZ_STORE_ID") ?? "testbox";
                string storePass = Environment.GetEnvironmentVariable("SSLCOMMERZ_STORE_PASSWORD") ?? "qwerty";
                string apiUrl = Environment.GetEnvironmentVariable("SSLCOMMERZ_API_URL") ?? "https://sandbox.sslcommerz.com/gwprocess/v4/api.php";

                var values = new Dictionary<string, string>
                {
                    { "store_id", storeId },
                    { "store_passwd", storePass },
                    { "total_amount", _amount.ToString() },
                    { "currency", "BDT" },
                    { "tran_id", $"TRX_{DateTime.Now.Ticks}" },
                    { "success_url", "https://example.com/success" }, 
                    { "fail_url", "https://example.com/fail" },
                    { "cancel_url", "https://example.com/cancel" },
                    { "cus_name", "Test User" }, 
                    { "cus_email", "test@test.com" },
                    { "cus_add1", "Address" },
                    { "cus_city", "Dhaka" },
                    { "cus_country", "Bangladesh" },
                    { "cus_phone", "01711111111" },
                    
                    // Mandatory parameters for some accounts
                    { "shipping_method", "NO" },
                    { "product_name", "Rental Payment" },
                    { "product_category", "Rental" },
                    { "product_profile", "general" }, 

                    { "format", "json" }
                };

                var content = new FormUrlEncodedContent(values);
                
                // Ensure TLS 1.2/1.3 is used (Fix for connection hangs)
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

                var response = await client.PostAsync(apiUrl, content);
                var responseString = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(responseString);
                
                if (doc.RootElement.TryGetProperty("GatewayPageURL", out var urlElement) || 
                    doc.RootElement.TryGetProperty("gatewayPageURL", out urlElement) ||
                    doc.RootElement.TryGetProperty("redirectGatewayURL", out urlElement)) 
                {
                    string url = urlElement.GetString() ?? "";
                    if (!string.IsNullOrEmpty(url))
                    {
                        try 
                        {
                            var psi = new ProcessStartInfo
                            {
                                FileName = url,
                                UseShellExecute = true
                            };
                            Process.Start(psi);
                            AntdUI.Message.success(this, "Redirecting to Payment Gateway...");
                        }
                        catch
                        {
                            Process.Start("explorer", url);
                        }
                    }
                    else
                    {
                        AntdUI.Message.error(this, "Gateway URL missing: " + responseString);
                    }
                }
                else
                {
                    if (doc.RootElement.TryGetProperty("failedreason", out var reason))
                        AntdUI.Message.error(this, "SSLCommerz: " + (reason.GetString() ?? ""));
                    else
                        AntdUI.Message.error(this, "Failed: " + responseString);
                }
            }
            catch
            {
                AntdUI.Message.error(this, "Error occurred while initiating payment.");
            }
        }

        private void lblNavLinks_Click(object sender, EventArgs e)
        {
        }
    }
}