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

        [HttpGet]
        [Route("GetUserTrades")]
        public async Task<IActionResult> GetUserTrades([FromQuery]GetUserTradesQueryCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        [Route("OpenTrade")]
        public async Task<IActionResult> ExecuteTrade([FromBody]OpenTradeCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        [Route("CloseTrade")]
        public async Task<IActionResult> CloseTrade([FromBody]CloseTradeCommand request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
