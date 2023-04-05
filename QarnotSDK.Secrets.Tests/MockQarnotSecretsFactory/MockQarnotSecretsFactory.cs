namespace QarnotSDK.Secrets.Tests;

public class MockQarnotSecretsFactory : ISecretsClientFactory
{
    private readonly IDictionary<string, string> _secrets = new Dictionary<string, string>
    {
        { "path/to/secret", "secret value" },
        { "integer/secret", "42" },
    };

    public ISecretsClient Build() =>
        new MockQarnotSecretsClient(_secrets);
}
