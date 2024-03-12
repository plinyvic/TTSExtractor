using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTSExtractor.Palletes;

namespace TTSExtractor.Sprite
{
    public class ExtractedSprite : ISprite, IResource
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public int SpriteWidth
        {
            get
            {
                return Width;
            }
            set
            {
                Width = value;
            }
        }
        public int SpriteHeight
        {
            get
            {
                return Height;
            }
            set
            {
                Height = value;
            }
        }

        public Image<Rgba32> Sprite { get; private set; }

        public string SourceFile { get; private set; }

        public string Name { get; private set; }

        public ExtractedSprite(int width, int height, JascPalette palette, byte[] paletteIndexes, string sourceFile = "", string name = "")
        {
            Width = width;
            Height = height;
            SourceFile = sourceFile;
            Name = name;

            Span<Rgba32> colorSpan = paletteIndexes.Select(index => palette.Palette[index]).ToArray().AsSpan();
            Sprite = Image.LoadPixelData<Rgba32>(colorSpan, Width, Height);
        }
    }
}
