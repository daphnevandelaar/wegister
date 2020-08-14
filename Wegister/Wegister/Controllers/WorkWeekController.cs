using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wegister.BLL;
using Wegister.Models;

namespace Wegister.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class WorkWeekController : ControllerBase
    {
        private IWorkWeekLogic _workWeekLogic;

        public WorkWeekController(IWorkWeekLogic workWeekLogic)
        {
            _workWeekLogic = workWeekLogic;
        }

        [HttpGet]
        public IEnumerable<Workweek> GetHours()
        {
            return _workWeekLogic.GetAllWorkWeeks();
        }

        [HttpGet]
        [Route("month/{year}")]
        public IEnumerable<WorkweekEntry> GetMonths([FromRoute] int year)
        {
            return _workWeekLogic.GetWorkMonthsByYear(year);
        }

        [HttpGet]
        [Route("{year}")]
        public IEnumerable<Workweek> GetWeeks([FromRoute] int year)
        {
            return _workWeekLogic.GetWorkWeeksByYear(year);
        }

        [HttpGet]
        [Route("year")]
        public IEnumerable<WorkweekEntry> GetYears([FromRoute] int year)
        {
            return _workWeekLogic.GetYears();
        }
    }
}
