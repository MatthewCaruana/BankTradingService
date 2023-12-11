using BankTradingService.Data.Context.Interface;
using BankTradingService.Data.Models;
using BankTradingService.Data.Repositories;
using BankTradingService.Data.Repositories.Interface;
using BankTradingService.Test.Helpers;
using BankTradingService.Test.Mocks;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankTradingService.Test.Tests.Repositories
{
    [TestFixture]
    public class UserTradeRepositoryTest
    {
        private IUserTradeRepository _userTradeRepository;
        private List<UserDataModel> _mockUserData;
        private List<TradeDataModel> _mockTradeData;

        [SetUp]
        public void SetUp()
        {
            _mockUserData = MockDataInitialiser.GetMockUsers();
            _mockTradeData = MockDataInitialiser.GetMockTrades();

            var mockUserDbSet = new Mock<DbSet<UserDataModel>>();

            mockUserDbSet.As<IQueryable<UserDataModel>>().Setup(x => x.Provider).Returns(_mockUserData.AsQueryable().Provider);
            mockUserDbSet.As<IQueryable<UserDataModel>>().Setup(x => x.Expression).Returns(_mockUserData.AsQueryable().Expression);
            mockUserDbSet.As<IQueryable<UserDataModel>>().Setup(x => x.ElementType).Returns(_mockUserData.AsQueryable().ElementType);
            mockUserDbSet.As<IQueryable<UserDataModel>>().Setup(x => x.GetEnumerator()).Returns(_mockUserData.AsQueryable().GetEnumerator());
            mockUserDbSet.Setup(x => x.Add(It.IsAny<UserDataModel>())).Callback<UserDataModel>(_mockUserData.Add);

            var mockTradeDbSet = new Mock<DbSet<TradeDataModel>>();
            mockTradeDbSet.As<IAsyncEnumerable<TradeDataModel>>().Setup(x => x.GetAsyncEnumerator(default)).Returns(new AsyncHelper.TestAsyncEnumerator<TradeDataModel>(_mockTradeData.GetEnumerator()));

            mockTradeDbSet.As<IQueryable<TradeDataModel>>().Setup(x => x.Provider).Returns(new AsyncHelper.TestAsyncQueryProvider<TradeDataModel>(_mockTradeData.AsQueryable().Provider));
            mockTradeDbSet.As<IQueryable<TradeDataModel>>().Setup(x => x.Expression).Returns(_mockTradeData.AsQueryable().Expression);
            mockTradeDbSet.As<IQueryable<TradeDataModel>>().Setup(x => x.ElementType).Returns(_mockTradeData.AsQueryable().ElementType);
            mockTradeDbSet.As<IQueryable<TradeDataModel>>().Setup(x => x.GetEnumerator()).Returns(_mockTradeData.AsQueryable().GetEnumerator());
            mockTradeDbSet.Setup(x => x.Add(It.IsAny<TradeDataModel>())).Callback<TradeDataModel>(_mockTradeData.Add);
            mockTradeDbSet.Setup(x => x.AddAsync(It.IsAny<TradeDataModel>(), It.IsAny<CancellationToken>())).Callback<TradeDataModel, CancellationToken>((model, token) => _mockTradeData.Add(model));

            var mockDbContext = new Mock<ITradeDbContext>()
            {
                CallBase = true
            };

            mockDbContext.Setup(x => x.Trade).Returns(mockTradeDbSet.Object);
            mockDbContext.Setup(x => x.User).Returns(mockUserDbSet.Object);

            mockDbContext.Setup(x => x.SaveChanges()).Verifiable();

            _userTradeRepository = new UserTradeRepository(mockDbContext.Object);
        }

        [Test]
        public void When_Check_User_Does_Not_Exist_Check_User_Returns_False()
        {
            //arrange
            int Id = 10;

            //act
            bool ActualExists = _userTradeRepository.CheckUserExistsWithID(Id);

            //assert
            Assert.That(ActualExists, Is.EqualTo(false));
        }

        [Test]
        public void When_Check_User_Exists_Check_User_Returns_True()
        {
            //arrange
            int Id = 1;

            //act
            bool ActualExists = _userTradeRepository.CheckUserExistsWithID(Id);

            //assert
            Assert.That(ActualExists, Is.EqualTo(true));
        }

        [Test]
        public async Task When_Getting_Trade_With_ID_That_Does_Not_Exist_Returns_Null()
        {
            //arrange
            int tradeId = 10;

            //act
            var ActualTrade = await _userTradeRepository.GetTradeByID(tradeId);

            //assert
            Assert.IsNull(ActualTrade);
        }

        [Test]
        public void When_Getting_Trade_With_ID_That_Exists_Returns_Value()
        {
            //arrange
            int tradeId = 2;

            //act
            var ActualTrade = _userTradeRepository.GetTradeByID(tradeId);

            //assert
            Assert.IsNotNull(ActualTrade.Result);
            Assert.That(ActualTrade.Result, Is.EqualTo(_mockTradeData.Single(x => x.Id == tradeId)));
        }

        [Test]
        public void When_Getting_Trades_For_User_That_Has_Not_Made_Trades_Return_Empty_List()
        {
            //arrange
            int UserId = 3;

            //act
            var ActualTrades = _userTradeRepository.GetTradesForUser(UserId);

            //assert
            Assert.IsNotNull(ActualTrades);
            Assert.AreEqual(0, ActualTrades.Count);
        }

        [Test]
        public void When_Getting_Trades_For_User_That_Has_Made_Trades_Return_List_With_Trades()
        {
            //arrange
            int UserId = 1;

            //act
            var ActualTrades = _userTradeRepository.GetTradesForUser(UserId);

            //assert
            Assert.IsNotNull(ActualTrades);
            Assert.That(ActualTrades, Is.EqualTo(_mockTradeData.Where(x => x.UserID == UserId).ToList()));
        }

        [Test]
        public void When_Opening_A_Trade_A_New_Entry_Is_Created()
        {
            //arrange
            TradeDataModel NewTrade = new TradeDataModel()
            {
                UserID = 3,
                Amount = 1,
                Symbol = "EURUSD",
                TransactionType = 1,
                OpenPrice = 1,
                OpenTimestamp = DateTime.Now,
            };

            int countBefore = _mockTradeData.Count;

            //act
            _userTradeRepository.OpenTrade(NewTrade);
            _userTradeRepository.SaveChanges();

            //assert
            Assert.That(_mockTradeData.Count, Is.EqualTo(countBefore + 1));
            Assert.IsTrue(_mockTradeData.Any(x => x.UserID == NewTrade.UserID && x.Symbol == NewTrade.Symbol));
        }

        [Test]
        public void When_Closing_A_Trade_The_Values_Are_Updated()
        {
            //arrange
            int TradeId = 3;
            decimal ClosePrice = 50;

            int countBefore = _mockTradeData.Count;

            //act
            _userTradeRepository.CloseTrade(TradeId, ClosePrice);
            _userTradeRepository.SaveChanges();

            //assert
            Assert.That(_mockTradeData.Count, Is.EqualTo(countBefore));
        }
    }
}
