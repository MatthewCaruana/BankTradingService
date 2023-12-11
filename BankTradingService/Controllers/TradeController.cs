using BankTradingService.Application.Commands;
using BankTradingService.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankTradingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TradeController : ControllerBase
    {
        private readonly ILogger<TradeController> _logger;
        private readonly IMediator _mediator;

        public TradeController(ILogger<TradeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves all trades that are related to a particular user with the specific UserID
        /// </summary>
        /// <param name="request">The request only contains the User ID</param>
        /// <returns>List of trades</returns>
        [HttpGet]
        [Route("GetUserTrades")]
        public async Task<IActionResult> GetUserTrades([FromQuery]GetUserTradesQueryCommand request)
        {
            return Ok(await _mediator.Send(request));
        }


        /// <summary>
        /// Opens a trade with the provided information for a single user
        /// </summary>
        /// <param name="request">Refer to class: NewTradeActivityDTO</param>
        /// <returns>serial of opened trade</returns>
        [HttpPost]
        [Route("OpenTrade")]
        public async Task<IActionResult> OpenTrade([FromBody]OpenTradeCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        /// <summary>
        /// Closes a trade with TradeID
        /// </summary>
        /// <param name="request">The request only contains the Trade ID and the Closing Price</param>
        /// <returns>Id of closed trade</returns>
        [HttpPost]
        [Route("CloseTrade")]
        public async Task<IActionResult> CloseTrade([FromBody]CloseTradeCommand request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
