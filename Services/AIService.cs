using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DotNetEnv;

namespace RentalSystemUI.Services
{
    public class AIService
    {
        private readonly string _apiKey;
        private readonly string _systemPrompt;
        private readonly string _apiUrl = "https://api.groq.com/openai/v1/chat/completions";
        private readonly string _model = "llama-3.3-70b-versatile"; // Switching to a capable chat model

        public AIService()
        {
            _apiKey = Environment.GetEnvironmentVariable("GROQ_API_KEY") ?? "";
            
            string promptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "systemprompt.txt");
            if (File.Exists(promptPath))
            {
                _systemPrompt = File.ReadAllText(promptPath);
            }
            else
            {
                // Fallback logic
                try 
                {
                   promptPath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, "Assets", "systemprompt.txt");
                   if (File.Exists(promptPath))
                       _systemPrompt = File.ReadAllText(promptPath);
                   else
                       _systemPrompt = "You are a helpful assistant.";
                }
                catch { _systemPrompt = "You are a helpful assistant."; }
            }
        }

        public async Task<string> GetResponse(string userMessage)
        {
            if (string.IsNullOrEmpty(_apiKey))
                return "Error: GROQ_API_KEY not found in .env. Please add it.";

            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
                
                var requestBody = new
                {
                    model = _model,
                    messages = new[]
                    {
                        new { role = "system", content = _systemPrompt },
                        new { role = "user", content = userMessage }
                    }
                };

                string jsonContent = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_apiUrl, content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return $"Error: {response.StatusCode} - {responseString}";
                }

                using var doc = JsonDocument.Parse(responseString);
                // Groq/OpenAI format: choices[0].message.content
                var text = doc.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                return text ?? "No response.";
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
        }
    }
}
