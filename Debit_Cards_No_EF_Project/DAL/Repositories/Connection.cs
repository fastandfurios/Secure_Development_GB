using System.Data.SQLite;
using Debit_Cards_No_EF_Project.DAL.Interfaces;

namespace Debit_Cards_No_EF_Project.DAL.Repositories
{
    public class Connection : IConnection
    {
        private const string _connectionString = "Data Source=Cards.db;Version=3;Pooling=true;Max Pool Size=100;";

        public SQLiteConnection GetOpenedConnection()
            => new(_connectionString);
    }
}
