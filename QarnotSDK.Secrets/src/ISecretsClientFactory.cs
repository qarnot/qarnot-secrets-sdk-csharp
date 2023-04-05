namespace QarnotSDK.Secrets
{
    public interface ISecretsClientFactory
    {
        ISecretsClient Build();
    }
}
