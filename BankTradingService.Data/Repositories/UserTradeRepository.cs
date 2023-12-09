using BankTradingService.Data.Context.Interface;
using BankTradingService.Data.Models;
using BankTradingService.Data.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTradingService.Data.Repositories
{
    public class UserTradeRepository : IUserTradeRepository
    {
        private ITradeDbContext _dbContext;

        public UserTradeRepository(ITradeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UserDataModel? GetUserByID(int id)
        {
            return _dbContext.User.SingleOrDefault(x => x.Id == id);
        }
    }
}
