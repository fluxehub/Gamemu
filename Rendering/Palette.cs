using System;
using System.Collections.Generic;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.ColorSpaces.Conversion;
using SixLabors.ImageSharp.PixelFormats;

namespace Gamemu.Rendering
{
    public enum PaletteColor
    {
        White = 0,
        LightGray = 1,
        DarkGray = 2,
        Black = 3
    }

    public record Color(byte R, byte G, byte B);
    
    public class Palette
    {
        private readonly Color[] _colors;

        public Palette(Color white, Color lightGray, Color darkGray, Color black)
        {
            _colors = new Color[] {white, lightGray, darkGray, black};
        }
        
        // Support loading from lospec 1x images
        public static Palette FromPaletteImage(string path)
        {
            using var image = (Image<Rgba32>) Image.Load(path);

            if (image.Size() != new Size(4, 1))
                throw new ArgumentException("Invalid palette image size (should be 4x1)");

            // Palette files should only be 1 x 4 but take just the first 4 pixels just in case
            var colors = image.GetPixelRowSpan(0).ToArray().Take(4).ToList();
            var colorSpaceConverter = new ColorSpaceConverter();

            // Sort colors by luminance and create color objects
            var sortedColors = colors.Select(color => colorSpaceConverter.ToHsl(color))
                .OrderBy(color => color.L)
                .Select(color => new Rgba32(colorSpaceConverter.ToRgb(color).ToVector3()))
                .Select(color => new Color(color.R, color.G, color.B))
                .ToList();

            // After sorting, color array is dark-to-light
            var white = sortedColors[3];
            var lightGray = sortedColors[2];
            var darkGray = sortedColors[1];
            var black = sortedColors[0];

            return new Palette(white, lightGray, darkGray, black);
        }

        public Color GetColor(PaletteColor color) => _colors[(int) color];
    }
}