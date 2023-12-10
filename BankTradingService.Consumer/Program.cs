using Confluent.Kafka;

class Program
{
    private static void Main(string[] args)
    {
        // Setup Consumer using Kafka and subscribe to publishing service
        ConsumerConfig config = new ConsumerConfig
        {
            BootstrapServers = "kafka:29092",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            ClientId = "BankTradingServiceClient",
            GroupId = "BankTradingServiceGroup",
            BrokerAddressFamily = BrokerAddressFamily.V4
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe("BankTradingService");

        // Loop infinitely in order to be able to capture any incoming messages in message queue
        while (true)
        {
            // Will wait for a message to consume and display the message
            var consumeResult = consumer.Consume();
            Console.WriteLine($"Message received from {consumeResult.TopicPartitionOffset}: {consumeResult.Message.Value}");
        }

        consumer.Close();
        Console.ReadLine();
    }
}