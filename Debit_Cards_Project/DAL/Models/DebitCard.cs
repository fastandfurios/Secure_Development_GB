namespace Debit_Cards_Project.DAL.Models
{
    public sealed class DebitCard
    {
        /// <summary> Название валюты </summary>
        public string CurrencyName { get; set; }
        
        /// <summary> Держатель карты </summary>
        public Holder Holder { get; set; }
        public int Id { get; set; }

        /// <summary> Номер карты </summary>
        public long NumberCard { get; set; }

        /// <summary> Срок действия карты </summary>
        public TimeSpan ValidityPeriod { get; set; }
    }
}
