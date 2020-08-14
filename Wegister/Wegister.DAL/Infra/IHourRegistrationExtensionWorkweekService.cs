using System;
using System.Collections.Generic;
using Wegister.DAL.Infra.Internals;
using Wegister.Models;

namespace Wegister.DAL.Infra
{
    public interface IHourRegistrationExtensionWorkweekService : ICreate<Workweek>
    {
        Workweek ReadByDate(DateTime date);
    }
}
