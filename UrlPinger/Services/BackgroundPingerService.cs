using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using UrlPinger.Repositories;

namespace UrlPinger.Services
{
    public class BackgroundPingerService : BackgroundService
    {
        private readonly ILogger<BackgroundPingerService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;

        public BackgroundPingerService(IMapper mapper, IServiceProvider serviceProvider, ILogger<BackgroundPingerService> logger)
        {

            _logger = logger;
            _serviceProvider = serviceProvider;
            _mapper = mapper;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var urlRepository = scope.ServiceProvider.GetRequiredService<IRemoteAddressRepository>();
                var remoteAddresses = await urlRepository.GetManyActive();

                if (remoteAddresses.Any())
                {
                    var ping = new Ping();

                    foreach (var remoteAddressDto in remoteAddresses)
                    {
                        try
                        {
                            var pingReply = ping.Send(remoteAddressDto.Url);
                            _logger.LogInformation($"{remoteAddressDto.Id}: {remoteAddressDto.Url}: {pingReply.Status}");
                        }
                        catch (PingException)
                        {
                            _logger.LogError($"{remoteAddressDto.Id}: {remoteAddressDto.Url} is not URL. Please, delete or set to inactive");
                        }

                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);
            }
        }
    }
}
