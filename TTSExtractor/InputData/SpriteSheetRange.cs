using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSExtractor.InputData
{
    public class SpriteSheetRange
    {
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        public int StartIndex { get; set; } = 0;

        [JsonProperty(Required = Required.DisallowNull)]
        public int? Length { get; set; } = null;
    }
}
