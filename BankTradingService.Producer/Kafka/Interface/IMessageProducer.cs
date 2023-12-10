using BankTradingService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTradingService.Producer.Kafka.Interface
{
    public interface IMessageProducer
    {
        public Task CreateOpenTradeMessage(TradeDataModel trade);
        public Task CreateClosedTradeMessage(TradeDataModel trade);
    }
}
