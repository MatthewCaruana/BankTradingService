using BankTradingService.Application.DTOs;
using BankTradingService.Data.Models;
using BankTradingService.Shared.Messaging;
using BankTradingService.Shared.Utilities.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTradingService.Application.Commands
{
    public class CloseTradeCommand : CloseTradeActivityDTO, ICommand<CloseTradeResponseDTO>
    {
        public class CloseTradeCommandHandler : ICommandHandler<CloseTradeCommand, CloseTradeResponseDTO>
        {
            private readonly IUnitOfWork _unitOfWork;
            private ILogger<CloseTradeCommandHandler> _logger;

            public CloseTradeCommandHandler(IUnitOfWork unitOfWork, ILogger<CloseTradeCommandHandler> logger)
            {
                _logger = logger;
                _unitOfWork = unitOfWork;
            }

            public async Task<CloseTradeResponseDTO> Handle(CloseTradeCommand request, CancellationToken cancellationToken)
            {
                _logger.LogInformation($"Checking if Trade ID:{request.TradeID} exists");

                TradeDataModel? trade = await _unitOfWork.UserTradeRepository.GetTradeByID(request.TradeID);
                CloseTradeResponseDTO result = new CloseTradeResponseDTO();

                if(trade != null)
                {
                    _logger.LogInformation($"Trade Found, closing trade");
                    _unitOfWork.UserTradeRepository.CloseTrade(request.TradeID, request.CloseAmount);

                    _unitOfWork.SaveChanges();
                    _logger.LogInformation("Trade successfully closed");

                    result.TradeID = request.TradeID;
                }
                else
                {
                    _logger.LogInformation("Trade was not found, no trades were closed");
                }

                return result;
            }
        }
    }
}
