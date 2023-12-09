using BankTradingService.Application.DTOs;
using BankTradingService.Data.Models;
using BankTradingService.Shared.Messaging;
using BankTradingService.Shared.Utilities.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTradingService.Application.Commands
{
    public class OpenTradeCommand : NewTradeActivityDTO, ICommand<NewTradeActivityResponseDTO>
    {

        public class OpenTradeCommandHandler : ICommandHandler<OpenTradeCommand, NewTradeActivityResponseDTO>
        {
            private readonly IUnitOfWork _unitOfWork;
            private ILogger<OpenTradeCommandHandler> _logger;

            public OpenTradeCommandHandler(IUnitOfWork unitOfWork, ILogger<OpenTradeCommandHandler> logger)
            {
                _unitOfWork = unitOfWork;
                _logger = logger;
            }

            public async Task<NewTradeActivityResponseDTO> Handle(OpenTradeCommand request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("Adapting DTO into data model");
                TradeDataModel dataModel = Adapt(request);

                _logger.LogInformation($"Checking if user exists with id {request.UserID}");
                bool userExists = _unitOfWork.UserTradeRepository.CheckUserExistsWithID(request.UserID);

                NewTradeActivityResponseDTO result = new NewTradeActivityResponseDTO();

                if (userExists)
                {
                    _logger.LogInformation("User Found, Opening Trade");
                    await _unitOfWork.UserTradeRepository.OpenTrade(dataModel);

                    _unitOfWork.SaveChanges();
                    _logger.LogInformation($"Trade successfully opened, id: {dataModel.Id}");
                    result.Id = dataModel.Id;
                }

                return result;
            }

            private TradeDataModel Adapt(OpenTradeCommand request)
            {
                TradeDataModel result = new TradeDataModel()
                {
                    UserID = request.UserID,
                    TransactionType = (short)request.TransactionType,
                    Symbol = request.Symbol,
                    Amount = request.Amount,
                    OpenPrice = request.OpenPrice,
                    OpenTimestamp = request.OpenTimestamp,
                    ClosePrice = null,
                    CloseTimestamp = null
                };

                return result;
            }
        }
    }
}
