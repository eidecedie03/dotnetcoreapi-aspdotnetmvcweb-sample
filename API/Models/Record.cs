using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Record
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime ClockInTime { get; set; }
        public DateTime ClockOutTime { get; set; }
        public bool IsActive { get; set; }
    }
}
