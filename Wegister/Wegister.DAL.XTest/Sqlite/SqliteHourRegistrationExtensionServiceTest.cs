using Microsoft.EntityFrameworkCore;

namespace Wegister.DAL.XTest
{
    public class SqliteHourRegistrationExtensionServiceTest : HourRegistrationExtensionServiceTest
    {
        public SqliteHourRegistrationExtensionServiceTest()
            : base(
                 new DbContextOptionsBuilder<WegisterDbContext>()
                    .UseSqlite("Filename=HourRegistrationExstTestDb.db")
                    .Options
                 )
        { }
    }
}
