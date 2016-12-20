using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace woofli_be_v2._0.Models
{
    public class Veterinarian
    {
        [Key]
        public int VeterinarianId { get; set; }
        public string ClinicName { get; set; }
        public string Phone { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}