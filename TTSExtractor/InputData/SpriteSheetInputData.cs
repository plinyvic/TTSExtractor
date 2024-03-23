using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSExtractor.InputData
{
    public class SpriteSheetInputData : SpriteInputData
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int ColumnCount { get; set; } = 0;
    }
}
