using CRUD_MVC_WebApplication.Models;
using CRUD_MVC_WebApplication.Repository;
using System;
using System.Web.Mvc;

namespace CRUD_MVC_WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            System.Web.HttpContext.Current.Application.Lock();
            string findUser = System.Web.HttpContext.Current.Application["UserIdentifier"].ToString();
            System.Web.HttpContext.Current.Application.UnLock();
            return View();
        }
        // GET: Employee/GetAllEmpDetails    
        public ActionResult GetAllEmpDetails()
        {
            EmpRepository EmpRepo = new EmpRepository();
            ModelState.Clear();
            return View(EmpRepo.GetAllEmployees());
        }
        // GET: Employee/AddEmployee    
        public ActionResult AddEmployee()
        {
            return View();
        }

        // POST: Employee/AddEmployee    
        [HttpPost]
        public ActionResult AddEmployee(EmpModel Emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmpRepository EmpRepo = new EmpRepository();

                    if (EmpRepo.AddEmployee(Emp))
                    {
                        ViewBag.Message = "Employee details added successfully";
                    }
                }

                return RedirectToAction("GetAllEmpDetails");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/EditEmpDetails/5    
        public ActionResult EditEmpDetails(int id)
        {
            EmpRepository EmpRepo = new EmpRepository();
            return View(EmpRepo.GetAllEmployees().Find(Emp => Emp.Empid == id));
        }

        // POST: Employee/EditEmpDetails/5    
        [HttpPost]

        public ActionResult EditEmpDetails(int id, EmpModel obj)
        {
            try
            {
                EmpRepository EmpRepo = new EmpRepository();

                EmpRepo.UpdateEmployee(obj);
                return RedirectToAction("GetAllEmpDetails");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/DeleteEmp/5    
        public ActionResult DeleteEmp(int id)
        {
            try
            {
                string userType = System.Web.HttpContext.Current.Application["UserIdentifier"].ToString();
                EmpRepository EmpRepo = new EmpRepository();
                if (userType.Equals("Admin"))
                {
                    if (EmpRepo.DeleteEmployee(id))
                    {
                        ViewBag.AlertMsg = "Employee details deleted successfully.";
                        return RedirectToAction("GetAllEmpDetails");

                    }
                    else
                    {
                        ViewBag.AlertMsg = String.Format("Insufficient Parameter To Delete.");
                        return RedirectToAction("GetAllEmpDetails");
                    }
                }
                else
                {

                    ViewBag.AlertMsg = "User is not authorize to delete.";
                    return View();

                }


            }
            catch
            {
                return View();
            }
        }
        public ActionResult ViewEmployee(int id)
        {

            EmpRepository EmpRepo = new EmpRepository();
            ModelState.Clear();
            return View(EmpRepo.GetEmployee(id));
        }
    }
}