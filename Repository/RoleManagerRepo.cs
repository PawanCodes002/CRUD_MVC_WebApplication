using CRUD_MVC_WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRUD_MVC_WebApplication.Repository
{
    public class RoleManagerRepo
    {
        private SqlConnection con;

        //To Handle connection related activities
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            con = new SqlConnection(constr);
        }

        public bool AddRole(RolesModel role)
        {
            connection();
            SqlCommand com = new SqlCommand("AddNewRole", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@RoleName", role.RoleName);
            com.Parameters.AddWithValue("@Controller", role.ControllerName);
            com.Parameters.AddWithValue("@CreatedBy", role.CreatedBy);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<RolesModel> GetAllRoles()
        {
            connection();
            List < RolesModel> RoleList = new List<RolesModel>();

            SqlCommand com = new SqlCommand("GetRoles", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow
            foreach (DataRow dr in dt.Rows)
            {
                RoleList.Add(

                    new RolesModel
                    {
                        RoleId = Convert.ToInt32(dr["RoleID"]),
                        RoleName = Convert.ToString(dr["RoleName"]),
                        ControllerName = Convert.ToString(dr["Controller"]),
                        CreationDate = Convert.ToString(dr["CreationDate"]),
                        CreatedBy=Convert.ToString(dr["CreatedBy"]),
                        ModificationDate=Convert.ToString(dr["ModificationDate"]),
                        ModifiedBy=Convert.ToString(dr["ModifiedBy"])
                    }
                    );
            }

            return RoleList;
        }

        public RolesModel GetRoleById(int RoleId)
        {
            connection();
            RolesModel RoleView = new RolesModel();

            SqlCommand com = new SqlCommand("GetRoleById", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            com.Parameters.AddWithValue("RoleId", RoleId);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();

            RoleView.RoleId = Convert.ToInt32(dt.Rows[0]["RoleID"]);
            RoleView.RoleName = Convert.ToString(dt.Rows[0]["RoleName"]);
            RoleView.ControllerName = Convert.ToString(dt.Rows[0]["Controller"]);
            RoleView.CreationDate = Convert.ToString(dt.Rows[0]["CreationDate"]);
            RoleView.CreatedBy = Convert.ToString(dt.Rows[0]["CreatedBy"]);
            RoleView.ModificationDate = Convert.ToString(dt.Rows[0]["ModificationDate"]);
            RoleView.ModifiedBy = Convert.ToString(dt.Rows[0]["ModifiedBy"]);
            return RoleView;
        }

        public bool ModifyRole(RolesModel role)
        {
            connection();
            SqlCommand com = new SqlCommand("UpdateExistingRole", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@RoleName", role.RoleName);
            com.Parameters.AddWithValue("@Controller", role.ControllerName);
            com.Parameters.AddWithValue("@ModifiedBy", role.ModifiedBy);
            com.Parameters.AddWithValue("@RoleId", role.RoleId);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
   }