using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTradingService.Data.Models
{
    public class UserDataModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string FullAddress { get; set; }
        public string Email { get; set; }
        public virtual List<TradeDataModel> Trades { get; set; }
    }
}
