using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlankarNewDesign
{
    public partial class NewBookingRpt : System.Web.UI.Page
    {
        private DbConnection _generic;

        public NewBookingRpt()
        {
            _generic = new DbConnection();
        }


        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Qry = string.Empty;
                Qry = "declare @OcQty as decimal(18,2) = (select count(OC_TRANSACTIONS.OC_NO) from OC_TRANSACTIONS) declare @totQty as decimal(18,2) = (select sum(OC_TRANSACTIONS.OCQTY) from OC_TRANSACTIONS ) declare @OcVal as decimal(18,2) = (select sum(OC_TRANSACTIONS.OCQTY * OC_TRANSACTIONS.NETPRICE) from OC_TRANSACTIONS where OC_TRANSACTIONS.OCQTY > 0) Select distinct(OC_TRANSACTIONS.PARTY_CODE) as [Customer Code], MASTER_PARTY.PARTY_NAME as [CUSTOMER] , count(OC_TRANSACTIONS.OC_NO)  as [NoofOC], (count(OC_TRANSACTIONS.OC_NO)/ (@OcQty) * 100) as [NoofOCper], sum(OC_TRANSACTIONS.OCQTY) as [OCQTY], ((sum(OC_TRANSACTIONS.OCQTY))/(@totQty) * 100) as [OCQTYper], (SUM(OC_TRANSACTIONS.OCQTY * OC_TRANSACTIONS.NETPRICE))	 as [OCVALUE], ( SUM(OC_TRANSACTIONS.OCQTY * OC_TRANSACTIONS.NETPRICE)) / @OcVal *100 as [OCVALper] from OC_TRANSACTIONS inner join MASTER_PARTY  on OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE where  OC_TRANSACTIONS.OCQTY > 0 Group by OC_TRANSACTIONS.PARTY_CODE, PARTY_NAME";
                _generic._fillGrid(Qry, GridBooking);

                if(GridBooking.Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt = _generic.dataTable(Qry);

                    decimal _nofoc = 0, _nofocper = 0, _ocqty = 0, _ocqtyper = 0, _ocVal = 0, _ocValPer = 0;
                    object NoOfOc, NoOfOcPer, OcQty, Ocper, OcVal, OcValPer;
                    NoOfOc = dt.Compute("Sum(NoofOC)", string.Empty);
                    NoOfOcPer = dt.Compute("Sum(NoofOCper)", string.Empty);
                    OcQty = dt.Compute("Sum(OCQTY)", string.Empty);
                    Ocper = dt.Compute("Sum(OCQTYper)", string.Empty);
                    OcVal = dt.Compute("Sum(OCVALUE)", string.Empty);
                    OcValPer = dt.Compute("Sum(OCVALper)", string.Empty);
                    _nofoc = Convert.ToDecimal(NoOfOc);
                    _nofocper = Convert.ToDecimal(NoOfOcPer);
                    _ocqty = Convert.ToDecimal(OcQty);
                    _ocqtyper = Convert.ToDecimal(Ocper);
                    _ocVal = Convert.ToDecimal(OcVal);
                    _ocValPer = Convert.ToDecimal(OcValPer);
                    GridBooking.FooterRow.Font.Bold = true;
                    GridBooking.FooterRow.Cells[0].Text = "Total";
                    GridBooking.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                    GridBooking.FooterRow.Cells[2].Text = _nofoc.ToString("N2");
                    GridBooking.FooterRow.Cells[3].Text = _nofocper.ToString("N2");
                    GridBooking.FooterRow.Cells[4].Text = _ocqty.ToString("N2");
                    GridBooking.FooterRow.Cells[5].Text = _ocqtyper.ToString("N2");
                    GridBooking.FooterRow.Cells[6].Text = _ocVal.ToString("N2");
                    GridBooking.FooterRow.Cells[7].Text = _ocValPer.ToString("N2");
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string Qry = string.Empty;
            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                //where Convert(datetime,OCDT,103) between '2019-08-01' and '2019-08-09'

                Qry = "declare @OcQty as decimal(18,2) = (select count(OC_TRANSACTIONS.OC_NO) from OC_TRANSACTIONS where Convert(datetime,OCDT,103) between '"+ txtFromDate.Text +"' and '"+ txtToDate.Text+"') declare @totQty as decimal(18,2) = (select sum(OC_TRANSACTIONS.OCQTY) from OC_TRANSACTIONS where Convert(datetime,OCDT,103) between '"+ txtFromDate.Text +"' and '"+ txtToDate.Text +"') declare @OcVal as decimal(18,2) = (select sum(OC_TRANSACTIONS.OCQTY * OC_TRANSACTIONS.NETPRICE) from OC_TRANSACTIONS where OC_TRANSACTIONS.OCQTY > 0 and Convert(datetime,OCDT,103) between '"+ txtFromDate.Text +"' and '"+ txtToDate.Text +"') Select distinct(OC_TRANSACTIONS.PARTY_CODE) as [Customer Code], MASTER_PARTY.PARTY_NAME as [CUSTOMER] , count(OC_TRANSACTIONS.OC_NO)  as [NoofOC],  (count(OC_TRANSACTIONS.OC_NO)/ (@OcQty) * 100) as [NoofOCper]  ,sum(OC_TRANSACTIONS.OCQTY) as [OCQTY], ((sum(OC_TRANSACTIONS.OCQTY))/(@totQty) * 100) as [OCQTYper], (SUM(OC_TRANSACTIONS.OCQTY * OC_TRANSACTIONS.NETPRICE))	 as [OCVALUE],	( SUM(OC_TRANSACTIONS.OCQTY * OC_TRANSACTIONS.NETPRICE)) / @OcVal *100 as [OCVALper]	 from OC_TRANSACTIONS inner join MASTER_PARTY  on OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE where Convert(datetime,OCDT,103) between '"+ txtFromDate.Text +"' and '"+ txtToDate.Text +"' and OC_TRANSACTIONS.OCQTY > 0 Group by OC_TRANSACTIONS.PARTY_CODE, PARTY_NAME";
                _generic._fillGrid(Qry, GridBooking);
            }
            else
            {
                Qry = "declare @OcQty as decimal(18,2) = (select count(OC_TRANSACTIONS.OC_NO) from OC_TRANSACTIONS) declare @totQty as decimal(18,2) = (select sum(OC_TRANSACTIONS.OCQTY) from OC_TRANSACTIONS ) declare @OcVal as decimal(18,2) = (select sum(OC_TRANSACTIONS.OCQTY * OC_TRANSACTIONS.NETPRICE) from OC_TRANSACTIONS where OC_TRANSACTIONS.OCQTY > 0) Select distinct(OC_TRANSACTIONS.PARTY_CODE) as [Customer Code], MASTER_PARTY.PARTY_NAME as [CUSTOMER] , count(OC_TRANSACTIONS.OC_NO)  as [NoofOC], (count(OC_TRANSACTIONS.OC_NO)/ (@OcQty) * 100) as [NoofOCper], sum(OC_TRANSACTIONS.OCQTY) as [OCQTY], ((sum(OC_TRANSACTIONS.OCQTY))/(@totQty) * 100) as [OCQTYper], (SUM(OC_TRANSACTIONS.OCQTY * OC_TRANSACTIONS.NETPRICE))	 as [OCVALUE], ( SUM(OC_TRANSACTIONS.OCQTY * OC_TRANSACTIONS.NETPRICE)) / @OcVal *100 as [OCVALper] from OC_TRANSACTIONS inner join MASTER_PARTY  on OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE where  OC_TRANSACTIONS.OCQTY > 0 Group by OC_TRANSACTIONS.PARTY_CODE, PARTY_NAME";
                _generic._fillGrid(Qry, GridBooking);

            }

            if (GridBooking.Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = _generic.dataTable(Qry);

                decimal _nofoc = 0, _nofocper = 0, _ocqty = 0, _ocqtyper = 0, _ocVal = 0, _ocValPer = 0;
                object NoOfOc, NoOfOcPer, OcQty, Ocper, OcVal, OcValPer;
                NoOfOc = dt.Compute("Sum(NoofOC)", string.Empty);
                NoOfOcPer = dt.Compute("Sum(NoofOCper)", string.Empty);
                OcQty = dt.Compute("Sum(OCQTY)", string.Empty);
                Ocper = dt.Compute("Sum(OCQTYper)", string.Empty);
                OcVal = dt.Compute("Sum(OCVALUE)", string.Empty);
                OcValPer = dt.Compute("Sum(OCVALper)", string.Empty);
                _nofoc = Convert.ToDecimal(NoOfOc);
                _nofocper = Convert.ToDecimal(NoOfOcPer);
                _ocqty = Convert.ToDecimal(OcQty);
                _ocqtyper = Convert.ToDecimal(Ocper);
                _ocVal = Convert.ToDecimal(OcVal);
                _ocValPer = Convert.ToDecimal(OcValPer);
                GridBooking.FooterRow.Font.Bold = true;
                GridBooking.FooterRow.Cells[0].Text = "Total";
                GridBooking.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                GridBooking.FooterRow.Cells[2].Text = _nofoc.ToString("N2");
                GridBooking.FooterRow.Cells[3].Text = _nofocper.ToString("N2");
                GridBooking.FooterRow.Cells[4].Text = _ocqty.ToString("N2");
                GridBooking.FooterRow.Cells[5].Text = _ocqtyper.ToString("N2");
                GridBooking.FooterRow.Cells[6].Text = _ocVal.ToString("N2");
                GridBooking.FooterRow.Cells[7].Text = _ocValPer.ToString("N2");
            }

        }

        protected void lnknxt_Click(object sender, EventArgs e)
        {
            var cmdArg = (sender as LinkButton).CommandArgument;
            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                //var s = System.Web.HttpUtility.UrlEncode("BookingRptPart2.aspx?var1=" + cmdArg + "&var2=" + txtFromDate.Text + "&var3=" + txtToDate.Text + "");
                Response.Redirect("BookingRptPart2.aspx?var1=" + cmdArg + "&var2=" + txtFromDate.Text + "&var3=" + txtToDate.Text + "");
            }
            else
            {
                //var s = System.Web.HttpUtility.UrlEncode("BookingRptPart2.aspx?var1=" + cmdArg + "&var2=null&var3=null");
                Response.Redirect("BookingRptPart2.aspx?var1=" + cmdArg + "&var2=null&var3=null");
            }
        }
    }
}