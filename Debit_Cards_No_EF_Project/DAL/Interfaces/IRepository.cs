namespace Debit_Cards_No_EF_Project.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(T item);
        IReadOnlyList<T> ReadAll();
        T ReadById(int id);
        void Update(T item, int id);
        void Delete(int id);
    }
}
