using System;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;

namespace GalaxyMatchGUI.Converters
{
    public class InitialsConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string name && !string.IsNullOrWhiteSpace(name))
            {
                // Split the name by spaces and get the first letter of each part
                var nameParts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (nameParts.Length > 0)
                {
                    // If it's just a single name, return the first letter
                    if (nameParts.Length == 1)
                    {
                        return nameParts[0].Substring(0, 1).ToUpper();
                    }
                    // If there are multiple name parts, return the first letter of first and last parts
                    else
                    {
                        return $"{nameParts[0].Substring(0, 1)}{nameParts[nameParts.Length - 1].Substring(0, 1)}".ToUpper();
                    }
                }
            }
            
            // Default fallback value
            return "?";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}