using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Configuration;
using AlankarNewDesign.DAL;
namespace AlankarNewDesign
{
    public partial class all_oc_entries : System.Web.UI.Page
    {

        
        
        DbConnection obj = new DbConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        string userName = string.Empty;
        string _action = string.Empty;
        alankar_db_providerDataContext _dbContext = new alankar_db_providerDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _getOcEntries();
            }
        }

        protected void lnkStop_Click(object sender, EventArgs e)
        {

        }

        public void _getOcEntries()
        {
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("select  OC_TRANSACTIONS.ID, OC_TRANSACTIONS.OCDT, OC_TRANSACTIONS.OC_NO, Party = OC_TRANSACTIONS.PARTY_CODE+' - '+MASTER_PARTY.PARTY_NAME, OC_TRANSACTIONS.ITEM_CODE, OC_TRANSACTIONS.PONO, OC_TRANSACTIONS.NETPRICE ,OC_TRANSACTIONS.PO_DATE,OC_TRANSACTIONS.OCQTY , OC_TRANSACTIONS.TOOLTYPE, OC_TRANSACTIONS.MATCHTYPE, OC_TRANSACTIONS.FOCNO from OC_TRANSACTIONS inner join MASTER_PARTY on OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE ORDER BY OC_TRANSACTIONS.ID DESC", obj.con);
             
                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                GridOCENTRY.DataSource = ds;
                GridOCENTRY.DataBind();
                if(GridOCENTRY.Rows.Count > 0)
                {
                    GridOCENTRY.UseAccessibleHeader = true;
                    GridOCENTRY.HeaderRow.TableSection = TableRowSection.TableHeader;
                    
                }
                
            }
            catch (Exception ex)
            {
                //Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }

            //commented for load time issue
            //_changelinkbuttonText();
            //_change_code();
            //_changeSkin();

        }

        private void _changeSkin()
        {
            try
            {
                foreach (var _oc in _dbContext.OC_TRANSACTIONs)
                {
                    if (_oc.PONO == "" || _oc.PONO == null)
                    {
                        foreach (GridViewRow _rows in this.GridOCENTRY.Rows)
                        {
                            Label lblOcno = (_rows.FindControl("lblOCNO") as Label);
                            Label lblOcdate = (_rows.FindControl("lblDate") as Label);
                            Label lblcustCode = (_rows.FindControl("lblCustCode") as Label);
                            Label lblItemCode = (_rows.FindControl("lblItemCode") as Label);
                            Label lbltoolType = (_rows.FindControl("lblToolType") as Label);
                            Label lblsubType = (_rows.FindControl("lblSubType") as Label);
                            Label lblqty = (_rows.FindControl("lblQty") as Label);


                            if (_oc.OC_NO == lblOcno.Text)
                            {
                                lblOcno.ForeColor = System.Drawing.Color.Red;
                                lblOcdate.ForeColor = System.Drawing.Color.Red;
                                lblcustCode.ForeColor = System.Drawing.Color.Red;
                                lblItemCode.ForeColor = System.Drawing.Color.Red;
                                lbltoolType.ForeColor = System.Drawing.Color.Red;
                                lblsubType.ForeColor = System.Drawing.Color.Red;
                                lblqty.ForeColor = System.Drawing.Color.Red;
                            }
                            foreach (TableCell cell in _rows.Cells)
                            {
                                cell.Attributes.CssStyle["text-align"] = "center";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
 
            }
        }

        public void _changelinkbuttonText()
        {
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("Select OC_NO from OC_TRANSACTIONS where STATUS = 0", obj.con);
                obj.con.Open();
                SqlDataReader rdr = obj.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string ocno = string.Empty;
                    string _matchValue = rdr[0].ToString();
                    foreach (GridViewRow gvr in GridOCENTRY.Rows)
                    {

                        ocno = gvr.Cells[1].Text;

                        if (_matchValue == ocno)
                        {
                            LinkButton lnk = gvr.FindControl("lnkStop") as LinkButton;
                            lnk.Text = "STOP";
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

        public void _change_code()
        {
            try
            {

                foreach (GridViewRow _row in this.GridOCENTRY.Rows)
                {
                    Label _ocNO = (_row.FindControl("lblOCNO") as Label);
                    Label _date = (_row.FindControl("lblDate") as Label);
                    Label _custCode = (_row.FindControl("lblCustCode") as Label);
                    Label _itemCode = (_row.FindControl("lblItemCode") as Label);
                    Label _toolType = (_row.FindControl("lblToolType") as Label);
                    Label _subtype = (_row.FindControl("lblSubType") as Label);
                    Label _qty = (_row.FindControl("lblQty") as Label);




                    foreach (OC_TRANSACTION _ocTran in _dbContext.OC_TRANSACTIONs)
                    {
                        if (_ocTran.OC_NO == _ocNO.Text && _ocTran.STATUS == 0)
                        {
                            _ocNO.ForeColor = System.Drawing.Color.Red;
                            _date.ForeColor = System.Drawing.Color.Red;
                            _custCode.ForeColor = System.Drawing.Color.Red;
                            _itemCode.ForeColor = System.Drawing.Color.Red;
                            _toolType.ForeColor = System.Drawing.Color.Red;
                            _subtype.ForeColor = System.Drawing.Color.Red;
                            _qty.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnStop_Click(object sender, EventArgs e)
        {
            string _ocno = (sender as LinkButton).CommandArgument;
            foreach (GridViewRow gvr in GridOCENTRY.Rows)
            {
                string _matchValue = gvr.Cells[1].Text;
                if (_ocno == _matchValue)
                {
                    LinkButton lnk = gvr.FindControl("lnkStop") as LinkButton;

                    string _controlText = lnk.Text;

                    if (_controlText == "STOP")
                    {
                        txtOcStopId.Text = _ocno;

                        _getOcEntries();
                        STOP_POPUP.Show();
                    }
                    else if (_controlText == "RESTART")
                    {
                        Int64 _stopId = obj._getMaxId("ID", "OC_ACTIONS");
                        _StopOc(_stopId, _ocno, "INSERT", "RESTART", 1);
                            lblMessage.Visible = true;

                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                       
                        lblMessage.Text = _ocno + ": Restart Successfully..!";

                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        _getOcEntries();
                    }
                }
            
            }}

             public void _StopOc(Int64 _id, string _ocno, string _action, string _status, int _ocTranactionStatus)
        {
            string _userName = Session["username"].ToString();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SP_OC_ACTIONS_TRANSACTIONS_FUNCTIONS", obj.con);
                obj.cmd.CommandType = CommandType.StoredProcedure;
                obj.cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = _id;
                obj.cmd.Parameters.Add("@OCNO", SqlDbType.NVarChar).Value = _ocno;
                obj.cmd.Parameters.Add("@REMARKS", SqlDbType.NVarChar).Value = txtStopOcRemarks.Text;
                obj.cmd.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = _status;
                obj.cmd.Parameters.Add("@CREATED_BY", SqlDbType.NVarChar).Value = _userName;
                obj.cmd.Parameters.Add("@CREATED_ON", SqlDbType.Date).Value = null;
                obj.cmd.Parameters.Add("@MODIFIED_BY", SqlDbType.NVarChar).Value = _userName;
                obj.cmd.Parameters.Add("@MODIFIED_ON", SqlDbType.Date).Value = null;
                obj.cmd.Parameters.Add("@ACTION", SqlDbType.NVarChar).Value = _action;
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


            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("UPDATE OC_TRANSACTIONS SET STATUS = '" + _ocTranactionStatus + "' WHERE OC_NO = '" + _ocno + "'", obj.con);
                obj.con.Open();
                obj.cmd.ExecuteNonQuery();
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

             protected void lnkDelete_Click(object sender, EventArgs e)
             {
               //  bool _StExists = false;
                 bool _invExists = false;
                 string _ocNo = (sender as LinkButton).CommandArgument;

                 bool ExistsInStage = obj._checkIsExists("Select * from STAGE_TRANSACTIONS where OC_NO = '"+_ocNo+"'");
                 if (ExistsInStage == true)
                 {
                     Response.Write("<script>alert('This oc is used in Quantity Updation ..!')</script>");
                     return;
                 }

                 //foreach (var _st in _dbContext.STAGE_TRANSACTIONs)
                 //{
                 //    if (_st.OC_NO == _ocNo)
                 //    {
                 //        if (_st.STAGE_TYPE == "Issue")
                 //        {
                 //            _StExists = true;
                 //            break;
 
                 //        }
                       
                 //    }
                 //}


                 foreach (var inv in _dbContext.GST_INVs)
                 {
                     if (inv.oc_no == _ocNo)
                     {
                         _invExists = true;
                         break;
                     }
                 }



                 if (_invExists == true)
                 {
                     Response.Write("<script>alert('This oc is used in invoice..!')</script>");
                 }
                 else
                 {
                     OC_TRANSACTION oc = _dbContext.OC_TRANSACTIONs.Single(s => s.OC_NO == _ocNo);

                     _dbContext.OC_TRANSACTIONs.DeleteOnSubmit(oc);
                     _dbContext.SubmitChanges();
                     Response.Write("<script>alert('Deleted Successfully..!')</script>");

                     _getOcEntries();
                 }

                 
             }

             protected void lnkClose_Click(object sender, EventArgs e)
             {
                 string _ocNo = Convert.ToString((sender as LinkButton).CommandArgument);

                 var _oc = _dbContext.OC_TRANSACTIONs.SingleOrDefault(x => x.OC_NO == _ocNo);

                 _oc.STATUS = 0;

                 _dbContext.SubmitChanges();



             }


             [System.Web.Services.WebMethod]
             public static string CloseOc(string OcNo)
             {
                 string Result = string.Empty;
                 SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D__DEVELOPMENT_SOFTWARE_UPDATED_DB_ALANKAR_DB_MDFConnectionString"].ConnectionString);
                 SqlCommand cmd;
                 try
                 {
                     cmd = new SqlCommand("UPDATE OC_TRANSACTIONS SET STATUS = 0 WHERE OC_NO = @OCNO", con);
                     cmd.CommandType = CommandType.Text;
                     cmd.Parameters.AddWithValue("@OCNO", OcNo);
                     con.Open();
                     cmd.ExecuteNonQuery();
                     Result = "True";
                    
                 }
                 catch (Exception Ex)
                 {
                     Result = Ex.Message.ToString();
                 }
                 finally
                 {
                     con.Close();
                    
                 }

                 return Result;
             }




             [System.Web.Services.WebMethod]
             public static string StartOc(string OcNo)
             {
                 string Result = string.Empty;
                 SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D__DEVELOPMENT_SOFTWARE_UPDATED_DB_ALANKAR_DB_MDFConnectionString"].ConnectionString);
                 SqlCommand cmd;
                 try
                 {
                     cmd = new SqlCommand("UPDATE OC_TRANSACTIONS SET STATUS = 1 WHERE OC_NO = @OCNO", con);
                     cmd.CommandType = CommandType.Text;
                     cmd.Parameters.AddWithValue("@OCNO", OcNo);
                     con.Open();
                     cmd.ExecuteNonQuery();
                     Result = "True";

                 }
                 catch (Exception Ex)
                 {
                     Result = Ex.Message.ToString();
                 }
                 finally
                 {
                     con.Close();

                 }

                 return Result;
             }


        }
    }
