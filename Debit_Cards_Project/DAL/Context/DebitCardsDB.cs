using Debit_Cards_Project.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Debit_Cards_Project.DAL.Context
{
    public sealed class DebitCardsDB : DbContext
    {
        public DbSet<DebitCard> Cards { get; set; }

        public DebitCardsDB(DbContextOptions<DebitCardsDB> options) : base(options) 
            => Database.EnsureCreated();
    }
}
