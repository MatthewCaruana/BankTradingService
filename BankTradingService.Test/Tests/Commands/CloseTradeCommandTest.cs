using BankTradingService.Application.Commands;
using BankTradingService.Application.DTOs;
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

namespace BankTradingService.Test.Tests.Commands
{
    [TestFixture]
    public class CloseTradeCommandTest
    {
        [Test]
        public async Task When_Closing_Trade_That_Does_Not_Exist_Return_0()
        {
            //arrange
            var closeTradeCommand = new CloseTradeCommand()
            {
                TradeID = 1,
                CloseAmount = 10
            };

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockLogger = new Mock<ILogger<CloseTradeCommandHandler>>();
            var mockMessageProducer = new Mock<IMessageProducer>();
            var cancellationToken = new CancellationToken();

            TradeDataModel nullTradeModel = null;

            mockUnitOfWork.Setup(x => x.UserTradeRepository.GetTradeByID(It.IsAny<int>())).ReturnsAsync(nullTradeModel);

            CloseTradeCommandHandler commandHandler = new CloseTradeCommandHandler(mockUnitOfWork.Object, mockLogger.Object, mockMessageProducer.Object);

            //act
            var actualResult = await commandHandler.Handle(closeTradeCommand, cancellationToken);

            //assert
            Assert.That(actualResult.TradeID, Is.EqualTo(0));
        }

        [Test]
        public async Task When_Closing_Trade_That_Exists_Return_Trade_ID()
        {
            //arrange
            var closeTradeCommand = new CloseTradeCommand()
            {
                TradeID = 1,
                CloseAmount = 10
            };

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockLogger = new Mock<ILogger<CloseTradeCommandHandler>>();
            var mockMessageProducer = new Mock<IMessageProducer>();
            var cancellationToken = new CancellationToken();

            TradeDataModel filledTradeModel = new TradeDataModel();

            mockUnitOfWork.Setup(x => x.UserTradeRepository.GetTradeByID(It.IsAny<int>())).ReturnsAsync(filledTradeModel);
            mockUnitOfWork.Setup(x => x.UserTradeRepository.CloseTrade(It.IsAny<int>(), It.IsAny<decimal>())).Verifiable();
            mockUnitOfWork.Setup(x => x.SaveChanges()).Verifiable();

            mockMessageProducer.Setup(x => x.CreateClosedTradeMessage(It.IsAny<TradeDataModel>())).Verifiable();

            CloseTradeCommandHandler commandHandler = new CloseTradeCommandHandler(mockUnitOfWork.Object, mockLogger.Object, mockMessageProducer.Object);

            //act
            var actualResult = await commandHandler.Handle(closeTradeCommand, cancellationToken);

            //assert
            Assert.That(actualResult.TradeID, Is.EqualTo(1));
        }
    }
}
