namespace Debit_Cards_Project.DTO
{
    public class DebitCardDto
    {
        public int Id { get; set; }
        /// <summary> Название валюты </summary>
        public string CurrencyName { get; set; }

        /// <summary> Держатель карты </summary>
        public HolderDto HolderDto { get; set; }

        /// <summary> Номер карты </summary>
        public long NumberCard { get; set; }

        /// <summary> Месяц действия </summary>
        public int Month { get; set; }

        /// <summary> Год действия  </summary>
        public int Year { get; set; }
    }
}
