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
    public class PckExtractor
    {
        public const byte _SKIP = 0xFE;
        public const byte _END = 0xFF;

        public static Image<Rgba32> DecodeFromInputData(in ExtractedSpriteInputData inputData, JascPalette palette)
        {
            Span<byte> inputSpan = File.ReadAllBytes(inputData.SourceFile);
            int width = inputData.Width;
            int height = inputData.Height;

            return Image.LoadPixelData<Rgba32>(DecodeBytesFromSpan(inputSpan, width, height).Select(x => palette.Palette[x]).ToArray(), width, height);
        }

        public static Image<Rgba32> DecodeFromInputSpan(in ReadOnlySpan<byte> inputSpan, JascPalette palette, int width, int height)
        {
            return Image.LoadPixelData<Rgba32>(DecodeBytesFromSpan(inputSpan, width, height).Select(x => palette.Palette[x]).ToArray(), width, height);
        }

        protected static byte[] DecodeBytesFromSpan(in ReadOnlySpan<byte> inputSpan, int width, int height)
        {
            byte[] byteStream = new byte[width * height];
            int byteCount = 0;

            // start index at 1; first byte to read
            int index = 1;

            // skip width * rows to skip
            byte rowsToSkip = inputSpan[0];
            byteCount += rowsToSkip * width;

            byte currentValue = 0;
            while (true)
            {
                currentValue = inputSpan[index];

                switch (currentValue)
                {
                    case _SKIP:
                        index++;
                        currentValue = inputSpan[index];
                        byteCount += currentValue;
                        index++;
                        break;

                    case _END:
                        goto end;

                    default:
                        byteStream[byteCount] = currentValue;
                        byteCount++;
                        index++;
                        break;
                }
            }

        end:

            return byteStream;
        }
    }
}
