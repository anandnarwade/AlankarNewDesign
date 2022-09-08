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
    public partial class employees : System.Web.UI.Page
    {
        DbConnection obj = new DbConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _getEmpDetails();
            }

        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {

            lblMessage.Text = "";
            string EMP_CODE = (sender as LinkButton).CommandArgument;

            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("UPDATE EMP_MASTER SET emp_status = 1 WHERE EMP_CODE = '" + EMP_CODE + "'", obj.con);
                obj.con.Open();
                obj.cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            _getEmpDetails();
            lblMessage.Visible = true;
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
          
            lblMessage.Text = "Deleted Successfully";

            //GridEMP.UseAccessibleHeader = true;
            //GridEMP.HeaderRow.TableSection = TableRowSection.TableHeader;
           

        }

        public void _getEmpDetails()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT EMP_CODE, [EMP_FNAME]+' '+[EMP_LNAME] as 'Employee Name', SEX, MARITIAL_STATUS, DOB, DOJ, DOL from EMP_MASTER  where emp_status = 0", obj.con);

                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                GridEMP.DataSource = ds;
                GridEMP.DataBind();
                GridEMP.UseAccessibleHeader = true;
                GridEMP.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception ex)
            {
                //Response.Write("<script>alert('"+ ex.Message+"')</script>");
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }

        protected void btnAddnew_Click(object sender, EventArgs e)
        {
            Response.Redirect("emp_master.aspx");
        }
    }
}