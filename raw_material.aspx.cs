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
    public partial class raw_material : System.Web.UI.Page
    {
        DbConnection obj = new DbConnection();
        Int64 _rmId;
        string _action = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _displayrm();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (btnSave.Text == "SAVE")
            {
                bool isExists = obj.AlreadyExists("RM_NAME", "RAW_MATERIAL", txtRawMaterial.Text);
                if (isExists == true)
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "This RAW Material Already Exists..!";
                }
                else if (isExists == false)
                {
                    _rmId = obj._getMaxId("RM_ID", "RAW_MATERIAL");
                    _action = "INSERT";
                    _rmfunctions();
                    _displayrm();
                    txtRawMaterial.Text = "";
                    txtdescription.Text = "";
                    txtunit.Text = "";
                    txtSequence.Text = "";
                    _rmId = 0;
                }
            }
            else if (btnSave.Text == "UPDATE")
            {


                //bool _tranExists = _UpExists(Convert.ToInt64(lblMessage.Text), txtRawMaterial.Text);
                bool _tranExists = _chkDimension_in_transaction(txtRawMaterial.Text);
                if (_tranExists == true)
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = lblUp.Text + " already exists in transaction.";

                }
                else if (_tranExists == false)
                {
                    bool _upExistschk = _UpExists(Convert.ToInt64(lblMessage.Text), txtRawMaterial.Text);
                    if (_upExistschk == true)
                    {

                        lblMessage.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        lblMessage.Text = "Dimension already exists..";

                    }
                    else
                    {


                        _update_dimension_all();
                        lblMessage.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        _rmId = Convert.ToInt64(lblMessage.Text);
                        _action = "UPDATE";
                        _rmfunctions();
                        lblMessage.Text = "Updated Successfully..";


                    }


                }


                _displayrm();
                txtRawMaterial.Text = "";
                txtdescription.Text = "";
                txtunit.Text = "";
                txtSequence.Text = "";
                txtMarketPrice.Text = "";
                _rmId = 0;
                btnSave.Text = "SAVE";
            }

        }

        public void _rmfunctions()
        {
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_RAW_MATERIAL_FUNCTIONS", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@RM_ID", _rmId);
                obj.cmd.Parameters.AddWithValue("@RM_NAME", txtRawMaterial.Text);
                obj.cmd.Parameters.AddWithValue("@RM_DESCRIPTION", txtdescription.Text);
                obj.cmd.Parameters.AddWithValue("@RM_UNIT", txtunit.Text);
                obj.cmd.Parameters.AddWithValue("@SEQUENCE", txtSequence.Text);
                obj.cmd.Parameters.AddWithValue("@STATUS", 0);
                obj.cmd.Parameters.AddWithValue("@MARKET_RATE", txtMarketPrice.Text);
                obj.cmd.Parameters.AddWithValue("@ACTION", _action);
                obj.con.Open();
                obj.cmd.ExecuteNonQuery();
                if (_action == "INSERT")
                {

                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Saved Successfully..!";
                }
                else if (_action == "UPDATE")
                {
                    lblMessage.Visible = true;
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

      


        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("raw_material.aspx");
        }


        public void _displayrm()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT RM_ID, RM_NAME, RM_DESCRIPTION, RM_UNIT, SEQUENCE, MARKET_RATE FROM RAW_MATERIAL WHERE STATUS = 0", obj.con);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                GridRM.DataSource = ds;
                GridRM.DataBind();
                GridRM.UseAccessibleHeader = true;
                GridRM.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception ex)
            {
                //Response.Write("<script>alert('Error in _displayrm(): " + ex.Message + " ');</script>");
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
            Int64 _ID = Convert.ToInt64((sender as LinkButton).CommandArgument);
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("UPDATE RAW_MATERIAL SET STATUS = 1 WHERE RM_ID = '" + _ID + "'", obj.con);
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
            _displayrm();

        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {

            lblMessage.Text = "";
            _rmId = Convert.ToInt64((sender as LinkButton).CommandArgument);
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("Select RM_ID, RM_NAME, RM_DESCRIPTION, RM_UNIT, SEQUENCE, MARKET_RATE FROM RAW_MATERIAL WHERE RM_ID = '" + _rmId + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    lblMessage.Text = rdr[0].ToString();
                    txtRawMaterial.Text = rdr[1].ToString();
                    lblUp.Text = rdr[1].ToString();
                    txtdescription.Text = rdr[2].ToString();
                    txtunit.Text = rdr[3].ToString();
                    txtSequence.Text = rdr[4].ToString();
                    txtMarketPrice.Text = rdr[5].ToString();
                }
            }
            catch (Exception ex)
            {
                //Response.Write("<script>alert('Error in updation(): " + ex.Message + " ');</script>");
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
            btnSave.Text = "UPDATE";
            _displayrm();

        }


        private bool _chkDimension_in_transaction(string _dimension)
        {
            bool _result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT RAW_MATERIAL_TRANSACTIONS.RAW_MATERIAL FROM RAW_MATERIAL_TRANSACTIONS WHERE RAW_MATERIAL_TRANSACTIONS.RAW_MATERIAL = '" + lblUp.Text + "'", obj.con);
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
                obj.cmd.Parameters.AddWithValue("@OLD_NAME", lblUp.Text);
                obj.cmd.Parameters.AddWithValue("@NEW_NAME", txtRawMaterial.Text);
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

        public bool _UpExists(Int64 _id, string _rmName)
        {
            bool _result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("Select [RM_NAME] from [dbo].[RAW_MATERIAL] where RM_NAME = '" + _rmName + "' and RM_ID != '" + _id + "'", obj.con);
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