using AutoMapper;
using Debit_Cards_Project.DAL.Interfaces;
using Debit_Cards_Project.DAL.Models.CashBack;
using Debit_Cards_Project.DTO;
using Debit_Cards_Project.Infrastructure.ChainOfResponsibility.Interfaces;
using FluentValidation;
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
        private readonly ICashBackRepository _repository;
        private readonly ILogger<CashBackController> _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<Category> _validatorCategory;
        private readonly IValidator<CashBack> _validatorCashBack;

        public CashBackController(IChain chain, 
            ILogger<CashBackController> logger,
            IMapper mapper,
            IValidator<Category> validatorCategory, 
            ICashBackRepository repository, IValidator<CashBack> validatorCashBack)
        {
            _chain = chain;
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _validatorCategory = validatorCategory;
            _validatorCashBack = validatorCashBack;
        }

        [HttpPost("add_cash_back")]
        public IActionResult Create([FromBody] Category category)
        {
            var result = _validatorCategory.Validate(category);

            if (result.IsValid)
            {
                var cashBack = _chain.GetCashBack(category.CategoryName);

                result = _validatorCashBack.Validate(cashBack);

                if (result.IsValid)
                {
                    _repository.Create(cashBack);
                    return Ok();
                }
            }

            var errorsMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            return BadRequest(errorsMessages);
        }

        [HttpGet("cash_back/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var cashBack = _repository.ReadById(id);

            var cashBackDto = _mapper.Map<CashBackDto>(cashBack);

            return Ok(cashBackDto);
        }

        [HttpGet("all_cash_back")]
        public IActionResult GetAllCashBackOptions()
        {
            var collection = _repository.ReadAll()
                .Select(cashBack => _mapper.Map<CashBackDto>(cashBack))
                .ToList();

            return Ok(collection);
        }

        [HttpDelete("remove_cash_back/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _repository.Delete(id);
            return Ok();
        }
    }
}
