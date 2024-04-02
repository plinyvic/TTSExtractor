using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTSExtractor.ImageExtractors;
using TTSExtractor.InputData;
using TTSExtractor.Palletes;
using TTSExtractor.Resource;

namespace TTSExtractor.Sprites
{
    public class SpriteSheet : Sprite
    {
        
        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public int ColumnCount { get; protected set; } = 16;

        public List<Image<Rgba32>> SpriteList { get; protected set; } = new List<Image<Rgba32>>();

        public SpriteSheet(SpriteSheetInputData inputData)
        {
            Width = inputData.Width;
            Height = inputData.Height;
            ColumnCount = inputData.ColumnCount;
            Name = inputData.Name;
            SourceFile = inputData.SourceFile;

            LoadSpriteSheet(inputData);

            if (inputData.TargetWidth != null && inputData.TargetHeight != null)
            {
                Upscale(inputData.TargetWidth.Value, inputData.TargetHeight.Value);
            }
            else if (inputData.TargetWidth != null ^ inputData.TargetHeight != null)
            {
                throw new ArgumentException($"Both target width and target height must be set for sprite {Name}");
            }
        }

        public SpriteSheet(ExtractedSpriteSheetInputData inputData, ResourceManager manager)
        {
            Width = inputData.Width;
            Height = inputData.Height;
            ColumnCount = inputData.ColumnCount;
            Name = inputData.Name;
            SourceFile = inputData.SourceFile;

            LoadSpriteSheet(inputData, manager);

            if (inputData.TargetWidth != null && inputData.TargetHeight != null)
            {
                Upscale(inputData.TargetWidth.Value, inputData.TargetHeight.Value);
            }
            else if (inputData.TargetWidth != null ^ inputData.TargetHeight != null)
            {
                throw new ArgumentException($"Both target width and target height must be set for sprite {Name}");
            }
        }

        public SpriteSheet(SpriteSheetRangeInputData inputData, ResourceManager manager)
        {
            Width = inputData.TargetWidth;
            Height = inputData.TargetHeight;
            ColumnCount = inputData.ColumnCount;
            Name = inputData.Name;

            foreach(var slice in inputData.SpriteSheets)
            {
                if(slice.Name.StartsWith("_BLANK_"))
                {
                    for(int i = 0; i < slice.Length; i++)
                    {
                        SpriteList.Add(new Image<Rgba32>(Width, Height, new Rgba32(0,0,0,0)));
                    }

                    continue;
                }

                IResource currentItem = manager.GetResource(slice.Name);

                if(currentItem is SpriteSheet currentSheet)
                {
                    if (slice.Length == null)
                    {
                        SpriteList.AddRange(currentSheet.SpriteList.GetRange(slice.StartIndex, currentSheet.SpriteList.Count - slice.StartIndex));
                    }
                    else
                    {
                        SpriteList.AddRange(currentSheet.SpriteList.GetRange(slice.StartIndex, slice.Length.Value));
                    }
                }
                else if(currentItem is Sprite currentSprite)
                {
                    SpriteList.Add(currentSprite.ContainedSprite);
                }
                else
                {
                    throw new ArgumentException($"Resource {slice.Name} is not a sprite or sprite sheet.");
                }
            }

            // call upscale to set all images to same dimension
            Upscale(inputData.TargetWidth, inputData.TargetHeight);
        }

        protected void LoadSpriteSheet(SpriteSheetInputData inputData)
        {
            Image<Rgba32> spriteSheet = Image.Load<Rgba32>(SourceFile);

            int spritesWide = spriteSheet.Width / Width;
            int spritesTall = spriteSheet.Height / Height;

            for (int i = 0; i < spritesWide * spritesTall; i++)
            {
                int xPos = (i % spritesWide) * Width;
                int yPos = (i / spritesWide * Height);

                SpriteList.Add(spriteSheet.Clone(image => image.Crop(new Rectangle(xPos, yPos, Width, Height))));
            }
        }

        protected void LoadSpriteSheet(ExtractedSpriteSheetInputData inputData, ResourceManager manager)
        {
            TabFile tab = manager.GetResource<TabFile>(inputData.TabName);
            JascPalette palette = manager.GetResource<JascPalette>(inputData.PaletteName);

            SpriteList = PckSheetExtractor.DecodeFromInputData(inputData, palette, tab);
        }

        public override void Upscale(int targetWidth, int targetHeight, IResampler resampler = null)
        {
            if (resampler == null)
            {
                resampler = KnownResamplers.NearestNeighbor;
            }

            Width = targetWidth; 
            Height = targetHeight;

            foreach (var sprite in SpriteList)
            {
                sprite.Mutate(x => x.Resize(targetWidth, targetHeight, resampler));
            }
        }

        public override void SaveResource(string path)
        {
            int sheetWidth = ColumnCount * Width;
            //int sheetHeight = (SpriteList.Count / ColumnCount + 1) * Height;
            int sheetHeight = ColumnCount == 1 ? Height : (SpriteList.Count / ColumnCount + 1) * Height;

            // find sheet height
            int tempI = 1;
            while(true)
            {
                int temp = (int)Math.Pow(2, tempI);
                if(temp >= sheetHeight)
                {
                    sheetHeight = temp;
                    break;
                }
                tempI++;
            }

            Image<Rgba32> sheetToSave = new Image<Rgba32>(sheetWidth, sheetHeight, new Rgba32(0, 0, 0, 0));

            for(int i = 0; i < SpriteList.Count; i++)
            {
                int xPos = (i % ColumnCount) * Width;
                int yPos = (i / ColumnCount) * Height;
                sheetToSave.Mutate(x => x.DrawImage(SpriteList[i], new Point(xPos, yPos), 1f));
            }

            if (SpriteList.Count == 1 && Name.ToLower().Contains("bigob"))
            {
                // evil hardcoded bigob resize
                Image<Rgba32> resizedBigob = new Image<Rgba32>(64, 128, new Rgba32(0, 0, 0, 0));
                //sheetToSave.Mutate(x => x.DrawImage(SpriteList[i], new Point(xPos, yPos), 1f));
                resizedBigob.Mutate(x => x.DrawImage(sheetToSave, new Point(0, 0), 1f));
                sheetToSave = resizedBigob;
            }
            
            
            sheetToSave.SaveAsPng(path);
        }
    }
}
