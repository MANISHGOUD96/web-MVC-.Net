using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectRevision1.Models
{
    public class EmpModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Pllzzzz Enter Your Name......")]
        public string Name { get; set; }
        [Required]
        public double? Age { get; set; }
        [Required]
        public string Adress { get; set; }
        public string Company { get; set; }
        public double? Salary { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Addhar { get; set; }
    }
}