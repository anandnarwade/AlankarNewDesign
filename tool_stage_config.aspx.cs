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
    public partial class tool_stage_config : System.Web.UI.Page
    {
        DbConnection obj = new DbConnection();
        Int64? _config_Id, _StageId;
        string _action = string.Empty;
        string _stage = string.Empty;
        public static Int64? _configurationId;
        protected void Page_Load(object sender, EventArgs e)
        {
            fillGrid();
            if (!IsPostBack)
            {
                FillToolType();
              
            }
        }

        public void FillToolType()
        {
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT  MASTER_TOOL_TYPE.MASTER_TOOL_TYPE FROM MASTER_TOOL_TYPE WHERE STATUS = 0 ORDER BY SEQUENCE ASC", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ddmtooltype.Items.Add(rdr[0].ToString());
                }
                ddmtooltype.Items.Insert(0, "Select Tool");
            }
            catch (Exception ex)
            {

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            //fillGrid();

        }

        public void _fillSubTooltype(string _mainTool)
        {
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_GET_DATA_TO_MAIN_SUB_TYPE", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.Add("@MAIN_TYPE", SqlDbType.NVarChar).Value = _mainTool;
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ddmtoolSubtype.Items.Add(rdr[0].ToString());
                }
                ddmtoolSubtype.Items.Insert(0, "Select sub tool");
            }
            catch (Exception ex)
            {

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            //fillGrid();
        }

        protected void ddmtooltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddmtoolSubtype.Enabled = true;
            ddmtoolSubtype.Items.Clear();
            _fillSubTooltype(ddmtooltype.Text);  
         

        }

        public void fillStages()
        {
            ddmStagetype.Items.Clear();

            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("Select distinct STAGE_TYPE FROM STAGE_MASTER", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ddmStagetype.Items.Add(rdr[0].ToString());
                }
                ddmStagetype.Items.Insert(0, "Select Stage");

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error in fillStages: " + ex.Message + "')</script>");
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }

        protected void ddmStagetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            DataListStages.Visible = true;
            fillchkStages();
            fillGrid();
        }

        public void fillchkStages()
        {

            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                obj._getConnection();
                // obj.cmd = new SqlCommand("SELECT STAGE FROM STAGE_MASTER WHERE STAGE_TYPE = '" + ddmStagetype.Text + "' AND STATUS = 0 ORDER BY SEQUENCE ASC", obj.con);
                obj.cmd = new SqlCommand("SP_FIIL_STAGE_DATALIST", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@STAGE_TYPE", ddmStagetype.Text);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                DataListStages.DataSource = ds;
                DataListStages.DataBind();
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

        protected void ddmtoolSubtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddmStagetype.Enabled = true;
            ddmStagetype.Items.Clear();
            //ddmStagetype.Items.Insert(0, "Select stage");
            fillStages();
        }


        public void _stagesToolConfigFunc()
        {
            string _userName = Session["username"].ToString();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_TOOL_TYPE_STAGES_CONFIGURATION", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.con.Open();
                obj.cmd.Parameters.AddWithValue("@ID", _config_Id);
                obj.cmd.Parameters.AddWithValue("@TOOL_TYPE", ddmtooltype.Text);
                obj.cmd.Parameters.AddWithValue("@TOOL_SUB_TYPE", ddmtoolSubtype.Text);
                obj.cmd.Parameters.AddWithValue("@STAGE_TYPE", ddmStagetype.Text);
                obj.cmd.Parameters.AddWithValue("@STATUS", 0);
                obj.cmd.Parameters.AddWithValue("@CREATED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@CREATED_ON", null);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_ON", null);
                obj.cmd.Parameters.AddWithValue("@ACTION", _action);
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

        public void _stagesFunctions()
        {
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_STAGES_FUNCTIONS", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@ID", _StageId);
                obj.cmd.Parameters.AddWithValue("@CONFIG_ID", _config_Id);
                obj.cmd.Parameters.AddWithValue("@STAGE", _stage);
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

        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataListItem items in this.DataListStages.Items)
            {
                CheckBox chk = (items.FindControl("CheckStages") as CheckBox);
                if (chkAll.Checked == true)
                {
                    chk.Checked = true;
                }
                else if (chkAll.Checked == false)
                {
                    chk.Checked = false;
                }
            }
            fillGrid();
            _changeCss();
        }


        public void _changeCss()
        {
            foreach (DataListItem items in this.DataListStages.Items)
            {
                CheckBox chk = (items.FindControl("CheckStages") as CheckBox);
                if (chk.Checked == true)
                {
                    chk.CssClass = "checkbox btn-success test";
                }
                else if (chk.Checked == false)
                {
                    chk.CssClass = "checkbox btn-danger test";
                }
            }
        }

        protected void CheckStages_CheckedChanged(object sender, EventArgs e)
        {
            _changeCss();
            fillGrid();
        }

        public void fillGrid()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT ID, TOOL_TYPE, TOOL_SUB_TYPE, STAGE_TYPE FROM TOOL_STAGES_CONFIGURATION WHERE STATUS = 0", obj.con);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                GridStages.DataSource = ds;
                GridStages.DataBind();
                GridStages.UseAccessibleHeader = true;
                GridStages.HeaderRow.TableSection = TableRowSection.TableHeader;
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


            bool _result = _isAlreadyExists();

            if (btnSave.Text == "SAVE")
            {
                if (_result == true)
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "This configuration already exits...!";
                }
                else
                {
                    if (ddmtooltype.SelectedIndex != 0 && ddmtoolSubtype.SelectedIndex != 0 && ddmStagetype.SelectedIndex != 0)
                    {
                        _config_Id = obj._getMaxId("ID", "TOOL_STAGES_CONFIGURATION");
                        lblMessage.Text = _config_Id.ToString(); ;
                        _action = "INSERT";
                        _stagesToolConfigFunc();
                        foreach (DataListItem items in this.DataListStages.Items)
                        {
                            _StageId = obj._getMaxId("ID", "STAGES");
                            _config_Id = Convert.ToInt64(lblMessage.Text);
                            CheckBox chk = (items.FindControl("CheckStages") as CheckBox);
                            if (chk.Checked == true)
                            {
                                _stage = chk.Text;
                                _action = "INSERT";
                                _stagesFunctions();
                            }
                        }
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        lblMessage.Text = "Saved Successfully...!";

                    }
                    else
                    {
                        lblMessage.Text = "";
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        lblMessage.Text = "Select Tool";

                    }


                }

                fillGrid();
            }
            else if (btnSave.Text == "UPDATE")
            {
                if (ddmtooltype.SelectedIndex != 0 && ddmtoolSubtype.SelectedIndex != 0 && ddmStagetype.SelectedIndex != 0)
                {
                    bool _updatedExists = _upExists(ddmtooltype.Text, ddmtoolSubtype.Text, ddmStagetype.Text, _configurationId);
                    if (_updatedExists == true)
                    {
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        lblMessage.Text = "Configuration already exists..!";

                    }
                    else
                    {
                        resetStages();
                        foreach (DataListItem items in this.DataListStages.Items)
                        {
                            _StageId = obj._getMaxId("ID", "STAGES");
                            _config_Id = _configurationId;
                            CheckBox chk = (items.FindControl("CheckStages") as CheckBox);
                            if (chk.Checked == true)
                            {
                                _stage = chk.Text;
                                _action = "INSERT";
                                _stagesFunctions();
                            }
                        }
                        lblMessage.Visible = true;
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        lblMessage.Text = "Updated Successfully...!";


                    }





                }
                else if (ddmtooltype.SelectedIndex == 0 || ddmtoolSubtype.SelectedIndex == 0)
                {
                    lblMessage.Text = "";
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Select Tool";
                }

            }
            ddmStagetype.SelectedIndex = 0;
            ddmtoolSubtype.Items.Clear();
            ddmtooltype.SelectedIndex = 0;
            //DataListStageUpdate.Visible = false;
            DataListStages.Visible = false;
            btnSave.Text = "SAVE";
            chkAll.Checked = false;
            fillGrid();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect("tool_stage_config.aspx");

        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {

            lblMessage.Text = "";
            Int64 _id = Convert.ToInt64((sender as LinkButton).CommandArgument);
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("UPDATE TOOL_STAGES_CONFIGURATION SET STATUS = 1 WHERE ID = '" + _id + "'", obj.con);
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
            lblMessage.ForeColor = System.Drawing.Color.Green;
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            lblMessage.Text = "Deleted Successfully";
            DataListStages.Visible = false;
            DataListStageUpdate.Visible = false;
            fillGrid();
            btnSave.Text = "SAVE";

        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {

            lblMessage.Text = "";
            Int64 configId = Convert.ToInt64((sender as LinkButton).CommandArgument);
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT ID, TOOL_TYPE, TOOL_SUB_TYPE, STAGE_TYPE FROM TOOL_STAGES_CONFIGURATION where ID = '" + configId + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                   _configurationId = Convert.ToInt64(rdr[0]);
                    //FillToolType();
                    ddmtooltype.Text = rdr[1].ToString();
                    _fillSubTooltype(ddmtooltype.Text);
                    ddmtoolSubtype.Enabled = true;
                    ddmtoolSubtype.Items.Insert(0, "Select Sub Tool");
                    ddmtoolSubtype.Text = rdr[2].ToString();
                    ddmStagetype.Items.Clear();
                    ddmStagetype.Enabled = true;
                    fillStages();

                    ddmStagetype.Text = rdr[3].ToString();

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
          
          
            DataListStages.Visible = true;
           
            DataListStageUpdate.Visible = true;
            fillchkStages();
            fillchkonEdit(configId);
            btnSave.Text = "UPDATE";
            fillGrid();

        }


        public void fillchkonEdit(Int64 configId)
        {
            lblMessage.Text = "";
            string _Stage = string.Empty;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT STAGE FROM STAGES WHERE CONFI_ID = '" + configId + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    _stage = rdr[0].ToString();
                    foreach (DataListItem items in this.DataListStages.Items)
                    {
                        CheckBox chk = (items.FindControl("CheckStages") as CheckBox);
                        string chkeName = chk.Text;
                        if (_stage == chkeName)
                        {
                            chk.Checked = true;
                        }
                    }
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
            _changeCss();
        }


        public void resetStages()
        {
            Int64? configId = _configurationId;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("DELETE FROM STAGES WHERE CONFI_ID = '" + configId + "'", obj.con);
                SqlCommand cmd2 = new SqlCommand("UPDATE TOOL_STAGES_CONFIGURATION SET TOOL_TYPE = '" + ddmtooltype.Text + "' , TOOL_SUB_TYPE = '" + ddmtoolSubtype.Text + "', STAGE_TYPE = '" + ddmStagetype.Text + "' WHERE ID = '" + configId + "'", obj.con);
                obj.con.Open();
                obj.cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
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

        public bool _isAlreadyExists()
        {
            bool _result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT [TOOL_TYPE],[TOOL_SUB_TYPE],[STAGE_TYPE] FROM TOOL_STAGES_CONFIGURATION WHERE TOOL_TYPE = '" + ddmtooltype.Text + "' AND TOOL_SUB_TYPE = '" + ddmtoolSubtype.Text + "' AND STAGE_TYPE = '" + ddmStagetype.Text + "'AND STATUS = 0", obj.con);
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


        private bool _upExists(string _toolType, string _subTool, string _stageType, Int64?  _id)
        {
            bool _result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT TOOL_TYPE, TOOL_SUB_TYPE, STAGE_TYPE FROM TOOL_STAGES_CONFIGURATION WHERE TOOL_TYPE = '" + _toolType + "' AND TOOL_SUB_TYPE = '" + _subTool + "' AND STAGE_TYPE = '" + _stageType + "' AND ID != '" + _id + "'AND STATUS = 0", obj.con);
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

    }
}