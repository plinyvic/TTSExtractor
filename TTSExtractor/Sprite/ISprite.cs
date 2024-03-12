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

        public int Width { get; }
        public int Height { get; }

        public void Upscale(int factor, IResampler resampler)
        {
            Sprite.Mutate(x => x.Resize(Width * factor, Height * factor, resampler));
        }
    }
}
