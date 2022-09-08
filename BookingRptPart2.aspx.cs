using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlankarNewDesign
{
    public partial class BookingRptPart2 : System.Web.UI.Page
    {
        private DbConnection _generic;

        public BookingRptPart2()
        {
            _generic = new DbConnection();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string Qry = string.Empty;

                var CustCode = Request.QueryString["var1"].ToString();
                var fromDate = Request.QueryString["var2"].ToString();
                var toDate = Request.QueryString["var3"].ToString();

                


                if(fromDate != "null" && toDate != "null")
                {
                    Qry = "declare @OcQty as decimal(18,2) = (select count(OC_TRANSACTIONS.OC_NO) from OC_TRANSACTIONS where Convert(datetime,OCDT,103) between '" + fromDate + "' and '" + toDate + "' and OC_TRANSACTIONS.PARTY_CODE = '" + CustCode + "')  declare @totQty as decimal(18,2) = (select sum(OC_TRANSACTIONS.OCQTY) from OC_TRANSACTIONS where Convert(datetime,OCDT,103) between '" + fromDate + "' and '" + toDate + "' and OC_TRANSACTIONS.PARTY_CODE = '" + CustCode + "') declare @OcVal as decimal(18,2) = (select sum(OC_TRANSACTIONS.OCQTY * OC_TRANSACTIONS.NETPRICE) from OC_TRANSACTIONS where OC_TRANSACTIONS.OCQTY > 0 and Convert(datetime,OCDT,103) between '" + fromDate + "' and '" + toDate + "' and OC_TRANSACTIONS.PARTY_CODE ='" + CustCode + "') Select OC_TRANSACTIONS.OC_NO,OC_TRANSACTIONS.PARTY_CODE as [Customer Code], MASTER_PARTY.PARTY_NAME as [CUSTOMER] , count(OC_TRANSACTIONS.OC_NO)  as [No of OC],  (count(OC_TRANSACTIONS.OC_NO)/ (@OcQty) * 100) as [No of OC%], sum(OC_TRANSACTIONS.OCQTY) as [OC QTY],  ((sum(OC_TRANSACTIONS.OCQTY))/(@totQty) * 100) as [OC QTY %], (SUM(OC_TRANSACTIONS.OCQTY * OC_TRANSACTIONS.NETPRICE))	 as [OC VALUE],	( SUM(OC_TRANSACTIONS.OCQTY * OC_TRANSACTIONS.NETPRICE)) / @OcVal *100 as [OC VAL %]	 from OC_TRANSACTIONS inner join MASTER_PARTY  on OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE where Convert(datetime,OCDT,103) between '" + fromDate + "' and '" + toDate + "' and OC_TRANSACTIONS.OCQTY > 0 and OC_TRANSACTIONS.PARTY_CODE = '" + CustCode + "' Group by OC_TRANSACTIONS.PARTY_CODE, PARTY_NAME, OC_TRANSACTIONS.OC_NO";
                }
                else
                {
                    Qry = "declare @OcQty as decimal(18,2) = (select count(OC_TRANSACTIONS.OC_NO) from OC_TRANSACTIONS where  OC_TRANSACTIONS.PARTY_CODE = '" + CustCode + "')  declare @totQty as decimal(18,2) = (select sum(OC_TRANSACTIONS.OCQTY) from OC_TRANSACTIONS where  OC_TRANSACTIONS.PARTY_CODE = '" + CustCode + "') declare @OcVal as decimal(18,2) = (select sum(OC_TRANSACTIONS.OCQTY * OC_TRANSACTIONS.NETPRICE) from OC_TRANSACTIONS where OC_TRANSACTIONS.OCQTY > 0  and OC_TRANSACTIONS.PARTY_CODE ='" + CustCode + "') Select OC_TRANSACTIONS.OC_NO,OC_TRANSACTIONS.PARTY_CODE as [Customer Code], MASTER_PARTY.PARTY_NAME as [CUSTOMER] , count(OC_TRANSACTIONS.OC_NO)  as [No of OC],  (count(OC_TRANSACTIONS.OC_NO)/ (@OcQty) * 100) as [No of OC%], sum(OC_TRANSACTIONS.OCQTY) as [OC QTY],  ((sum(OC_TRANSACTIONS.OCQTY))/(@totQty) * 100) as [OC QTY %], (SUM(OC_TRANSACTIONS.OCQTY * OC_TRANSACTIONS.NETPRICE))	 as [OC VALUE],	( SUM(OC_TRANSACTIONS.OCQTY * OC_TRANSACTIONS.NETPRICE)) / @OcVal *100 as [OC VAL %]	 from OC_TRANSACTIONS inner join MASTER_PARTY  on OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE where  OC_TRANSACTIONS.OCQTY > 0 and OC_TRANSACTIONS.PARTY_CODE = '" + CustCode + "' Group by OC_TRANSACTIONS.PARTY_CODE, PARTY_NAME, OC_TRANSACTIONS.OC_NO";
                }


                _generic._fillGrid(Qry, GridBooking);


                lblCust.Text = _generic.GetStr("Select PARTY_NAME from MASTER_PARTY where PARTY_CODE = '" + CustCode + "'");
                lblDate.Text = fromDate +" - "+toDate;
            }

        }
    }
}