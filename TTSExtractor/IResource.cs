using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSExtractor
{
    internal interface IResource
    {
        public string SourceFile { get; }

        public string Name { get; }
    }
}
