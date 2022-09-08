using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq;
using AlankarNewDesign.DAL;
namespace AlankarNewDesign
{
    public partial class stage_type : System.Web.UI.Page
    {

        DbConnection obj = new DbConnection();
        Int64 _id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _fillGrid();
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
             lblMessage.Text = "";
            Int64 _ID = Convert.ToInt64((sender as LinkButton).CommandArgument);
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("UPDATE STAGE_TYPE_MASTER SET STATUS = 1 WHERE ID = '" + _ID + "'", obj.con);
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
                    lblMessage.Text = "Deleted Successfully..!";

                    _fillGrid();
        
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {

            lblMessage.Text = "";
            Int64 _ID = Convert.ToInt64((sender as LinkButton).CommandArgument);
            _getData(_ID);

            _fillGrid();
            btnSave.Text = "UPDATE";
            lblvalid.Text = "";

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {


            bool _isExists = _isAlreadyExits();
            if (btnSave.Text == "SAVE")
            {
                if (txtSTAGEtype.Text == "")
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Stage type Required..!";
                }
                else
                {
                    if (_isExists == true)
                    {
                        lblMessage.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        lblMessage.Text = "Stage Type Already Exists..!";
                    }
                    else if (_isExists == false)
                    {
                        _id = obj._getMaxId("ID", "STAGE_TYPE_MASTER");
                        _stageTypeFunctions(_id, "INSERT");

                    }

                }

            }
            else if (btnSave.Text == "UPDATE")
            {
                bool _existsInTrans = _chkStageType_in_transaction();
                if (_existsInTrans == true)
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = txtSTAGEtype.Text + " used in transactions..!";
                }
                else
                {
                    bool _chkUpExists = _UpExists(Convert.ToInt64(lblId.Text), txtSTAGEtype.Text);
                    if (_chkUpExists == true)
                    {
                        lblMessage.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        lblMessage.Text = "Already Exists..!";
                    }
                    else
                    {
                        _id = Convert.ToInt64(lblId.Text);
                        _stageTypeFunctions(_id, "UPDATE");
                        _update_Stage_all();

                    }

                }

            }
            _fillGrid();
            txtSTAGEtype.Text = "";
            txtSequence.Text = "";
            btnSave.Text = "SAVE";


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("stage_type.aspx");
        }

        public void _stageTypeFunctions(Int64 _id, string _Action)
        {
            string username = Session["username"].ToString();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_STAGE_TYPE_MASTER_FUNCTIONS", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@ID", _id);
                obj.cmd.Parameters.AddWithValue("@STAGE_TYPE", txtSTAGEtype.Text);
                obj.cmd.Parameters.AddWithValue("@SEQUENCE", txtSequence.Text);
                obj.cmd.Parameters.AddWithValue("@STATUS", 0);
                obj.cmd.Parameters.AddWithValue("@CREATED_BY", username);
                obj.cmd.Parameters.AddWithValue("@CREATED_ON", null);
                obj.cmd.Parameters.AddWithValue("@ACTION", _Action);
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
                obj.cmd = new SqlCommand("SELECT STAGE_TYPE FROM STAGE_TYPE_MASTER WHERE STAGE_TYPE = '" + txtSTAGEtype.Text + "'", obj.con);
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
                obj.cmd = new SqlCommand("SELECT ID, STAGE_TYPE, SEQUENCE FROM STAGE_TYPE_MASTER WHERE ID = '" + _id + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    lblId.Text = rdr[0].ToString();
                    txtSTAGEtype.Text = rdr[1].ToString();
                    lblOldName.Text = rdr[1].ToString();
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

        public void _fillGrid()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT ID, STAGE_TYPE, SEQUENCE FROM STAGE_TYPE_MASTER WHERE STATUS = 0 ORDER BY SEQUENCE ASC", obj.con);
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

        private void _update_Stage_all()
        {
            string _username = Session["username"].ToString();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_CHANGE_STAGE_NAME", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@OLD_NAME", lblOldName.Text);
                obj.cmd.Parameters.AddWithValue("@NEW_NAME", txtSTAGEtype.Text);
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
        private bool _chkStageType_in_transaction()
        {
            bool _result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT STAGE_TRANSACTIONS.STAGE_TYPE FROM STAGE_TRANSACTIONS WHERE STAGE_TRANSACTIONS.STAGE_TYPE = '" + lblOldName.Text + "'", obj.con);
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
                obj.cmd = new SqlCommand("SELECT STAGE_TYPE_MASTER.STAGE_TYPE FROM STAGE_TYPE_MASTER WHERE STAGE_TYPE_MASTER.STAGE_TYPE = '" + _tName + "' AND STAGE_TYPE_MASTER.ID != '" + _id + "'", obj.con);
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
    }
}