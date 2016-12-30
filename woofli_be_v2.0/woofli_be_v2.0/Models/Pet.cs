using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace woofli_be_v2._0.Models
{
    public class Pet
    {
        [Key]
        public int PetId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public CustomUser Owner { get; set; }
        public virtual Veterinarian PrimaryVet { get; set; }
        public virtual List<Medicine> Medications { get; set; }
        public string ImageUrl { get; set; }
        public bool IsCanine { get; set; }
    }
}