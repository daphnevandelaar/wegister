using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wegister.BLL;
using Wegister.Models;
using Wegister.Viewmodels;

namespace Wegister.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class HourRegistrationController : ControllerBase
    {
        private IHourRegistrationLogic _hourRegistrationLogic;

        public HourRegistrationController(IHourRegistrationLogic hourRegistrationService)
        {
            _hourRegistrationLogic = hourRegistrationService;
        }

        [HttpGet]
        public HourRegistrationListVM GetHours()
        {
            return _hourRegistrationLogic.GetRegisteredHours();
        }

        [HttpGet]
        [Route("active")]
        public HourRegistrationVM GetActiveRegistration()
        {
            return _hourRegistrationLogic.GetActiveRegistration();
        }

        [HttpPut]
        [Route("active")]
        public ActionResult EndRegistration([FromBody] HourRegistrationSimpleVM hourRegistration)
        {
            try
            {
                _hourRegistrationLogic.UpdateRegistration(hourRegistration);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult ChangeRegistration([FromRoute] int id, [FromBody] HourRegistrationSimpleVM hourRegistration)
        {
            try
            {
                _hourRegistrationLogic.UpdateRegistration(hourRegistration);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult RemoveRegistration([FromRoute] int id)
        {
            try
            {
                _hourRegistrationLogic.DeleteRegistration(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("week/{weeknumber}/{employerid}")]
        public HourRegistrationListVM GetHoursByWeekAndEmployerId([FromRoute] int weeknumber, [FromRoute] int employerid)
        {
            return _hourRegistrationLogic.GetRegisteredHoursByWeekAndEmployerId(weeknumber, employerid);
        }

        [HttpGet]
        [Route("month/{month}/{employerid}")]
        public HourRegistrationListVM GetHoursByMonthAndEmployerId([FromRoute] int month, [FromRoute] int employerid)
        {
            return _hourRegistrationLogic.GetRegisteredHoursByMonthAndEmployerId(month, employerid);
        }

        [HttpPost]
        public ActionResult AddHours([FromBody] HourRegistrationSimpleVM hourRegistration)
        {
            try
            {
                _hourRegistrationLogic.RegisterHour(hourRegistration);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}