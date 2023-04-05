using QarnotSDK.Secrets.Internal;

namespace QarnotSDK.Secrets
{
    public static class SecretsClientFactory
    {
        private static ISecretsClientFactory _actualFactory;

        private static ISecretsClientFactory ActualFactory
        {
            get
            {
                if (_actualFactory is null)
                {
                    _actualFactory = new Loader().Load();
                }

                return _actualFactory;
            }
        }

        public static ISecretsClient Build() =>
            ActualFactory.Build();
    }
}
