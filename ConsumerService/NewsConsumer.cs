using IndividualTask.Extensions;
using IndividualTask.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using ConsumerService.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace ConsumerService
{
    public class NewsConsumer : IHostedService
    {
        private readonly IModel _channel;
        private Task _executingTask;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public NewsConsumer(IModel channel, IServiceScopeFactory serviceScopeFactory)
        {
            _channel = channel;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _executingTask = ReadQueue();

            if (_executingTask.IsCompleted)
            {
                return _executingTask;
            }

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask == null)
            {
                return;
            }
        }

        private async Task ReadQueue()
        {
            var scope = _serviceScopeFactory.CreateScope();

            var myScopedService = scope.ServiceProvider.GetService<IRepository<News>>();

            var queueName = "news";
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received +=  (ch, ea) =>
            {
                var body = ea.Body.ToArray();

                var news = BinaryConverter.ByteArrayToObject<News>(body);

               myScopedService.Insert(news);

                _channel.BasicAck(ea.DeliveryTag, false);
            };
            // this consumer tag identifies the subscription
            // when it has to be cancelled
            var consumerTag = _channel.BasicConsume(queueName, false, consumer);
        }
    }
}
