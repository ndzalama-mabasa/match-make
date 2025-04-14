using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using System.Linq;
using GalaxyMatchGUI.Services;
using GalaxyMatchGUI.Models;

namespace GalaxyMatchGUI.ViewModels;

public partial class MessageRoomViewModel : ViewModelBase
{
    private readonly HttpClient _httpClient;
    
    // IDs for the current user and recipient
    private readonly string _recipientId = "1e197a1e-a5f7-4044-bba9-13fb431808c0"; // Replace with actual user ID
    private readonly string _currentUserId; // Replace with actual recipient ID
    
    [ObservableProperty]
    private string currentMessage = string.Empty;
    
    [ObservableProperty]
    private bool isLoading = false;

    public ObservableCollection<ChatMessage> Messages { get; } = new();

    public MessageRoomViewModel()
    {
        var auth = JwtStorage.Instance.authDetails;
        _currentUserId = ParseUserIdFromJwt(auth.JwtToken);
        
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", auth.JwtToken);

        LoadInitialMessagesCommand.Execute(null);
    }

    [RelayCommand]
    private async Task RefreshMessagesAsync()
    {
        try
        {
            IsLoading = true;
            await LoadInitialMessagesAsync();
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task SendMessageAsync()
    {
        if (string.IsNullOrWhiteSpace(CurrentMessage))
            return;
            
        IsLoading = true;
        
        try
        {
            // Create message object with UTC time
            var messageToSend = new
            {
                messageContent = CurrentMessage,
                sentDate = DateTime.UtcNow,  // Use UTC time
                senderId = _currentUserId,
                recipientId = _recipientId
            };
            
            // Send to API
            var response = await _httpClient.PostAsJsonAsync(
                "http://localhost:5284/api/messages", 
                messageToSend);
                
            if (response.IsSuccessStatusCode)
            {
                var savedMessage = await response.Content.ReadFromJsonAsync<ApiMessage>();
                
                if (savedMessage != null)
                {
                    // Convert UTC time to local time when displaying
                    Messages.Add(new ChatMessage
                    {
                        Id = savedMessage.Id,
                        Content = savedMessage.MessageContent,
                        IsSentByMe = savedMessage.SenderId == _currentUserId,
                        Timestamp = savedMessage.SentDate.ToLocalTime()  // Convert to local time
                    });
                    
                    CurrentMessage = string.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending message: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }
    
    [RelayCommand]
    private async Task LoadInitialMessagesAsync()
    {
        try
        {
            IsLoading = true;
            
            // Fetch previous messages
            var response = await _httpClient.GetAsync(
                $"http://localhost:5284/api/messages?senderId={_currentUserId}&recipientId={_recipientId}");
                
            if (response.IsSuccessStatusCode)
            {
                var messages = await response.Content.ReadFromJsonAsync<ApiMessage[]>();
                
                if (messages != null)
                {
                    // Clear existing messages first to avoid duplicates
                    Messages.Clear();
                    
                    // Add messages in chronological order
                    foreach (var msg in messages.OrderBy(m => m.SentDate))
                    {
                        Messages.Add(new ChatMessage
                        {
                            Id = msg.Id,
                            Content = msg.MessageContent,
                            IsSentByMe = msg.SenderId == _currentUserId,
                            Timestamp = msg.SentDate.ToLocalTime() 
                        });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading messages: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }
}

// Class representing API message structure
public class ApiMessage
{
    public int Id { get; set; }
    public string MessageContent { get; set; } = string.Empty;
    public DateTime SentDate { get; set; }
    public string SenderId { get; set; } = string.Empty;
    public string RecipientId { get; set; } = string.Empty;
}

// Keep your ChatMessage class as is
public class ChatMessage
{
    public int Id { get; set; } // Add this property
    public string Content { get; set; } = string.Empty;
    public bool IsSentByMe { get; set; }
    public DateTime Timestamp { get; set; }
}