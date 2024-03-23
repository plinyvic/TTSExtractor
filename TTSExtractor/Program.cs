using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TTSExtractor.ImageExtractors;
using TTSExtractor.InputData;
using TTSExtractor.Palletes;
using TTSExtractor.Resource;

namespace TTSExtractor
{
    internal class Program
    {
        static int Main(string[] args)
        {
#if DEBUG
            Directory.SetCurrentDirectory("../../../");
#endif

            // instantiate manager
            ResourceManager resourceManager = new ResourceManager();
            InputReader.ParseInputs(resourceManager);

            resourceManager.SaveAllToDisk();

            return 0;
        }
    }
}