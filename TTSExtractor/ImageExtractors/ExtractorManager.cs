using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTSExtractor.InputData;
using TTSExtractor.Palletes;

namespace TTSExtractor.ImageExtractors
{
    public class ExtractorManager
    {
        public static Image<Rgba32> ExtractImage(ExtractedSpriteInputData inputData, JascPalette palette)
        {
            string extension = Path.GetExtension(inputData.SourceFile).ToLower();

            if(extension == ".pck")
            {
                return PckExtractor.DecodeFromInputData(inputData, palette);
            }
            else if(extension == ".scr" || extension == ".dat")
            {
                // these file types are uncompressed and dont really need a reader
                return Image.LoadPixelData<Rgba32>(File.ReadAllBytes(inputData.SourceFile).Select(x => palette.Palette[x]).ToArray(), inputData.Width, inputData.Height);
            }
            else
            {
                throw new NotImplementedException($"{extension} is not supported for extraction.");
            }
        }
    }
}
