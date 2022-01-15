namespace Debit_Cards_Project.DAL.Models
{
    public sealed class DebitCard
    {
        public string CurrencyName { get; set; }
        public Holder Holder { get; set; }
        public int Id { get; set; }
        public long NumberCard { get; set; }
        public TimeSpan ValidityPeriod { get; set; }
    }
}
