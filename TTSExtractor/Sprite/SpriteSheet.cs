using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSExtractor.Sprite
{
    public class SpriteSheet : ISprite
    {
        public int Width { get; protected set; }

        public int Height { get; protected set; }

        public int SpriteWidth { get; }

        public int SpriteHeight { get; }

        public int ColumnCount { get; }

        public List<ISprite> SpriteList { get; protected set; }

        public Image<Rgba32> Sprite { get; protected set; }

        public SpriteSheet(int spriteWidth, int spriteHeight, int columnCount, List<ISprite> spriteList)
        {
            SpriteHeight = spriteHeight;
            SpriteWidth = spriteWidth;
            ColumnCount = columnCount;

            SpriteList = spriteList;

            MakeSheet();
        }

        private SpriteSheet(int spriteWidth, int spriteHeight, Image<Rgba32> sprite)
        {
            SpriteWidth = spriteWidth;
            SpriteHeight = spriteHeight;
            Width = sprite.Width;
            Height = sprite.Height;
            Sprite = sprite;
        }

        public static SpriteSheet LoadAndSliceSpriteSheet(string file, int spriteWidth, int spriteHeight, int maxToSlice = Int32.MaxValue)
        {
            SpriteSheet spriteSheet = new SpriteSheet(spriteWidth, spriteHeight, Image.Load<Rgba32>(file));

            int spritesWide = spriteSheet.Width / spriteWidth;
            int spritesTall = spriteSheet.Height / spriteHeight;

            for(int i = 0; i < spritesWide*spritesTall && i < maxToSlice; i++)
            {
                int xPos = (i % spritesWide) * spriteWidth;
                int yPos = (i / spritesWide * spriteHeight);


            }

            return spriteSheet;
        }

        private void MakeSheet()
        {
            // verify all sprites in list are same size
            foreach(var sprite in SpriteList)
            {
                if(sprite.Width != SpriteWidth || sprite.Height != SpriteHeight)
                {
                    throw new ArgumentException($"Not all sprites in sheet are same dimensions.");
                }
            }

            // create proper sheet size
            Width = SpriteWidth * ColumnCount;
            Height = SpriteList.Count / ColumnCount * SpriteHeight;
            Sprite = new Image<Rgba32>(Width, Height);

            for(int i = 0; i < SpriteList.Count; i++)
            {
                int xPos = (i % ColumnCount) * SpriteWidth;
                int yPos = (i / ColumnCount * SpriteHeight);
                Sprite.Mutate(x => x.DrawImage(SpriteList[i].Sprite, new Point(xPos, yPos), 1f));
            }
        }

    }
}
