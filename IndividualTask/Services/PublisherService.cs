using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IndividualTask.Contracts;
using Microsoft.Extensions.Hosting;

namespace IndividualTask.Services
{
    public class PublisherService : IHostedService
    {
        private readonly INewsPublisher _publisher;
        private readonly INewsReader _reader;
        private Timer _timer;

        public PublisherService(INewsPublisher publisher, INewsReader reader)
        {
            _publisher = publisher;
            _reader = reader;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(
                PublishNews,
                null,
                TimeSpan.Zero,
                TimeSpan.FromHours(24)
            );

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void PublishNews(object state)
        {
            foreach (var news in _reader.ReadNews())
            {
                _publisher.Publish(news);
            }
        }
    }
}
