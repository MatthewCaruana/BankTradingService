using BankTradingService.Application.DTOs;
using BankTradingService.Data.Models;
using BankTradingService.Data.Repositories.Interface;
using BankTradingService.Shared.Enums;
using BankTradingService.Shared.Messaging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTradingService.Application.Commands
{
    public class GetUserTradesQueryCommand : IQuery<IEnumerable<TradeActivityDTO>>
    {
        public int UserID { get; set; }

        public class GetUserTradersQueryCommandHandler : IQueryHandler<GetUserTradesQueryCommand, IEnumerable<TradeActivityDTO>>
        {
            private ILogger<GetUserTradersQueryCommandHandler> _logger;
            private IUserTradeRepository _userTradeRepository;

            public GetUserTradersQueryCommandHandler(ILogger<GetUserTradersQueryCommandHandler> logger, IUserTradeRepository userTradeRepository)
            {
                _logger = logger;
                _userTradeRepository = userTradeRepository;
            }

            public async Task<IEnumerable<TradeActivityDTO>> Handle(GetUserTradesQueryCommand request, CancellationToken cancellationToken)
            {
                List<TradeActivityDTO> result = new List<TradeActivityDTO>();
                _logger.LogInformation($"Checking if user with UserID: {request.UserID} exists");

                bool userExists = _userTradeRepository.CheckUserExistsWithID(request.UserID);

                if (userExists)
                {
                    _logger.LogInformation("User found, retrieving trades for user");
                    var trades = _userTradeRepository.GetTradesForUser(request.UserID);

                    foreach(var trade in trades)
                    {
                        result.Add(Convert(trade));
                    }
                    _logger.LogInformation($"Successfully retrieved {result.Count} trades");
                }
                else
                {
                    _logger.LogInformation("User not found, could not retrieve trades");
                }

                return result;
            }

            private TradeActivityDTO Convert(TradeDataModel dataModel)
            {
                TradeActivityDTO result = new TradeActivityDTO()
                {
                    UserID = dataModel.UserID,
                    TransactionType = (TransactionTypes)dataModel.TransactionType,
                    Symbol = dataModel.Symbol,
                    Amount = dataModel.Amount,
                    OpenPrice = dataModel.OpenPrice,
                    OpenTimestamp = dataModel.OpenTimestamp,
                    ClosePrice = dataModel.ClosePrice,
                    CloseTimestamp = dataModel.CloseTimestamp
                };

                return result;
            }
        }
    }
}
