using BankTradingService.Data.Context.Interface;
using BankTradingService.Data.Models;
using BankTradingService.Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
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

        public void CloseTrade(int ID, decimal CloseAmount)
        {
            //If this area is accessed, the trade exists.. no need to check if exists already
            var trade = _dbContext.Trade.Single(x => x.Id == ID);

            trade.ClosePrice = CloseAmount;
            trade.CloseTimestamp = DateTime.Now;
        }

        public async Task<TradeDataModel?> GetTradeByID(int ID)
        {
            return await _dbContext.Trade.SingleOrDefaultAsync(x=>x.Id == ID);
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
