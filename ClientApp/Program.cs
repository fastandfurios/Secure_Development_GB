using ClientApp.Services.Implementation;
using ClientApp.Services.Interfaces;

await Task.Delay(40000).ConfigureAwait(true);
IServiceDiscoveryProvider consul = new ConsulServiceProvider();
var r = await consul.GetServicesAsync(CancellationToken.None);
IServiceDiscoveryProvider pollingConsul = new PollingConsulServiceProvider();
var result = await pollingConsul.GetServicesAsync(CancellationToken.None);
foreach (var entry in result)
{
    Console.WriteLine(entry);
}
Console.ReadLine();