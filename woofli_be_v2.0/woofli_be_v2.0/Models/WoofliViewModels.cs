using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace woofli_be_v2._0.Models
{
    public class WoofliViewModels
    {
        public class PetsitterViewModel
        {
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            [Required]
            public string Phone { get; set; }
            [Required]
            public string Email { get; set; }
        }

        public class AddPetViewModel
        {
            [Required]
            public string Name { get; set; }
        }
    }
}