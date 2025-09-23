using AP.Demo_Project.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.Demo_Project.Infrastructure.Services
{
    public class FakeEmailService : IEmailService
    {
        public Task SendCityDeletedEmail(string cityName)
        {
            Console.WriteLine($"[EMAIL SIMULATIE] To: admin@site.com | Subject: Stad verwijderd | Body: Stad '{cityName}' is verwijderd.");
            return Task.CompletedTask;
        }
    }
}
