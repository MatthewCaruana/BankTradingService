using BankTradingService.Data.Context.Interface;
using BankTradingService.Data.Repositories;
using BankTradingService.Data.Repositories.Interface;
using BankTradingService.Shared.Utilities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTradingService.Shared.Utilities
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ITradeDbContext _dbContext;

        private IUserTradeRepository _userTradeRepository;

        public UnitOfWork(ITradeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUserTradeRepository UserTradeRepository => _userTradeRepository ?? (_userTradeRepository = new UserTradeRepository(_dbContext));

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
