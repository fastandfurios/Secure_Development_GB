using Debit_Cards_Project.DAL.Context;
using Debit_Cards_Project.DAL.Interfaces;
using Debit_Cards_Project.DAL.Models;

namespace Debit_Cards_Project.DAL.Repositories
{
    public sealed class DebitCardRepository : IDebitCardRepository
    {
        private readonly DebitCardsDb _db;
        private int _length;
        private readonly ILogger<DebitCardRepository> _logger;

            public DebitCardRepository(DebitCardsDb db, ILogger<DebitCardRepository> logger)
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

            if (card.Month is < 0 or > 12 ||
                card.NumberCard.ToString().Length is < 0 or > 16 ||
                card.Year < 0 ||
                card.Year.ToString().Length > 2)
                throw new ArgumentOutOfRangeException();

            _db.Cards.Add(card);
            _db.SaveChanges();
        }

        public IReadOnlyList<DebitCard> ReadAll()
        {
            try
            {
                _length = _db.Cards.ToList().Count;
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
                return;

            if (card.Month is < 0 or > 12 ||
                card.NumberCard.ToString().Length is < 0 or > 16 ||
                card.Year < 0 ||
                card.Year.ToString().Length > 2)
                throw new ArgumentOutOfRangeException();

            var card_db = _db.Cards.Find(id)!;

            card_db.CurrencyName = card.CurrencyName;
            card_db.Holder = card.Holder;
            card_db.NumberCard = card.NumberCard;
            card_db.Month = card.Month;
            card_db.Year = card.Year;

            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            if (ReadAll().Contains(ReadById(id)))
            {
                _db.Cards.Remove(ReadById(id));
                _db.SaveChanges();
            }
        }
    }
}
