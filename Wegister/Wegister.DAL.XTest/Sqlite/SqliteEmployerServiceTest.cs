using Microsoft.EntityFrameworkCore;

namespace Wegister.DAL.XTest.Sqlite
{
    public class SqliteEmployerServiceTest : EmployerServiceTest
    {
        public SqliteEmployerServiceTest()
            : base(
                 new DbContextOptionsBuilder<WegisterDbContext>()
                    .UseSqlite("Filename=EmployerTestDb.db")
                    .Options
                 )
        {  }
    }
}
