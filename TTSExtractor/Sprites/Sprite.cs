using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;
using TTSExtractor.ImageExtractors;
using TTSExtractor.InputData;
using TTSExtractor.Palletes;
using TTSExtractor.Resource;

namespace TTSExtractor.Sprites
{
    /// <summary>
    /// An imported sprite.
    /// </summary>
    public class Sprite : IResource
    {
        public Image<Rgba32> ContainedSprite { get; protected set; }

        public string SourceFile { get; protected set; } = "";

        public string Name { get; protected set; } = "";

        public Sprite(ExtractedSpriteInputData input, ResourceManager manager)
        {
            SourceFile = input.SourceFile;
            Name = input.Name;

            LoadSpriteFromFile(input, manager.GetResource<JascPalette>(input.PaletteName));
        }

        public Sprite(SpriteInputData input)
        {
            SourceFile = input.SourceFile;
            Name = input.Name;
            LoadSpriteFromFile();
        }

        public Sprite() { }

        public void Upscale(int factor, IResampler resampler = null)
        {
            if (resampler == null)
            {
                resampler = KnownResamplers.NearestNeighbor;
            }

            ContainedSprite.Mutate(x => x.Resize(ContainedSprite.Width, ContainedSprite.Height, resampler));
        }

        /// <summary>
        /// Loads image from file.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        protected void LoadSpriteFromFile()
        {
            ContainedSprite = Image.Load<Rgba32>(SourceFile);
        }

        /// <summary>
        /// Load extracted sprite from file.
        /// </summary>
        /// <param name="palette"></param>
        protected void LoadSpriteFromFile(ExtractedSpriteInputData inputData, JascPalette palette)
        {
            ContainedSprite = ExtractorManager.ExtractImage(inputData, palette);
        }

        public virtual void SaveResource()
        {
            ContainedSprite.SaveAsPng($"Output/Dump/Sprite/{Name}.png");
        }
    }
}
