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
    public partial class stages : System.Web.UI.Page
    {
        DbConnection obj = new DbConnection();
        alankar_db_providerDataContext _dbContext = new alankar_db_providerDataContext();
        string _action = string.Empty;
        Int64 _stageId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _getMasterStage();
                _fillStageddm();
                ddmStageType.Items.Insert(0, "Select");
            }
        }

        protected void ddmStageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            if (ddmStageType.SelectedIndex == 0)
            {
                _getMasterStage();
            }
            else
            {
                _fillGridBYMianStage();
            }
            
        }

        public void _fillGridBYMianStage()
        {
            lblMessage.Text = "";
            try
            {
                var _query = from s in _dbContext.STAGE_MASTERs where (s.STAGE_TYPE == ddmStageType.Text) select new { s.STAGE_ID, s.STAGE, s.STAGE_TYPE, s.IO, s.SEQUENCE };
                GridStageMaster.DataSource = _query;
                GridStageMaster.DataBind();
                GridStageMaster.UseAccessibleHeader = true;
                GridStageMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception ex)
            {

            }
        }

        public void _getMasterStage()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("Select STAGE_ID, STAGE, STAGE_TYPE, IO, SEQUENCE FROM STAGE_MASTER WHERE STATUS = 0 ORDER BY SEQUENCE ASC", obj.con);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                GridStageMaster.DataSource = ds;
                GridStageMaster.DataBind();
                GridStageMaster.UseAccessibleHeader = true;
                GridStageMaster.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
            catch (Exception ex)
            {
                //Response.Write("<script>alert('Error in _getMasterStage(): " + ex.Message + "');</script>");
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }


        private bool _chkStage_in_transaction(string _stage)
        {
            bool _result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT STAGE_TRANSACTIONS.STAGE FROM STAGE_TRANSACTIONS WHERE STAGE_TRANSACTIONS.STAGE = '" + lblStageUp.Text + "'", obj.con);
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


        private void _update_Stage_all()
        {
            string _username = Session["username"].ToString();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_CHANGE_STAGE_NAME", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@OLD_NAME", lblStageUp.Text);
                obj.cmd.Parameters.AddWithValue("@NEW_NAME", txtstage.Text);
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

        public bool AlreadyExists(String _columnName, String _tableName, String check)
        {
            bool result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("select " + _columnName + " from " + _tableName + " where " + _columnName + " = '" + check + "'", obj.con);
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

        public bool _UpExists(Int64 _id, string _STAGE)
        {
            bool _result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT [STAGE] FROM STAGE_MASTER WHERE STAGE = '" + _STAGE + "' AND STAGE_ID != '" + _id + "'", obj.con);
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

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            Int64 _ID = Convert.ToInt64((sender as LinkButton).CommandArgument);
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("UPDATE STAGE_MASTER SET STATUS = 1 WHERE STAGE_ID = '" + _ID + "'", obj.con);
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
            _getMasterStage();

        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {

            lblMessage.Text = "";
            _stageId = Convert.ToInt64((sender as LinkButton).CommandArgument);

            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT STAGE_ID, STAGE, STAGE_TYPE, IO, SEQUENCE FROM STAGE_MASTER WHERE STAGE_ID = '" + _stageId + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {

                    lblStageId.Text = rdr[0].ToString();
                    txtstage.Text = rdr[1].ToString();
                    lblStageUp.Text = rdr[1].ToString();
                    ddmStageType.Text = rdr[2].ToString();
                    ddmIo.Text = rdr[3].ToString();
                    txtSequence.Text = rdr[4].ToString();
                }
            }
            catch (Exception ex)
            {
                //Response.Write("<script>alert('Error in fetch : " + ex.Message + "');</script>");

            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            btnSave.Text = "UPDATE";
            _getMasterStage();

        }

        public void _fillStageddm()
        {
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT STAGE_TYPE FROM STAGE_TYPE_MASTER WHERE STATUS = 0 ORDER BY SEQUENCE ASC", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ddmStageType.Items.Add(rdr[0].ToString());
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

        protected void btnSave_Click(object sender, EventArgs e)
        {

            bool isExists = false;
            isExists = AlreadyExists("STAGE", "STAGE_TRANSACTIONS", txtstage.Text);
            if (btnSave.Text == "SAVE")
            {
                if (isExists == true)
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Stage Already Exists..!";
                }
                else if (isExists == false)
                {
                    if (ddmStageType.SelectedIndex != 0)
                    {
                        _stageId = obj._getMaxId("STAGE_ID", "STAGE_MASTER");
                        _action = "INSERT";
                        _masterStagefunctions();
                        txtstage.Text = "";
                        ddmStageType.SelectedIndex = 0;
                        txtSequence.Text = "";
                        ddmIo.SelectedIndex = 0;
                        _getMasterStage();

                    }
                    else if (ddmStageType.SelectedIndex == 0)
                    {
                        lblMessage.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        lblMessage.Text = "Select Stage type";
                        _getMasterStage();

                    }



                }

            }
            else if (btnSave.Text == "UPDATE")
            {
                bool _tranExists = _chkStage_in_transaction(lblStageUp.Text);
                if (_tranExists == false)
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = lblStageUp.Text + " already exists in transaction.";
                    _getMasterStage();

                }
                else if (_tranExists == true)
                {
                    bool _ExistsUp = _UpExists(Convert.ToInt64(lblStageId.Text), txtstage.Text);
                    if (_ExistsUp == true)
                    {
                        lblMessage.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        lblMessage.Text = "Already exists..!";
                        _getMasterStage();
                    }
                    else
                    {
                        _update_Stage_all();

                        lblMessage.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);

                        _stageId = Convert.ToInt64(lblStageId.Text);
                        _action = "UPDATE";

                        _masterStagefunctions();

                    }



                }





                txtstage.Text = "";
                ddmStageType.SelectedIndex = 0;
                txtSequence.Text = "";
                ddmIo.SelectedIndex = 0;
                btnSave.Text = "SAVE";
                _getMasterStage();

            }


        }


        public void _masterStagefunctions()
        {
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_STAGE_MASTER_FUNCTIONS", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@STAGE_ID", _stageId);
                obj.cmd.Parameters.AddWithValue("@STAGE", txtstage.Text);
                obj.cmd.Parameters.AddWithValue("@STAGE_TYPE", ddmStageType.Text);
                obj.cmd.Parameters.AddWithValue("@IO", ddmIo.Text);
                obj.cmd.Parameters.AddWithValue("@SEQUENCE", txtSequence.Text);
                obj.cmd.Parameters.AddWithValue("@STATUS", 0);
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
                    lblMessage.Text = "Updated Successfully..!";
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}