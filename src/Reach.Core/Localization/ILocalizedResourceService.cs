using System.Threading.Tasks;

namespace Reach.Localization
{
    public interface ILocalizedResourceService
    {
        Task<string> GetAsync(string key);
    }
}
