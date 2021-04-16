using CRUD_MVC_WebApplication.Models;
using CRUD_MVC_WebApplication.Repository;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace CRUD_MVC_WebApplication.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(UserModel user)
        {
            UserAuthentication UA = new UserAuthentication();
            bool check = UA.GetLogin(user);
            if (check)
            {
                System.Web.HttpContext.Current.Application.Lock();
                System.Web.HttpContext.Current.Application["UserIdentifier"] = user.UserType.ToString();
                System.Web.HttpContext.Current.Application.UnLock();
                return RedirectToAction("GetAllEmpDetails", "Home");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Register()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            SqlConnection _con = new SqlConnection(constr);
            SqlDataAdapter _da = new SqlDataAdapter("GetControllers", constr);
            DataTable _dt = new DataTable();
            _da.Fill(_dt);
            ViewBag.CityList = ToSelectList(_dt, "ControllerName", "ControllerName");
            return View();
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
        [HttpPost]
        public ActionResult Register(UserModel usr) {
            return View();
        }
    }
}