using QarnotSDK.Secrets;

var client = SecretsClientFactory.Build();

var secret1 = await client.GetSecret("path/to/secret", default);
Console.WriteLine(secret1);

var secret2 = await client.GetSecret<int>("integer/secret", default);
Console.WriteLine(secret2);
