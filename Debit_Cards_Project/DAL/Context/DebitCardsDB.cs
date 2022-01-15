using Debit_Cards_Project.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Debit_Cards_Project.DAL.Context
{
    public class DebitCardsDB : DbContext
    {
        public DbSet<DebitCard> Cards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql();
    }
}
