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

        /// <summary>
        /// Checks whether a user exists for ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>boolean of whether it exists or not</returns>
        public bool CheckUserExistsWithID(int id)
        {
            UserDataModel? user = _dbContext.User.SingleOrDefault(x => x.Id == id);

            if(user != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Closes Trade with Id and sets the closing amount to the parameter value and the timestamp to DateTime.Now
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CloseAmount"></param>
        public void CloseTrade(int ID, decimal CloseAmount)
        {
            //If this area is accessed, the trade exists.. no need to check if exists already
            var trade = _dbContext.Trade.Single(x => x.Id == ID);

            trade.ClosePrice = CloseAmount;
            trade.CloseTimestamp = DateTime.Now;
        }

        /// <summary>
        /// Retrieves the trade with the provided ID if exists
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<TradeDataModel?> GetTradeByID(int ID)
        {
            return await _dbContext.Trade.SingleOrDefaultAsync(x=>x.Id == ID);
        }

        /// <summary>
        /// Retrieves all trades made by user with ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<TradeDataModel> GetTradesForUser(int ID)
        {
            return _dbContext.Trade.Where(x=>x.UserID == ID).ToList();
        }

        /// <summary>
        /// Opens a Trade with the provided details
        /// </summary>
        /// <param name="trade"></param>
        /// <returns></returns>
        public async Task OpenTrade(TradeDataModel trade)
        {
            await _dbContext.Trade.AddAsync(trade);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
