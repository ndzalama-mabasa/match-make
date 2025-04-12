using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Threading;
using GalaxyMatchGUI.Models;

namespace GalaxyMatchGUI.Services;
public class AuthService : IDisposable
{
    private HttpListener? _listener;
    private bool _disposed;
    // Base API URL - change between HTTP and HTTPS as needed
    private const string ApiBaseUrl = "http://localhost:5284"; 
    // private const string ApiBaseUrl = "https://localhost:7280"; 

    public async Task StartLoginFlow()
    {
        const string localCallbackUrl = "http://localhost:3500/google-signin/";

        try
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add(localCallbackUrl);
            _listener.Start();
            
            // Create HttpClient with SSL certificate validation bypass for development
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            using var httpClient = new HttpClient(httpClientHandler);
            
            var authUrl = await httpClient.GetStringAsync($"{ApiBaseUrl}/api/auth/google-login");

            Process.Start(new ProcessStartInfo
            {
                FileName = authUrl,
                UseShellExecute = true
            });
            
            var getContextTask = _listener.GetContextAsync();
            var timeoutTask = Task.Delay(TimeSpan.FromMinutes(5));

            var completedTask = await Task.WhenAny(getContextTask, timeoutTask);
            if (completedTask == timeoutTask)
            {
                throw new TimeoutException("OAuth callback timed out after 5 minutes");
            }

            var context = await getContextTask;
            var authCode = context.Request.QueryString["code"];
            
            var response = context.Response;
            var responseString = "<html><body>Login successful! You can close this window.</body></html>";
            var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            response.Close();
            
            if (!string.IsNullOrWhiteSpace(authCode))
            {
                var jwtResponse = await httpClient.GetAsync($"{ApiBaseUrl}/api/auth/google-callback?code={authCode}");
                jwtResponse.EnsureSuccessStatusCode();

                var content = await jwtResponse.Content.ReadAsStringAsync();
                var authResponse = JsonSerializer.Deserialize<AuthResponse>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var jwtStorage = JwtStorage.Instance;
                jwtStorage.authDetails = authResponse;
            }
        }
        finally
        {
            Dispose();
        }
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _listener?.Stop();
            _listener?.Close();
            _disposed = true;
        }
    }
}
