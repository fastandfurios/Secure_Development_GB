using Debit_Cards_Project.DAL.Interfaces;
using Debit_Cards_Project.DAL.Models.DebitCard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Debit_Cards_Project.Controllers
{
    [ApiController]
    [Route("/debit_card")]
    [Authorize]
    public class DebitCardController : ControllerBase
    {
        private readonly IDebitCardRepository _debitCardRepository;
        private readonly ILogger<DebitCardController> _logger;

        public DebitCardController(IDebitCardRepository debitCardRepository, ILogger<DebitCardController> logger)
        {
            _debitCardRepository = debitCardRepository;
            _logger = logger;
        }
        
        [HttpPost("add_card")]
        public IActionResult Create([FromBody] DebitCard card)
        {
            _logger.LogInformation("Card created: {0}", card);

            _debitCardRepository.Create(card);
            return Ok();
        }

        [HttpGet("card/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            _logger.LogInformation("Id: {0}", id);

            var card = _debitCardRepository.ReadById(id);
            return Ok(card);
        }

        [HttpGet("cards")]
        public IActionResult GetAllCards()
        {
            var cards = _debitCardRepository.ReadAll();
            return Ok(cards);
        }

        [HttpPut("edit_card/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] DebitCard card) 
        {
            _debitCardRepository.Update(card, id);
            return Ok();
        }

        [HttpDelete("remove_card/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _debitCardRepository.Delete(id);
            return Ok();
        }
    }
}
