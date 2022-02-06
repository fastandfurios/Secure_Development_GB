namespace Debit_Cards_Project.DAL.Models.CashBack
{
    public sealed class CashBack
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public double Percent { get; set; }
    }
}
