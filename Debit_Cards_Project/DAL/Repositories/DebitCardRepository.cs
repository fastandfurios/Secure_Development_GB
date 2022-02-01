using Debit_Cards_Project.DAL.Context;
using Debit_Cards_Project.DAL.Interfaces;
using Debit_Cards_Project.DAL.Models.DebitCard;

namespace Debit_Cards_Project.DAL.Repositories
{
    public sealed class DebitCardRepository : IDebitCardRepository
    {
        private readonly DebitCardsDb _db;
        private readonly ILogger<DebitCardRepository> _logger;

        public DebitCardRepository(DebitCardsDb db, ILogger<DebitCardRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public void Create(DebitCard card)
        {
            if(ReadAll().Contains(card))
                return;

            _db.Cards.Add(card);
            _db.SaveChanges();
        }

        public IReadOnlyList<DebitCard> ReadAll()
        {
            try
            {
                var cards = _db.Cards.Join(_db.Holders,
                    dc => dc.HolderId,
                    holder => holder.Id,
                    (dc, holder) => new DebitCard()
                    {
                        Id = dc.Id,
                        HolderId = holder.Id,
                        CurrencyName = dc.CurrencyName,
                        Holder = holder,
                        Month = dc.Month,
                        Year = dc.Year,
                        NumberCard = dc.NumberCard
                    })
                    .ToList();

                return cards;
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
                var debitCard = ReadAll().FirstOrDefault(card => card.Id == id)!;
                var holderCard = _db.Holders.FirstOrDefault(holder => holder.Id == debitCard.HolderId);
                debitCard.Holder = holderCard!;
                return debitCard;
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                throw;
            }
        }

        public void Update(DebitCard card, int id)
        {
            if (ReadAll().Contains(card))
                return;

            var cardDb = _db.Cards.Find(id)!;

            cardDb.CurrencyName = card.CurrencyName;
            cardDb.Holder = card.Holder;
            cardDb.NumberCard = card.NumberCard;
            cardDb.Month = card.Month;
            cardDb.Year = card.Year;

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
