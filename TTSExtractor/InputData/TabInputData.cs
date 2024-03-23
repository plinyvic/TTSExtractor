using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TTSExtractor.InputData
{
    public class TabInputData
    {
        public string Name { get; }
        public string SourceFile { get; }

        public int OffsetSize { get; }

        [JsonConstructor]
        public TabInputData(string name, string sourceFile, int offsetSize)
        {
            Name = name;
            SourceFile = sourceFile;
            OffsetSize = offsetSize;
        }
    }
}
