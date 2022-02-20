namespace ClientApp.Services.Interfaces;

public interface IServiceDiscoveryProvider
{
    Task<List<string>> GetServicesAsync(CancellationToken token);
}