using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSExtractor.Resource
{
    public interface IResource
    {
        public string SourceFile { get; }

        public string Name { get; }

        public abstract void SaveResource(string path);
    }
}
