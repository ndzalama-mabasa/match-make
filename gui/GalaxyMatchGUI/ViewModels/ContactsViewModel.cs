﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GalaxyMatchGUI.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Windows.Input;

namespace GalaxyMatchGUI.ViewModels
{
    public partial class ContactsViewModel : ViewModelBase
    {
        [ObservableProperty] private ObservableCollection<Contact> _messageContacts = new();
        [ObservableProperty] private bool _isLoadingMessages;
        [ObservableProperty] private string _messagesStatusMessage = string.Empty;
        
        [ObservableProperty] private ObservableCollection<Contact> _sentRequestContacts = new();
        [ObservableProperty] private bool _isLoadingSentRequests;
        [ObservableProperty] private string _sentRequestsStatusMessage = string.Empty;
        
        [ObservableProperty] private ObservableCollection<Contact> _receivedRequestContacts = new();
        [ObservableProperty] private bool _isLoadingReceivedRequests;
        [ObservableProperty] private string _receivedRequestsStatusMessage = string.Empty;
        
        [ObservableProperty] private bool _showEmptyState;
        [ObservableProperty] private string _emptyStateMessage = "No connections found. Start exploring the cosmos!";

        [ObservableProperty] private Contact _selectedContact;
        
        public ContactsViewModel()
        {
            Task.Run(async () => {
                await LoadMessagesAsync();
                await LoadSentRequestsAsync();
                await LoadReceivedRequestsAsync();
            });
        }

        #region Messages Tab

        [RelayCommand]
        private async Task RefreshMessagesAsync()
        {
            await LoadMessagesAsync();
        }

        private async Task LoadMessagesAsync()
        {
            try
            {
                IsLoadingMessages = true;
                MessagesStatusMessage = "Loading message contacts...";
                
                await Task.Delay(1200);

                MessageContacts.Clear();
                
                var messages = GetMockMessageContacts();
                foreach (var contact in messages)
                {
                    MessageContacts.Add(contact);
                }

                MessagesStatusMessage = MessageContacts.Count > 0 
                    ? $"Found {MessageContacts.Count} message contacts" 
                    : "";
                
                UpdateEmptyState();
            }
            catch (Exception ex)
            {
                MessagesStatusMessage = $"Error: {ex.Message}";
            }
            finally
            {
                IsLoadingMessages = false;
            }
        }

        private ObservableCollection<Contact> GetMockMessageContacts()
        {
            return new ObservableCollection<Contact>
            {
                new Contact
                {
                    UserId = Guid.NewGuid(),
                    DisplayName = "Nova Stargazer",
                    AvatarUrl = "https://i.pravatar.cc/150?img=1"
                },
                new Contact
                {
                    UserId = Guid.NewGuid(),
                    DisplayName = "Orion Hunter",
                    AvatarUrl = "https://i.pravatar.cc/150?img=2"
                },
                new Contact
                {
                    UserId = Guid.NewGuid(),
                    DisplayName = "Luna Eclipse",
                    AvatarUrl = "https://i.pravatar.cc/150?img=3"
                }
            };
        }

        #endregion

        #region Sent Requests Tab

        [RelayCommand]
        private async Task RefreshSentRequestsAsync()
        {
            await LoadSentRequestsAsync();
        }

        private async Task LoadSentRequestsAsync()
        {
            try
            {
                IsLoadingSentRequests = true;
                SentRequestsStatusMessage = "Loading sent requests...";
                
                await Task.Delay(1000);

                SentRequestContacts.Clear();
                
                var sentRequests = GetMockSentRequestContacts();
                foreach (var contact in sentRequests)
                {
                    SentRequestContacts.Add(contact);
                }

                SentRequestsStatusMessage = SentRequestContacts.Count > 0 
                    ? $"You have {SentRequestContacts.Count} pending requests" 
                    : "No pending requests";
                
                UpdateEmptyState();
            }
            catch (Exception ex)
            {
                SentRequestsStatusMessage = $"Error: {ex.Message}";
            }
            finally
            {
                IsLoadingSentRequests = false;
            }
        }

        private ObservableCollection<Contact> GetMockSentRequestContacts()
        {
            return new ObservableCollection<Contact>
            {
                new Contact
                {
                    UserId = Guid.NewGuid(),
                    DisplayName = "Andromeda Explorer",
                    AvatarUrl = "https://i.pravatar.cc/150?img=4"
                },
                new Contact
                {
                    UserId = Guid.NewGuid(),
                    DisplayName = "Comet Chaser",
                    AvatarUrl = "https://i.pravatar.cc/150?img=5"
                }
            };
        }

        [RelayCommand]
        private async Task CancelRequestAsync(Contact contact)
        {
            try
            {
                if (contact == null) return;
                
                IsLoadingSentRequests = true;
                SentRequestsStatusMessage = $"Canceling request to {contact.DisplayName}...";
                
                await Task.Delay(800);
                
                SentRequestContacts.Remove(contact);
                
                SentRequestsStatusMessage = $"Request to {contact.DisplayName} canceled";
                
                UpdateEmptyState();
            }
            catch (Exception ex)
            {
                SentRequestsStatusMessage = $"Error canceling request: {ex.Message}";
            }
            finally
            {
                IsLoadingSentRequests = false;
            }
        }

        #endregion

        #region Received Requests Tab

        [RelayCommand]
        private async Task RefreshReceivedRequestsAsync()
        {
            await LoadReceivedRequestsAsync();
        }

        private async Task LoadReceivedRequestsAsync()
        {
            try
            {
                IsLoadingReceivedRequests = true;
                ReceivedRequestsStatusMessage = "Loading received requests...";
                
                await Task.Delay(1500);

                ReceivedRequestContacts.Clear();
                
                var receivedRequests = GetMockReceivedRequestContacts();
                foreach (var contact in receivedRequests)
                {
                    ReceivedRequestContacts.Add(contact);
                }

                ReceivedRequestsStatusMessage = ReceivedRequestContacts.Count > 0 
                    ? $"You have {ReceivedRequestContacts.Count} connection requests" 
                    : "No connection requests";
                
                UpdateEmptyState();
            }
            catch (Exception ex)
            {
                ReceivedRequestsStatusMessage = $"Error: {ex.Message}";
            }
            finally
            {
                IsLoadingReceivedRequests = false;
            }
        }

        private ObservableCollection<Contact> GetMockReceivedRequestContacts()
        {
            return new ObservableCollection<Contact>
            {
                new Contact
                {
                    UserId = Guid.NewGuid(),
                    DisplayName = "Galaxy Wanderer",
                    AvatarUrl = "https://i.pravatar.cc/150?img=6"
                },
                new Contact
                {
                    UserId = Guid.NewGuid(),
                    DisplayName = "Cosmic Ray",
                    AvatarUrl = "https://i.pravatar.cc/150?img=7"
                },
                new Contact
                {
                    UserId = Guid.NewGuid(),
                    DisplayName = "Nebula Navigator",
                    AvatarUrl = "https://i.pravatar.cc/150?img=8"
                }
            };
        }

        [RelayCommand]
        private async Task AcceptRequestAsync(Contact contact)
        {
            try
            {
                if (contact == null) return;
                
                IsLoadingReceivedRequests = true;
                ReceivedRequestsStatusMessage = $"Accepting request from {contact.DisplayName}...";
                
                // Simulate network delay
                await Task.Delay(800);
                
                ReceivedRequestContacts.Remove(contact);
                MessageContacts.Add(contact);
                
                ReceivedRequestsStatusMessage = $"Connected with {contact.DisplayName}!";
                
                // Update empty state and messaging status
                UpdateEmptyState();
                MessagesStatusMessage = $"Found {MessageContacts.Count} message contacts";
            }
            catch (Exception ex)
            {
                ReceivedRequestsStatusMessage = $"Error accepting request: {ex.Message}";
            }
            finally
            {
                IsLoadingReceivedRequests = false;
            }
        }

        #endregion

        // Helper to update empty state visibility
        private void UpdateEmptyState()
        {
            bool allEmpty = MessageContacts.Count == 0 && 
                           SentRequestContacts.Count == 0 && 
                           ReceivedRequestContacts.Count == 0;
            
            ShowEmptyState = allEmpty;
            
            if (allEmpty)
            {
                EmptyStateMessage = "Your cosmic network is empty.\nStart exploring to connect with fellow space travelers!";
            }
        }
    }
}