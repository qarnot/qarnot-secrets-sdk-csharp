using QarnotSDK.Secrets;

var client = SecretsClientFactory.Build();

var secret1 = await client.GetSecretRaw("path/to/secret", default);
Console.WriteLine(secret1);

var secret2 = await client.GetSecret<int>("integer/secret", default);
Console.WriteLine(secret2);

var secrets =  await client.ListSecrets("path/to");
Console.WriteLine(string.Join(", ", secrets));

