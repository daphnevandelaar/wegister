using System;
using Wegister.Models;

namespace Wegister.BLL
{
    public interface IWorkWeekCalculation
    {
        public Workweek getCorrectWorkweek(DateTime workweek);
    }
}