using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSExtractor.InputData
{
    public class PaletteInputData
    {
        public string SourceFile { get; }
        public string Name { get; }

        [JsonConstructor]
        public PaletteInputData(string sourceFile, string name)
        {
            SourceFile = sourceFile;
            Name = name;
        }
    }
}
