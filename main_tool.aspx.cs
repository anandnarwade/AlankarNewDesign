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
    public partial class main_tool : System.Web.UI.Page
    {
        DbConnection obj = new DbConnection();
        Int64 _id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillGrid();
            }
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {

            lblMessage.Text = "";
            Int64 _ID = Convert.ToInt64((sender as LinkButton).CommandArgument);
            _getData(_ID);
            fillGrid();
            btnSave.Text = "UPDATE";

        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {

            lblMessage.Text = "";
            Int64 _id = Convert.ToInt64((sender as LinkButton).CommandArgument);
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("UPDATE MASTER_TOOL_TYPE SET STATUS = 1 WHERE ID = '" + _id + "'", obj.con);
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
            lblMessage.Visible = true;
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            lblMessage.Text = "Deleted Successfully";
            fillGrid();

        }

        public void fillGrid()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT ID, MASTER_TOOL_TYPE, SEQUENCE FROM MASTER_TOOL_TYPE WHERE STATUS = 0 ORDER BY SEQUENCE ASC", obj.con);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                GridStageType.DataSource = ds;
                GridStageType.DataBind();
                GridStageType.UseAccessibleHeader = true;
                GridStageType.HeaderRow.TableSection = TableRowSection.TableHeader;

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

        protected void btnSave_Click(object sender, EventArgs e)
        {

            bool _isExits = _isAlreadyExits();
            if (btnSave.Text == "SAVE")
            {
                if (_isExits == true)
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Already Exits";
                    fillGrid();
                }
                else if (_isExits == false)
                {
                    _id = obj._getMaxId("ID", "MASTER_TOOL_TYPE");
                    _stageTypeFunctions(_id, "INSERT");
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Saved Successfully..!";

                }

            }
            else if (btnSave.Text == "UPDATE")
            {
                bool _existsInTrans = _chkTool_in_transaction();
                if (_existsInTrans == true)
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = txtMainToolType.Text + " Used in transaction..!";
                }
                else
                {
                    bool _upExistschk = _UpExists(Convert.ToInt64(lblId.Text), txtMainToolType.Text);
                    if (_upExistschk == true)
                    {
                        lblMessage.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        lblMessage.Text = "Master tool already exists..!";
                    }
                    else
                    {


                        _id = Convert.ToInt64(lblId.Text);
                        _stageTypeFunctions(_id, "UPDATE");
                        _changeToolName();
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        lblMessage.Text = "Updated Successfully..!";




                    }
                }


            }
            fillGrid();
            txtMainToolType.Text = "";
            txtSequence.Text = "";
            btnSave.Text = "SAVE";

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("main_tool.aspx");
        }

        public void _stageTypeFunctions(Int64 _id, string _action)
        {
            string _username = Session["username"].ToString();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_MASTER_TOOL_TYPE_FUNCTIONS", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@ID", _id);
                obj.cmd.Parameters.AddWithValue("@MASTER_TOOL_TYPE", txtMainToolType.Text);
                obj.cmd.Parameters.AddWithValue("@SEQUENCE", txtSequence.Text);
                obj.cmd.Parameters.AddWithValue("@STATUS", 0);
                obj.cmd.Parameters.AddWithValue("@CREATED_BY", _username);
                obj.cmd.Parameters.AddWithValue("@CREATED_ON", null);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_BY", _username);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_ON", null);
                obj.cmd.Parameters.AddWithValue("@ACTION", _action);
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
        }

        public bool _isAlreadyExits()
        {
            bool _result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT MASTER_TOOL_TYPE FROM MASTER_TOOL_TYPE WHERE MASTER_TOOL_TYPE = '" + txtMainToolType.Text + "' AND STATUS = 0", obj.con);
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



        public void _getData(Int64 _id)
        {
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT ID, MASTER_TOOL_TYPE, SEQUENCE FROM MASTER_TOOL_TYPE WHERE ID = '" + _id + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    lblId.Text = rdr[0].ToString();
                    txtMainToolType.Text = rdr[1].ToString();
                    Label1.Text = rdr[1].ToString();
                    txtSequence.Text = rdr[2].ToString();
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


        public bool _UpExists(Int64 _id, string _tName)
        {
            bool _result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT [MASTER_TOOL_TYPE] FROM MASTER_TOOL_TYPE where MASTER_TOOL_TYPE = '" + _tName + "' AND ID != '" + _id + "'", obj.con);
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
                obj.cmd.Dispose();
                obj.con.Close();
            }
            return _result;
        }

        private bool _chkTool_in_transaction()
        {
            bool _result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT TOOLTYPE FROM OC_TRANSACTIONS WHERE TOOLTYPE = '" + Label1.Text + "'", obj.con);
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


        public void _changeToolName()
        {
            string username = Session["username"].ToString();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_CHANGE_TOOL_NAME", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@OLD_NAME", Label1.Text);
                obj.cmd.Parameters.AddWithValue("@NEW_NAME", txtMainToolType.Text);
                obj.cmd.Parameters.AddWithValue("@CREATED_BY", username);
                obj.cmd.Parameters.AddWithValue("@CREATED_ON", null);
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
        }



    }
}