using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSExtractor.Sprite
{
    public interface ISprite
    {
        public Image<Rgba32> Sprite { get; }

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public int SpriteWidth { get; protected set; }

        public int SpriteHeight { get; protected set; }

        public void Upscale(int factor, IResampler resampler)
        {
            Width = Width * factor;
            Height = Height * factor;
            SpriteWidth = SpriteWidth * factor;
            SpriteHeight = SpriteHeight * factor;
            
            Sprite.Mutate(x => x.Resize(Width, Height, resampler));
        }
    }
}
