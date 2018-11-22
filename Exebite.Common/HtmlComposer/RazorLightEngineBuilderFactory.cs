using System;
using RazorLight;

namespace Exebite.Common
{
    public class RazorLightEngineBuilderFactory : IRazorLightEngineBuilderFactory
    {
        public IRazorLightEngine Create()
        {
            //var engine = new RazorLightEngine()
            //                      .UseFilesystemProject(Environment.CurrentDirectory + "\\View")
            //                      .UseMemoryCachingProvider()
            //                      .Build();

            var engine = new RazorLightEngineBuilder()
              .UseFilesystemProject(Environment.CurrentDirectory + "\\View")
              .UseMemoryCachingProvider()
              .Build();
            return engine;
        }
    }
}
