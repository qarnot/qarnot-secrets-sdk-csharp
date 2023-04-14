using System.Threading;
using System.Threading.Tasks;

namespace QarnotSDK.Secrets
{
    public interface ISecretsClient
    {
        Task<string> GetSecretRaw(string path, CancellationToken ct);
        Task<T> GetSecret<T>(string path, CancellationToken ct);
    }
}
