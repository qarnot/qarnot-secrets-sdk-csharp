using System.Text.Json;

namespace QarnotSDK.Secrets.Tests;

public class MockQarnotSecretsClient : ISecretsClient
{
    private readonly IDictionary<string, string> _secrets;

    public MockQarnotSecretsClient(IDictionary<string, string> secrets)
    {
        _secrets = secrets;
    }

    public Task<string> GetSecret(string path, CancellationToken ct) =>
        Task.FromResult(_secrets[path]);

    public Task<T> GetSecret<T>(string path, CancellationToken ct) =>
        Task.FromResult(
            JsonSerializer.Deserialize<T>(_secrets[path])
            ?? throw new JsonException($"can't deserialize to {typeof(T)}")
        );
}