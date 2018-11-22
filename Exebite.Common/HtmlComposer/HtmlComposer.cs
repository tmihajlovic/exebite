using System.Threading.Tasks;

namespace Exebite.Common
{
    public class HtmlComposer : IHtmlComposer
    {
        private readonly IRazorLightEngineBuilderFactory _razorLightEngineBuilderFactory;

        public HtmlComposer(IRazorLightEngineBuilderFactory razorLightEngineBuilderFactory)
        {
            _razorLightEngineBuilderFactory = razorLightEngineBuilderFactory;
        }

        public async Task<string> ComposeFromPath<T>(string path, T model)
        {
            var engine = _razorLightEngineBuilderFactory.Create();
            //key is not essential it takes a long time for firs run, after compilation speed is acceptable            
            string result = await engine.(path, model);
            return result;
        }

        public async Task<string> ComposeFromString<T>(string template, T model)
        {
            var engine = _razorLightEngineBuilderFactory.Create();
            //key is not essential it takes a long time for firs run, after compilation speed is acceptable
            string result = await engine.CompileRenderAsync("keyNotNeeded", template, model);
            return result;
        }

        public async Task<string> ComposeFromString(string definition)
        {
            return await ComposeFromString<object>(definition, null);
        }
    }
}
