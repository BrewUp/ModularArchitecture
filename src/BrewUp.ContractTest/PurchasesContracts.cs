using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PactNet;
using PactNet.Matchers;
using Xunit;
using Xunit.Abstractions;

namespace BrewUp.ContractTest;

/// <summary>
/// https://github.com/DiUS/pact-workshop-dotnet-core-v3/
/// </summary>
public class PurchasesContracts
{
    private IPactBuilderV3 _pact;
    private readonly int _port = 9000;
    
    public PurchasesContracts(ITestOutputHelper outputHelper)
    {
        var config = new PactConfig
        {
            PactDir = Path.Join("..", "..", "..", "..", "..", "pacts"),
            LogDir = "pact_logs",
            Outputters = new[] { new XUnitOutput(outputHelper) },
            DefaultJsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }
        };
        
        _pact = Pact.V3("ApiClient", "ProductService", config).UsingNativeBackend(_port);
        ApiClient = new ApiClient(new Uri($"http://localhost:{_port}"));
    }

    [Fact]
    public void EnsurePurchasesApiHonoursPactWithConsumer()
    {
    }
}