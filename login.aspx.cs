using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace AlankarNewDesign
{
    public partial class login : System.Web.UI.Page
    {
        DbConnection obj = new DbConnection();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            bool isExists;
            string userName = string.Empty;
            string Password = string.Empty;
            userName = txtUserName.Text;
            Password = txtPassword.Text;
            isExists = userLogin(userName, Password);
            if (isExists == true)
            {
                Session["username"] = userName;
                Session["password"] = Password;
                if (Session["username"] != null && Session["password"] != null)
                {
                    Response.Redirect("WebForm1.aspx");
                }
            }
            else if (isExists == false)
            {
                lblMessage.CssClass = "alert-danger";
                lblMessage.Text = "Invalid Login attemts";
            }

            /*Test Demo Login*/
            if (txtUserName.Text == "alankar" && txtPassword.Text == "alankar")
            {
                Session["username"] = userName;
                Session["password"] = Password;
                if (Session["username"] != null && Session["password"] != null)
                {
                    Response.Redirect("WebForm1.aspx");
                }
            }
        }

        public bool userLogin(string email, string password)
        {
            SqlDataReader rdr;
            bool result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_USER_LOGIN", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = txtUserName.Text;
                obj.cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = txtPassword.Text;
                obj.con.Open();
                rdr = obj.cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {

                result = false;
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            return result;
        }


    }
}