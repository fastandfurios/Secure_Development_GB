using Debit_Cards_Project.Infrastructure.ChainOfResponsibility.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Debit_Cards_Project.Controllers
{
    [ApiController]
    [Route("/cash_back")]
    [Authorize]
    public class CashBackController : ControllerBase
    {
        private readonly IChain _chain;
        private readonly ILogger<CashBackController> _logger;

        public CashBackController(IChain chain, ILogger<CashBackController> logger)
        {
            _chain = chain;
            _logger = logger;
        }

        [HttpPost("add_cash_back/{category}")]
        public IActionResult Create([FromRoute] string category)
        {

            var cashback = _chain.GetCashBack(category);

            return Ok();
        }

        [HttpGet("cash_back/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            return Ok();
        }

        [HttpGet("all_cash_back")]
        public IActionResult GetAllCashBackOptions()
        {
            return Ok();
        }

        [HttpDelete("remove_cash_back/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            return Ok();
        }
    }
}
