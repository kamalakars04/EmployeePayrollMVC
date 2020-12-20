using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using EmployeeWebAPI.Data;
using EmployeeWebAPI.Models;
using EmployeeWebAPI.Models.Common;
using EmployeeWebAPI.Repository;

namespace EmployeeWebAPI.Controllers
{
    public class EmployeesController : ApiController
    {

        EmployeeRepository repo = new EmployeeRepository();

        [HttpGet]
        [Route("api/Employee/GetEmployees")]
        public IHttpActionResult Get()
        {
            var result = repo.GetEmployees();
            if (result.Count == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Route("api/Employee/GetEmployee/{empId}")]
        public IHttpActionResult GetEmployee(int empId)
        {
            var result = repo.GetEmployee(empId);
            if (result ==  null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Route("api/Employee/GetRegisterRequestModel")]
        public RegisterRequestModel GetRegisterRequestModel()
        {
            return new RegisterRequestModel();
        }

        [Route("api/Employee")]
        // Post: api/Employee
        public IHttpActionResult Post([FromBody]RegisterRequestModel employee)
        {
            EmployeeRepository repo = new EmployeeRepository();
            var result = repo.RegisterEmployee(employee);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        [Route("api/Employee")]
        public IHttpActionResult Put([FromBody] RegisterRequestModel employee)
        {
            EmployeeRepository repo = new EmployeeRepository();
            var result = repo.Update(employee);
            if (result == 0)
            {
                return NotFound();
            }
            return Ok();
        }

        // Delete: api/Employee
        [Route("api/Employee")]
        public IHttpActionResult Delete(int id)
        {
            EmployeeRepository repo = new EmployeeRepository();
            var result = repo.DeleteEmployee(id);
            if (result == 0)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}