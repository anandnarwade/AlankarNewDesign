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
    public partial class tool_rm_config : System.Web.UI.Page
    {
        DbConnection obj = new DbConnection();

        Int64 _id;
        string _action = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillToolType();
                displayInGrid();
            }
        }

        protected void ddmtooltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddmtoolSubtype.Enabled = true;
            ddmtoolSubtype.Items.Clear();
            _fillSubTooltype(ddmtooltype.Text);
            displayInGrid();
        }

        protected void ddmtoolSubtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            DataListRM.Visible = true;
            fillchkRM();
            displayInGrid();
        }


        public void fillchkRM()
        {

            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT RM_NAME FROM RAW_MATERIAL WHERE STATUS = 0 ORDER BY SEQUENCE ASC", obj.con);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                DataListRM.DataSource = ds;
                DataListRM.DataBind();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            //displayInGrid();
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

        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {

            foreach (DataListItem items in this.DataListRM.Items)
            {
                CheckBox chk = (items.FindControl("CheckRM") as CheckBox);
                if (chkAll.Checked == true)
                {
                    chk.Checked = true;
                }
                else if (chkAll.Checked == false)
                {
                    chk.Checked = false;
                }

            }
            _changeCss();

        }

        protected void CheckRM_CheckedChanged(object sender, EventArgs e)
        {
            _changeCss();
        }

        public void _changeCss()
        {
            foreach (DataListItem items in this.DataListRM.Items)
            {
                CheckBox chk = (items.FindControl("CheckRM") as CheckBox);
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

        protected void lnkDelete_Click(object sender, EventArgs e)
        {

            lblMessage.Text = "";
            Int64 _id = Convert.ToInt64((sender as LinkButton).CommandArgument);
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("UPDATE TOOL_TYPE_RM_CONFIGURATION SET STATUS = 1 WHERE ID = '" + _id + "'", obj.con);
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
            DataListRM.Visible = false;
            DataListRMUpdate.Visible = false;
            btnSave.Text = "SAVE";
            lblMessage.Text = "Deleted Successfully";
            displayInGrid();

        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {

            lblMessage.Text = "";
            _id = Convert.ToInt64((sender as LinkButton).CommandArgument);


            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT ID, TOOL_TYPE, TOOL_SUB_TYPE FROM TOOL_TYPE_RM_CONFIGURATION WHERE ID ='" + _id + "' AND STATUS = 0", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    //fillToolType();



                    lblId.Text = rdr[0].ToString();

                    ddmtooltype.Text = rdr[1].ToString();
                    _fillSubTooltype(ddmtooltype.Text);
                    ddmtoolSubtype.Enabled = true;
                    ddmtoolSubtype.Text = rdr[2].ToString();


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
            DataListRM.Visible = true;
            fillchkRM();
            fillchkonEdit(_id);
            btnSave.Text = "UPDATE";
            displayInGrid();


        }


        public void _rwConfigFunctions()
        {
            string _username = Session["username"].ToString();
            try
            {
                
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_TOOL_TYPE_RM_CONFIGURATION_FUNCTIONS", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@ID", _id);
                obj.cmd.Parameters.AddWithValue("@TOOL_TYPE", ddmtooltype.Text);
                obj.cmd.Parameters.AddWithValue("@TOOL_SUB_TYPE", ddmtoolSubtype.Text);
                obj.cmd.Parameters.AddWithValue("@STATUS", 0);
                obj.cmd.Parameters.AddWithValue("@CREATED_BY", _username);
                obj.cmd.Parameters.AddWithValue("@CREATED_ON", null);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_BY", _username);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_ON", null);
                obj.cmd.Parameters.AddWithValue("@ACTION", _action);
                obj.con.Open();
                obj.cmd.ExecuteNonQuery();
                if (_action == "INSERT")
                {
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Saved Successfully..!";

                }
                else if (_action == "UPDATE")
                {
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
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


        public void _rmConfig_functions(Int64 _rm_id, Int64 _configId, string _raw_material)
        {
            string _userName = Session["username"].ToString();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_RM_CONFIG", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@ID", _rm_id);
                obj.cmd.Parameters.AddWithValue("@CONFIG_ID", _configId);
                obj.cmd.Parameters.AddWithValue("@RAW_MATERIAL", _raw_material);
                obj.cmd.Parameters.AddWithValue("@CREATED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@CREATED_ON", null);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_ON", null);
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





        public bool checkAleadyExists()
        {
            
                
            bool result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT TOOL_TYPE, TOOL_SUB_TYPE FROM TOOL_TYPE_RM_CONFIGURATION WHERE TOOL_TYPE = '" + ddmtooltype.Text + "' AND TOOL_SUB_TYPE = '" + ddmtoolSubtype.Text + "' AND STATUS = 0", obj.con);
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

        public bool checkAleadyExists2()
        {
            bool result = false;
            try
            {
                
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT TOOL_TYPE, TOOL_SUB_TYPE FROM TOOL_TYPE_RM_CONFIGURATION WHERE TOOL_TYPE = '" + ddmtooltype.Text + "' AND TOOL_SUB_TYPE = '" + ddmtoolSubtype.Text + "' AND STATUS = 0 AND ID != '" + lblId.Text + "'", obj.con);
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


       

        public void fillchkonEdit(Int64 configId)
        {
            string rm = string.Empty;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT RAW_MATERIAL FROM RAW_MATERIAL_CONFIG WHERE CONFIG_ID = '" + configId + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {

                    rm = rdr[0].ToString();
                    foreach (DataListItem items in this.DataListRM.Items)
                    {
                        CheckBox chk = (items.FindControl("CheckRM") as CheckBox);
                        string chkeName = chk.Text;
                        if (rm == chkeName)
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
            Int64 configId = Convert.ToInt64(lblId.Text);
            try
            {
                obj._getConnection();
                SqlCommand cmd1 = new SqlCommand("DELETE FROM RAW_MATERIAL_CONFIG WHERE CONFIG_ID = '" + configId + "'", obj.con);
                SqlCommand cmd2 = new SqlCommand("UPDATE TOOL_TYPE_RM_CONFIGURATION SET TOOL_TYPE = '" + ddmtooltype.Text + "' , TOOL_SUB_TYPE = '" + ddmtoolSubtype.Text + "'  WHERE ID = '" + configId + "'", obj.con);
                obj.con.Open();
                cmd1.ExecuteNonQuery();
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

       


        public void displayInGrid()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT ID, TOOL_TYPE, TOOL_SUB_TYPE FROM TOOL_TYPE_RM_CONFIGURATION WHERE STATUS = 0", obj.con);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                GridDimentionMaster.DataSource = ds;
                GridDimentionMaster.DataBind();
                GridDimentionMaster.UseAccessibleHeader = true;
                GridDimentionMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
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

            lblMessage.Text = "";
            if (ddmtoolSubtype.SelectedIndex == 0 || ddmtooltype.SelectedIndex == 0)
            {
                lblMessage.Visible = true;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                lblMessage.Text = "Please Select fields";
                displayInGrid();
                return;
            }

            if (btnSave.Text == "SAVE")
            {
                bool _isExists = checkAleadyExists();
                if (ddmtooltype.SelectedIndex == 0 && ddmtoolSubtype.SelectedIndex == 0)
                {
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Please select tools";
                    displayInGrid();
                    return;
                }
                if (_isExists == true)
                {
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Configuration Already Exists";
                    displayInGrid();
                    return;

                }
                else
                {
                    if (ddmtooltype.SelectedIndex != 0 && ddmtoolSubtype.SelectedIndex != 0)
                    {
                        _id = obj._getMaxId("ID", "TOOL_TYPE_RM_CONFIGURATION");
                        _action = "INSERT";
                        _rwConfigFunctions();
                        foreach (DataListItem items in this.DataListRM.Items)
                        {
                            Int64 _RM_ID = obj._getMaxId("ID", "RAW_MATERIAL_CONFIG");
                            CheckBox chk = (items.FindControl("CheckRM") as CheckBox);

                            if (chk.Checked == true)
                            {
                                string _rm = (items.FindControl("CheckRM") as CheckBox).Text;
                                _rmConfig_functions(_RM_ID, _id, _rm);

                            }

                            //_rmConfig_functions(_RM_ID, _id, _rm);

                        }

                    }





                }

                //_id = obj._getMaxId("ID", "TOOL_TYPE_RM_CONFIGURATION");
                //_action = "INSERT";
                //_rwConfigFunctions();
                //ddmDimention.Items.Clear();
                //ddmToolSubType.SelectedIndex = 0;
                //ddmToolType.SelectedIndex = 0;




            }
            else if (btnSave.Text == "UPDATE")
            {
                bool _upExsists = checkAleadyExists2();
                if (_upExsists == true)
                {

                }
                else
                {
                    _id = Convert.ToInt64(lblId.Text);
                    resetStages();
                    foreach (DataListItem items in this.DataListRM.Items)
                    {
                        Int64 _RM_ID = obj._getMaxId("ID", "RAW_MATERIAL_CONFIG");
                        CheckBox chk = (items.FindControl("CheckRM") as CheckBox);

                        if (chk.Checked == true)
                        {
                            string _rm = (items.FindControl("CheckRM") as CheckBox).Text;
                            _rmConfig_functions(_RM_ID, _id, _rm);
                        }

                        //_rmConfig_functions(_RM_ID, _id, _rm);

                    }
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Updated Successfully..!";
                    //_id = Convert.ToInt64(lblId.Text);
                    //_action = "UPDATE";
                    //_rwConfigFunctions();
                    //resetStages();

                }




            }
            displayInGrid();
            btnSave.Text = "SAVE";
            
            ddmtoolSubtype.SelectedIndex = 0;
            ddmtooltype.SelectedIndex = 0;
            displayInGrid();
            DataListRM.Visible = false;
            chkAll.Checked = false;



        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("tool_rm_config.aspx");
        }

        protected void chkAll_CheckedChanged1(object sender, EventArgs e)
        {
            foreach (DataListItem items in this.DataListRM.Items)
            {
                CheckBox chk = (items.FindControl("CheckRM") as CheckBox);
                if (chkAll.Checked == true)
                {
                    chk.Checked = true;
                }
                else if (chkAll.Checked == false)
                {
                    chk.Checked = false;
                }

            }
            _changeCss();
            displayInGrid();
        }


    }
}