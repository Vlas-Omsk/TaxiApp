using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Media;

namespace TaxiApp.WindowsApp
{
    public static class SafeColors
    {
        private static readonly Random _random = new();

        public static Color Red { get; } = Color.FromRgb(255, 99, 132);
        public static Color Blue { get; } = Color.FromRgb(54, 162, 235);
        public static Color Yellow { get; } = Color.FromRgb(255, 206, 86);
        public static Color Turquoise { get; } = Color.FromRgb(75, 192, 192);
        public static Color Purple { get; } = Color.FromRgb(153, 102, 255);

        public static IReadOnlyList<Color> All { get; } = typeof(SafeColors)
            .GetProperties(BindingFlags.Static | BindingFlags.Public)
            .Where(x => x.Name != nameof(All) && x.PropertyType == typeof(Color))
            .Select(x => (Color)x.GetValue(null))
            .ToArray();

        public static Color GetColorFromNumber(int i)
        {
            if (i >= All.Count)
                i -= (int)Math.Floor((double)i / All.Count) * All.Count;

            return All[i];
        }

        public static Color GetRandomColor()
        {
            return All[_random.Next(0, All.Count)];
        }
    }
}
