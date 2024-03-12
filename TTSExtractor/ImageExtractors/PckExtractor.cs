using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSExtractor.ImageExtractors
{
    public class PckExtractor : IImageExtractor
    {
        public const byte _SKIP = 0xFE;
        public const byte _END = 0xFF;

        public byte[] DecodeBytes(in ReadOnlySpan<byte> inputStream, int width, int height)
        {
            byte[] byteStream = new byte[width * height];
            int byteCount = 0;

            // start index at 1; first byte to read
            int index = 1;

            // skip width * rows to skip
            byte rowsToSkip = inputStream[0];
            byteCount += rowsToSkip * width;

            byte currentValue = 0;
            while (true)
            {
                currentValue = inputStream[index];

                switch(currentValue)
                {
                    case _SKIP:
                        index++;
                        currentValue = inputStream[index];
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
