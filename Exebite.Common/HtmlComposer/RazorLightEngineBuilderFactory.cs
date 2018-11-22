using System;
using RazorLight;

namespace Exebite.Common
{
    public class RazorLightEngineBuilderFactory : IRazorLightEngineBuilderFactory
    {
        public RazorLightEngine Create()
        {
            var engine = new RazorLightEngineBuilder()
                                  .UseFilesystemProject(Environment.CurrentDirectory + "\\View")
                                  .UseMemoryCachingProvider()
                                  .Build();
            return engine;
        }
    }
}
