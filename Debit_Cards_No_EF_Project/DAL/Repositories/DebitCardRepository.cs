using Dapper;
using Debit_Cards_No_EF_Project.DAL.Interfaces;
using Debit_Cards_No_EF_Project.DAL.Models;

namespace Debit_Cards_No_EF_Project.DAL.Repositories
{
    public sealed class DebitCardRepository : IDebitCardRepository
    {
        private readonly IConnection _connection;
        private int _length;
        private readonly ILogger<DebitCardRepository> _logger;

        public DebitCardRepository(IConnection connection, ILogger<DebitCardRepository> logger)
        {
            _connection = connection;
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

            using var connection = _connection.GetOpenedConnection();

            connection.Execute("INSERT INTO Cards(Id, NumberCard, CurrencyName, Holder, Month, Year) VALUES(@Id, @NumberCard, @CurrencyName, @Holder, @Month, @Year)",
                new
                {
                    Id = card.Id,
                    Month = card.Month,
                    NumberCard = card.NumberCard,
                    Year = card.Year,
                    Holder = card.Holder,
                    CurrencyName = card.CurrencyName
                });
        }

        public IReadOnlyList<DebitCard> ReadAll()
        {
            try
            {
                using var connection = _connection.GetOpenedConnection();
                return connection.Query<DebitCard>("SELECT * FROM Cards").ToList();
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

            using var connection = _connection.GetOpenedConnection();
            connection.Execute("UPDATE Cards SET CurrencyName=@CurrencyName, Holder=@Holder, NumberCard=@NumberCard, Month=@Month, Year=@Year WHERE Id=@id",
                new
                {
                    CurrencyName = card.CurrencyName,
                    Holder = card.Holder,
                    NumberCard = card.NumberCard,
                    Month = card.Month,
                    Year = card.Year,
                });
        }

        public void Delete(int id)
        {
            if (id < 0 || id > _length)
                throw new ArgumentOutOfRangeException();

            using var connection = _connection.GetOpenedConnection();

            if (ReadAll().Contains(ReadById(id)))
                connection.Execute("DELETE FROM Cards WHERE Id=@id");
        }
    }
}
