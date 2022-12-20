using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Log
    {
        public string Message { get; set; }
        public string Date { get; set; } //1/1/2023 14:41
        public string Type { get; set; } //info, warning, error, etc.
    }
}
