using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class Record
    {
        public int ID { get; set; }
        [DisplayName("Employee Name")]
        public string EmployeeName { get; set; }
        [DisplayName("Clock In Time")]
        public DateTime ClockInTime { get; set; }
        [DisplayName("Clock Out Time")]
        public DateTime ClockOutTime { get; set; }
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }
    }
}