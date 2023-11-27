using System;
using System.Text;

namespace QarnotSDK.Secrets
{
    public class GetSecretException : Exception
    {
        public string Path { get; }

        public GetSecretException(string path, Exception innerException = null)
            : base(
                "", // We override it anyway
                innerException: innerException
            )
        {
            Path = path;
        }

        public override string Message => $"Error retrieving secret at `{Path}`: {InnerException}";
    }

    public class SecretNotFoundException : GetSecretException
    {
        public SecretNotFoundException(string path)
            : base(path)
        {
        }

        public override string Message => $"Secret `{Path}` doesn't exist";
    }

    public class ListSecretsException : Exception
    {
        public string Prefix { get; }

        public ListSecretsException(string prefix, Exception innerException = null)
            : base("", innerException: innerException)
        {
            Prefix = prefix;
        }

        public override string Message
        {
            get
            {
                var msg = new StringBuilder("Error listing secrets");
                if (!string.IsNullOrEmpty(Prefix)) msg.Append($" starting with `{Prefix}`");
                if (InnerException != null) msg.Append($": {InnerException}");

                return msg.ToString();
            }
        }
    }

    public class SecretDeserializationException : GetSecretException
    {
        public string Type { get; }

        public SecretDeserializationException(string path, string type_, Exception innerException)
            : base(path, innerException)
        {
            Type = type_;
        }

        public override string Message => $"Error deserializing secret `{Path}` to `{Type}`: {InnerException}";
    }
}
