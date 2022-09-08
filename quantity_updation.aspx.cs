using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using AlankarNewDesign.DAL;
namespace AlankarNewDesign
{
    public partial class quantity_updation : System.Web.UI.Page
    {
        DbConnection _obj = new DbConnection();
        alankar_db_providerDataContext _dbContext = new alankar_db_providerDataContext();
        Int64 _stageId = 0;
        string _action = string.Empty;
        string _userName = string.Empty;
        Int64 _qty = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string x = Request.QueryString["Action"];
                if (x == "QTYUP")
                {
                    string _ocNo = Request.QueryString["OC_NO"];
                    _getData(_ocNo);
                }
               
            }

        }

        protected void txtocno_TextChanged(object sender, EventArgs e)
        {
            string _ocNo = txtocno.Text;
            bool _dispatched = _dispatchExists();
            _getData(txtocno.Text);

            lblMessage.Visible = true;
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            lblMessage.CssClass = "label label-success";
           // lblMessage.Text = _getUnplanQty().ToString();

               var _ocDetails = _dbContext.OC_TRANSACTIONs.SingleOrDefault(x => x.OC_NO == _ocNo);
               if (_ocDetails.STATUS == 0)
               {
                   lblCloseOcMsg.Text = "This OC is Closed!";
               }
               else
               {
                   lblCloseOcMsg.Text = "";
               }


               if (_dispatched == true)
               {


                   Int64 _dispatch = Convert.ToInt64(_getDispatchedValue(txtocno.Text));
                   txtdispatched.Text = _dispatch.ToString();
                   lblCloseOcMsg.Text = "";



               }
             
          
          
        }


        public void _getData(string _ocNo)
        {
            txtocno.Text = _ocNo;
            string _custCode = string.Empty;
            OC_TRANSACTION _OC = _dbContext.OC_TRANSACTIONs.SingleOrDefault(OC => OC.OC_NO == _ocNo);
            lblItemCode.Text = _OC.ITEM_CODE;
            lblDrgNO.Text = _OC.DRGNO;
            lblQuantity.Text = _OC.OCQTY.ToString();
            lblToolType.Text = _OC.TOOLTYPE;
            lblSubToolType.Text = _OC.MATCHTYPE;
            _custCode = _OC.PARTY_CODE;
            MASTER_PARTY _mp = _dbContext.MASTER_PARTies.SingleOrDefault(mp => mp.PARTY_CODE == _custCode);
            lblCustName.Text = _mp.PARTY_NAME+" - "+_mp.FL_ADD;

            bool issuExists = _stageQtyExists("Issue");

            if (issuExists == true)
            {
                DataListIssue.Visible = false;
                DataListIssueUpdate.Visible = true;
                getStagesValues("Issue", DataListIssueUpdate);
            }
            else if (issuExists == false)
            {
                DataListIssueUpdate.Visible = false;
                DataListIssue.Visible = true;
                _getNewStages("Issue", DataListIssue);

            }



            bool notIssueExists = _stageQtyExists("Not Issue");
            if (notIssueExists == true)
            {
                DataListNotIssue.Visible = false;
                DataListNotIssueUpdate.Visible = true;
                getStagesValues("Not Issue", DataListNotIssueUpdate);
            }
            else if (notIssueExists == false)
            {
                DataListNotIssue.Visible = true;
                DataListNotIssueUpdate.Visible = false;
                _getNewStages("Not Issue", DataListNotIssue);
            }


            _getStageQty();
 
        }

        [System.Web.Services.WebMethod]
        public static string[] GetTagNames(string prefixText, int count)
        {
            alankar_db_providerDataContext _dbContext = new alankar_db_providerDataContext();
            return _dbContext.OC_TRANSACTIONs.Where(n => n.OC_NO.StartsWith(prefixText)).OrderBy(n => n.ID).Select(n => n.OC_NO).Distinct().Take(count).ToArray();
            
        }

        public void _getNewStages(string _stageType, DataList _datalistName)
        {
           
                SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                _obj._getConnection();
               // _obj.cmd = new SqlCommand("SELECT S.STAGE FROM STAGES AS S INNER JOIN TOOL_STAGES_CONFIGURATION AS TS ON S.CONFI_ID = TS.ID INNER JOIN STAGE_MASTER ON STAGE_MASTER.STAGE = S.STAGE WHERE TS.STAGE_TYPE = '" + _stageType + "' AND TS.TOOL_TYPE = '" + lblToolType.Text + "' AND TS.TOOL_SUB_TYPE = '" + lblSubToolType.Text + "' AND STAGE_MASTER.STATUS = 0  AND TS.STATUS = 0 ORDER BY STAGE_MASTER.SEQUENCE ASC", _obj.con);
                _obj.cmd = new SqlCommand("SP_GET_STAGE_VALUES", _obj.con);
                _obj.cmd.CommandType = CommandType.StoredProcedure;
                _obj.cmd.Parameters.AddWithValue("@STAGE", _stageType);
                _obj.cmd.Parameters.AddWithValue("@TOOL", lblToolType.Text);
                _obj.cmd.Parameters.AddWithValue("@SUBTOOL", lblSubToolType.Text);
                _obj.con.Open();
                da.SelectCommand = _obj.cmd;
                da.Fill(ds);
                _datalistName.DataSource = ds;
                _datalistName.DataBind();
                SqlDataReader rdr = _obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string _dispatch = string.Empty;
                    _dispatch = rdr[0].ToString();

                    foreach (DataListItem items in _datalistName.Items)
                    {
                        Int64 _scQty =0, _forIssue = 0;
                        Label lbl = (items.FindControl("lblStage") as Label);
                        TextBox txt = (items.FindControl("txtStages") as TextBox);

                        if (_dispatch == "")
                        {
                            lbl.Visible = false;
                            txt.Visible = false;
                        }
                        else
                        {
                            lbl.Visible = true;
                            txt.Visible = true;
                        }


                        if (lbl.Text == "Schedule")
                        {
                            
                            txt.Text = _getUnplanQty().ToString();
                          
                            txt.Enabled = false;
                        }
                        //else if (lbl.Text == "For Issue")
                        //{
                        //    if (lblQuantity.Text != "" || lblQuantity.Text == null)
                        //    {
                        //        _scQty = _getUnplanQty();
                        //        Int64 qty =  Convert.ToInt64(lblQuantity.Text);
                        //        _forIssue = qty - _scQty;
                        //        txt.Text = _forIssue.ToString();
                        //    }
                           
                        //}
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _obj.con.Close();
                _obj.cmd.Dispose();
            }
        }


        public void getStagesValues(string _StageType, DataList datalistName)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();

            try
            {
                _obj._getConnection();

                //_obj.cmd = new SqlCommand("SELECT ID, STAGE, VALUE FROM STAGE_TRANSACTIONS WHERE STAGE_TYPE = '" + _StageType + "' AND OC_NO = '" + txtOCno.Text + "'", _obj.con);
                _obj.cmd = new SqlCommand("SELECT S.ID, S.STAGE, S.VALUE FROM STAGE_TRANSACTIONS AS S INNER JOIN STAGE_MASTER AS SM ON S.STAGE = SM.STAGE WHERE S.OC_NO = '" + txtocno.Text + "' AND S.STAGE_TYPE = '" + _StageType + "' ORDER BY SM.SEQUENCE ASC", _obj.con);
                _obj.con.Open();
                da.SelectCommand = _obj.cmd;
                da.Fill(ds);
                datalistName.DataSource = ds;
                datalistName.DataBind();
                if (_StageType == "Not Issue")
                {
                    foreach (DataListItem items in datalistName.Items)
                    {
                        Label lbl = (items.FindControl("lblStage") as Label);
                        TextBox txt = (items.FindControl("txtStages") as TextBox);
                        if (lbl.Text == "Schedule")
                        {
                            txt.Text = _getUnplanQty().ToString();
                            txt.Enabled = false;
                            break;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //Response.Write("<script>alert('Error in getStagesValues: " + ex.Message + "');</script>");
            }
            finally
            {

                _obj.con.Close();
                _obj.cmd.Dispose();
            }
        }


        public bool _stageQtyExists(string _stage_type_name)
        {
            bool _result = false;
            try
            {
                foreach (STAGE_TRANSACTION st in _dbContext.STAGE_TRANSACTIONs)
                {
                    if (st.OC_NO == txtocno.Text && st.STAGE_TYPE == _stage_type_name)
                    {
                        _result = true;
                        break;
                    }
                }


            }
            catch (Exception ex)
            {
                lblMessage.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                lblMessage.CssClass = "label label-success";
                lblMessage.Text = "Error in _stageQtyExists() : " + ex.Message;

            }
            return _result;
        }

        protected void btnSAVE_Click(object sender, EventArgs e)
        {
            Int64 _notIssue = 0, issue = 0, dispatch = 0, _ocQty =0, total = 0;
            if (txtdispatched.Text == "")
            {
 
            }
            else if (txtdispatched.Text != "")
            {
                dispatch = Convert.ToInt64(txtdispatched.Text);
            }

            _ocQty = Convert.ToInt64(lblQuantity.Text);

            _notIssue = _getNotIssueValue();
            issue = _getIssueValue();


            total = _notIssue + issue + dispatch;

            if (total == _ocQty)
            {
                //lblMessage.Visible = true;
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                //lblMessage.CssClass = "label label-success";
                //lblMessage.Text = "Quantity match";

                string stages = string.Empty;
                string _value = string.Empty;
                /*______________________*/
                _notIssueFunctions(_notIssue);
                _issueFunctions(issue);

            }
            else
            {
                lblMessage.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                lblMessage.CssClass = "label label-success";
                lblMessage.Text = "Quantity missmatch" + "</br>" + "issue : "+ issue+ "</br>" + "Not Issue : " +_notIssue;
                return;
 
            }

            _getStageQty();
            lblMessage.Visible = true;
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
            lblMessage.CssClass = "alert alert-success";
            
            lblMessage.Text = "Saved Successfully";
            Response.Redirect("all_oc_entries.aspx");
            

          
        }



        public void _issueFunctions(Int64 _issueValue)
        {
            string stages = string.Empty;
            string _value = string.Empty;
            if (DataListIssue.Visible == true && DataListIssueUpdate.Visible == false)
            {
                foreach (DataListItem items in this.DataListIssue.Items)
                {
                    bool stgExist;

                    _value = (items.FindControl("txtStages") as TextBox).Text;
                    stages = (items.FindControl("lblStage") as Label).Text;
                    _stageId = _obj._getMaxId("ID", "STAGE_TRANSACTIONS");

                    _action = "INSERT";
                    stgExist = _checkStageAlreadyExists(stages, "Issue");
                    if (stgExist == true)
                    {

                    }
                    else if (stgExist == false)
                    {
                        _action = "INSERT";
                        _StageTransactions(stages, _value, "Issue", _issueValue.ToString());
                    }
                }

            }
            else if (DataListIssue.Visible == false && DataListIssueUpdate.Visible == true)
            {
                foreach (DataListItem items in this.DataListIssueUpdate.Items)
                {
                    _stageId = Convert.ToInt64((items.FindControl("lblId") as Label).Text);
                    _value = (items.FindControl("txtStages") as TextBox).Text;
                    stages = (items.FindControl("lblStage") as Label).Text;
                    _action = "UPDATE";
                    _StageTransactions(stages, _value, "Issue", _issueValue.ToString());
                }
            }
 
        }

        public void _notIssueFunctions(Int64 _notIssueValue)
        {
            string stages = string.Empty;
            string _value = string.Empty;
            if (DataListNotIssue.Visible == true && DataListNotIssueUpdate.Visible == false)
            {
                foreach (DataListItem items in this.DataListNotIssue.Items)
                {

                    bool stgExist;

                    _value = (items.FindControl("txtStages") as TextBox).Text;
                    stages = (items.FindControl("lblStage") as Label).Text;
                    _stageId = _obj._getMaxId("ID", "STAGE_TRANSACTIONS");

                    _action = "INSERT";
                    stgExist = _checkStageAlreadyExists(stages, "Not Issue");
                    if (stgExist == true)
                    {

                    }
                    else if (stgExist == false)
                    {
                        _action = "INSERT";
                        _StageTransactions(stages, _value, "Not Issue", _notIssueValue.ToString());
                    }

                }


            }
            else if (DataListNotIssue.Visible == false && DataListNotIssueUpdate.Visible == true)
            {
                foreach (DataListItem items in this.DataListNotIssueUpdate.Items)
                {
                    _stageId = Convert.ToInt64((items.FindControl("lblId") as Label).Text);
                    _value = (items.FindControl("txtStages") as TextBox).Text;
                    stages = (items.FindControl("lblStage") as Label).Text;
                    _action = "UPDATE";
                    _StageTransactions(stages, _value, "Not Issue", _notIssueValue.ToString());

                }

            }
 
        }



        public void _getStageQty()
        {
            try
            {
                var query = _dbContext.STAGE_TRANSACTIONs.Where(notIssue => notIssue.OC_NO == txtocno.Text & notIssue.STAGE_TYPE == "Not Issue").Select(o => o.STAGE_TYPE_VALUE).FirstOrDefault();
                if (query == null)
                {

                }
                else
                {

                    txtNosIssue.Text = query.ToString();
                }

                var _query2 = _dbContext.STAGE_TRANSACTIONs.Where(notIssue => notIssue.OC_NO == txtocno.Text & notIssue.STAGE_TYPE == "Issue").Select(o => o.STAGE_TYPE_VALUE).FirstOrDefault();

                if (_query2 == null)
                {

                }
                else
                {
                    txtIssue.Text = _query2.ToString();
                }

                var _query3 = _dbContext.STAGE_TRANSACTIONs.Where(cleaning => cleaning.OC_NO == txtocno.Text && cleaning.STAGE_TYPE == "Issue" && cleaning.STAGE == "Cleaning").Select(o => o.VALUE).FirstOrDefault();
                if (_query3 == null)
                {


                }
                else
                {

                    txtCleaning.Text = _query3.ToString();

                }
                var _query4 = _dbContext.STAGE_TRANSACTIONs.Where(dispatch => dispatch.OC_NO == txtocno.Text && dispatch.STAGE_TYPE == "Dispatched").Select(o => o.STAGE_TYPE_VALUE).FirstOrDefault();
                if (_query4 == null)
                {

                }
                else
                {

                    txtdispatched.Text = _query4.ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }





        public void _StageTransactions(string stage, string value, string stageType, string _stageTypeValue)
        {

           
           
            if (_action == "INSERT")
            {
                _stageId = _obj._getMaxId("ID", "STAGE_TRANSACTIONS");
            }

            try
            {
                _obj._getConnection();
                _obj.cmd = new SqlCommand("SP_STAGE_TRANSACTION_FUNCTIONS", _obj.con);
                _obj.cmd.CommandType = CommandType.StoredProcedure;
                _obj.cmd.Parameters.AddWithValue("@ID", _stageId);
                _obj.cmd.Parameters.AddWithValue("@OC_NO", txtocno.Text);
                _obj.cmd.Parameters.AddWithValue("@STAGE_TYPE", stageType);
                _obj.cmd.Parameters.AddWithValue("@STAGE", stage);
                _obj.cmd.Parameters.AddWithValue("@VALUE", value);
                _obj.cmd.Parameters.AddWithValue("@CREATED_BY", _userName);
                _obj.cmd.Parameters.AddWithValue("@CREATED_ON", null);
                _obj.cmd.Parameters.AddWithValue("@MODIFIED_BY", _userName);
                _obj.cmd.Parameters.AddWithValue("@MODIFIED_ON", null);
                _obj.cmd.Parameters.AddWithValue("@STAGE_TYPE_VALUE", _stageTypeValue);
                _obj.cmd.Parameters.AddWithValue("@ACTION", _action);
                _obj.con.Open();
                _obj.cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Response.Write("<script>alert('Error in _StageTransactions: "+ex.Message+"');</script>");

                lblMessage.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                lblMessage.Text = "Error in _StageTransactions: " + ex.Message;
            }
            finally
            {
                _obj.con.Close();
                _obj.cmd.Dispose();
            }
        }



        public bool _checkStageAlreadyExists(string _stageName, string _stageType)
        {
            bool result = false;
            try
            {
                _obj._getConnection();
                _obj.cmd = new SqlCommand("SELECT STAGE, VALUE FROM STAGE_TRANSACTIONS WHERE OC_NO = '" + txtocno.Text + "' AND STAGE_TYPE = '" + _stageType + "' AND STAGE = '" + _stageName + "'", _obj.con);
                _obj.con.Open();
                SqlDataReader rdr = _obj.cmd.ExecuteReader();
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
                _obj.con.Close();
                _obj.cmd.Dispose();
            }
            return result;
        }



        /*Method to calculate Issue Value*/
        private Int64 _getIssueValue()
        {
            Int64 _result = 0, Issue = 0, _NissuValue = 0 ;
            try
            {

                if (DataListIssue.Visible == true && DataListIssueUpdate.Visible == false)
                {
                    foreach (DataListItem items in this.DataListIssue.Items)
                    {
                       Int64 _issuValue = 0;
                        TextBox text = (items.FindControl("txtStages") as TextBox);
                        if (text.Text == "")
                        {

                        }
                        else if (text.Text != "")
                        {
                            _issuValue = Convert.ToInt64(text.Text);
                            Issue = Issue + _issuValue;
                        }
                      
                       
                    }
                }
                else if (DataListIssueUpdate.Visible == true && DataListIssue.Visible == false)
                {

                    foreach (DataListItem items in this.DataListIssueUpdate.Items)
                    {
                        Int64 _issuValue = 0;
                        TextBox text = (items.FindControl("txtStages") as TextBox);
                        if (text.Text == "")
                        {

                        }
                        else if (text.Text != "")
                        {
                            _issuValue = Convert.ToInt64(text.Text);
                        }
                        Issue = Issue + _issuValue;
                    }

                }
                _result = Issue;

            }
            catch (Exception ex)
            {
 
            }
            return _result;

        }

        /*Methos to calculate NOT Issue value*/
        public Int64 _getNotIssueValue()
        {
            Int64 _result = 0, _notIssue = 0;
            try
            {
                if (DataListNotIssue.Visible == true && DataListNotIssueUpdate.Visible == false)
                {
                    foreach (DataListItem items in this.DataListNotIssue.Items)
                    {
                       Int64  _NissuValue = 0;
                        TextBox text = (items.FindControl("txtStages") as TextBox);
                        if (text.Text == "")
                        {

                        }
                        else if (text.Text != "")
                        {
                            _NissuValue = Convert.ToInt64(text.Text);
                             _notIssue = _notIssue + _NissuValue;
                        }
                       
                       
                    }
 
                }
                else if (DataListNotIssueUpdate.Visible == true && DataListNotIssue.Visible == false)
                {
                    foreach (DataListItem items in this.DataListNotIssueUpdate.Items)
                    {
                        Int64 _NissuValue = 0;
                      
                        TextBox text = (items.FindControl("txtStages") as TextBox);
                        if (text.Text == "")
                        {

                        }
                        else if (text.Text != "")
                        {
                            _NissuValue = Convert.ToInt64(text.Text);
                            _notIssue = _notIssue + _NissuValue;
                        }
                      

                    }
                }

                _result = _notIssue;
            }
            catch (Exception ex)
            {
 
            }

            return _result;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("quantity_updation.aspx");
        }


        public Int64 _getUnplanQty()
        {
            Int64 _result = 0;
            try
            {
                foreach (SCHEDULE_DETAIL st in _dbContext.SCHEDULE_DETAILs)
                {
                    if (st.OCNO == txtocno.Text)
                    {
                        _result = Convert.ToInt64(st.UNPLANED_QTY);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return _result;
        }

        public decimal _getDispatchedValue(string _ocno)
        {
           
            var _query = _dbContext.GST_INVs.Where(s => s.oc_no == _ocno).Sum(s => Convert.ToDecimal(s.inv_qty));

            return _query;
        }

        public bool _dispatchExists()
        {
            bool _result = false;
            try
            {
                foreach (GST_INV gi in _dbContext.GST_INVs)
                {
                    if (txtocno.Text == gi.oc_no)
                    {
                        _result = true;
                    }
                }
            }
            catch (Exception ex)
            {
 
            }
            return _result;
        }

    }
}