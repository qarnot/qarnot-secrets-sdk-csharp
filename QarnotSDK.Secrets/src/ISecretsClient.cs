using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QarnotSDK.Secrets
{
    public interface ISecretsClient
    {
        Task<string> GetSecretRaw(string path, CancellationToken ct);
        Task<T> GetSecret<T>(string path, CancellationToken ct);
        Task<IEnumerable<string>> ListSecrets(string prefix = "", bool recursive = false, CancellationToken ct = default);
        Task<IDictionary<string, string>> ListAndGetSecretsRaw(string prefix = "", bool recursive = false, CancellationToken ct = default);
    }
}
