using ECommerce.Basket.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Baskets.Api.Services
{
    public class CloseAbandonedBasketService : BackgroundService
    {
        private readonly IBasketRepository _repository;
        private readonly ILogger<CloseAbandonedBasketService> _logger;
        private Timer? _timer = null;

        public CloseAbandonedBasketService(IBasketRepository repository, ILogger<CloseAbandonedBasketService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Hosted Service running...");

            while(!stoppingToken.IsCancellationRequested)
            {
                _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(10));
            }

            _logger.LogInformation("Hosted Service stopping...");

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            var result = _repository.Filter(b => !b.IsEnded && DateTime.Compare(b.CreatedAt, DateTime.Now.AddDays(5)) < 0).Result;

            foreach (var item in result)
            {
                item.DeletedAt = DateTime.Now;
            }

            _logger.LogInformation(
                "Hosted Service is working...");
        }
    }
}
