using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace GalaxyMatchGUI.Services
{
    public static class AsyncImageLoader
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<IImage?> LoadAsync(string source)
        {
            if (string.IsNullOrEmpty(source))
                return null;

            try
            {
                // Local resource using avares:// protocol
                if (source.StartsWith("avares://"))
                {
                    var uri = new Uri(source);
                    // Use static AssetLoader instead of AvaloniaLocator
                    using var stream = AssetLoader.Open(uri);
                    return new Bitmap(stream);
                }
                // Remote URL (http:// or https://)
                else if (source.StartsWith("http://") || source.StartsWith("https://"))
                {
                    var bytes = await _httpClient.GetByteArrayAsync(source);
                    using var ms = new MemoryStream(bytes);
                    return new Bitmap(ms);
                }
                // Base64 encoded image
                else if (source.StartsWith("data:image"))
                {
                    var base64Data = source.Substring(source.IndexOf(',') + 1);
                    var bytes = Convert.FromBase64String(base64Data);
                    using var ms = new MemoryStream(bytes);
                    return new Bitmap(ms);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading image: {ex.Message}");
            }

            return null;
        }
    }
}