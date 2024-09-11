using Azure.Messaging.ServiceBus;

namespace ServiceBus
{
    public static class MessageSender
    {
        public static async Task Send()
        {
            ServiceBusClient client;
            ServiceBusSender sender;

            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };

            client = new ServiceBusClient("<NAMESPACE-CONNECTION-STRING>", clientOptions);
            sender = client.CreateSender("<QUEUE-NAME>");

            var messages = new List<ServiceBusMessage>
            {
                new() {
                    MessageId = Guid.NewGuid().ToString(),
                    Subject = "subject",
                    Body = BinaryData.FromString($"My message")
                }
            };

            try
            {
                await sender.SendMessagesAsync(messages);
            }
            catch(Exception e)
            {
                Console.WriteLine("error: " + e.ToString());
            }
            finally
            {
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }
        }
    }
}
