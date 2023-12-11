using BankTradingService.Application.Commands;
using BankTradingService.Data.Models;
using BankTradingService.Data.Repositories.Interface;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BankTradingService.Application.Commands.GetUserTradesQueryCommand;

namespace BankTradingService.Test.Tests.Commands
{
    [TestFixture]
    public class GetUserTradesQueryCommandTest
    {
        [Test]
        public async Task When_Getting_User_Trades_Of_User_That_Does_Not_Exist_Return_Nothing()
        {
            //arrange
            var getUserTradeCommand = new GetUserTradesQueryCommand()
            {
                UserID = 1,
            };

            var mockLogger = new Mock<ILogger<GetUserTradersQueryCommandHandler>>();
            var mockUserTradeRepository = new Mock<IUserTradeRepository>();
            var cancellationToken = new CancellationToken();

            mockUserTradeRepository.Setup(x => x.CheckUserExistsWithID(It.IsAny<int>())).Returns(false);

            GetUserTradersQueryCommandHandler queryCommandHandler = new GetUserTradersQueryCommandHandler(mockLogger.Object, mockUserTradeRepository.Object);

            //act
            var actualResult = await queryCommandHandler.Handle(getUserTradeCommand, cancellationToken);

            //assert
            Assert.That(actualResult.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task When_Getting_User_Trades_Of_User_That_Exists_Return_List_Of_Trades()
        {
            //arrange
            var getUserTradeCommand = new GetUserTradesQueryCommand()
            {
                UserID = 1,
            };

            List<TradeDataModel> tradesByUser = new List<TradeDataModel>()
            {
                new TradeDataModel()
                {
                    Id =1,
                    UserID = 1,
                    Amount = 100,
                    Symbol = "A",
                    TransactionType = 0,
                    OpenPrice = 15,
                    OpenTimestamp = new DateTime(2020,1,1)
                }
            };

            var mockLogger = new Mock<ILogger<GetUserTradersQueryCommandHandler>>();
            var mockUserTradeRepository = new Mock<IUserTradeRepository>();
            var cancellationToken = new CancellationToken();

            mockUserTradeRepository.Setup(x => x.CheckUserExistsWithID(It.IsAny<int>())).Returns(true);
            mockUserTradeRepository.Setup(x => x.GetTradesForUser(It.IsAny<int>())).Returns(tradesByUser);

            GetUserTradersQueryCommandHandler queryCommandHandler = new GetUserTradersQueryCommandHandler(mockLogger.Object, mockUserTradeRepository.Object);

            //act
            var actualResult = await queryCommandHandler.Handle(getUserTradeCommand, cancellationToken);

            //assert
            Assert.That(actualResult.Count, Is.EqualTo(1));
        }
    }
}
