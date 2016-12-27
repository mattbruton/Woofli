using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using woofli_be_v2._0.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using woofli_be_v2._0.DAL;
using Microsoft.AspNet.Identity;
using Moq;

namespace woofli_be_v2._0.Tests.DAL
{
    [TestClass]
    public class WoofliRepoTests
    {
        private Mock<DbSet<Pet>> mock_pets { get; set; }
        private Mock<DbSet<Veterinarian>> mock_vets { get; set; }
        private Mock<DbSet<Petsitter>> mock_petsitters { get; set; }
        private Mock<DbSet<Medicine>> mock_medicines { get; set; }
        private Mock<DbSet<SitterAppointment>> mock_sitter_appointments { get; set; }
        private Mock<DbSet<CustomUser>> mock_app_users { get; set; }


        private List<Pet> pets { get; set; }
        private List<Veterinarian> vets { get; set; }
        private List<Petsitter> petsitters { get; set; }
        private List<Medicine> medicines { get; set; }
        private List<SitterAppointment> sitter_appointments { get; set; }
        private List<CustomUser> app_users { get; set; }

        private Mock<UserManager<CustomUser>> mock_user_manager_context { get; set; }
        private Mock<AuthContext> mock_context { get; set; }
        private AuthRepository repo { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            mock_pets = new Mock<DbSet<Pet>>();
            mock_petsitters = new Mock<DbSet<Petsitter>>();
            mock_vets = new Mock<DbSet<Veterinarian>>();
            mock_medicines = new Mock<DbSet<Medicine>>();
            mock_sitter_appointments = new Mock<DbSet<SitterAppointment>>();
            mock_app_users = new Mock<DbSet<CustomUser>>();

            pets = new List<Pet>();
            petsitters = new List<Petsitter>();
            vets = new List<Veterinarian>();
            medicines = new List<Medicine>();
            sitter_appointments = new List<SitterAppointment>();
            app_users = new List<CustomUser>();

            mock_context = new Mock<AuthContext>() { CallBase = true };
            mock_user_manager_context = new Mock<UserManager<CustomUser>>();
            repo = new AuthRepository(mock_context.Object);

            ConnectToDatastore();
        }

        public void ConnectToDatastore()
        {
            var query_pets = pets.AsQueryable();
            var query_app_users = app_users.AsQueryable();
            var query_vets = vets.AsQueryable();
            var query_medicines = medicines.AsQueryable();
            var query_sitter_appointments = sitter_appointments.AsQueryable();
            var query_petsitters = petsitters.AsQueryable();

            mock_pets.As<IQueryable<Pet>>().Setup(m => m.Provider).Returns(query_pets.Provider);
            mock_pets.As<IQueryable<Pet>>().Setup(m => m.Expression).Returns(query_pets.Expression);
            mock_pets.As<IQueryable<Pet>>().Setup(m => m.ElementType).Returns(query_pets.ElementType);
            mock_pets.As<IQueryable<Pet>>().Setup(m => m.GetEnumerator()).Returns(() => query_pets.GetEnumerator());

            mock_context.Setup(c => c.Pets).Returns(mock_pets.Object);
            mock_pets.Setup(u => u.Add(It.IsAny<Pet>())).Callback((Pet t) => pets.Add(t));
            mock_pets.Setup(u => u.Remove(It.IsAny<Pet>())).Callback((Pet t) => pets.Remove(t));


            mock_vets.As<IQueryable<Veterinarian>>().Setup(m => m.Provider).Returns(query_vets.Provider);
            mock_vets.As<IQueryable<Veterinarian>>().Setup(m => m.Expression).Returns(query_vets.Expression);
            mock_vets.As<IQueryable<Veterinarian>>().Setup(m => m.ElementType).Returns(query_vets.ElementType);
            mock_vets.As<IQueryable<Veterinarian>>().Setup(m => m.GetEnumerator()).Returns(() => query_vets.GetEnumerator());

            mock_context.Setup(c => c.Veterinarians).Returns(mock_vets.Object);
            mock_vets.Setup(u => u.Add(It.IsAny<Veterinarian>())).Callback((Veterinarian t) => vets.Add(t));
            mock_vets.Setup(u => u.Remove(It.IsAny<Veterinarian>())).Callback((Veterinarian t) => vets.Remove(t));


            mock_medicines.As<IQueryable<Medicine>>().Setup(m => m.Provider).Returns(query_medicines.Provider);
            mock_medicines.As<IQueryable<Medicine>>().Setup(m => m.Expression).Returns(query_medicines.Expression);
            mock_medicines.As<IQueryable<Medicine>>().Setup(m => m.ElementType).Returns(query_medicines.ElementType);
            mock_medicines.As<IQueryable<Medicine>>().Setup(m => m.GetEnumerator()).Returns(() => query_medicines.GetEnumerator());

            mock_context.Setup(c => c.Medicines).Returns(mock_medicines.Object);
            mock_medicines.Setup(u => u.Add(It.IsAny<Medicine>())).Callback((Medicine t) => medicines.Add(t));
            mock_medicines.Setup(u => u.Remove(It.IsAny<Medicine>())).Callback((Medicine t) => medicines.Remove(t));


            mock_petsitters.As<IQueryable<Petsitter>>().Setup(m => m.Provider).Returns(query_petsitters.Provider);
            mock_petsitters.As<IQueryable<Petsitter>>().Setup(m => m.Expression).Returns(query_petsitters.Expression);
            mock_petsitters.As<IQueryable<Petsitter>>().Setup(m => m.ElementType).Returns(query_petsitters.ElementType);
            mock_petsitters.As<IQueryable<Petsitter>>().Setup(m => m.GetEnumerator()).Returns(() => query_petsitters.GetEnumerator());

            mock_context.Setup(c => c.Petsitters).Returns(mock_petsitters.Object);
            mock_petsitters.Setup(u => u.Add(It.IsAny<Petsitter>())).Callback((Petsitter t) => petsitters.Add(t));
            mock_petsitters.Setup(u => u.Remove(It.IsAny<Petsitter>())).Callback((Petsitter t) => petsitters.Remove(t));


            mock_sitter_appointments.As<IQueryable<SitterAppointment>>().Setup(m => m.Provider).Returns(query_sitter_appointments.Provider);
            mock_sitter_appointments.As<IQueryable<SitterAppointment>>().Setup(m => m.Expression).Returns(query_sitter_appointments.Expression);
            mock_sitter_appointments.As<IQueryable<SitterAppointment>>().Setup(m => m.ElementType).Returns(query_sitter_appointments.ElementType);
            mock_sitter_appointments.As<IQueryable<SitterAppointment>>().Setup(m => m.GetEnumerator()).Returns(() => query_sitter_appointments.GetEnumerator());

            mock_context.Setup(c => c.SitterAppointments).Returns(mock_sitter_appointments.Object);
            mock_sitter_appointments.Setup(u => u.Add(It.IsAny<SitterAppointment>())).Callback((SitterAppointment t) => sitter_appointments.Add(t));
            mock_sitter_appointments.Setup(u => u.Remove(It.IsAny<SitterAppointment>())).Callback((SitterAppointment t) => sitter_appointments.Remove(t));

            mock_app_users.As<IQueryable<CustomUser>>().Setup(m => m.Provider).Returns(query_app_users.Provider);
            mock_app_users.As<IQueryable<CustomUser>>().Setup(m => m.Expression).Returns(query_app_users.Expression);
            mock_app_users.As<IQueryable<CustomUser>>().Setup(m => m.ElementType).Returns(query_app_users.ElementType);
            mock_app_users.As<IQueryable<CustomUser>>().Setup(m => m.GetEnumerator()).Returns(() => query_app_users.GetEnumerator());

            mock_context.Setup(c => c.Users).Returns(mock_app_users.Object);
            mock_app_users.Setup(u => u.Add(It.IsAny<CustomUser>())).Callback((CustomUser t) => app_users.Add(t));
        }

        public void IncludeMockData()
        {
            SitterAppointment test_appt_2 = new SitterAppointment { SitterAppointmentId = 3, Guest = new Pet { Name = "Little Man" } };

            Petsitter testsitter_2 = new Petsitter { PetsitterId = 3, FirstName = "Harry", LastName = "McLarry", Appointments = new List<SitterAppointment> { test_appt_2 } };

            Medicine medicine_to_add = new Medicine { Name = "Not Tylenol", DoesPrescriptionGetRefill = true, Dosage = 2, DosageUnit = "capsule", MedicineId = 2, PrescriptionQuantity = 30 };

            Veterinarian vet1 = new Veterinarian { VeterinarianId = 10, ClinicName = "TestHospital", City = "Dallas", State = "TX" };
            Veterinarian vet2 = new Veterinarian { VeterinarianId = 12, ClinicName = "TestClinic", City = "Nashville", State = "TN" };

            Pet pet1 = new Pet { PetId = 1, Name = "testdog", PrimaryVet = vet1, Medications = new List<Medicine> { medicine_to_add } };
            Pet pet2 = new Pet { PetId = 2, Name = "testcat", PrimaryVet = vet2 };

            CustomUser testPerson = new CustomUser { Id = "abc", UserName = "test123", Petsitters = new List<Petsitter> { testsitter_2 }, Pets = new List<Pet> { pet1, pet2 } };
            // Little hacky, but can't set the owner on the pet until the Owner exists.

            pet1.Owner = testPerson;
            pet2.Owner = testPerson;

            petsitters.Add(testsitter_2);
            sitter_appointments.Add(test_appt_2);
            medicines.Add(medicine_to_add);
            vets.Add(vet1);
            vets.Add(vet2);
            pets.Add(pet1);
            pets.Add(pet2);
            app_users.Add(testPerson);

        }

        [TestMethod]
        public void RepoAssertRepoIsNotNull()
        {
            Assert.IsNotNull(repo);
        }

        [TestMethod]
        public void RepoEnsureCanReturnListOfPetsForUser()
        {
            IncludeMockData();
            List<Pet> actual_pets = repo.GetAllPetsForUser("test123");
            Assert.IsNotNull(actual_pets);
        }

        [TestMethod]
        public void RepoEnsureCanReturnCorrectSinglePet()
        {
            IncludeMockData();
            string expected_pet_name = "testdog";
            string actual_pet_name = repo.GetPetById(1).Name;

            Assert.AreEqual(expected_pet_name, actual_pet_name);
        }

        [TestMethod]
        public void RepoEnsureCanReturnCorrectSinglePetsitter()
        {
            IncludeMockData();
            string expected_petsitter_firstname = "Harry";
            string actual_petsitter_firstname = repo.GetPetsitterById(3).FirstName;

            Assert.AreEqual(expected_petsitter_firstname, actual_petsitter_firstname);
        }

        [TestMethod]
        public void RepoEnsureReturnNullWhenGettingPetWithInvalidId()
        {
            IncludeMockData();
            Assert.IsNull(repo.GetPetById(5));
        }

        [TestMethod]
        public void RepoEnsureReturnPetsittersForUser()
        {
            IncludeMockData();
            Assert.IsTrue(repo.GetAllPetsittersForUser("test123").Count == 1);
        }

        [TestMethod]
        public void RepoEnsureCanAddPetsitterToUserListOfSitters()
        {
            IncludeMockData();
            Petsitter sitter_to_add = new Petsitter { FirstName = "Ronald", LastName = "McDonald" };
            repo.AddPetsitterToUser("test123", sitter_to_add);

            Assert.IsTrue(repo.GetAllPetsittersForUser("test123").Count == 2);
        }

        [TestMethod]
        public void RepoRemovePetsitterRemovesFromDb()
        {
            IncludeMockData();
            repo.RemovePetsitterById("test123", 3);
            int expected_sitter_count = 0;
            int actual_sitter_count = repo.GetAllPetsittersForUser("test123").Count;
            
            Assert.AreEqual(expected_sitter_count, actual_sitter_count);
        }
    }
}

