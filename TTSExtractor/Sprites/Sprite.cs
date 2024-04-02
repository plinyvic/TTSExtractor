using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Png;
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

            if (input.TargetWidth != null && input.TargetHeight != null)
            {
                Upscale(input.TargetWidth.Value, input.TargetHeight.Value);
            }
            else if (input.TargetWidth != null ^ input.TargetHeight != null)
            {
                throw new ArgumentException($"Both target width and target height must be set for sprite {Name}");
            }
        }

        public Sprite(SpriteInputData input)
        {
            SourceFile = input.SourceFile;
            Name = input.Name;
            LoadSpriteFromFile();

            if(input.TargetWidth != null && input.TargetHeight != null) 
            {
                Upscale(input.TargetWidth.Value, input.TargetHeight.Value);
            }
            else if (input.TargetWidth != null ^ input.TargetHeight != null)
            {
                throw new ArgumentException($"Both target width and target height must be set for sprite {Name}");
            }
        }

        public Sprite() { }

        public virtual void Upscale(int targetWidth, int targetHeight, IResampler resampler = null)
        {
            if (resampler == null)
            {
                resampler = KnownResamplers.NearestNeighbor;
            }

            ContainedSprite.Mutate(x => x.Resize(targetWidth, targetHeight, resampler));
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

        public virtual void SaveResource(string path)
        {
            // if image is a bigob, do resize image and place in top left.
            // i really hate the existance of this, but i'm not sure of a better way to switch on the logic.
            if(Name.ToLower().Contains("bigob"))
            {
                // evil hardcoded bigob resize
                Image<Rgba32> resizedBigob = new Image<Rgba32>(64, 128, new Rgba32(0, 0, 0, 0));
                //sheetToSave.Mutate(x => x.DrawImage(SpriteList[i], new Point(xPos, yPos), 1f));
                resizedBigob.Mutate(x => x.DrawImage(ContainedSprite, new Point(0, 0), 1f));
                ContainedSprite = resizedBigob;
            }

            var options = new GraphicsOptions
            {
                //AlphaCompositionMode = PixelAlphaCompositionMode.
                Antialias = true,

            };

            ContainedSprite.Configuration.SetGraphicsOptions(options);

            var encoder = new PngEncoder
            {
                CompressionLevel = PngCompressionLevel.NoCompression,
                BitDepth = PngBitDepth.Bit8
            };

            ContainedSprite.SaveAsPng(path, encoder);
        }
    }
}
