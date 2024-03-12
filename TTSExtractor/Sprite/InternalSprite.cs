using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSExtractor.Sprite
{
    public class InternalSprite : ISprite
    {
        public Image<Rgba32> Sprite { get; }

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

        public InternalSprite(int width, int height, Image<Rgba32> sprite)
        {
            Sprite = sprite;
            Width = width;
            Height = height;
            SpriteWidth = width;
            SpriteHeight = height;
        }
    }
}
