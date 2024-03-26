using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TTSExtractor.InputData;

namespace TTSExtractor.Resource
{
    public class OutputPair
    {
        public IResource OutputResource { get; }

        public string OutputPath { get; }

        public OutputPair(IResource outputResource, string outputPath)
        {
            OutputResource = outputResource;
            OutputPath = outputPath;
        }
    }

    public class ResourceManager
    {
        public Dictionary<string, IResource> ResourceLibrary { get; } = new Dictionary<string, IResource>();

        public List<OutputPair> OutputPairs { get; } = new List<OutputPair>();

        /// <summary>
        /// Register a resource with this library.
        /// </summary>
        /// <param name="resource"></param>
        /// <exception cref="ArgumentException"></exception>
        public void RegisterResource(IResource resource)
        {
            if(ResourceLibrary.ContainsKey(resource.Name))
            {
                throw new ArgumentException($"{resource.GetType().Name} {resource.Name} has already been created or imported.");
            }

            ResourceLibrary.Add(resource.Name, resource);
        }

        /// <summary>
        /// Return matching resource from library.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public IResource GetResource(string name)
        {
            if(!ResourceLibrary.ContainsKey(name))
            {
                throw new ArgumentException($"{name} not in library.");
            }
            
            return ResourceLibrary[name];
        }

        /// <summary>
        /// Return matching downcasted resource from library.
        /// </summary>
        /// <typeparam name="TResource"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public TResource GetResource<TResource>(string name) where TResource : IResource
        {
            IResource resource = GetResource(name);
            if (resource is TResource casted)
            {
                return casted;
            }
            else throw new ArgumentException($"{name} is not a {nameof(TResource)}");
        }

        public void MapOutput(OutputResourceData output)
        {
            OutputPairs.Add(new OutputPair(GetResource(output.Name), output.OutputPath));
        }

        public void SaveAllOutputs()
        {
            foreach(var output in OutputPairs)
            {
                output.OutputResource.SaveResource(output.OutputPath);
            }
        }
    }
}
