using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTSExtractor.InputData;
using TTSExtractor.Resource;

namespace TTSExtractor.Sprites
{
    public class TabFile : IResource
    {
        public string SourceFile {get; protected set;}

        public string Name { get; protected set; }

        public int OffsetSize { get; protected set; }

        public int[] Offsets { get; protected set; }

        public TabFile(in TabInputData data)
        {
            SourceFile = data.SourceFile;
            Name = data.Name;
            OffsetSize = data.OffsetSize;
            LoadTab();
        }

        private void LoadTab()
        {
            Span<byte> buffer = File.ReadAllBytes(SourceFile).AsSpan<byte>();

            Offsets = new int[buffer.Length / OffsetSize];
            int index = 0;
            for(int i = 0; i < buffer.Length; i += OffsetSize)
            {
                Offsets[index] = OffsetSize == 2 ? (int)BitConverter.ToUInt16(buffer.Slice(start: i, length: OffsetSize)) : (int)BitConverter.ToUInt32(buffer.Slice(start: i, length: OffsetSize));
                index++;
            }
        }

        public void SaveResource()
        {
            // do nothing <3
        }
    }
}
