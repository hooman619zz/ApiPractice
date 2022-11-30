﻿namespace FirstApiProject.Services
{
    public class LocalMailService : IMailService
    {
        private readonly string _mailTo = string.Empty;
        private readonly string _mailFrom = string.Empty;
        public LocalMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSetting:mailToAddress"];
            _mailFrom = configuration["mailSetting:mailFromAddress"];

        }
        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail From {_mailFrom} : Mail To {_mailTo} , with {nameof(LocalMailService)} \n" +
                $"subject : {subject} \n" +
                $"message :{message}");

        }
    }
}
