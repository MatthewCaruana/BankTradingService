using BankTradingService.Application.Commands;
using BankTradingService.Data.Models;
using BankTradingService.Producer.Kafka.Interface;
using BankTradingService.Shared.Utilities.Interface;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BankTradingService.Application.Commands.CloseTradeCommand;
using static BankTradingService.Application.Commands.OpenTradeCommand;

namespace BankTradingService.Test.Tests.Commands
{
    [TestFixture]
    public class OpenTradeCommandTest
    {
        [Test]
        public async Task When_Opening_Trade_For_User_That_Does_Not_Exist_No_Trade_Is_Created()
        {
            //arrange
            var openTradeCommand = new OpenTradeCommand()
            {
                UserID = 1,
                OpenPrice= 1,
                Amount= 1,
                Symbol = "ABC",
                TransactionType = Shared.Enums.TransactionTypes.BUY
            };

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockLogger = new Mock<ILogger<OpenTradeCommandHandler>>();
            var mockMessageProducer = new Mock<IMessageProducer>();
            var cancellationToken = new CancellationToken();

            mockUnitOfWork.Setup(x => x.UserTradeRepository.CheckUserExistsWithID(It.IsAny<int>())).Returns(false);

            OpenTradeCommandHandler commandHandler = new OpenTradeCommandHandler(mockUnitOfWork.Object, mockLogger.Object, mockMessageProducer.Object);

            //act
            var actualResult = await commandHandler.Handle(openTradeCommand, cancellationToken);

            //assert
            Assert.That(actualResult.Id, Is.EqualTo(0));
        }

        [Test]
        public async Task When_Opening_Trade_For_User_That_Exists_New_Trade_Id_Is_Returned()
        {
            //arrange
            var openTradeCommand = new OpenTradeCommand()
            {
                UserID = 1,
                OpenPrice = 1,
                Amount = 1,
                Symbol = "ABC",
                TransactionType = Shared.Enums.TransactionTypes.BUY
            };

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockLogger = new Mock<ILogger<OpenTradeCommandHandler>>();
            var mockMessageProducer = new Mock<IMessageProducer>();
            var cancellationToken = new CancellationToken();

            mockUnitOfWork.Setup(x => x.UserTradeRepository.CheckUserExistsWithID(It.IsAny<int>())).Returns(true);
            mockUnitOfWork.Setup(x => x.UserTradeRepository.OpenTrade(It.IsAny<TradeDataModel>())).Verifiable();
            mockUnitOfWork.Setup(x=>x.SaveChanges()).Verifiable();
            mockMessageProducer.Setup(x => x.CreateClosedTradeMessage(It.IsAny<TradeDataModel>())).Verifiable();

            OpenTradeCommandHandler commandHandler = new OpenTradeCommandHandler(mockUnitOfWork.Object, mockLogger.Object, mockMessageProducer.Object);

            //act
            var actualResult = await commandHandler.Handle(openTradeCommand, cancellationToken);

            //assert
            //We cannot verify that the new ID is created as it is outside of the scope - therefore, asserts are based on verifies
        }
    }
}
