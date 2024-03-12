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

        public int Width { get; }

        public int Height {get;}
    }
}
