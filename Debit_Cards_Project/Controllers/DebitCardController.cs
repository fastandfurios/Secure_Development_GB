using AutoMapper;
using Debit_Cards_Project.DAL.Interfaces;
using Debit_Cards_Project.DAL.Models.DebitCard;
using Debit_Cards_Project.DTO;
using FluentValidation;
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
        private readonly IValidator<DebitCard> _validator;
        private readonly IMapper _mapper;

        public DebitCardController(IDebitCardRepository debitCardRepository,
            ILogger<DebitCardController> logger,
            IMapper mapper,
            IValidator<DebitCard> validator)
        {
            _debitCardRepository = debitCardRepository;
            _logger = logger;
            _mapper = mapper;
            _validator = validator;
        }
        
        [HttpPost("add_card")]
        public IActionResult Create([FromBody] DebitCard card)
        {
            _logger.LogInformation("Card created: {0}", card);

            var result = _validator.Validate(card);

            if (result.IsValid)
            {
                _debitCardRepository.Create(card);
                return Ok();
            }

            var errorsMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            return BadRequest(errorsMessages);
        }

        [HttpGet("card/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            _logger.LogInformation("Id: {0}", id);

            var card = _debitCardRepository.ReadById(id);

            var holderDto = _mapper.Map<HolderDto>(card.Holder);

            var cardDto = _mapper.Map<DebitCardDto>(card);

            cardDto.HolderDto = holderDto;

            return Ok(cardDto);
        }

        [HttpGet("cards")]
        public IActionResult GetAllCards()
        {
            var cardsDto = _debitCardRepository.ReadAll().Select(card =>
            {
                var holderDto = _mapper.Map<HolderDto>(card.Holder);
                var cardDto = _mapper.Map<DebitCardDto>(card);
                cardDto.HolderDto = holderDto;
                return cardDto;
            }).ToList();
            return Ok(cardsDto);
        }

        [HttpPut("edit_card/{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] DebitCard card)
        {
            var result = _validator.Validate(card);

            if (result.IsValid)
            {
                _debitCardRepository.Update(card, id);
                return Ok();
            }

            var errorsMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            return BadRequest(errorsMessages);
        }

        [HttpDelete("remove_card/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _debitCardRepository.Delete(id);
            return Ok();
        }
    }
}
