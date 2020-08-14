using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wegister.BLL;
using Wegister.Models;

namespace Wegister.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IHourRegistrationLogic _hourRegistrationLogic;
        private readonly IWorkWeekLogic _workweekLogic;
        public EmailController(IHourRegistrationLogic hourRegistrationLogic, IWorkWeekLogic workweekLogic)
        {
            _hourRegistrationLogic = hourRegistrationLogic;
            _workweekLogic = workweekLogic;
        }

        [HttpGet]
        public void SendMail()
        {
            var currentWeeknumber = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Now.Date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var workedHours = _hourRegistrationLogic.GetRegisteredHoursByWeek(currentWeeknumber);
            if(workedHours.HourRegistrations.Count != 0)
            {
                try
                {
                    MailMessage message = new MailMessage();
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                    message.From = new MailAddress("daphnevandelaar@gmail.com");
                    message.To.Add(new MailAddress(Environment.GetEnvironmentVariable("MAIL_TO")));
                    message.CC.Add(new MailAddress(Environment.GetEnvironmentVariable("CC_TO")));
                    message.Subject = "Gewerkte uren week: " + currentWeeknumber;
                    message.IsBodyHtml = true;
                    message.Body = "testbody"; //GetHtmlTable(workedHours.HourRegistrations, currentWeeknumber.ToString());
                    smtp.Port = 587;
                    smtp.Host = "smtp.gmail.com";
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("daphnevandelaar@gmail.com", Environment.GetEnvironmentVariable("GMAIL_PASSWORD"));
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                    smtp.Send(message);
                }
                catch (Exception e) { 
                    Console.WriteLine(e); 
                }
            }
        }

        [HttpPost]
        public void SendMailByWeekIds([FromBody] List<string> weekIds)
        {
            var currentWeeknumber = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Now.Date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var workedHours = _hourRegistrationLogic.GetHourRegistrationsByWeekIds(weekIds);
            var weekNumbers = new List<int>();
            var titleString = "";
            workedHours.HourRegistrations.ForEach(workedHour =>
            {
                if (!weekNumbers.Contains(Convert.ToInt32(workedHour.Workweek.WeekNumber)))
                    weekNumbers.Add(Convert.ToInt32(workedHour.Workweek.WeekNumber));
            });

            weekNumbers.ForEach(weeknumber =>
            {
                titleString += weeknumber + "; ";
            });

            if (workedHours.HourRegistrations.Count != 0)
            {
                try
                {
                    MailMessage message = new MailMessage();
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                    message.From = new MailAddress("daphnevandelaar@gmail.com");
                    message.To.Add(new MailAddress(Environment.GetEnvironmentVariable("MAIL_TO")));
                    message.CC.Add(new MailAddress(Environment.GetEnvironmentVariable("CC_TO")));
                    message.Subject = "Gewerkte uren week: " + titleString;
                    message.IsBodyHtml = true;
                    message.Body = "testbody"; //GetHtmlTable(workedHours.HourRegistrations, titleString);
                    smtp.Port = 587;
                    smtp.Host = "smtp.gmail.com";
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("daphnevandelaar@gmail.com", Environment.GetEnvironmentVariable("GMAIL_PASSWORD"));
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                    smtp.Send(message);

                    _workweekLogic.UpdateWorkweekStatusByIds(weekIds);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

        }

        private string GetHtmlTable(List<HourRegistration> hourRegistrations, string weeknumbers)
        {
            var tableRows = "";
            foreach(HourRegistration hourRegistration in hourRegistrations)
            {
                var tableRecord = $"<tr><td>{hourRegistration.StartTime}</td><td>{hourRegistration.EndTime}</td><td>{hourRegistration.Recreation}</td></tr>";
                tableRows = tableRows + tableRecord;
            }
            var style = "<style>p { font-family: arial, sans-serif; } table {font-family: arial, sans-serif; width: 100%; border-collapse: collapse;} td, th {padding-left: 8px;   text-align: left;   border-bottom: 1px solid #ddd; } </style>";
            var table = @$"
<body>
{style}
<p>Hallo,</p>
<p>Bij deze de gewerkte uren van week {weeknumbers}:</p>
<table>
    <tr>
        <th>Starttijd</th>
        <th>Eindtijd</th>
        <th>Pauze <br>(minuten)</th>
    </tr>
    {tableRows}
</table>

<p>Met vriendelijke groet,  </p>
<p>Daphne van de Laar       </p>
</body>
";
            return table;
        }
    }
}