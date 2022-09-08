using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlankarNewDesign.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Configuration;
namespace AlankarNewDesign
{
    public partial class sub_tool : System.Web.UI.Page
    {
          DbConnection obj = new DbConnection();
          alankar_db_providerDataContext _dbContext = new alankar_db_providerDataContext();
       // AlankarDataContext _dbContext = new AlankarDataContext();
      
        string _action = string.Empty;
        string _userName = string.Empty;
        Int64 tid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                filltoolDDM();
                ddmMainToolType.Items.Insert(0, "Select");
                fillGrid();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {


            bool isExist = chketoolisExist();

            if (btnSave.Text == "SAVE")
            {
                if (isExist == true)
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Already Exists..!";
                }
                else if (isExist == false)
                {
                    tid = obj._getMaxId("ID", "MASTER_TOOL");
                    _action = "INSERT";
                    _masterToolFunctions(tid);
                    ddmMainToolType.SelectedIndex = 0;
                    txtSubType.Text = "";
                    //txtToolType.Text = "";
                    txtSequence.Text = "";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Saved Successfully..!";
                    fillGrid();

                }


            }
            else if (btnSave.Text == "UPDATE")
            {
                //bool chksubIN_Trans = _chkSubTool_in_transaction();
                //if (chksubIN_Trans == true)
                //{
                //    lblMessage.Visible = true;
                //    lblMessage.ForeColor = System.Drawing.Color.Red;
                //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                //    lblMessage.Text = lblUp.Text + " used in transaction..!";

                //}
                //else
                //{
                    bool _ExistsInUp = _UpExists(Convert.ToInt64(lblMessage.Text), txtSubType.Text);
                    if (_ExistsInUp == true)
                    {
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        lblMessage.Text = "Already exists..!";

                    }
                    else
                    {
                        tid = Convert.ToInt64(lblMessage.Text);
                        _action = "UPDATE";
                        _masterToolFunctions(tid);
                        _update_Stage_all();
                        txtSubType.Text = "";
                        ddmMainToolType.SelectedIndex = 0;
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        lblMessage.Text = "Updated Successfully..!";
                    }


                //}

                fillGrid();
                btnSave.Text = "SAVE";

            }
            ddmMainToolType.SelectedIndex = 0;
            txtSubType.Text = "";
            //txtToolType.Text = "";
            txtSequence.Text = "";

        }
        public void filltoolDDM()
        {
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT MASTER_TOOL_TYPE FROM MASTER_TOOL_TYPE WHERE STATUS = 0", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ddmMainToolType.Items.Add(rdr[0].ToString());
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

        protected void ddmMainToolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddmMainToolType.SelectedIndex == 0)
            {
                fillGrid();
            }
            else if (ddmMainToolType.SelectedIndex != 0)
            {
                _fillGridBy_ToolFilter();
            }
        }

        public void _masterToolFunctions(Int64 _tId)
        {
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_TOOL_TYPE_FUNCTIONS", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@ID", _tId);
                obj.cmd.Parameters.AddWithValue("@TOOL_TYPE", txtHsnCode.Text);
                obj.cmd.Parameters.AddWithValue("@MAIN_TYPE", ddmMainToolType.Text);
                obj.cmd.Parameters.AddWithValue("@SUB_TYPE", txtSubType.Text);
                obj.cmd.Parameters.AddWithValue("@SEQUENCE", txtSequence.Text);
                obj.cmd.Parameters.AddWithValue("@CREATED_ON", null);
                obj.cmd.Parameters.AddWithValue("@CREATED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_ON", null);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@STATUS", 0);
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


        public void fillGrid()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT ID, HSN_NO, MAIN_TYPE, SUB_TYPE, SEQUENCE FROM MASTER_TOOL WHERE STATUS = 0 ORDER BY SEQUENCE ASC", obj.con);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                GridTool.DataSource = ds;
                GridTool.DataBind();
                GridTool.UseAccessibleHeader = true;
                GridTool.HeaderRow.TableSection = TableRowSection.TableHeader;
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


        public void _fillGridBy_ToolFilter()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_FILL_TOOL_BY_MAIN_TYPE", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@MAIN_TOOL_TYPE", ddmMainToolType.Text);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                GridTool.DataSource = ds;
                GridTool.DataBind();
                GridTool.UseAccessibleHeader = true;
                GridTool.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
            catch (Exception e)
            {

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {

            lblMessage.Text = "";
            tid = Convert.ToInt64((sender as LinkButton).CommandArgument);
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_GET_MASTER_TOOL_DATA", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@ID", tid);
                //obj.cmd = new SqlCommand("Select ID, TOOL_TYPE, MAIN_TYPE, SUB_TYPE, SEQUENCE FROM MASTER_TOOL WHERE ID = '" + tid + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    lblMessage.Text = rdr[0].ToString();
                    lblMessage.Visible = false;
                    //ddmMainToolType.Text = rdr[1].ToString();
                    ddmMainToolType.Items.Clear();
                    ddmMainToolType.Items.Insert(0, "Select Main Type");
                    filltoolDDM();
                    txtHsnCode.Text = rdr[1].ToString();
                    ddmMainToolType.Text = rdr[2].ToString();
                    txtSubType.Text = rdr[3].ToString();
                    lblUp.Text = rdr[3].ToString();
                    lblUp.Visible = false;
                    txtSequence.Text = rdr[4].ToString();
                }
            }

            catch (Exception ex)
            {

            }
            finally
            {
                obj.con.Close();
            }
            btnSave.Text = "UPDATE";
            fillGrid();

        }


        public bool chketoolisExist()
        {
            bool result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("Select  TOOL_TYPE, MAIN_TYPE, SUB_TYPE FROM MASTER_TOOL WHERE MAIN_TYPE = '" + ddmMainToolType.Text + "' and SUB_TYPE = '" + txtSubType.Text + "' WHERE STATUS = 0", obj.con);
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

        protected void lnkDelete_Click(object sender, EventArgs e)
        {

            lblMessage.Text = "";
            Int64 _ID = Convert.ToInt64((sender as LinkButton).CommandArgument);
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("UPDATE MASTER_TOOL SET STATUS = 1 WHERE ID = '" + _ID + "'", obj.con);
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
            fillGrid();

        }

        private void _update_Stage_all()
        {
            string _username = Session["username"].ToString();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_CHANGE_STAGE_NAME", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@OLD_NAME", lblUp.Text);
                obj.cmd.Parameters.AddWithValue("@NEW_NAME", txtSubType.Text);
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


        private bool _chkSubTool_in_transaction()
        {
            bool _result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT MATCHTYPE FROM OC_TRANSACTIONS WHERE MATCHTYPE = '" + lblUp.Text + "'", obj.con);
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


        public bool _UpExists(Int64 _id, string _tName)
        {
            bool _result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT MASTER_TOOL.SUB_TYPE FROM MASTER_TOOL WHERE MASTER_TOOL.SUB_TYPE = '" + _tName + "' AND MASTER_TOOL.ID != '" + _id + "'", obj.con);
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("sub_tool.aspx");
        }





    }
}