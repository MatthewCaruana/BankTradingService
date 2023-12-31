﻿using BankTradingService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTradingService.Data.Repositories.Interface
{
    public interface IUserTradeRepository
    {
        bool CheckUserExistsWithID(int ID);
        List<TradeDataModel> GetTradesForUser(int ID);
        Task OpenTrade(TradeDataModel trade);
        Task<TradeDataModel?> GetTradeByID(int ID);
        void CloseTrade(int ID, decimal CloseAmount);
        void SaveChanges();
    }
}
