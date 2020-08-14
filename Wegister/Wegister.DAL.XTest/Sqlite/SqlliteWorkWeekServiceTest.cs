using Microsoft.EntityFrameworkCore;

namespace Wegister.DAL.XTest
{
    public class SqlliteWorkWeekServiceTest : WorkweekServiceTest
    {
        public SqlliteWorkWeekServiceTest()
            : base(
                 new DbContextOptionsBuilder<WegisterDbContext>()
                    .UseSqlite("Filename=WorkweekTestDb.db")
                    .Options
                 )
        { }
    }
}
