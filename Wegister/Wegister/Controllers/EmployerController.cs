using System;
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
    public class EmployerController : ControllerBase
    {
        private readonly IEmployerLogic _employerLogic;
        public EmployerController(IEmployerLogic employerLogic)
        {
            _employerLogic = employerLogic;
        }

        [HttpGet]
        public IEnumerable<Employer> GetEmployers()
        {
            return _employerLogic.GetEmployers();
        }

        [HttpPost]
        public void AddHours([FromBody] Employer employer)
        {
            _employerLogic.AddEmployer(employer);
        }

        [HttpPut("{id}")]
        public void UpdateEmployer([FromRoute] int id, Employer employer)
        {
            _employerLogic.UpdateEmployer(employer);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteEmployer([FromRoute] int id)
        {
            try
            {
                _employerLogic.DeleteEmployer(id);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
