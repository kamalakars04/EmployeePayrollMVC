using MVCStructureApp.Models;
using MVCStructureApp.Models.Common;
using MVCStructureApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCStructureApp.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeRepository employeeRepository = new EmployeeRepository();

        /// <summary>
        /// Register employee action method to redirect to register page
        /// </summary>
        /// <returns></returns>
        public ActionResult RegisterEmployee()
        {
            return View(new RegisterRequestModel());
        }

        /// <summary>
        /// Index action method to redirect to index page 
        /// </summary>
        /// <returns></returns>
        // GET: Employee
        public ActionResult Index()
        {
            List<EmployeeViewModel> list = employeeRepository.GetEmployees();
            return View(list);
        }

        /// <summary>
        /// Register employee model to post the register form details
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RegisterEmployee(RegisterRequestModel employee)
        {
            if (Request.Form["Update"] != null)
                return RedirectToAction("Update",employee);
            bool result = false;
            if (ModelState.IsValid)
            {
                result = employeeRepository.RegisterEmployee(employee);
            }
            ModelState.Clear();
            if (result == true)
            {
                return RedirectToAction("Index");
            }
            return View(employee);
        }
        
        /// <summary>
        /// Edit action method to redirect from index page to update page
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Edit(RegisterRequestModel model)
        {
            RegisterRequestModel emp = employeeRepository.GetEmployee(model.EmpId);
            
            ModelState.Clear();
            return View("RegisterEmployee",emp);
        }

        /// <summary>
        /// Action method delete used to delete a entry from index from page
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult delete(Employee model)
        {
            RegisterRequestModel emp = employeeRepository.GetEmployee(model.EmpId);
            return View(emp);
        }

        /// <summary>
        /// Delete employee after confirmation from user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteEmployee(Employee model)
        {
            int result = employeeRepository.DeleteEmployee(model.EmpId);
            if (result != 0)
                return RedirectToAction("Index");
            else
                return View("Delete",result);
        }

        /// <summary>
        /// Update method called from view to store the updated changes
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Update(RegisterRequestModel model)
        {
           int data=employeeRepository.Update(model);
            return RedirectToAction("Index");
        }
    }
}