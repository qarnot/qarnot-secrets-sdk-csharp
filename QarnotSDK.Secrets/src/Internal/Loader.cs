using System;
using System.Reflection;

namespace QarnotSDK.Secrets.Internal
{
    public class Loader
    {
        private const string INTERNAL_DLL_ENV_VAR = "QARNOT_SECRETS_INTERNAL_DLL";
        private const string INTERNAL_CLASS_NAME_ENV_VAR = "QARNOT_SECRETS_INTERNAL_CLASS_NAME";

        public ISecretsClientFactory Load()
        {
            var dllPath = ReadEnvVar(INTERNAL_DLL_ENV_VAR);
            var className = ReadEnvVar(INTERNAL_CLASS_NAME_ENV_VAR);

            if (dllPath is null || className is null)
            {
                throw new ArgumentException(
                    $"Both {INTERNAL_DLL_ENV_VAR} and {INTERNAL_CLASS_NAME_ENV_VAR} must be"
                    + " specified when running remotely"
                );
            }

            var assembly = Assembly.LoadFrom(dllPath);
            var type_ = assembly.GetType(className, throwOnError: true);
            var ctor = type_.GetConstructor(Type.EmptyTypes);
            if (ctor is null)
            {
                throw new Exception($"No default constructor found for {className}");
            }

            return (ISecretsClientFactory)ctor.Invoke(null);
        }

        private string ReadEnvVar(string name)
        {
            var value = Environment.GetEnvironmentVariable(name);
            if (value != null)
            {
                return value;
            }

            return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Machine);
        }
    }
}
