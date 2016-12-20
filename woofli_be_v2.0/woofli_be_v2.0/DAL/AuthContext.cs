using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using woofli_be_v2._0.Models;

namespace woofli_be_v2._0.DAL
{
    public class AuthContext : IdentityDbContext<CustomUser>
    {
        public AuthContext() : base("AuthContext") { }
        public virtual DbSet<Veterinarian> Veterinarians { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<Petsitter> Petsitters { get; set; }
        public virtual DbSet<SitterAppointment> SitterAppointments { get; set; }
    }
}