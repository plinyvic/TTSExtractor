using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTSExtractor.InputData;
using TTSExtractor.Resource;

namespace TTSExtractor.Palletes
{
    public sealed class JascPalette : IResource
    {
        public string SourceFile { get; } = null;

        public string Name { get; } = null;

        public Rgba32[] Palette { get; set; } = new Rgba32[256];

        public JascPalette(PaletteInputData inputData)
        {
            SourceFile = inputData.SourceFile;
            Name = inputData.Name;
            LoadPalette();
        }

        private void LoadPalette()
        {
            using(StreamReader reader = new StreamReader(SourceFile))
            {
                _ = reader.ReadLine();
                _ = reader.ReadLine();
                _ = reader.ReadLine();
                _ = reader.ReadLine();
                string currentLine = default;
                int paletteIndex = 1;
                byte r, g, b;
                Palette[0].R = 0x00;
                Palette[0].G = 0x00;
                Palette[0].B = 0x00;
                Palette[0].A = 0x00;
                while (!String.IsNullOrWhiteSpace((currentLine = reader.ReadLine())))
                {
                    string[] rgb = currentLine.Split(' ');
                    r = Byte.Parse(rgb[0]);
                    g = Byte.Parse(rgb[1]);
                    b = Byte.Parse(rgb[2]);
                    Palette[paletteIndex].R = r;
                    Palette[paletteIndex].G = g;
                    Palette[paletteIndex].B = b;
                    Palette[paletteIndex].A = 0xFF;
                    paletteIndex++;
                }
            }
        }

        public void SaveResource(string path)
        {
            // do nothing <3
        }
    }
}
