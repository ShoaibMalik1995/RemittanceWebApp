using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RemittanceWebApp.Models
{
    public class RemittanceLog
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime LogDate { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan LogTime { get; set; }
        public int FiscalYear { get; set; }
        public string Ref_No { get; set; }
        public string UserId { get; set; }
        public string ChannelId { get; set; }
        public string ProfileNumber { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
    }
}
