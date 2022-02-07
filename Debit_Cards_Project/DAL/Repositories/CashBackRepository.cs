using Debit_Cards_Project.DAL.Context;
using Debit_Cards_Project.DAL.Interfaces;
using Debit_Cards_Project.DAL.Models.CashBack;

namespace Debit_Cards_Project.DAL.Repositories
{
    public class CashBackRepository : ICashBackRepository
    {
        private readonly CashBackDb _db;
        private readonly ILogger<CashBackRepository> _logger;

        public CashBackRepository(CashBackDb db, ILogger<CashBackRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public void Create(CashBack cashBack)
        {
            if(ReadAll().Contains(cashBack))
                return;

            _db.CashBacks.Add(cashBack);
            _db.SaveChanges();
        }

        public IReadOnlyList<CashBack> ReadAll()
        {
            try
            {
                return _db.CashBacks.ToList();
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                throw;
            }
        }

        public CashBack ReadById(int id)
        {
            try
            {
                var cashBack = ReadAll().FirstOrDefault(c => c.Id == id)!;
                return cashBack;
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                throw;
            }
        }

        public void Update(CashBack cashBack, int id)
        {
            return;
        }

        public void Delete(int id)
        {
            if (ReadAll().Contains(ReadById(id)))
            {
                _db.CashBacks.Remove(ReadById(id));
                _db.SaveChanges();
            }
        }
    }
}
