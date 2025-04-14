using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GalaxyMatchGUI.Models;
using GalaxyMatchGUI.Services;

namespace GalaxyMatchGUI.ViewModels
{
    public partial class ContactsListViewModel : ViewModelBase
    {
        private readonly ContactsService _contactsService;

        [ObservableProperty] private bool _isLoading;

        [ObservableProperty] private string _statusMessage = string.Empty;

        [ObservableProperty] private ObservableCollection<GalaxyMatchGUI.Models.Contact> _contacts = new();

        [ObservableProperty] private GalaxyMatchGUI.Models.Contact selectedContact;

        public ContactsListViewModel()
        {
            _contactsService = new ContactsService();
            LoadContactsCommand.ExecuteAsync(null);
        }

        [RelayCommand]
        private async Task LoadContactsAsync()
        {
            try
            {
                IsLoading = true;
                StatusMessage = "Loading cosmic contacts...";

                var contacts = await _contactsService.GetContactsAsync();

                Contacts.Clear();
                foreach (var contact in contacts)
                {
                    Contacts.Add(contact);
                }

                StatusMessage = $"Found {Contacts.Count} space explorers";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Connection error: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task RefreshContactsAsync()
        {
            await LoadContactsAsync();
        }

        [RelayCommand]
        private void ContactSelected(Contact contact)
        {
            if (contact == null) return;
            StatusMessage = $"Selected {contact.DisplayName}";
        }
    }

    public class ContactsService
    {
        public async Task<ObservableCollection<Contact>> GetContactsAsync()
        {
            var url = $"https://localhost:7280/api/Messages/contacts";

            using (var client = new HttpClient())
            {
                try
                {
                    var _authToken = JwtStorage.Instance.authDetails.JwtToken;
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", _authToken);

                    client.Timeout = TimeSpan.FromSeconds(30);

                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var contacts = JsonSerializer.Deserialize<ObservableCollection<Contact>>(jsonResponse,
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true
                            });
                        return contacts;
                    }
                    else
                    {
                        Console.WriteLine($"Error fetching contacts. Status code: {response.StatusCode}");
                        return new ObservableCollection<Contact>();
                    }
                }
                catch (HttpRequestException ex)
                {

                    Console.WriteLine($"Network error fetching contacts: {ex.Message}");
                    return new ObservableCollection<Contact>();
                }
                catch (TaskCanceledException ex)
                {
                    Console.WriteLine($"Request timeout: {ex.Message}");
                    return new ObservableCollection<Contact>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching contacts: {ex.Message}");
                    return new ObservableCollection<Contact>();
                }
            }
        }
    }
}