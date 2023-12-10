using BankTradingService.Data.Context.Interface;
using BankTradingService.Data.Models;
using BankTradingService.Data.Repositories;
using BankTradingService.Data.Repositories.Interface;
using BankTradingService.Test.Mocks;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTradingService.Test.Tests
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
            mockTradeDbSet.As<IQueryable<TradeDataModel>>().Setup(x => x.Provider).Returns(_mockTradeData.AsQueryable().Provider);
            mockTradeDbSet.As<IQueryable<TradeDataModel>>().Setup(x => x.Expression).Returns(_mockTradeData.AsQueryable().Expression);
            mockTradeDbSet.As<IQueryable<TradeDataModel>>().Setup(x => x.ElementType).Returns(_mockTradeData.AsQueryable().ElementType);
            mockTradeDbSet.As<IQueryable<TradeDataModel>>().Setup(x => x.GetEnumerator()).Returns(_mockTradeData.AsQueryable().GetEnumerator());
            mockTradeDbSet.Setup(x => x.Add(It.IsAny<TradeDataModel>())).Callback<TradeDataModel>(_mockTradeData.Add);

            var mockDbContext = new Mock<ITradeDbContext>()
            {
                CallBase = true
            };

            mockDbContext.Setup(x=>x.Trade).Returns(mockTradeDbSet.Object);
            mockDbContext.Setup(x=>x.User).Returns(mockUserDbSet.Object);

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
            Assert.AreEqual(false, ActualExists);
        }

        [Test]
        public void When_Check_User_Exists_Check_User_Returns_True()
        {
            //arrange
            int Id = 1;

            //act
            bool ActualExists = _userTradeRepository.CheckUserExistsWithID(Id);

            //assert
            Assert.AreEqual(true, ActualExists);
        }

        [Test]
        public void When_Getting_Trade_With_ID_That_Does_Not_Exist_Returns_Null()
        {
            //arrange
            int tradeId = 10;

            //act
            var ActualTrade = _userTradeRepository.GetTradeByID(tradeId);

            //assert
            Assert.IsNull(ActualTrade.Result);
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
            Assert.That(_mockTradeData.Single(x=>x.Id == tradeId), Is.EqualTo(ActualTrade.Result));
        }

        [Test]
        public void When_Getting_Trades_For_User_That_Has_Not_Made_Trades_Return_Empty_List()
        {
            //arrange

            //act

            //assert
        }

        [Test]
        public void When_Getting_Trades_For_User_That_Has_Made_Trades_Return_List_With_Trades()
        {
            //arrange

            //act

            //assert
        }
    }
}
