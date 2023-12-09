using BankTradingService.Data.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTradingService.Shared.Utilities.Interface
{
    public interface IUnitOfWork
    {
        void SaveChanges();

        IUserTradeRepository UserTradeRepository { get; }
    }
}
