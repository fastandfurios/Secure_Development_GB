using ClientApp.Services.Interfaces;

namespace ClientApp.Services.Implementation
{
    public class PollingConsulServiceProvider : IServiceDiscoveryProvider
    {
        private bool _polling;
        private List<string> _services = new();

        public PollingConsulServiceProvider()
        {
            var _timer = new Timer(async _ =>
            {
                if (_polling) return;
                _polling = true;
                await Poll().ConfigureAwait(true);
                _polling = false;
            }, state: null, dueTime: 0, period: 1000);
        }

        public async Task<List<string>> GetServicesAsync(CancellationToken token = default)
        {
            if (_services.Count == 0) await Poll(token).ConfigureAwait(true);
            return _services!;
        }

        private async Task Poll(CancellationToken token = default)
        {
            _services = await new ConsulServiceProvider().GetServicesAsync(token).ConfigureAwait(true);
        }
    }
}
