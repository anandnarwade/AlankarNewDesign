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
    public partial class cust_master : System.Web.UI.Page
    {
        DbConnection obj = new DbConnection();

        string _createdOn = string.Empty;
        string _createdBy = string.Empty;
        string _userName = string.Empty;
        string _modifiedOn = string.Empty;
        string _action = string.Empty;
        Int64 _id;
        string _modifiedBY = string.Empty;
        public Int64 _ccId2 { get; set; }
        public string _partyCode2 { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string x = Request.QueryString["Action"];
                string partycode = Request.QueryString["PARTY_CODE"];
                if (x == "UPDATE")
                {
                    btnPartySave.Text = "UPDATE";
                    _getPartyDetails(partycode);
                    _getCompanyContact(partycode);
                    GRIDContactPerson.CssClass = "GridView1 table table-responsive table-bordered table-hover";
                    lblPartyCode.Text = partycode;
                }
            }
        }


        private bool _alreadyExists1()
        {
            bool result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_CC_ALREADY_EXISTS", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@PARTY_CODE", lblPartyCode.Text);
                obj.cmd.Parameters.AddWithValue("@CC_NAME", txtContactPerson.Text);
                obj.cmd.Parameters.AddWithValue("@DEPARTMENT", txtDepartment.Text);
                obj.cmd.Parameters.AddWithValue("@DESIGNATION", txtDesignation.Text);
                obj.cmd.Parameters.AddWithValue("@EMAIL", txtEmail.Text);
                obj.cmd.Parameters.AddWithValue("@MOBILE", txtMobileNo.Text);
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



        private bool _alreadyExists2(Int64 _ccid)
        {
            bool result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_CC_ALREADY_EXISTS2", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@CC_ID", _ccid);
                obj.cmd.Parameters.AddWithValue("@PARTY_CODE", lblPartyCode.Text);
                obj.cmd.Parameters.AddWithValue("@CC_NAME", txtContactPerson.Text);
                obj.cmd.Parameters.AddWithValue("@DEPARTMENT", txtDepartment.Text);
                obj.cmd.Parameters.AddWithValue("@DESIGNATION", txtDesignation.Text);
                obj.cmd.Parameters.AddWithValue("@EMAIL", txtEmail.Text);
                obj.cmd.Parameters.AddWithValue("@MOBILE", txtMobileNo.Text);
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




        public bool _chkPartyCode()
        {
            bool result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_CHECK_PARTY_CODE", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@PARTY_CODE", txtPartyCode.Text);
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



        protected void btnPartySave_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";

            if (btnPartySave.Text == "SAVE")
            {
                bool isExist = _chkPartyCode();
                if (isExist == true)
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Party Code Already exists..!";

                }
                else
                {
                    _id = obj._getMaxId("ID", "MASTER_PARTY");
                    _createdOn = DateTime.Now.ToString(); //Created on Date time
                    _userName = Session["username"].ToString();
                    _modifiedOn = null;
                    _modifiedBY = Session["username"].ToString();
                    _action = "INSERT";
                    _MasterPartyFunctions(_id, _createdOn, _userName, _modifiedOn, _modifiedBY);
                    Response.Redirect("customers.aspx");
                }


            }
            else if (btnPartySave.Text == "UPDATE")
            {
                bool _isExistsIn_Transaction = _chkParty_in_transaction(lblPartyCode.Text);
                if (_isExistsIn_Transaction == true)
                {
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = lblPartyCode.Text + " is used in transactions...!";
                }
                else
                {
                    bool _updatedExists = _UpExists(txtPartyCode.Text, txtPartyName.Text, txtShortName.Text);
                    if (_updatedExists == true)
                    {
                        lblMessage.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        lblMessage.Text = "Already Exists";

                    }
                    else
                    {
                        _id = 1;
                        //_id = Convert.ToInt64(lblMessage.Text);
                        _modifiedBY = Session["username"].ToString();
                        _modifiedOn = DateTime.Now.ToString();
                        _action = "UPDATE";
                        _MasterPartyFunctions(_id, _createdOn, _createdBy, _modifiedOn, _modifiedBY);
                        Response.Redirect("customers.aspx");

                    }


                }


            }

           


        }





        public void _MasterPartyFunctions(Int64 id, string _createOn, string _createdBy, string modifiedOn, string modifiedBy)
        {

            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_MASTER_PARTY_FUNCTIONS", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = id;
                obj.cmd.Parameters.Add("@PARTY_CODE", SqlDbType.NVarChar).Value = txtPartyCode.Text;
                obj.cmd.Parameters.Add("@PARTY_NAME", SqlDbType.NVarChar).Value = txtPartyName.Text;
                obj.cmd.Parameters.Add("@SHORT_NAME", SqlDbType.NVarChar).Value = txtShortName.Text;
                obj.cmd.Parameters.Add("@FL_ADD", SqlDbType.NVarChar).Value = txtFL_ADD.Text;
                obj.cmd.Parameters.Add("@DIVISION", SqlDbType.NVarChar).Value = txtSL_ADD.Text;
                obj.cmd.Parameters.Add("@CITY", SqlDbType.NVarChar).Value = txtCity.Text;
                obj.cmd.Parameters.Add("@PIN", SqlDbType.NVarChar).Value = txtPin.Text;
                obj.cmd.Parameters.Add("@PHONE", SqlDbType.NVarChar).Value = txtPhone.Text;
                obj.cmd.Parameters.Add("@EMAIL", SqlDbType.NVarChar).Value = txtFax.Text;
                obj.cmd.Parameters.Add("@NEW_PARTY_CODE", SqlDbType.NVarChar).Value = null;
                obj.cmd.Parameters.Add("@C1DESIG", SqlDbType.NVarChar).Value = null;
                obj.cmd.Parameters.Add("@CONTACT2", SqlDbType.NVarChar).Value = null;
                obj.cmd.Parameters.Add("@C2DESIG", SqlDbType.NVarChar).Value = null;
                obj.cmd.Parameters.Add("@VENDCODE", SqlDbType.NVarChar).Value = txtVENDCODE.Text;
                obj.cmd.Parameters.Add("@ECCNO", SqlDbType.NVarChar).Value = txtECCNO.Text;
                obj.cmd.Parameters.Add("@CSTNO", SqlDbType.NVarChar).Value = txtCSTNO.Text;
                obj.cmd.Parameters.Add("@CREATED_ON", SqlDbType.NVarChar).Value = _createOn;
                obj.cmd.Parameters.Add("@CREATED_BY", SqlDbType.NVarChar).Value = _createdBy;
                obj.cmd.Parameters.Add("@MODIFIED_ON", SqlDbType.NVarChar).Value = modifiedOn;
                obj.cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.NVarChar).Value = modifiedBy;
                obj.cmd.Parameters.Add("@STATUS", SqlDbType.Int).Value = 0;
                obj.cmd.Parameters.Add("@ACTION", SqlDbType.NVarChar).Value = _action;
                obj.con.Open();
                obj.cmd.ExecuteNonQuery();

                if (_action == "INSERT")
                {
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Saved Successfully...!";
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
                lblMessage.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                lblMessage.Text = "Error";
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }








        protected void btnNext_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            Tabs.ActiveTabIndex = 1;
        }

       

        public void _getPartyDetails(string partyCode)
        {

            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_GET_MASTER_PARTY_DATA", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@PARTY_CODE", partyCode);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    txtPartyCode.Text = rdr[0].ToString();
                    lblPartyCode.Text = rdr[0].ToString();
                    txtPartyName.Text = rdr[1].ToString();
                    txtShortName.Text = rdr[2].ToString();
                    txtFL_ADD.Text = rdr[3].ToString();
                    txtSL_ADD.Text = rdr[4].ToString();
                    txtCity.Text = rdr[5].ToString();
                    txtPin.Text = rdr[6].ToString();
                    txtPhone.Text = rdr[7].ToString();
                    txtFax.Text = rdr[8].ToString();
                    //txtContact1.Text = rdr[9].ToString();
                    //txtC1DESIG.Text = rdr[10].ToString();
                    //txtContact2.Text = rdr[11].ToString();
                    //txtC2DESIG.Text = rdr[12].ToString();
                    txtVENDCODE.Text = rdr[13].ToString();
                    txtECCNO.Text = rdr[14].ToString();
                    txtCSTNO.Text = rdr[15].ToString();

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

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";

            Int64 _id = Convert.ToInt64((sender as LinkButton).CommandArgument);

            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("UPDATE CUSTOMER_CONTACT_PERSONS SET STATUS = 1 WHERE CC_ID = '" + _id + "'", obj.con);
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
            string partycode = Request.QueryString["PARTY_CODE"];
            _getCompanyContact(partycode);
            lblMessage.Visible = true;
            lblMessage.ForeColor = System.Drawing.Color.Green;
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            lblMessage.Text = "Deleted Successfully...!";

        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            Int64 _id = Convert.ToInt64((sender as LinkButton).CommandArgument);
            lblccId.Text = _id.ToString();
            _getCCData(_id);
            btnSaveContact.Text = "UPDATE";
            Tabs.ActiveTabIndex = 1;
            string partycode = Request.QueryString["PARTY_CODE"];
            GRIDContactPerson.UseAccessibleHeader = true;
            GRIDContactPerson.HeaderRow.TableSection = TableRowSection.TableHeader;

            _getCompanyContact(partycode);
            GRIDContactPerson.CssClass = "GridView1 table table-responsive table-bordered table-hover";

        }


        private void _getCompanyContact(string _partyCode)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_GET_CC_DATA", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@PARTY_CODE", _partyCode);
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(dt);
                GRIDContactPerson.DataSource = dt;
                GRIDContactPerson.DataBind();
                GRIDContactPerson.UseAccessibleHeader = true;
                GRIDContactPerson.HeaderRow.TableSection = TableRowSection.TableHeader;

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


        private void _getCCData(Int64 _ccid)
        {
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT [CC_ID],[PARTY_CODE],[CC_NAME],[DEPARTMENT],[DESIGNATION],[EMAIL],[MOBILE],[LANDLINE], CC_INITIAL FROM CUSTOMER_CONTACT_PERSONS WHERE CC_ID = '" + _ccid + "'", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    _ccId2 = Convert.ToInt64(rdr[0]);
                    _partyCode2 = rdr[1].ToString();
                    txtContactPerson.Text = rdr[2].ToString();
                    txtDepartment.Text = rdr[3].ToString();
                    txtDesignation.Text = rdr[4].ToString();
                    txtEmail.Text = rdr[5].ToString();
                    txtMobileNo.Text = rdr[6].ToString();
                    txtlandline.Text = rdr[7].ToString();
                    //ddmUpadhi.Text = rdr[8].ToString();

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



        private bool _chkParty_in_transaction(string _partyCode)
        {
            bool _result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT [PARTY_CODE] FROM OC_TRANSACTIONS WHERE PARTY_CODE = ''", obj.con);
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

        public bool _UpExists(string _partyCode, string _partyName, string _shortName)
        {
            bool _result = false;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT [PARTY_NAME], [SHORT_NAME] FROM MASTER_PARTY WHERE PARTY_NAME ='" + _partyName + "' AND SHORT_NAME = '" + _shortName + "' AND PARTY_CODE != '" + _partyCode + "'", obj.con);
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





        protected void btnSaveContact_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            if (btnSaveContact.Text == "SAVE")
            {
                bool _isExists1 = _alreadyExists1();
                if (_isExists1 == true)
                {
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "This person already exists!";
                }
                else if (_isExists1 == false)
                {
                    Int64 _ccId = obj._getMaxId("CC_ID", "CUSTOMER_CONTACT_PERSONS");
                    _ccFunctions(_ccId, "INSERT");
                    _getCompanyContact(txtPartyCode.Text);
                }


            }
            else if (btnSaveContact.Text == "UPDATE")
            {
                Int64 _ccid = Convert.ToInt64(lblccId.Text);
                bool _isExists2 = _alreadyExists2(_ccid);
                if (_isExists2 == true)
                {
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "This person already exists!";
                }
                else if (_isExists2 == false)
                {
                    _ccFunctions(_ccid, "UPDATE");
                }


            }

            string partycode = Request.QueryString["PARTY_CODE"];

            _getCompanyContact(partycode);
        }


        private void _ccFunctions(Int64 _ccId, string _action)
        {
            string _userName = Session["username"].ToString();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_CUSTOMER_CONTACT_PERSON_FUNCTIONS", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.AddWithValue("@CC_ID", _ccId);
                obj.cmd.Parameters.AddWithValue("@PARTY_CODE", txtPartyCode.Text);
                obj.cmd.Parameters.AddWithValue("@CC_NAME", txtContactPerson.Text);
                obj.cmd.Parameters.AddWithValue("@DEPARTMENT", txtDepartment.Text);
                obj.cmd.Parameters.AddWithValue("@DESIGNATION", txtDesignation.Text);
                obj.cmd.Parameters.AddWithValue("@EMAIL", txtEmail.Text);
                obj.cmd.Parameters.AddWithValue("@MOBILE", txtMobileNo.Text);
                obj.cmd.Parameters.AddWithValue("@LANDLINE", txtlandline.Text);
                obj.cmd.Parameters.AddWithValue("@CREATED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@CREATED_ON", null);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_BY", _userName);
                obj.cmd.Parameters.AddWithValue("@MODIFIED_ON", null);
                //obj.cmd.Parameters.AddWithValue("@CC_INITIAL", ddmUpadhi.Text);
                obj.cmd.Parameters.AddWithValue("@STATUS", 0);
                obj.cmd.Parameters.AddWithValue("@ACTION", _action);
                obj.con.Open();
                obj.cmd.ExecuteNonQuery();


                if (_action == "INSERT")
                {
                    lblMessage.Visible = true;
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Saved Successfully...!";
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
    }
}