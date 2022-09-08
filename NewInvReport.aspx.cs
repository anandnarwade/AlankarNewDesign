using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AlankarNewDesign
{
    public partial class NewInvReport : System.Web.UI.Page
    {

        public DataTable dt;
        public static DataTable invDt;
        decimal NoOfInvF = 0, NoOfInvPerF = 0, InvValF = 0, InvValPerF= 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                _fillGrid();

            }
        }

        public void _fillGrid()
        {
            string Qry = "";
            string _fromDate = "", _toDate = "";

            _fromDate = txtDateFrom.Text;
            _toDate = txtToDate.Text;
           

            if(_fromDate != "" && _toDate != "")
            {
                Qry = "SELECT  DISTINCT(OC.PARTY_CODE), (SELECT PARTY_NAME  FROM MASTER_PARTY WHERE MASTER_PARTY.PARTY_CODE = OC.PARTY_CODE) AS [CUSTOMER], (SELECT COUNT(GST_INV.inv_no) FROM GST_INV WHERE GST_INV.oc_no IN (SELECT OC_NO FROM OC_TRANSACTIONS WHERE PARTY_CODE = OC.PARTY_CODE  AND CONVERT(DATE, OCDT, 103) > '" + _fromDate + "' and CONVERT(DATE, OCDT, 103) < '" + _toDate + "'  )) AS [INVOICES], (SELECT SUM(CAST(GST_INV.total_amount AS DECIMAL(18,2))) FROM GST_INV WHERE GST_INV.oc_no IN (SELECT OC_NO FROM OC_TRANSACTIONS WHERE PARTY_CODE = OC.PARTY_CODE AND CONVERT(DATE, OCDT, 103) > '"+ txtDateFrom.Text +"' and CONVERT(DATE, OCDT, 103) < '"+ txtToDate.Text +"'  )) AS [INVAL] FROM OC_TRANSACTIONS AS [OC] WHERE OC.PARTY_CODE != '' and  CONVERT(DATE, OCDT, 103) > '" + _fromDate + "' and  CONVERT(DATE, OCDT, 103) < '" + _toDate + "' GROUP BY OC.OC_NO , OC.PARTY_CODE";

            }
            else
            {
                Qry = "SELECT  DISTINCT(OC.PARTY_CODE), (SELECT PARTY_NAME  FROM MASTER_PARTY WHERE MASTER_PARTY.PARTY_CODE = OC.PARTY_CODE) AS [CUSTOMER], (SELECT COUNT(GST_INV.inv_no) FROM GST_INV WHERE GST_INV.oc_no IN (SELECT OC_NO FROM OC_TRANSACTIONS WHERE PARTY_CODE = OC.PARTY_CODE  )) AS [INVOICES], (SELECT SUM(CAST(GST_INV.total_amount AS DECIMAL(18,2))) FROM GST_INV WHERE GST_INV.oc_no IN (SELECT OC_NO FROM OC_TRANSACTIONS WHERE PARTY_CODE = OC.PARTY_CODE)) AS [INVAL] FROM OC_TRANSACTIONS AS [OC] WHERE OC.PARTY_CODE != '' GROUP BY OC.OC_NO , OC.PARTY_CODE";
            }

            DbConnection con = new DbConnection();
            dt = con.dataTable(Qry);
            con._fillGrid(Qry, Grid);

            if(Grid.Rows.Count > 0)
            {
                Grid.UseAccessibleHeader = true;
                Grid.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
           
         
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            _fillGrid();
        }

        protected void Grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblInv, lblInvPer, lblInVal, lblInValPer;
                lblInv = e.Row.FindControl("lblInv") as Label;
                lblInvPer = e.Row.FindControl("lblInvPer") as Label;
                lblInVal = e.Row.FindControl("lblInVal") as Label;
                lblInValPer = e.Row.FindControl("lblInValPer") as Label;

                decimal Inv, InvPer, InVal, InValPer, InvTot, ValTot;

                Inv = Convert.ToDecimal(lblInv.Text);
                

                if(Inv > 0)
                {
                    InVal = Convert.ToDecimal(lblInVal.Text);
                    InvTot = Convert.ToDecimal(dt.Compute("sum(INVOICES)", "").ToString());
                    ValTot = Convert.ToDecimal(dt.Compute("sum(INVAL)", "").ToString());
                    InvPer = (Inv / InvTot) * 100;
                    InValPer = (InVal / ValTot) * 100;
                    lblInvPer.Text = Math.Round(InvPer, 2).ToString();
                    lblInValPer.Text = Math.Round(InValPer, 2).ToString();

                    NoOfInvPerF = (NoOfInvPerF + InvPer);

                    InvValPerF = (InvValPerF + InValPer);   
                }

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                decimal Inv, InvPer, InVal, InValPer, InvTot, ValTot;
               Label lblNoOfInv = e.Row.FindControl("lblNoOfInv") as Label;
               Label lblNoOfInvPerF = e.Row.FindControl("lblNoOfInvPerF") as Label;
               Label lblInvValF = e.Row.FindControl("lblInvValF") as Label;
               Label lblInValPerF = e.Row.FindControl("lblInValPerF") as Label;
               InvTot = Convert.ToDecimal(dt.Compute("sum(INVOICES)", "").ToString());
               ValTot = Convert.ToDecimal(dt.Compute("sum(INVAL)", "").ToString());

               lblNoOfInv.Text = InvTot.ToString("N2");
               lblNoOfInvPerF.Text = NoOfInvPerF.ToString("N2");


               lblInvValF.Text = ValTot.ToString("N2");
               lblInValPerF.Text = InvValPerF.ToString("N2");

            }
        }

        protected void lnkCust_Click(object sender, EventArgs e)
        {
            string _custCode = (sender as LinkButton).CommandArgument;
            string query = string.Empty;
            if(txtDateFrom.Text != "" && txtToDate.Text != "")
            {
                query = "Select inv_no, inv_date, oc_no, inv_qty, total_amount from GST_INV where oc_no in (Select OC_NO from OC_TRANSACTIONS where PARTY_CODE = '" + _custCode + "' and  CONVERT(DATE, OCDT, 103) > '" + txtDateFrom.Text + "' and  CONVERT(DATE, OCDT, 103) < '" + txtToDate.Text + "')";
            }
            else
            {
                query = "Select inv_no, inv_date, oc_no, inv_qty, total_amount from GST_INV where oc_no in (Select OC_NO from OC_TRANSACTIONS where PARTY_CODE = '" + _custCode + "')";
            }
             
            DbConnection con = new DbConnection();
          //  if (invDt.Rows.Count > 0) { invDt.Clear(); }
            invDt = con.dataTable(query);
            Response.Redirect("NewInvReportDetail.aspx");
           
        }
    }
}