tests: TestLoadInternalFactory
	./test-load-internal-factory \
		--factory-class-name QarnotSDK.Secrets.Tests.MockQarnotSecretsFactory \
		--factory-dll-location ../MockQarnotSecretsFactory/bin/Release/MockQarnotSecretsFactory.dll \
		--test-executable bin/Release/TestLoadInternalFactory \
		--expected-stdout 'secret value\n42\npath/to/secret\npath/to/secret: secret value\ninteger/secret: 42'
TestLoadInternalFactory:
	dotnet publish -c Release -o bin/Release

.PHONY: tests TestLoadInternalRuntime

