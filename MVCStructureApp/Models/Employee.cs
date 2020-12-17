using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCStructureApp.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpId { get; set; }

        [Required]
        [RegularExpression("^[A-Z]{1}[A-Za-z]{2,}")]
        public string Name { get; set; }

        [Required]
        public string Gender { get; set; }

        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        public int SalaryId { get; set; }

        [ForeignKey("SalaryId")]
        public Salary Salary { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        public string Description { get; set; }
    }
}