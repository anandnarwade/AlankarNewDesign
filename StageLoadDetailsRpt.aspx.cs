using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;
using AlankarNewDesign.DAL;
using System.Data.SqlClient;
using System.Data;
namespace AlankarNewDesign
{
    public partial class StageLoadDetailsRpt : System.Web.UI.Page
    {
        alankar_db_providerDataContext _db = new alankar_db_providerDataContext();
        DbConnection Generic = new DbConnection();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null && Session["password"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                Int64 _tool = Convert.ToInt64(Request.QueryString["Tool"]);
                string _Stage = Request.QueryString["Stage"].ToString();

                if (_Stage == "T ")
                {
                    _Stage = "T & C";
                }

              


                if (!IsPostBack)
                {
                    string _query = "";
                    MASTER_TOOL_TYPE _t = null;
                    //var _t = _db.MASTER_TOOL_TYPEs.Single(s => s.ID == _tool);

                    if (_tool == 0 && _Stage != "0")
                    {
                        
                        _query = "SELECT STAGE_TRANSACTIONS.OC_NO,  OC_TRANSACTIONS.FOCNO as [First OC], MASTER_PARTY.PARTY_NAME, STAGE_TRANSACTIONS.STAGE_TYPE, STAGE_TRANSACTIONS.STAGE, QUANTITY = STAGE_TRANSACTIONS.VALUE , TOTAL_VALUE = (STAGE_TRANSACTIONS.VALUE * OC_TRANSACTIONS.GRPPRICE) FROM STAGE_TRANSACTIONS INNER JOIN OC_TRANSACTIONS ON STAGE_TRANSACTIONS.OC_NO = OC_TRANSACTIONS.OC_NO INNER JOIN MASTER_PARTY ON OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE WHERE STAGE_TYPE = 'Issue' AND STAGE = '" + _Stage + "' AND  STAGE_TRANSACTIONS.VALUE !=0";
                    }
                    else if (_Stage == "0" && _tool != 0)
                    {
                        _t = _db.MASTER_TOOL_TYPEs.Single(s => s.ID == _tool);
                        _query = "SELECT STAGE_TRANSACTIONS.OC_NO,  OC_TRANSACTIONS.FOCNO as [First OC], MASTER_PARTY.PARTY_NAME, STAGE_TRANSACTIONS.STAGE_TYPE, STAGE_TRANSACTIONS.STAGE, QUANTITY = STAGE_TRANSACTIONS.VALUE , TOTAL_VALUE = (STAGE_TRANSACTIONS.VALUE * OC_TRANSACTIONS.GRPPRICE) FROM STAGE_TRANSACTIONS INNER JOIN OC_TRANSACTIONS ON STAGE_TRANSACTIONS.OC_NO = OC_TRANSACTIONS.OC_NO INNER JOIN MASTER_PARTY ON OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE WHERE STAGE_TYPE = 'Issue' AND  OC_TRANSACTIONS.TOOLTYPE = '" + _t.MASTER_TOOL_TYPE1 + "' AND STAGE_TRANSACTIONS.VALUE !=0";
                    }
                    else if ( _Stage == "0" && _tool == 0 ){

                        _query = "SELECT STAGE_TRANSACTIONS.OC_NO,  OC_TRANSACTIONS.FOCNO as [First OC], MASTER_PARTY.PARTY_NAME, STAGE_TRANSACTIONS.STAGE_TYPE, STAGE_TRANSACTIONS.STAGE, QUANTITY = STAGE_TRANSACTIONS.VALUE , TOTAL_VALUE = (STAGE_TRANSACTIONS.VALUE * OC_TRANSACTIONS.GRPPRICE) FROM STAGE_TRANSACTIONS INNER JOIN OC_TRANSACTIONS ON STAGE_TRANSACTIONS.OC_NO = OC_TRANSACTIONS.OC_NO INNER JOIN MASTER_PARTY ON OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE WHERE STAGE_TYPE = 'Issue' AND STAGE_TRANSACTIONS.VALUE !=0";
                    }else{

                        _t =  _db.MASTER_TOOL_TYPEs.Single(s => s.ID == _tool);
                        _query = "SELECT STAGE_TRANSACTIONS.OC_NO,  OC_TRANSACTIONS.FOCNO as [First OC], MASTER_PARTY.PARTY_NAME, STAGE_TRANSACTIONS.STAGE_TYPE, STAGE_TRANSACTIONS.STAGE, QUANTITY = STAGE_TRANSACTIONS.VALUE , TOTAL_VALUE = (STAGE_TRANSACTIONS.VALUE * OC_TRANSACTIONS.GRPPRICE) FROM STAGE_TRANSACTIONS INNER JOIN OC_TRANSACTIONS ON STAGE_TRANSACTIONS.OC_NO = OC_TRANSACTIONS.OC_NO INNER JOIN MASTER_PARTY ON OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE WHERE STAGE_TYPE = 'Issue' AND STAGE = '" + _Stage + "' AND OC_TRANSACTIONS.TOOLTYPE = '" + _t.MASTER_TOOL_TYPE1 + "' AND STAGE_TRANSACTIONS.VALUE !=0";
                    }
                    
                    DataTable dt = new DataTable();
                    dt = Generic.dataTable(_query);

                    Generic._fillGrid(_query, Grid);
                    if(Grid.Rows.Count > 0)
                    {
                       // decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("TOTAL_VALUE"));
                        object totalQty;
                        totalQty = dt.Compute("Sum(QUANTITY)", string.Empty);
                        object sumObject;
                        sumObject = dt.Compute("Sum(TOTAL_VALUE)", string.Empty);
                        decimal total = Convert.ToDecimal(sumObject);
                        Grid.FooterRow.Font.Bold = true;
                        Grid.FooterRow.Cells[0].Text = "Total";
                        Grid.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                        Grid.FooterRow.Cells[6].Text = totalQty.ToString();
                        Grid.FooterRow.Cells[7].Text = total.ToString("N2");
                    }
                    //_fillGridBySp(_Stage, _t.MASTER_TOOL_TYPE1);
                    //ISingleResult<SP_STAGE_LOAD_RPTResult> tab = _db.SP_STAGE_LOAD_RPT(_Stage, _t.MASTER_TOOL_TYPE1);
                    //Grid.DataSource = tab;
                    //Grid.DataBind();

                    string _toolName = "";
                    try
                    {
                        _toolName = _t.MASTER_TOOL_TYPE1;
                    }
                    catch (Exception)
                    {

                        _toolName = "All";
                    }

                    if (_Stage == "0") { _Stage = "All"; }
                    lblDesc.Text = " <b>Stage</b> - " + _Stage + " <b>Tool Type </b>- " + _toolName;
                }
            }
        }


        private void _fillGridBySp(string _Stage, string _toolType)
        {
       
            try
            {
               Generic._getConnection();
               Generic.cmd = new SqlCommand("SP_STAGE_LOAD_RPT", Generic.con);
               Generic.cmd.CommandType = CommandType.StoredProcedure;
               Generic.cmd.Parameters.AddWithValue("@STAGE", _Stage);
               Generic.cmd.Parameters.AddWithValue("@TOOLTYPE", _toolType);
                SqlDataAdapter   _da = new SqlDataAdapter();
                //DataSet ds = new DataSet();
                DataTable ds = new DataTable();
                Generic.con.Open();
                _da.SelectCommand = Generic.cmd;
                _da.Fill(ds);
                Grid.DataSource = ds;
                Grid.DataBind();
                if (Grid.Rows.Count > 0)
                {

                   
                    Grid.FooterRow.Font.Bold = true;
                    Grid.UseAccessibleHeader = true;
                    Grid.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Generic.con.Close();
                Generic.cmd.Dispose();
            }
        }
        

       
    }
}