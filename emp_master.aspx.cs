using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AlankarNewDesign.DAL;
namespace AlankarNewDesign
{
    public partial class emp_master : System.Web.UI.Page
    {
        DbConnection obj = new DbConnection();
        int _status;
        string _userName;
        string _action = string.Empty;
        string _createdOn = string.Empty;
        string _modifiedOn = string.Empty;
        string _modifiedBy = string.Empty;
        Int64 _empCode;
        Int64 EmpCode;

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                string x = Request.QueryString["Action"];
                _empCode = Convert.ToInt64(Request.QueryString["EMP_CODE"]);
                string EmpCode = _empCode.ToString();
                if (x == "UPDATE")
                {

                    btnSave.Text = "UPDATE";
                    _getEmployeeData(EmpCode);

                }
            }

        }


        public void _EmpFunctions(Int64 EmpCode)
        {

            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_EMP_MASTER_FUNCTIONS", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.Add("@EMP_CODEOLD", SqlDbType.NVarChar).Value = null;
                obj.cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = EmpCode.ToString();
                obj.cmd.Parameters.Add("@EMP_FNAME", SqlDbType.NVarChar).Value = txtFirstName.Text;
                obj.cmd.Parameters.Add("@EMP_MNAME", SqlDbType.NVarChar).Value = txtMiddleName.Text;
                obj.cmd.Parameters.Add("@EMP_LNAME", SqlDbType.NVarChar).Value = txtLastName.Text;
                obj.cmd.Parameters.Add("@ADDRESS", SqlDbType.NVarChar).Value = txtAddress.Text;
                obj.cmd.Parameters.Add("@LOCATION", SqlDbType.NVarChar).Value = txtLocation.Text;
                obj.cmd.Parameters.Add("@SEX", SqlDbType.NVarChar).Value = DDMGender.Text;
                obj.cmd.Parameters.Add("@CITY", SqlDbType.NVarChar).Value = txtCity.Text;
                obj.cmd.Parameters.Add("@DISTRICT", SqlDbType.NVarChar).Value = txtDistrict.Text;
                obj.cmd.Parameters.Add("@MARITIAL_STATUS", SqlDbType.NVarChar).Value = DDMmaritalStatus.Text;
                obj.cmd.Parameters.Add("@DOB", SqlDbType.NVarChar).Value = txtDOB.Text;
                obj.cmd.Parameters.Add("@DOJ", SqlDbType.NVarChar).Value = txtDOJ.Text;
                obj.cmd.Parameters.Add("@DOL", SqlDbType.NVarChar).Value = txtDOL.Text;
                obj.cmd.Parameters.Add("@emp_pwd", SqlDbType.NVarChar).Value = txtPassword.Text;
                obj.cmd.Parameters.Add("@emp_status", SqlDbType.Int).Value = _status;
                obj.cmd.Parameters.Add("@CREATED_ON", SqlDbType.DateTime).Value = null;
                obj.cmd.Parameters.Add("@CREATED_BY", SqlDbType.NVarChar).Value = _userName;
                obj.cmd.Parameters.Add("@MODIFIED_ON", SqlDbType.DateTime).Value = null;
                obj.cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.NVarChar).Value = _modifiedBy;
                obj.cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtEmail.Text;
                obj.cmd.Parameters.Add("@ACTION", SqlDbType.NVarChar).Value = _action;
                obj.con.Open();
                obj.cmd.ExecuteNonQuery();
                if (_action == "INSERT")
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Saved Successfully..";
                }
                else if (_action == "UPDATE")
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Updated Successfully..";
                }


            }
            catch (Exception ex)
            {

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }


        public void _getEmployeeData(string empId)
        {

            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_GET_EMP_MASTER_DATA", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.Add("@EMP_CODE", SqlDbType.NVarChar).Value = empId;
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    txtFirstName.Text = rdr[0].ToString();
                    txtMiddleName.Text = rdr[1].ToString();
                    txtLastName.Text = rdr[2].ToString();
                    txtAddress.Text = rdr[3].ToString();
                    
                    txtLocation.Text = rdr[4].ToString();
                    txtCity.Text = rdr[5].ToString();
                    txtDistrict.Text = rdr[6].ToString();
                    DDMGender.Text = rdr[7].ToString();
                    DDMmaritalStatus.Text = rdr[8].ToString();
                    txtDOB.Text = rdr[9].ToString();
                    txtDOJ.Text = rdr[10].ToString();
                    txtDOL.Text = rdr[11].ToString();
                    txtPassword.Text = rdr[12].ToString();
                    _status = Convert.ToInt32(rdr[13]);
                    _createdOn = rdr[14].ToString();
                    _userName = rdr[15].ToString();
                    _modifiedOn = rdr[16].ToString();
                    _modifiedBy = rdr[17].ToString();
                    txtEmail.Text = rdr[18].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }

        public bool EmailExists()
        {
            bool result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("select Email from EMP_MASTER where Email = '" + txtEmail.Text + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
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

        protected void btnSave_Click(object sender, EventArgs e)
        {

            lblMessage.Text = "";
            if (btnSave.Text == "SAVE")
            {
                //bool isEmailexistts = EmailExists();
                bool isEmailexistts = obj.AlreadyExists("Email", "EMP_MASTER", txtEmail.Text);
                if (isEmailexistts == true)
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Email Already Exits!";

                }
                else if (isEmailexistts == false)
                {
                    EmpCode = obj._getMaxId("EMP_CODE", "EMP_MASTER");
                    _createdOn = DateTime.Now.ToString();
                    _userName = Session["username"].ToString();
                    _action = "INSERT";

                    _EmpFunctions(EmpCode);
                    Response.Redirect("employees.aspx");
                }

            }
            else if (btnSave.Text == "UPDATE")
            {
                EmpCode = Convert.ToInt64(Request.QueryString["EMP_CODE"]);
                string empcode = EmpCode.ToString();
                //_getEmployeeData(empcode);

                _userName = Session["username"].ToString();
                _action = "UPDATE";

                _EmpFunctions(EmpCode);
                Response.Redirect("employees.aspx");

            }

         

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("employees.aspx");
        }



    }
}