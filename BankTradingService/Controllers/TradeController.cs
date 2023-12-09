using Microsoft.AspNetCore.Mvc;

namespace BankTradingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TradeController : ControllerBase
    {
        private readonly ILogger<TradeController> _logger;

        public TradeController(ILogger<TradeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("GetUserTrades")]
        public void GetUserTrades(int userID)
        {
            
        }

        [HttpPost]
        [Route("ExecuteTrade")]
        public void ExecuteTrade()
        {

        }
    }
}
