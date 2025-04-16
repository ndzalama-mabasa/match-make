﻿// File: Services/AuthService.cs

using System;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using GalaxyMatchGUI.Models;
using System.Web;

namespace GalaxyMatchGUI.Services;

public class AuthService : IDisposable
{
    private HttpListener? _listener;
    private bool _disposed;

    public async Task<AuthResponse?> StartLoginFlow()
    {
        try
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add(App.Settings.CallbackUrl);
            _listener.Start();

            var httpService = HttpService.Instance;

            string authUrl;
            try
            {
                authUrl = await httpService.GetStringAsync("/api/auth/google-login");
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
                    var encodedCode = HttpUtility.UrlEncode(authCode);
                    var jwtResponseUrl = $"/api/auth/google-callback?code={encodedCode}";

                    var rawContent = await httpService.GetStringAsync(jwtResponseUrl);

                    var authResponse = JsonSerializer.Deserialize<AuthResponse>(rawContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (authResponse == null)
                    {
                        throw new InvalidOperationException("Failed to deserialize authentication response");
                    }

                    JwtStorage.Instance.authDetails = authResponse;
                    return authResponse;
                }
                catch (Exception ex)
                {
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