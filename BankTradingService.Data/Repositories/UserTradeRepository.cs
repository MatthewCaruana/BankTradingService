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

        public bool CheckUserExistsWithID(int id)
        {
            UserDataModel? user = _dbContext.User.SingleOrDefault(x => x.Id == id);

            if(user != null)
            {
                return true;
            }
            return false;
        }

        public List<TradeDataModel> GetTradesForUser(int ID)
        {
            return _dbContext.Trade.Where(x=>x.UserID == ID).ToList();
        }

        public async Task OpenTrade(TradeDataModel trade)
        {
            await _dbContext.Trade.AddAsync(trade);
        }
    }
}
