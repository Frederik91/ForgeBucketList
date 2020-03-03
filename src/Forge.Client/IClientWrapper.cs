using System.Threading.Tasks;
using RestSharp;

namespace Forge.Client
{
    public interface IClientWrapper
    {
        Task RefreshToken();
        Task<T> Execute<T>(IRestRequest request) where T : new();
        Task Execute(IRestRequest request);
    }
}