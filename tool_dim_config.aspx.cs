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
    public partial class tool_dim_config : System.Web.UI.Page
    {
        DbConnection obj = new DbConnection();
        Int64 _id;
        string _action = string.Empty;
        string _userName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillToolType();

                //fillSubType();
                fillGrid();

                if (Label1.Text != "")
                {
                    fillchkonEdit(Convert.ToInt64(Label1.Text));
                }
            }

        }

        public void fillToolType()
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
            }
            catch (Exception ex)
            {

            }
            finally
            {
                obj.con.Close();
            }
        }

        public void fillSubType()
        {
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_FIIL_SUB_TOOL_TYPE", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@MAIN_TOOL_TYPE", ddmtooltype.Text);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {

                    ddmtoolSubtype.Items.Add(rdr[0].ToString());
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



        public void _dimentionFunctions(Int64 _config_Id)
        {
            _userName = Session["username"].ToString();
            try
            {
                
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_TOOL_DIMENSION_CONFIGURATION_FUNCTIONS", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@ID", _config_Id);
                obj.cmd.Parameters.AddWithValue("@TOOL_TYPE", ddmtooltype.Text);
                obj.cmd.Parameters.AddWithValue("@TOOL_SUB_TYPE", ddmtoolSubtype.Text);
                obj.cmd.Parameters.AddWithValue("@STATUS", 0);
                obj.cmd.Parameters.AddWithValue("@CREATED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@CREATED_ON", null);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_ON", null);
                obj.cmd.Parameters.AddWithValue("@SEQUENCE", null);


                obj.cmd.Parameters.AddWithValue("@ACTION", _action);
                obj.con.Open();
                obj.cmd.ExecuteNonQuery();
                if (_action == "INSERT")
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Saved Successfully...!";
                }
                else if (_action == "UPDATE")
                {
                    lblMessage.Visible = true;
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


        public void fillGrid()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT ID, TOOL_TYPE, TOOL_SUB_TYPE FROM TOOL_DIMENSION_CONFIGURATION WHERE STATUS = 0", obj.con);
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


        protected void lnkDelete_Click(object sender, EventArgs e)
        {

            lblMessage.Text = "";
            Int64 _id = Convert.ToInt64((sender as LinkButton).CommandArgument);
            Label1.Text = _id.ToString();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("UPDATE TOOL_DIMENSION_CONFIGURATION SET STATUS = 1 WHERE ID = '" + _id + "'", obj.con);
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
            lblMessage.Text = "Deleted successfully..!";
            fillGrid();

        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {

            
            lblMessage.Text = "";
            ddmtoolSubtype.Items.Clear();
            _id = Convert.ToInt64((sender as LinkButton).CommandArgument);
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT ID, TOOL_TYPE, TOOL_SUB_TYPE FROM TOOL_DIMENSION_CONFIGURATION WHERE STATUS = 0 AND ID = '" + _id + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    lblId.Text = rdr[0].ToString();

                    ddmtooltype.Text = rdr[1].ToString();
                    fillSubType();
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
            btnSave.Text = "UPDATE";
            //DataListDimension.Visible = true;
            fillchkRM2();
            //fillchkonEdit(_id);
            //DataLIST.Visible = true;
            DataListDimension.Visible = false;
            DataLIST.Visible = false;
            fillUpdateDlist1(_id);
            //DataListDimension.Visible = false;
            fillGrid();

        }

        public bool checkIsExists()
        {
            bool result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT TOOL_TYPE, TOOL_SUB_TYPE FROM TOOL_DIMENSION_CONFIGURATION WHERE TOOL_TYPE = '" + ddmtooltype.Text + "' AND TOOL_SUB_TYPE = '" + ddmtoolSubtype.Text + "' AND STATUS = 0", obj.con);
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

        public bool checkIsExists2()
        {
            bool result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT TOOL_TYPE, TOOL_SUB_TYPE FROM TOOL_DIMENSION_CONFIGURATION WHERE TOOL_TYPE = '" + ddmtooltype.Text + "' AND TOOL_SUB_TYPE = '" + ddmtoolSubtype.Text + "' AND STATUS = 0 AND ID != '" + lblId.Text + "'", obj.con);
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

        public void fillchkRM()
        {
            DataListDimension.Visible = true;
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT DIMENTION FROM DIMENTION_TYPE_MASTER WHERE STATUS = 0 ORDER BY SEQUENCE ASC", obj.con);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                DataListDimension.DataSource = ds;
                DataListDimension.DataBind();
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

        public void fillchkRM2()
        {

            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT ID, DIMENTION FROM DIMENTION_TYPE_MASTER WHERE STATUS = 0 ORDER BY SEQUENCE ASC", obj.con);
                //obj.cmd = new SqlCommand("SELECT ID, DIMENTION, '' as SEQUENCE FROM DIMENTION_TYPE_MASTER WHERE STATUS = 0 ORDER BY SEQUENCE ASC", obj.con);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                DataLIST.DataSource = ds;
                DataLIST.DataBind();
                //DataListDimension.DataSource = ds;
                //DataListDimension.DataBind();
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





        public void fillUpdateDlist1(Int64 _config_id)
        {

            //SqlDataAdapter da = new SqlDataAdapter();
            DataLIST.Visible = true;
            DataSet ds = new DataSet();
            Int64 id; string _dimension;
            string _Sequence;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT DIMENSION_CONFIGURATION.ID, DIMENTION_TYPE_MASTER.DIMENTION, DIMENTION_TYPE_MASTER.SEQUENCE FROM DIMENSION_CONFIGURATION INNER JOIN DIMENTION_TYPE_MASTER ON DIMENSION_CONFIGURATION.DIMENSION = DIMENTION_TYPE_MASTER.DIMENTION WHERE DIMENSION_CONFIGURATION.CONFIG_ID = '" + _config_id + "' AND DIMENTION_TYPE_MASTER.STATUS = 0 ORDER BY DIMENTION_TYPE_MASTER.SEQUENCE ASC", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    id = Convert.ToInt64(rdr[0]);
                    _dimension = rdr[1].ToString();
                    _Sequence = rdr[2].ToString();
                    foreach (DataListItem items in this.DataLIST.Items)
                    {
                        CheckBox chk = (items.FindControl("CheckRM2") as CheckBox);
                        TextBox txt = (items.FindControl("txtSequence") as TextBox);
                        Label lbl = (items.FindControl("lblId") as Label);
                        string _dimension1 = chk.Text;
                        if (_dimension == _dimension1)
                        {

                            chk.Checked = true;
                            //txt.Text = _Sequence.ToString();

                        }

                    }
                }

                //da.Fill(ds);
                //DataLIST.DataSource = ds;
                //DataLIST.DataBind();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            _changeCss2();
        }

        private void _diaConfig_functions(Int64 _config_id, string _dimension, string _sequence, Int64 _id, string _action)
        {
            string _userName = Session["username"].ToString();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_DIMENSION_CONFIGURATION_FUNCTIONS", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@CONFIG_ID", _config_id);
                obj.cmd.Parameters.AddWithValue("@DIMENSION", _dimension);
                obj.cmd.Parameters.AddWithValue("@CREATED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@CREATED_ON", null);
                obj.cmd.Parameters.AddWithValue("@SEQUENCE", _sequence);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_ON", null);
                obj.cmd.Parameters.AddWithValue("@ACTION", _action);
                if (_action == "UPDATE")
                {
                    obj.cmd.Parameters.AddWithValue("@ID", _id);
                }
                else if (_action == "INSERT")
                {
                    obj.cmd.Parameters.AddWithValue("@ID", null);
                }
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

        public void resetStages()
        {
            Int64 configId = Convert.ToInt64(lblId.Text);
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("DELETE FROM DIMENSION_CONFIGURATION WHERE CONFIG_ID = '" + configId + "'", obj.con);
                SqlCommand cmd2 = new SqlCommand("UPDATE TOOL_DIMENSION_CONFIGURATION SET TOOL_TYPE ='" + ddmtooltype.Text + "', TOOL_SUB_TYPE = '" + ddmtoolSubtype.Text + "' WHERE STATUS = 0 AND ID = '" + configId + "'", obj.con);
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

        public void fillchkonEdit(Int64 configId)
        {
            string rm = string.Empty;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT DIMENSION FROM DIMENSION_CONFIGURATION WHERE CONFIG_ID = '" + configId + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {

                    rm = rdr[0].ToString();
                    foreach (DataListItem items in this.DataListDimension.Items)
                    {
                        CheckBox chk = (items.FindControl("CheckRM") as CheckBox);
                        string chkeName = chk.Text;
                        if (rm == chkeName)
                        {
                            chk.Checked = true;
                            chk.CssClass = "checkbox checkbox-inline btn";
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
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            lblMessage.Text = "";
            bool isExists = false;
            isExists = checkIsExists();

            if (btnSave.Text == "SAVE")
            {
                if (ddmtooltype.SelectedIndex == 0 || ddmtoolSubtype.SelectedIndex == 0)
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Select tool..!";
                    return;
                }
                else
                {
                    if (isExists == true)
                    {
                        lblMessage.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        lblMessage.Text = "This Configuration already Exsists..!";
                    }
                    else if (isExists == false)
                    {
                        Int64 _config_id = obj._getMaxId("ID", "TOOL_DIMENSION_CONFIGURATION");
                        _action = "INSERT";
                        _dimentionFunctions(_config_id);
                        foreach (DataListItem items in this.DataListDimension.Items)
                        {
                            CheckBox chk = (items.FindControl("CheckRM") as CheckBox);
                            TextBox txt = (items.FindControl("txtSequence") as TextBox);
                            if (chk.Checked == true)
                            {
                                string _dimension = (items.FindControl("CheckRM") as CheckBox).Text;
                                //_diaConfig_functions(_config_id, _dimension);
                                _diaConfig_functions(_config_id, _dimension, txt.Text, 0, "INSERT");

                            }
                        }

                        lblMessage.Visible = true;
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        lblMessage.Text = "Saved Successfully..!";
                    }

                }

                fillGrid();
            }
            else if (btnSave.Text == "UPDATE")
            {
                bool _existsUpdate = checkIsExists2();
                if (_existsUpdate == true)
                {

                }
                else
                {
                    _id = Convert.ToInt64(lblId.Text);
                    //_action = "UPDATE";
                    _dimentionFunctions(_id);
                    resetStages();
                    foreach (DataListItem items in this.DataLIST.Items)
                    {
                        CheckBox chk = (items.FindControl("CheckRM2") as CheckBox);
                        TextBox txt = (items.FindControl("txtSequence") as TextBox);
                        Label lbl = (items.FindControl("lblId") as Label);
                        string id = lbl.Text;
                        if (id == "")
                        {
                            id = (0).ToString();
                        }
                        else if (id != "")
                        {
                            Convert.ToInt64(id);
                        }
                        if (chk.Checked == true)
                        {
                            string _dimension = (items.FindControl("CheckRM2") as CheckBox).Text;
                            //  _diaConfig_functions(_id, _dimension, txt.Text, null, "INSERT");
                            _diaConfig_functions(_id, _dimension, txt.Text, Convert.ToInt64(id), "INSERT");
                        }
                    }
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Updated successfully..!";

                }
            }
            fillGrid();
            btnSave.Text = "SAVE";
            ddmtooltype.SelectedIndex = 0;
            ddmtoolSubtype.Items.Clear();

            DataListDimension.Visible = false;
            DataLIST.Visible = false;

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            fillGrid();
            btnSave.Text = "SAVE";
            ddmtoolSubtype.Items.Clear();
            ddmtooltype.Items.Clear();
            fillToolType();
            ddmtooltype.Items.Insert(0, new ListItem("Select Tool Type", "0"));
            ddmtooltype.SelectedIndex = 0;


            DataListDimension.Visible = false;
            DataLIST.Visible = false;

        }

        protected void CheckRM2_CheckedChanged(object sender, EventArgs e)
        {
            _changeCss2();
            fillGrid();

        }

        protected void CheckRM_CheckedChanged(object sender, EventArgs e)
        {
            _changeCss();
            fillGrid();

        }

        protected void CheckBoxAll_CheckedChanged(object sender, EventArgs e)
        {

            if (DataListDimension.Visible == true)
            {
                foreach (DataListItem items in this.DataListDimension.Items)
                {
                    CheckBox chkbox = (items.FindControl("CheckRM") as CheckBox);
                    if (CheckBoxAll.Checked == true)
                    {
                        chkbox.Checked = true;

                    }
                    else if (CheckBoxAll.Checked == false)
                    {
                        chkbox.Checked = false;

                    }
                }
                _changeCss();
            }
            else if (DataLIST.Visible == true)
            {
                foreach (DataListItem items in this.DataLIST.Items)
                {
                    CheckBox chkbox = (items.FindControl("CheckRM2") as CheckBox);
                    if (CheckBoxAll.Checked == true)
                    {
                        chkbox.Checked = true;

                    }
                    else if (CheckBoxAll.Checked == false)
                    {
                        chkbox.Checked = false;

                    }
                }
                _changeCss2();
            }


            fillGrid();
               

        }

        public void _changeCss()
        {
            foreach (DataListItem items in this.DataListDimension.Items)
            {
                CheckBox chk = (items.FindControl("CheckRM") as CheckBox);
                TextBox txt = (items.FindControl("txtSequence") as TextBox);
                if (chk.Checked == true)
                {
                    chk.CssClass = "checkbox btn-success test";
                    //txt.Visible = true;
                }
                else if (chk.Checked == false)
                {
                    chk.CssClass = "checkbox btn-danger test";
                    txt.Visible = false;
                }
            }
        }

        public void _changeCss2()
        {
            foreach (DataListItem items in this.DataLIST.Items)
            {
                CheckBox chk = (items.FindControl("CheckRM2") as CheckBox);
                TextBox txt = (items.FindControl("txtSequence") as TextBox);
                if (chk.Checked == true)
                {
                    chk.CssClass = "checkbox btn-success test";
                    //txt.Visible = true;

                }
                else if (chk.Checked == false)
                {
                    chk.CssClass = "checkbox btn-danger test";
                    //txt.Visible = false;
                }
            }
        }

        protected void ddmtoolSubtype_SelectedIndexChanged(object sender, EventArgs e)
        {

            lblMessage.Text = "";
            if (btnSave.Text == "SAVE")
            {
                fillchkRM();
                DataListDimension.Visible = true;
                DataLIST.Visible = false;
            }
            else if (btnSave.Text == "UPDATE")
            {
                fillchkRM2();
                DataListDimension.Visible = false;
                DataLIST.Visible = true;
            }

            fillGrid();

        }

        protected void ddmtooltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddmtoolSubtype.Enabled = true;
            lblMessage.Text = "";
            ddmtoolSubtype.Items.Clear();
            ddmtoolSubtype.Items.Insert(0, "Select Tool Sub Type");
            fillSubType();
            fillGrid();

        }
    }
}