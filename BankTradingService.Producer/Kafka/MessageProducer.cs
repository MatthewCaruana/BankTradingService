using BankTradingService.Data.Models;
using BankTradingService.Producer.Kafka.Interface;
using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTradingService.Producer.Kafka
{
    public class MessageProducer : IMessageProducer
    {
        private ProducerConfig config;
        private IProducer<Null, string> producer;
        
        public MessageProducer()
        {
            config = new ProducerConfig()
            {
                BootstrapServers = "kafka:29092",
                ClientId = "BankTradingServiceClient",
                BrokerAddressFamily = BrokerAddressFamily.V4
            };

            producer = new ProducerBuilder<Null, string>(config).Build();
        }

        /// <summary>
        /// Produces message onto message queue with new trade information
        /// </summary>
        /// <param name="trade"></param>
        /// <returns></returns>
        public async Task CreateOpenTradeMessage(TradeDataModel trade)
        {
            var message = new Message<Null, string>
            {
                Value = $"Successfully created new Trade for UserID: {trade.UserID}, Symbol: {trade.Symbol}, Amount: {trade.Amount}, Transaction Type: {trade.TransactionType}"
            };

            var result = await producer.ProduceAsync("BankTradingService", message);
            Console.WriteLine($"Event sent on Partition: {result.Partition} with Offset: {result.Offset}");
        }

        /// <summary>
        /// Produces message onto message queue with closed trade information
        /// </summary>
        /// <param name="trade"></param>
        /// <returns></returns>
        public async Task CreateClosedTradeMessage(TradeDataModel trade)
        {
            var message = new Message<Null, string>
            {
                Value = $"Successfully closed trade with ID: {trade.Id}"
            };

            var result = await producer.ProduceAsync("BankTradingService", message);
            Console.WriteLine($"Event sent on Partition: {result.Partition} with Offset: {result.Offset}");
        }
    }
}
