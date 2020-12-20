using MVCStructureApp.Models;
using MVCStructureApp.Models.Common;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;


namespace MVCStructureApp.Controllers
{
    public class EmployeeController : Controller
    {
        /// <summary>
        /// Register employee action method to redirect to register page
        /// </summary>
        /// <returns></returns>
        public ActionResult RegisterEmployee()
        {
            try
            {
                // Create a connection with web api
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:63688/api/");

                    //HTTP GET
                    var responseTask = client.GetAsync("Employee/GetRegisterRequestModel");
                    responseTask.Wait();

                    var apiResponse = responseTask.Result;
                    if (apiResponse.IsSuccessStatusCode)
                    {
                        // If success return register employee view with register request model
                        var model = apiResponse.Content.ReadAsAsync<RegisterRequestModel>().Result;
                        return View(model);
                    }
                    else
                    {
                        throw new Exception("No result found");
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Register employee model to post the register form details
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RegisterEmployee(RegisterRequestModel employee)
        {
            // If model state is valid then link with web api
            // Else return Register employee view with the object
            if (ModelState.IsValid)
            {
                try
                {
                    // If the update button is pressed thrn redirect to update action method
                    if (Request.Form["Update"] != null)
                    {
                        // Temp data is used to transfer data between action methods
                        TempData["model"] = employee;
                        return RedirectToAction("Update", "Employee");
                    }

                    // Connect with webapi and post as new employee
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:63688/api/");

                        //HTTP Post
                        var responseTask = client.PostAsJsonAsync("Employee", employee);
                        responseTask.Wait();
                        var apiResponse = responseTask.Result;

                        // If the post action is success then return to index action method
                        // Else return to register page with error message
                        if (apiResponse.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            throw new Exception(apiResponse.StatusCode.ToString());
                        }
                    }
                }
                catch (Exception e)
                {
                    ViewData.Add("Fail", "Registration failed, contact administrator"+e);
                    return View("Error");
                }
            }
            return View(employee);
        }

        /// <summary>
        /// Index action method to redirect to index page 
        /// </summary>
        /// <returns></returns>
        // GET: Employee
        public ActionResult Index()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:63688/api/");

                    //HTTP GET
                    var responseTask = client.GetAsync("Employee/GetEmployees");
                    responseTask.Wait();

                    var apiResponse = responseTask.Result;
                    if (apiResponse.IsSuccessStatusCode)
                    {
                        var model = apiResponse.Content.ReadAsAsync<IList<EmployeeViewModel>>().Result;
                        return View(model);
                    }
                    else
                    {
                        throw new Exception(apiResponse.StatusCode.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Edit action method to redirect from index page to update page
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Edit(RegisterRequestModel model)
        {
            try
            {
                // Connect with web api to get the details of employee to be updated
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:63688/api/");

                    //HTTP GET
                    var responseTask = client.GetAsync("Employee/GetEmployee/"+ model.EmpId);
                    responseTask.Wait();
                    var apiResponse = responseTask.Result;

                    // If the employee exists then  read the object and direct to register employee page 
                    // with pre-filled details
                    if (apiResponse.IsSuccessStatusCode)
                    {
                        var emp = apiResponse.Content.ReadAsAsync<RegisterRequestModel>().Result;
                        ModelState.Clear();
                        return View("RegisterEmployee", emp);
                    }
                    else
                    {
                        throw new Exception(apiResponse.StatusCode.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                ViewData.Add("Fail", "Update failed, contact administrator"+e);
                return View("Error");
            }
        }

        /// <summary>
        /// Update method called from view to store the updated changes
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Update(RegisterRequestModel model = null)
        {
            try
            {
                // Fetch the updated employee details from temp data
                model = TempData["model"] as RegisterRequestModel;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:63688/api/");

                    //HTTP Put
                    var responseTask = client.PutAsJsonAsync("Employee", model);
                    responseTask.Wait();
                    var apiResponse = responseTask.Result;
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ViewData.Add("Fail", "Update failed, contact administrator"+e);
                return View("Error");
            }
        }

        /// <summary>
        /// Action method delete used to delete a entry from index from page
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult delete(int empid)
        {
            try
            {
                // Establish a connection with web api
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:63688/api/");

                    // Send a get request to get the employee details of given empid
                    //HTTP GET
                    var responseTask = client.GetAsync("Employee/GetEmployee/" + empid);
                    responseTask.Wait();
                    var apiResponse = responseTask.Result;

                    // If employee exists then return to delete view for confirmation
                    // Else throw an exception
                    if (apiResponse.IsSuccessStatusCode)
                    {
                        var emp = apiResponse.Content.ReadAsAsync<RegisterRequestModel>().Result;
                        return View(emp);
                    }
                    else
                    {
                        throw new Exception(apiResponse.StatusCode.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                ViewData.Add("Fail", "Update failed, contact administrator"+e);
                return View("Error");
            }
        }

        /// <summary>
        /// Delete employee after confirmation from user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteEmployee(int empId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:63688/api/");

                    //HTTP Delete
                    var responseTask = client.DeleteAsync("Employee?id="+ empId);
                    responseTask.Wait();
                    var apiResponse = responseTask.Result;

                    // If delete successful then return to index page
                    // Else return to error page
                    if (apiResponse.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        throw new Exception(apiResponse.StatusCode.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                ViewData.Add("Fail", "Update failed, contact administrator"+e);
                return View("Error");
            }
        }
    }
}