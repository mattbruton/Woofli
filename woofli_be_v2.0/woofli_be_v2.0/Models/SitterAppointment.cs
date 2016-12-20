using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace woofli_be_v2._0.Models
{
    public class SitterAppointment
    {
        [Key]
        public int SitterAppointmentId { get; set; }
        public Pet Guest { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}