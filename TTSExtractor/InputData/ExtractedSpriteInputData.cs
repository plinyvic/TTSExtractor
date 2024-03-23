using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSExtractor.InputData
{
    [JsonObject(MemberSerialization = MemberSerialization.OptOut)]
    public class ExtractedSpriteInputData : SpriteInputData
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string PaletteName { get; set; }
    }
}
