using System.Threading.Tasks;

namespace Exebite.Common
{
    public interface IHtmlComposer
    {
        Task<string> ComposeFromPath<T>(string path, T model);
        Task<string> ComposeFromString<T>(string definition, T model);
        Task<string> ComposeFromString(string definition);
    }
}