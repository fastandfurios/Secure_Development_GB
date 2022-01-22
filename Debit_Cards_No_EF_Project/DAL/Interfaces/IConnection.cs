using System.Data.SQLite;

namespace Debit_Cards_No_EF_Project.DAL.Interfaces
{
    public interface IConnection
    {
        SQLiteConnection GetOpenedConnection();
    }
}
