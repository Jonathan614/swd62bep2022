using Domain.Interfaces;
using Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataAccess.Repositories
{
    public class LogViaFileRepository : ILogRepository
    {
        private string _filename;
        public LogViaFileRepository(string filename)
        {
            _filename = filename;
        }
        public void Log(Log log)
        {
            //StreamWriter,Filestream,FileInfo,System.IO.File, TextWriter
            using (StreamWriter sw = new StreamWriter(_filename, true))
            {
                //Json Serialization
                string str = JsonConvert.SerializeObject(log);
                sw.WriteLine(str);
              //  sw.WriteLine($"Type: {log.Type}, Date: {log.Date}, Message: {log.Message}");

            }
        }
    }
}
