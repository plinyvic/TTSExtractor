using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSExtractor.InputData
{
    public class ExtractedSpriteSheetInputData : SpriteSheetInputData
    {
        public string TabName { get; set; }
        public string PaletteName { get; set; }
    }
}
