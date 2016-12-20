namespace woofli_be_v2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Medicines",
                c => new
                    {
                        MedicineId = c.Int(nullable: false, identity: true),
                        DoesPrescriptionGetRefill = c.Boolean(nullable: false),
                        Name = c.String(),
                        Dosage = c.Int(nullable: false),
                        DosageUnit = c.String(),
                        DosageInterval = c.Int(nullable: false),
                        DosageIntervalUnit = c.String(),
                        DosageTime = c.DateTime(nullable: false),
                        PrescriptionQuantity = c.Int(nullable: false),
                        Pet_PetId = c.Int(),
                    })
                .PrimaryKey(t => t.MedicineId)
                .ForeignKey("dbo.Pets", t => t.Pet_PetId)
                .Index(t => t.Pet_PetId);
            
            CreateTable(
                "dbo.Pets",
                c => new
                    {
                        PetId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        ImageUrl = c.String(),
                        IsCanine = c.Boolean(nullable: false),
                        Owner_Id = c.String(maxLength: 128),
                        PrimaryVet_VeterinarianId = c.Int(),
                    })
                .PrimaryKey(t => t.PetId)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .ForeignKey("dbo.Veterinarians", t => t.PrimaryVet_VeterinarianId)
                .Index(t => t.Owner_Id)
                .Index(t => t.PrimaryVet_VeterinarianId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PrefersContactByPhone = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Petsitters",
                c => new
                    {
                        PetsitterId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        CustomUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PetsitterId)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomUser_Id)
                .Index(t => t.CustomUser_Id);
            
            CreateTable(
                "dbo.SitterAppointments",
                c => new
                    {
                        SitterAppointmentId = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Guest_PetId = c.Int(),
                        Petsitter_PetsitterId = c.Int(),
                    })
                .PrimaryKey(t => t.SitterAppointmentId)
                .ForeignKey("dbo.Pets", t => t.Guest_PetId)
                .ForeignKey("dbo.Petsitters", t => t.Petsitter_PetsitterId)
                .Index(t => t.Guest_PetId)
                .Index(t => t.Petsitter_PetsitterId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Veterinarians",
                c => new
                    {
                        VeterinarianId = c.Int(nullable: false, identity: true),
                        ClinicName = c.String(),
                        Phone = c.String(),
                        StreetAddress = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                    })
                .PrimaryKey(t => t.VeterinarianId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Pets", "PrimaryVet_VeterinarianId", "dbo.Veterinarians");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Petsitters", "CustomUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SitterAppointments", "Petsitter_PetsitterId", "dbo.Petsitters");
            DropForeignKey("dbo.SitterAppointments", "Guest_PetId", "dbo.Pets");
            DropForeignKey("dbo.Pets", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Medicines", "Pet_PetId", "dbo.Pets");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.SitterAppointments", new[] { "Petsitter_PetsitterId" });
            DropIndex("dbo.SitterAppointments", new[] { "Guest_PetId" });
            DropIndex("dbo.Petsitters", new[] { "CustomUser_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Pets", new[] { "PrimaryVet_VeterinarianId" });
            DropIndex("dbo.Pets", new[] { "Owner_Id" });
            DropIndex("dbo.Medicines", new[] { "Pet_PetId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Veterinarians");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.SitterAppointments");
            DropTable("dbo.Petsitters");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Pets");
            DropTable("dbo.Medicines");
        }
    }
}
