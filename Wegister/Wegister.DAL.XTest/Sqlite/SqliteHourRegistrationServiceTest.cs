using Microsoft.EntityFrameworkCore;

namespace Wegister.DAL.XTest
{
    public class SqliteHourRegistrationServiceTest : HourRegistrationServiceTest
    {
        public SqliteHourRegistrationServiceTest()
            : base(
                 new DbContextOptionsBuilder<WegisterDbContext>()
                    .UseSqlite("Filename=HourRegistrationTestDb.db")
                    .Options
                 )
        { }
    }
}
