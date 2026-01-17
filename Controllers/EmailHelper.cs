using System;
using System.Threading.Tasks;
using Resend; // Install Resend via NuGet
using DotNetEnv;

namespace RentalSystemUI.Controllers
{
    public static class EmailHelper
    {
        public static async Task<bool> SendOtp(string toEmail, string otpCode)
        {
            try
            {
                Env.Load();
                string? apiKey = Environment.GetEnvironmentVariable("RESEND_API_KEY");
                
                if (string.IsNullOrEmpty(apiKey))
                {
                    System.Diagnostics.Debug.WriteLine("RESEND_API_KEY is missing - skipping email");
                    return false;
                }

                IResend resend = ResendClient.Create(apiKey);

                var message = new EmailMessage();
                message.From = "onboarding@exodiscoverai.earth";
                message.To.Add(toEmail);
                message.Subject = "RentalSystem Verification Code";
                message.HtmlBody = $"<h1>Your Code: {otpCode}</h1><p>Use this to verify your account.</p>";

                await resend.EmailSendAsync(message);
                return true;
            }
            catch (System.Net.Sockets.SocketException)
            {
                // Network error - no internet or DNS failure
                System.Diagnostics.Debug.WriteLine("Network error: Cannot reach email server");
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Email error: {ex.Message}");
                return false;
            }
        }
    }
}