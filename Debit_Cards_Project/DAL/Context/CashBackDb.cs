using Debit_Cards_Project.DAL.Models.CashBack;
using Microsoft.EntityFrameworkCore;

namespace Debit_Cards_Project.DAL.Context
{
    public class CashBackDb : DbContext
    {
        public DbSet<CashBack> CashBacks { get; set; }

        public CashBackDb(DbContextOptions<CashBackDb> options) : base(options)
            => Database.EnsureCreated();
    }
}
