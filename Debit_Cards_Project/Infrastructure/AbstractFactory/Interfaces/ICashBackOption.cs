namespace Debit_Cards_Project.Infrastructure.AbstractFactory.Interfaces
{
    public interface ICashBackOption
    {
        string Category { get; set; }
        string Description { get; set; }
        double Percent { get; set; }
    }
}
