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
using System.Web;

namespace GalaxyMatchGUI.Services;
public class AuthService : IDisposable
{
    private HttpListener? _listener;
    private bool _disposed;
    // Base API URL - change between HTTP and HTTPS as needed
    private const string ApiBaseUrl = "http://localhost:5284"; 
    // private const string ApiBaseUrl = "https://localhost:7280"; 

    public async Task<AuthResponse?> StartLoginFlow()
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
            
            // Get the login URL from the server
            string authUrl;
            try
            {
                authUrl = await httpClient.GetStringAsync($"{ApiBaseUrl}/api/auth/google-login");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get login URL: {ex.Message}", ex);
            }

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
                try
                {
                    // Properly encode the authorization code
                    var encodedCode = HttpUtility.UrlEncode(authCode);
                    var jwtResponseUrl = $"{ApiBaseUrl}/api/auth/google-callback?code={encodedCode}";
                    
                    // Enable more detailed error information for debugging
                    var jwtRequestMessage = new HttpRequestMessage(HttpMethod.Get, jwtResponseUrl);
                    var jwtResponse = await httpClient.SendAsync(jwtRequestMessage);
                    
                    // Read raw response content for debugging
                    var rawContent = await jwtResponse.Content.ReadAsStringAsync();
                    
                    // Check for error status code
                    if (!jwtResponse.IsSuccessStatusCode)
                    {
                        throw new HttpRequestException($"Error from server: {jwtResponse.StatusCode}, Content: {rawContent}");
                    }
                    
                    // Deserialize the successful response
                    var authResponse = JsonSerializer.Deserialize<AuthResponse>(rawContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (authResponse == null)
                    {
                        throw new InvalidOperationException("Failed to deserialize authentication response");
                    }

                    var jwtStorage = JwtStorage.Instance;
                    jwtStorage.authDetails = authResponse;
                    
                    // Return the auth response so the calling code can check the profile status
                    return authResponse;
                }
                catch (Exception ex)
                {
                    // Provide more detailed error information
                    throw new Exception($"API authentication error: {ex.Message}", ex);
                }
            }
            else
            {
                throw new InvalidOperationException("No authorization code received from Google");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Login flow failed: {ex.Message}", ex);
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
