using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TTSExtractor.ImageExtractors
{
    public class PckSheetExtractor
    {
        public List<byte[]> ExtractSpriteSheet(in ReadOnlySpan<byte> pckSpan, in ReadOnlySpan<byte> tabSpan, int width, int height, int tabSize = 2)
        {
            List<byte[]> spriteDataList = new List<byte[]>();
            PckExtractor extractor = new PckExtractor();

            for(int i = 0; i < tabSpan.Length; i += tabSize)
            {
                int offset = tabSize == 2 ? (int)BitConverter.ToUInt16(tabSpan.Slice(start: i, length: tabSize)) : (int)BitConverter.ToUInt32(tabSpan.Slice(start: i, length: tabSize));
                spriteDataList.Add(extractor.DecodeBytes(pckSpan.Slice(start: offset), width, height));
            }

            return spriteDataList;
        }
    }
}
