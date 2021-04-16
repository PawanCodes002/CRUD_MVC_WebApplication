using CRUD_MVC_WebApplication.Models;
using CRUD_MVC_WebApplication.Repository;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace CRUD_MVC_WebApplication.Controllers
{
    public class RolesManagerController : Controller
    {
        // GET: RolesManager
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddRoleManger()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            SqlConnection _con = new SqlConnection(constr);
            SqlDataAdapter _da = new SqlDataAdapter("GetControllers", constr);
            DataTable _dt = new DataTable();
            _da.Fill(_dt);
            ViewBag.CityList = ToSelectList(_dt, "ControllerName", "ControllerName");
            return View();
        }
        [HttpPost]
        public ActionResult AddRoleManger(RolesModel role)
        {
            RoleManagerRepo r = new RoleManagerRepo();
            r.AddRole(role);
            return RedirectToAction("ShowRoleManagers", "RolesManager");
        }
        public ActionResult ShowRoleManagers()
        {
            RoleManagerRepo r = new RoleManagerRepo();
            ModelState.Clear();
            return View(r.GetAllRoles());
        }

        [NonAction]
        public SelectList ToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }
            return new SelectList(list, "Value", "Text");
        }
        [HttpGet]
        public ActionResult EditExistingRole(int id)
        {
            RoleManagerRepo RoleRepo = new RoleManagerRepo();
            return View(RoleRepo.GetAllRoles().Find(role => role.RoleId == id));
        }
        [HttpPost]
        public ActionResult EditExistingRole(int id,RolesModel role)
        {
            RoleManagerRepo rm = new RoleManagerRepo();
            if (rm.ModifyRole(role))
            {
                return RedirectToAction("ShowRoleManagers", "RolesManager");
            }
            else
            {
                return View();
            }
            
        }
    }
}