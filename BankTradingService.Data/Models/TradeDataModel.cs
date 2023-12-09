using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTradingService.Data.Models
{
    public class TradeDataModel
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public short TransactionType { get; set; }
        public string Symbol { get; set; }
        public decimal Amount { get; set; }
        public decimal OpenPrice { get; set; }
        public DateTime OpenTimestamp { get; set; }
        public decimal ClosePrice { get; set; }
        public DateTime CloseTimestamp { get; set; }
        public virtual UserDataModel User { get; set; }
    }
}
