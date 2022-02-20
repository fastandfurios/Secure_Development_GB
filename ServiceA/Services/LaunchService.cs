using System.Net;
using Consul;

namespace ServiceA.Services
{
    public class LaunchService : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly IConsulClient _consulClient;
        private CancellationTokenSource _cts;
        private string _serviceId;

        public LaunchService(IConsulClient consulClient, IConfiguration configuration)
        {
            _consulClient = consulClient;
            _configuration = configuration;
        }

        public async Task StartAsync(CancellationToken token = default)
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(token);
            var uri = new Uri(_configuration.GetConnectionString("ServiceA"));
            _serviceId = $"Service-v1-{Dns.GetHostName()}-{uri.Authority}";
            var registration = new AgentServiceRegistration
            {
                ID = _serviceId,
                Name = "ServiceA",
                Address = uri.Host,
                Port = uri.Port,
                Tags = new[] { "api" },
                Check = new()
                {
                    HTTP = $"{uri.Scheme}://{uri.Host}:{uri.Port}/healthz",
                    Timeout = TimeSpan.FromSeconds(2),
                    Interval = TimeSpan.FromSeconds(10)
                }
            };

            await _consulClient.Agent.ServiceDeregister(registration.ID, _cts.Token);
            await _consulClient.Agent.ServiceRegister(registration, _cts.Token);
        }

        public async Task StopAsync(CancellationToken token = default)
        {
            _cts.Cancel();
            await _consulClient.Agent.ServiceDeregister(_serviceId, token);
        }
    }
}
