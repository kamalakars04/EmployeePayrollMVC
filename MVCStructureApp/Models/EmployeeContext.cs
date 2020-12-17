using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCStructureApp.Models
{
    public class EmployeeContext:DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Salary> Salaries { get; set; }
    }
}