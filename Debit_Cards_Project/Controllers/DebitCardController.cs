using Microsoft.AspNetCore.Mvc;

namespace Debit_Cards_Project.Controllers
{
    [ApiController]
    [Route("/debit_card")]
    public class DebitCardController : ControllerBase
    {
        private readonly ILogger<DebitCardController> _logger;

        public DebitCardController(ILogger<DebitCardController> logger)
        {
            _logger = logger;
        }
    }
}
