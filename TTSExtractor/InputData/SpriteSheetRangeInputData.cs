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

        [JsonProperty(Required = Required.Always)]
        public int ColumnCount { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int TargetWidth {  get; set; }

        [JsonProperty(Required = Required.Always)]
        public int TargetHeight { get; set; }
    }
}
