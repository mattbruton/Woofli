using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace woofli_be_v2._0.Models
{
    public class Medicine
    {
        [Key]
        public int MedicineId { get; set; }
        public bool DoesPrescriptionGetRefill { get; set; }
        public string Name { get; set; }
        public int Dosage { get; set; }
        public string DosageUnit { get; set; }
        public int DosageInterval { get; set; }
        public string DosageIntervalUnit { get; set; }
        public DateTime DosageTime { get; set; }
        public int PrescriptionQuantity { get; set; }
    }
}