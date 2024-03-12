using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSExtractor.ImageExtractors
{
    public class SpkExtractor : IImageExtractor
    {
        /// <summary>
        /// skip preceding number of pixels * 2
        /// </summary>
        const ushort _SKIP = 0xFFFF;

        /// <summary>
        /// draw preceding number of pixels * 2
        /// </summary>
        const ushort _DRAW = 0xFFFE;

        /// <summary>
        /// EOF
        /// </summary>
        const ushort _END = 0xFFFD;

        public byte[] DecodeBytes(in ReadOnlySpan<byte> inputStream, int width = 320, int height = 200)
        {
            byte[] byteStream = new byte[width * height];
            int byteCount = 0;
            int index = 0;

            ushort currentValue = 0;
            ushort skipValue = 0;
            ushort readValue = 0;
            while (true)
            {
                // read current ushort, move index forward by 4 bytes
                currentValue = BitConverter.ToUInt16(inputStream.Slice(start: index, length: 2));
                index += 2;
                switch(currentValue)
                {
                    case (_SKIP):
                        skipValue = (ushort)(BitConverter.ToUInt16(inputStream.Slice(start: index, length: 2)) * 2);
                        index += 2;
                        byteCount += skipValue;
                        break;

                    case (_DRAW):
                        readValue = (ushort)(BitConverter.ToUInt16(inputStream.Slice(start: index, length: 2)) * 2);
                        index += 2;
                        for(int i = 0; i < readValue; i++)
                        {
                            byteStream[byteCount] = inputStream[index];
                            index++;
                            byteCount++;
                        }
                        break;

                    case (_END):
                        goto end;

                    default:
                        throw new ApplicationException("Invalid SPK file.");
                }
            }
        end:

            return byteStream;
        }
    }
}
