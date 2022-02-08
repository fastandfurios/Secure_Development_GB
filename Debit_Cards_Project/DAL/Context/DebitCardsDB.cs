using Debit_Cards_Project.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Debit_Cards_Project.DAL.Context
{
    public sealed class DebitCardsDb : DbContext
    {
        public DbSet<DebitCard> Cards { get; set; }

        public DebitCardsDb(DbContextOptions<DebitCardsDb> options) : base(options) 
            => Database.EnsureCreated();
    }
}
