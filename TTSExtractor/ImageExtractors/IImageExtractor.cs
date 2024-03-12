using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSExtractor.ImageExtractors
{
    public interface IImageExtractor
    {
        public byte[] DecodeBytes(in ReadOnlySpan<byte> inputStream, int width, int height);
    }
}
