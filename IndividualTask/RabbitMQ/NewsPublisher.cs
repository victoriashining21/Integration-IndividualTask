using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IndividualTask.Contracts;
using IndividualTask.Extensions;
using IndividualTask.Models;
using RabbitMQ.Client;

namespace IndividualTask.RabbitMQ
{
    public class NewsPublisher : INewsPublisher
    {
        private readonly IModel _model;
        public NewsPublisher(IModel model)
        {
            _model = model;
        }

        public void Publish(News news)
        {
            Console.WriteLine(news.Title);
            _model.BasicPublish(
                "",
                "news",
                body: BinaryConverter.ObjectToByteArray(
                    news
                )
            );
        }
    }
}
