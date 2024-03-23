using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSExtractor.InputData
{
    [JsonObject(MemberSerialization = MemberSerialization.OptOut)]
    public class SpriteInputData
    {
        public string SourceFile { get; set; }
        public string Name { get; set; }
    }
}
