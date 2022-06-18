using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;

namespace NewsConsumerAzureFunc
{
    public static class ConsumerService
    {
        [FunctionName("ConsumerService")]
        public static void Run([RabbitMQTrigger("news", ConnectionStringSetting = "host=localhost;username=guest;password=guest")] string args, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {args}");
        }
    }
}
