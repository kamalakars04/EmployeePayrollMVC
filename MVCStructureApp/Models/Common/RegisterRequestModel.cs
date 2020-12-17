using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MVCStructureApp.Repository;

namespace MVCStructureApp.Models.Common
{
    public class RegisterRequestModel
    {
        public int EmpId { get; set; }

        [Required]
        [RegularExpression("^[A-Z]{1}[A-Za-z]{2,}",ErrorMessage = "Give min 3 letters and start with capital")]
        public string Name { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Salary { get; set; }

        [Required]
        [DateRange("01/01/2000")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        public string Description { get; set; }

        public List<SalaryDTO> salaries = new EmployeeRepository().GetSalaries();

    }
}