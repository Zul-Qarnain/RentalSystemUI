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
            Env.Load();
            string apiKey = Environment.GetEnvironmentVariable("RESEND_API_KEY")
                            ?? throw new Exception("RESEND_API_KEY is missing in .env");

            IResend resend = ResendClient.Create(apiKey);

            var message = new EmailMessage();
            message.From = "onboarding@exodiscoverai.earth"; // Updated domain
            message.To.Add(toEmail);
            message.Subject = "RentalSystem Verification Code";
            message.HtmlBody = $"<h1>Your Code: {otpCode}</h1><p>Use this to verify your account.</p>";

            await resend.EmailSendAsync(message);
            return true;
        }
    }
}