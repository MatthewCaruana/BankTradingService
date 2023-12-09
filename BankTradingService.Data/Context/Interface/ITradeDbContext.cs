using BankTradingService.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTradingService.Data.Context.Interface
{
    public interface ITradeDbContext
    {
        DbSet<UserDataModel> User { get; set; }
        DbSet<TradeDataModel> Trade { get; set; }

        void SaveChanges();
    }
}
