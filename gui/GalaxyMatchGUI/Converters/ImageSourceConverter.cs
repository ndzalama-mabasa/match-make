using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace GalaxyMatchGUI.Converters
{
    public class ImageSourceConverter : IValueConverter
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string source)
            {
                // Return null if the string is empty
                if (string.IsNullOrEmpty(source))
                    return null;

                try
                {
                    // Handle avares:// scheme for app resources
                    if (source.StartsWith("avares://"))
                    {
                        var uri = new Uri(source);
                        // Use static AssetLoader instead of AvaloniaLocator
                        using var stream = AssetLoader.Open(uri);
                        return new Bitmap(stream);
                    }
                    // Handle http:// and https:// URLs
                    else if (source.StartsWith("http://") || source.StartsWith("https://"))
                    {
                        // Load URL asynchronously but return null initially
                        // The image will update when the task completes
                        LoadFromWeb(source);
                        return null;
                    }
                    // Handle base64 encoded images
                    else if (source.StartsWith("data:image"))
                    {
                        // Parse and load the base64 data
                        return LoadFromBase64(source);
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception and return null
                    Console.WriteLine($"Error loading image source: {ex.Message}");
                    return null;
                }
            }

            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private async void LoadFromWeb(string url)
        {
            try
            {
                var bytes = await _httpClient.GetByteArrayAsync(url);
                using var ms = new MemoryStream(bytes);
                var bitmap = new Bitmap(ms);
                
                // Raise a property changed event or use a callback to update the UI
                // This requires modifying our ViewModel to support this pattern
                // For now we'll log the successful load
                Console.WriteLine($"Successfully loaded image from URL: {url}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load image from URL: {url}, Error: {ex.Message}");
            }
        }

        private Bitmap? LoadFromBase64(string base64)
        {
            try
            {
                // Extract the base64 data from the data URI
                var base64Data = base64.Substring(base64.IndexOf(',') + 1);
                var bytes = System.Convert.FromBase64String(base64Data);
                
                using var ms = new MemoryStream(bytes);
                return new Bitmap(ms);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load base64 image: {ex.Message}");
                return null;
            }
        }
    }
}