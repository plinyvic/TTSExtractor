using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTSExtractor.Palletes;
using TTSExtractor.Resource;
using TTSExtractor.Sprites;

namespace TTSExtractor.InputData
{
    public class InputReader
    {
        //#if DEBUG
        //        public const string _INPUTDIRECTORY = @"../../../Config/";
        //#else
        //        public const string _INPUTDIRECTORY = @"./Config/";
        //#endif

        public const string _INPUTDIRECTORY = @"./Config/";

        public static void ParseInputs(ResourceManager manager)
        {
            string[] allFiles = Directory.GetFiles(_INPUTDIRECTORY);

            string[] paletteConfigs = allFiles.Where(x => x.EndsWith("palettes.json")).ToArray();
            foreach (string paletteConfig in paletteConfigs)
            {
                string jsonText = File.ReadAllText(paletteConfig);
                JascPalette[] palettes = JsonConvert.DeserializeObject<PaletteInputData[]>(jsonText).Select(x => new JascPalette(x)).ToArray();
                foreach(JascPalette palette in palettes)
                {
                    manager.RegisterResource(palette);
                }
            }

            string[] tabConfigs = allFiles.Where(x => x.EndsWith("tabs.json")).ToArray();
            foreach (string tabConfig in tabConfigs)
            {
                string jsonText = File.ReadAllText(tabConfig);
                TabFile[] tabs = JsonConvert.DeserializeObject<TabInputData[]>(jsonText).Select(x => new TabFile(x)).ToArray();
                foreach (TabFile tab in tabs)
                {
                    manager.RegisterResource(tab);
                }
            }

            string[] spriteConfigs = allFiles.Where(x => x.EndsWith("sprites.json")).ToArray();
            foreach (string spriteConfig in spriteConfigs)
            {
                string jsonText = File.ReadAllText(spriteConfig);
                Sprite[] sprites = JsonConvert.DeserializeObject<SpriteInputData[]>(jsonText).Select(x => new Sprite(x)).ToArray();
                foreach(Sprite sprite in sprites)
                {
                    manager.RegisterResource(sprite);
                }
            }

            string[] extractedSpriteConfigs = allFiles.Where(x => x.EndsWith("extractedSprites.json")).ToArray();
            foreach(string extractedSpriteConfig in extractedSpriteConfigs)
            {
                string jsonText = File.ReadAllText(extractedSpriteConfig);
                Sprite[] sprites = JsonConvert.DeserializeObject<ExtractedSpriteInputData[]>(jsonText).Select(x => new Sprite(x, manager)).ToArray();
                foreach( Sprite sprite in sprites)
                {
                    manager.RegisterResource(sprite);
                }
            }

            string[] spriteSheetConfigs = allFiles.Where(x => x.EndsWith("spriteSheets.json")).ToArray();
            foreach(string spriteSheetConfig in spriteSheetConfigs)
            {
                string jsonText = File.ReadAllText(spriteSheetConfig);
                SpriteSheet[] spriteSheets = JsonConvert.DeserializeObject<SpriteSheetInputData[]>(jsonText).Select(x => new SpriteSheet(x)).ToArray();
                foreach(SpriteSheet spriteSheet in spriteSheets)
                {
                    manager.RegisterResource(spriteSheet);
                }
            }

            string[] extractedSpriteSheetConfigs = allFiles.Where(x => x.EndsWith("extractedSpriteSheets.json")).ToArray();
            foreach(string extractedSpriteSheetConfig in extractedSpriteSheetConfigs)
            {
                string jsonText = File.ReadAllText(extractedSpriteSheetConfig);
                SpriteSheet[] spriteSheets = JsonConvert.DeserializeObject<ExtractedSpriteSheetInputData[]>(jsonText).Select(x => new SpriteSheet(x, manager)).ToArray();
                foreach (SpriteSheet spriteSheet in spriteSheets)
                {
                    manager.RegisterResource(spriteSheet);
                }
            }

            string[] sheetRangeConfigs = allFiles.Where(x => x.EndsWith("combinedSpriteSheets.json")).ToArray();
            foreach(string sheetRangeConfig in sheetRangeConfigs) 
            {
                string jsonText = File.ReadAllText(sheetRangeConfig);
                SpriteSheet[] spriteSheets = JsonConvert.DeserializeObject<SpriteSheetRangeInputData[]>(jsonText).Select(x => new SpriteSheet(x, manager)).ToArray();
                foreach (SpriteSheet spriteSheet in spriteSheets)
                {
                    manager.RegisterResource(spriteSheet);
                }
            }

            string[] outputConfigs = allFiles.Where(x => x.EndsWith("output.json")).ToArray();
            foreach (string outputConfig in outputConfigs)
            {
                string jsonText = File.ReadAllText(outputConfig);
                OutputResourceData[] outputDatas = JsonConvert.DeserializeObject<OutputResourceData[]>(jsonText);
                foreach (OutputResourceData outputData in outputDatas)
                {
                    manager.MapOutput(outputData);
                }
            }
        }
    }
}
