using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
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
        }

        public SpriteSheet(ExtractedSpriteSheetInputData inputData, ResourceManager manager)
        {
            Width = inputData.Width;
            Height = inputData.Height;
            ColumnCount = inputData.ColumnCount;
            Name = inputData.Name;
            SourceFile = inputData.SourceFile;

            LoadSpriteSheet(inputData, manager);
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

        public override void SaveResource()
        {
            int sheetWidth = ColumnCount * Width;
            int sheetHeight = (SpriteList.Count / ColumnCount + 1) * Height;
            Image<Rgba32> sheetToSave = new Image<Rgba32>(sheetWidth, sheetHeight, new Rgba32(0, 0, 0, 0));

            for(int i = 0; i < SpriteList.Count; i++)
            {
                int xPos = (i % ColumnCount) * Width;
                int yPos = (i / ColumnCount) * Height;
                sheetToSave.Mutate(x => x.DrawImage(SpriteList[i], new Point(xPos, yPos), 1f));
            }

            sheetToSave.SaveAsPng($"Output/Dump/Sprite/{Name}.png");
        }
    }
}
