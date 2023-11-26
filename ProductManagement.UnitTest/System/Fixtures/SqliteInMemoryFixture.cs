using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ProductManagement.UnitTest.System.Fixtures
{

    public class SqliteInMemoryFixture : IDisposable
    {
        private readonly SqliteConnection _connection;

        public SqliteInMemoryFixture()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
        }

        public DbContextOptions<TDbContext> CreateOptions<TDbContext>() where TDbContext : DbContext
        {
            return new DbContextOptionsBuilder<TDbContext>()
                .UseSqlite(_connection)
                .Options;
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }

}
