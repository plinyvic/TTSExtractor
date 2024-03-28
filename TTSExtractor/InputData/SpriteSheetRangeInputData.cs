using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSExtractor.InputData
{
    public class SpriteSheetRangeInputData
    {
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(Required = Required.Always)]
        public List<SpriteSheetRange> SpriteSheets { get; set; } = new List<SpriteSheetRange>();

        [JsonProperty(Required = Required.DisallowNull)]
        public int ColumnCount { get; set; } = 1;

        [JsonProperty(Required = Required.DisallowNull)]
        public int TargetWidth { get; set; } = 64;

        [JsonProperty(Required = Required.DisallowNull)]
        public int TargetHeight { get; set; } = 96;
    }
}
