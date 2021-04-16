using CRUD_MVC_WebApplication.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CRUD_MVC_WebApplication.Repository
{
    public class UserAuthentication
    {
        private SqlConnection con;

        //To Handle connection related activities
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            con = new SqlConnection(constr);
        }

        public bool GetLogin(UserModel user)
        {
            try
            {

                connection();
                UserModel usr = new UserModel();

                SqlCommand com = new SqlCommand("GetUser", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@emailID", user.Email.ToString());
                com.Parameters.AddWithValue("@pass", user.Password.ToString());
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();

                con.Open();
                da.Fill(dt);
                con.Close();
                //Bind EmpModel generic list using dataRow
                usr.Email = Convert.ToString(dt.Rows[0]["EmailID"]);
                usr.Password = Convert.ToString(dt.Rows[0]["UserPassword"]);
                usr.UserType = Convert.ToString(dt.Rows[0]["TypeID"]);
                if (usr.Password.Equals(user.Password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return false;
        }
        public bool GetRegistered(UserModel usr)
        {
            return true;
        }
    }
}