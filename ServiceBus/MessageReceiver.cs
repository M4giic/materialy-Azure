using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace ServiceBus
{
    public static class MessageReceiver
    {
        public static async Task Receive()
        {
            ServiceBusClient client;
            ServiceBusProcessor processor;

            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };

            client = new ServiceBusClient("<NAMESPACE-CONNECTION-STRING>", clientOptions);
            processor = client.CreateProcessor("<QUEUE-NAME>", new ServiceBusProcessorOptions());

            try
            {
                processor.ProcessMessageAsync += MessageHandler;
                processor.ProcessErrorAsync += ErrorHandler;

                await processor.StartProcessingAsync();

                Console.WriteLine("Press any key to end the processing");
                Console.ReadKey();

                Console.WriteLine("\nStopping the receiver...");
                await processor.StopProcessingAsync();
                Console.WriteLine("Stopped receiving messages");
            }
            finally
            {
                await processor.DisposeAsync();
                await client.DisposeAsync();
            }
        }

        private async static Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            Console.WriteLine($"Received: {body}");

            await args.CompleteMessageAsync(args.Message);
        }

        private static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
