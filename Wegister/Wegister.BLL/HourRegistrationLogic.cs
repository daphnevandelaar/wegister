using System;
using System.Collections.Generic;
using System.Linq;
using Wegister.DAL.Infra;
using Wegister.Models;
using Wegister.Viewmodels;

namespace Wegister.BLL
{
    public class HourRegistrationLogic : IHourRegistrationLogic
    {
        private IHourRegistrationService _hourRegistrationService;
        private IHourRegistrationExtensionWorkweekService _workweekService;
        private IHourRegistrationExtensionEmployerService _employerService;
        private IWorkWeekCalculation _workWeekCalculation;

        public HourRegistrationLogic(IHourRegistrationService hourRegistrationService, IHourRegistrationExtensionEmployerService employerService, IHourRegistrationExtensionWorkweekService workweekService, IWorkWeekCalculation workWeekCalculation)
        {
            _hourRegistrationService = hourRegistrationService;
            _workweekService = workweekService;
            _employerService = employerService;
            _workWeekCalculation = workWeekCalculation;
        }

        public void DeleteRegistration(int id)
        {
            _hourRegistrationService.Delete(id);
            _hourRegistrationService.Save();
        }

        public HourRegistrationVM GetActiveRegistration()
        {
            return _hourRegistrationService.ReadHourRegistrationWithEmptyEndDate().ToVM();
        }

        public HourRegistrationListVM GetHourRegistrationsByWeekIds(List<string> weekIds)
        {
            return CastToListAndCalculateTotalWorkedHours(_hourRegistrationService.ReadByListOfWeekIds(weekIds).ToList());
        }

        public HourRegistrationListVM GetRegisteredHours()
        {
            return CastToListAndCalculateTotalWorkedHours(_hourRegistrationService.ReadAll().ToList());
        }
        public HourRegistrationListVM GetRegisteredHoursByWeek(int weeknumber)
        {
            return CastToListAndCalculateTotalWorkedHours(_hourRegistrationService.ReadByWeekNumber(weeknumber).ToList());
        }

        public HourRegistrationListVM GetRegisteredHoursByMonthAndEmployerId(int month, int employerId)
        {
            if (month == 0 && employerId != 0)
                return CastToListAndCalculateTotalWorkedHours(_hourRegistrationService.ReadHourRegistrationsByEmployerId(employerId).ToList());
            else if (employerId == 0 && month != 0)
                return CastToListAndCalculateTotalWorkedHours(_hourRegistrationService.ReadByMonth(month).ToList());
            else if (employerId != 0 && month != 0)
                return CastToListAndCalculateTotalWorkedHours(_hourRegistrationService.ReadHourRegistrationsByMonthAndEmployerId(month, employerId).ToList());
            else
                return CastToListAndCalculateTotalWorkedHours(_hourRegistrationService.ReadAll().ToList());
        }

        public HourRegistrationListVM GetRegisteredHoursByWeekAndEmployerId(int weeknumber, int employerId)
        {
            if (weeknumber == 0 && employerId != 0)
                return CastToListAndCalculateTotalWorkedHours(_hourRegistrationService.ReadHourRegistrationsByEmployerId(employerId).ToList());
            else if (employerId == 0 && weeknumber != 0)
                return CastToListAndCalculateTotalWorkedHours(_hourRegistrationService.ReadByWeekNumber(weeknumber).ToList());
            else if (employerId != 0 && weeknumber != 0)
                return CastToListAndCalculateTotalWorkedHours(_hourRegistrationService.ReadHourRegistrationsByWeekAndEmployerId(weeknumber, employerId).ToList());
            else
                return CastToListAndCalculateTotalWorkedHours(_hourRegistrationService.ReadAll().ToList());
        }

        public void RegisterHour(HourRegistrationSimpleVM registration)
        {
            var hourRegister = registration.ToPoco();
            hourRegister.Employer = _employerService.ReadById(hourRegister.Employer.Id);
            hourRegister.Workweek = _workweekService.ReadByDate(hourRegister.StartTime.Date);

            if (hourRegister.Workweek == null)
            {
                _workweekService.Create(_workWeekCalculation.getCorrectWorkweek(registration.StartTime.Date));
                _workweekService.Save();
                hourRegister.Workweek = _workweekService.ReadByDate(registration.StartTime.Date);
            }

            hourRegister.TotalWorkedHoursDayInSeconds = CalculateTotalWorkedHoursFromTwoDates(hourRegister);
            _hourRegistrationService.Create(hourRegister);

            _hourRegistrationService.Save();
        }

        public void UpdateRegistration(HourRegistrationSimpleVM registration)
        {
            var hourRegister = registration.ToPoco();
            hourRegister.Employer = _employerService.ReadById(hourRegister.Employer.Id);
            hourRegister.Workweek = _workweekService.ReadByDate(registration.StartTime.Date);

            if (hourRegister.Workweek == null)
            {
                _workweekService.Create(_workWeekCalculation.getCorrectWorkweek(registration.StartTime.Date));
                _workweekService.Save();
                hourRegister.Workweek = _workweekService.ReadByDate(registration.StartTime.Date);
            }

            hourRegister.TotalWorkedHoursDayInSeconds = CalculateTotalWorkedHoursFromTwoDates(hourRegister);
            _hourRegistrationService.Update(hourRegister);

            _hourRegistrationService.Save();
        }

        public int CalculateTotalWorkedHoursFromTwoDates(HourRegistration hourRegistration)
        {
            var date = hourRegistration.EndTime - hourRegistration.StartTime;
            var hours = date.TotalSeconds - (hourRegistration.Recreation * 60);
            return (int)hours;
        }

        public HourRegistrationListVM CastToListAndCalculateTotalWorkedHours(List<HourRegistration> hourRegistrations)
        {
            //If the cast and calculation are separated it'll be a double loop, which isn't completely necessary and resource consuming. Where the calculation is pretty simple to put this together.
            var hourRegistrationList = new HourRegistrationListVM() { 
                HourRegistrations = new List<HourRegistrationVM>()
            };

            hourRegistrations.ForEach(registration => {
                hourRegistrationList.HourRegistrations.Add(registration.ToVM());
                hourRegistrationList.TotalWorkedHoursInSeconds += registration.TotalWorkedHoursDayInSeconds;
            });

            return hourRegistrationList;
        }
    }
}
