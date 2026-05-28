using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebAPI.common.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public Guid GuidId { get; set; }

        public string GuidText { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50)]
        public string Name { get; set; }

        [Range(18, 60)]
        public int Age { get; set; }

        [Required]
        public string Department { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public bool IsActive { get; set; }


    }
}
