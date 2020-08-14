using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Wegister.DAL;

namespace Wegister.BLL.XTest
{
    public class SqliteWorkWeekTest : HourRegistrationLogicTest
    {
        public SqliteWorkWeekTest()
            : base(
                    new DbContextOptionsBuilder<WegisterDbContext>()
                    .UseSqlite("Filename=Test.db")
                    .Options
                    )
        { }
        
    }
}
