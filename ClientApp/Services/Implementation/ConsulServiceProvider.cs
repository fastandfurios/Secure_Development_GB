using ClientApp.Services.Interfaces;
using Consul;

namespace ClientApp.Services.Implementation
{
    public class ConsulServiceProvider : IServiceDiscoveryProvider
    {
        public async Task<List<string>> GetServicesAsync(CancellationToken token = default)
        {
            var consulClient = new ConsulClient(configOverride =>
            {
                configOverride.Address = new("http://localhost:8500");
            });

            var queryResult = await consulClient.Health
                .Service(service: "ServiceA", tag: "api", passingOnly: true, ct: token)
                .ConfigureAwait(true);
            var result = new List<string>();
            foreach (var service in queryResult.Response)
            {
                result.Add($"{service.Service.Address}:{service.Service.Port}");
            }

            return result;
        }
    }
}
