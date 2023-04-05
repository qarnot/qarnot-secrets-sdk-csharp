﻿using System.Text.Json;
using QarnotSDK.Secrets;

// This is a very small sample showcasing how one can use QarnotSDK.Secrets
// in their own application.

// This switch can obviously be retrived from environment variable,
// command line argument, etc.
var shouldRunLocally = true;

// Choose between:
// * A mock or alternative implementation; ideal for testing or running
//   the application outside of a Qarnot task.
// * The Qarnot implentation; this will throw if not run inside a task
var secretClient = shouldRunLocally
  ? new MockSecretsClientFactory().Build()
  : SecretsClientFactory.Build();

// Query the secret as a string.
var secret1 = await secretClient.GetSecret("path/to/secret", default);
Console.WriteLine($"Secret at `path/to/secret` is `{secret1}`");

// Query the secret and JSON-deserialize it.
var secret2 = await secretClient.GetSecret("integer/secret", default);
Console.WriteLine($"Secret at `integer/secret` is `{secret2}`");


// A mock factory implementation with a few secrets for testing purposes.
// If the application is meant to be ran outside of a Qarnot task for other
// that testing purposes, a more complex implemenetation can be used.
public class MockSecretsClientFactory : ISecretsClientFactory
{
    private readonly IDictionary<string, string> _secrets = new Dictionary<string, string>
    {
        { "path/to/secret", "secret value" },
        { "integer/secret", "42" },
    };

    public ISecretsClient Build() =>
        new MockSecretsClient(_secrets);
}

// Really simply mock client, reading secret from an in-memory dictionary.
public class MockSecretsClient : ISecretsClient
{
    private readonly IDictionary<string, string> _secrets;

    public MockSecretsClient(IDictionary<string, string> secrets)
    {
        _secrets = secrets;
    }

    public Task<string> GetSecret(string path, CancellationToken ct) =>
        Task.FromResult(_secrets[path]);

    public Task<T> GetSecret<T>(string path, CancellationToken ct) =>
        Task.FromResult(
            JsonSerializer.Deserialize<T>(_secrets[path])
            ?? throw new JsonException($"can't deserialize secret to {typeof(T)}")
        );
}