using ECommerce.Baskets.Domain.Interfaces.Data;
using ECommerce.Baskets.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Baskets.Api.Services
{
    public class CloseAbandonedBasketService : IHostedService, IDisposable
    {
        private readonly ILogger<CloseAbandonedBasketService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private Timer? _timer = null;

        public CloseAbandonedBasketService(ILogger<CloseAbandonedBasketService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        private void DoWork(object? state)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _repository = scope.ServiceProvider.GetRequiredService<IBasketRepository>();
                var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var baskets = _repository.Filter(b => !b.IsEnded && DateTime.Compare(b.CreatedAt, DateTime.Now.AddDays(5)) < 0).Result;

                foreach (var basket in baskets)
                {
                    basket.DeletedAt = DateTime.Now;

                    _repository.Update(basket);
                    _unitOfWork.Commit();
                }

                _logger.LogInformation(
                    "Hosted Service is working...");
            };
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Hosted Service running...");

#if DEBUG
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                               TimeSpan.FromMinutes(10));
#else
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(30));
#endif

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping...");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
