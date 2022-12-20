using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services
{
    public class LogsService
    {
        ILogRepository _logRepo;
        public LogsService(ILogRepository logRepo)
        { _logRepo = logRepo; }

        public void LogMessage (string message, string type)
        {
            Log log = new Log()
            {
                Message = message,
                Type = type,
                Date = DateTime.Now.ToString("dd/MM/yyyy HH:ss")
            };

            _logRepo.Log(log);
        }

    }
}
