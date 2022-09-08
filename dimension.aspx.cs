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
    public partial class dimension : System.Web.UI.Page
    {
        Int64 _dimentionId;
        string _action = string.Empty;
       
        string _userName = string.Empty;
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        DbConnection obj = new DbConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getDimentions();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
           

            if (btnSave.Text == "SAVE")
            {
                bool _isExists = false;

                _isExists = obj.AlreadyExists("DIMENTION", "DIMENTION_TYPE_MASTER", txtDimention.Text);
                if (_isExists == true)
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Already Exists..!";

                }
                else if (_isExists == false)
                {
                    _dimentionId = obj._getMaxId("ID", "DIMENTION_TYPE_MASTER");
                    _action = "INSERT";

                    _dimentionFunctions();


                }
                txtDimention.Text = "";
                txtUnit.Text = "";
                txtSequence.Text = "";
                getDimentions();

            }
            else if (btnSave.Text == "UPDATE")
            {


                bool _tranExists = _chkDimension_in_transaction(txtDimention.Text);
                if (_tranExists == true)
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = lbldimUp.Text + " already exists in transaction.";

                }
                else if (_tranExists == false)
                {
                    _update_dimension_all();
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    _dimentionId = Convert.ToInt64(lblMessage.Text);
                    _action = "UPDATE";
                    _dimentionFunctions();
                    lblMessage.Text = "Updated Successfully...";

                }

                txtDimention.Text = "";
                txtUnit.Text = "";
                txtSequence.Text = "";
                btnSave.Text = "SAVE";
                getDimentions();


            }

        }



        public void _dimentionFunctions()
        {
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_DIMENTION_TYPE_MASTER", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@ID", _dimentionId);
                obj.cmd.Parameters.AddWithValue("@DIMENTION", txtDimention.Text);
                obj.cmd.Parameters.AddWithValue("@UNIT", txtUnit.Text);
                obj.cmd.Parameters.AddWithValue("@SEQUENCE", txtSequence.Text);
                obj.cmd.Parameters.AddWithValue("@CREATED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@CREATED_ON", null);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_ON", null);
                obj.cmd.Parameters.AddWithValue("@STATUS", "0");
                obj.cmd.Parameters.AddWithValue("@ACTION", _action);
                obj.con.Open();
                obj.cmd.ExecuteNonQuery();
                if (_action == "INSERT")
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Saved Successfully...";
                }
                else if (_action == "UPDATE")
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Updated Successfully...!";
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



        public void getDimentions()
        {
            SqlDataAdapter DA = new SqlDataAdapter();
            DataSet ds = new DataSet();
            obj = new DbConnection();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("Select ID, DIMENTION, UNIT, SEQUENCE from DIMENTION_TYPE_MASTER WHERE STATUS = 0 ORDER BY SEQUENCE ASC", obj.con);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                GridDimention.DataSource = ds;
                GridDimention.DataBind();
                GridDimention.UseAccessibleHeader = true;
                GridDimention.HeaderRow.TableSection = TableRowSection.TableHeader;
                obj.con.Close();
            }
            catch (Exception ex)
            {
                //Response.Write("<script>alert('Error in getDimentions() : " + ex.Message + "');</script>");
            }
            finally
            {
                //obj.con.Close();
                //obj.cmd.Dispose();
            }
        }



        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            _dimentionId = Convert.ToInt64((sender as LinkButton).CommandArgument);
            lbldimUp.Text = _dimentionId.ToString();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("Select ID, DIMENTION, UNIT, SEQUENCE from DIMENTION_TYPE_MASTER where ID = '" + _dimentionId + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    lblMessage.Text = rdr[0].ToString();
                    txtDimention.Text = rdr[1].ToString();
                    //lbldimUp.Text = rdr[1].ToString();
                    txtUnit.Text = rdr[2].ToString();
                    txtSequence.Text = rdr[3].ToString();
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
            btnSave.Text = "UPDATE";

            getDimentions();

        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            Int64 _id = Convert.ToInt64((sender as LinkButton).CommandArgument);

            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("UPDATE DIMENTION_TYPE_MASTER SET STATUS = 1 WHERE ID = '" + _id + "'", obj.con);
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
            getDimentions();
            lblMessage.Visible = true;
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Deleted Successfully..";

        }



        private bool _chkDimension_in_transaction(string _dimension)
        {
            bool _result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT DIMENTION_TYPE_MASTER.DIMENTION FROM DIMENTION_TYPE_MASTER.DIMENTION = '"+txtDimention.Text+"' AND DIMENTION_TYPE_MASTER WHERE DIMENTION_TYPE_MASTER.ID != '" + lbldimUp.Text + "'AND STATUS = 0", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                _result = false;
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            return _result;
        }

        private void _update_dimension_all()
        {
            string _username = Session["username"].ToString();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_CHANGE_DIMENSION_NAME", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@OLD_NAME", lbldimUp.Text);
                obj.cmd.Parameters.AddWithValue("@NEW_NAME", txtDimention.Text);
                obj.cmd.Parameters.AddWithValue("@CREATED_BY", _username);
                obj.cmd.Parameters.AddWithValue("@CREATED_ON", null);
                obj.con.Open();
                obj.cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("dimension.aspx");
        }
    }
}