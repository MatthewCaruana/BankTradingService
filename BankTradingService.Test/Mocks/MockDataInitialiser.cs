using BankTradingService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTradingService.Test.Mocks
{
    public static class MockDataInitialiser
    {
        public static List<UserDataModel> GetMockUsers()
        {
            return new List<UserDataModel>()
            {
                new UserDataModel()
                {
                    Id = 1,
                    FullName = "Test 1",
                    FullAddress = "Location 1",
                    Email = "Email 1"
                },
                new UserDataModel()
                {
                    Id = 2,
                    FullName = "Test 2",
                    FullAddress = "Location 2",
                    Email = "Email 2"
                },
                new UserDataModel()
                {
                    Id = 3,
                    FullName = "Test 3",
                    FullAddress = "Location 3",
                    Email = "Email 3"
                }
            };
        }

        public static List<TradeDataModel> GetMockTrades()
        {
            return new List<TradeDataModel>()
            {
                new TradeDataModel()
                {
                    Id = 1,
                    Symbol = "EURGBP",
                    Amount = 100,
                    TransactionType = 0,
                    UserID = 1,
                    OpenPrice = 50,
                    OpenTimestamp = new DateTime(2023,11,1),
                    ClosePrice = 52,
                    CloseTimestamp = new DateTime(2023,11,5)
                },
                new TradeDataModel()
                {
                    Id = 2,
                    Symbol = "GBPUSD",
                    Amount = 2,
                    TransactionType = 1,
                    UserID = 1,
                    OpenPrice = 40,
                    OpenTimestamp = new DateTime(2023,12,1),
                    ClosePrice = 52,
                    CloseTimestamp = new DateTime(2023,12,5)
                },
                new TradeDataModel()
                {
                    Id = 3,
                    Symbol = "GBPJPY",
                    Amount = 20,
                    TransactionType = 0,
                    UserID = 2,
                    OpenPrice = 60,
                    OpenTimestamp = new DateTime(2023,11,1)
                },
            };
        }
    }
}
