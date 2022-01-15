using Debit_Cards_Project.DAL.Context;
using Debit_Cards_Project.DAL.Interfaces;
using Debit_Cards_Project.DAL.Models;

namespace Debit_Cards_Project.DAL.Repositories
{
    public sealed class DebitCardRepository : IDebitCardRepository
    {
        private readonly DebitCardsDB _db;
        private readonly ILogger<DebitCardRepository> _logger;

            public DebitCardRepository(DebitCardsDB db, ILogger<DebitCardRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public void Create(DebitCard card)
        {
            if(card is null)
                throw new ArgumentNullException(nameof(card));

            if(ReadAll().Contains(card))
                return;

            _db.Cards.Add(card);
        }

        public IReadOnlyList<DebitCard> ReadAll()
        {
            try
            {
                return _db.Cards.ToList();
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                throw;
            }
        }

        public DebitCard ReadById(int id)
        {
            try
            {
                return ReadAll().FirstOrDefault(card => card.Id == id)!;
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                throw;
            }
        }

        public void Update(DebitCard card, int id)
        {
            if (card is null)
                throw new ArgumentNullException(nameof(card));

            if (ReadAll().Contains(card))
            {
                var db_card = ReadById(id);
                db_card.CurrencyName = card.CurrencyName;
                db_card.Holder = card.Holder;
                db_card.NumberCard = card.NumberCard;
                db_card.ValidityPeriod = card.ValidityPeriod;
            }
        }

        public void Delete(int id)
        {
            if(!ReadAll().Contains(ReadById(id))) return;

            _db.Cards.Remove(ReadById(id));
        }
    }
}
