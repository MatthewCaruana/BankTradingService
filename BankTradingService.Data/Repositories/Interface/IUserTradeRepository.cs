using BankTradingService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTradingService.Data.Repositories.Interface
{
    public interface IUserTradeRepository
    {
        UserDataModel? GetUserByID(int id);
    }
}
