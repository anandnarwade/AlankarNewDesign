using AlankarNewDesign.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlankarNewDesign
{
    public partial class NewItemCodeRpt : System.Web.UI.Page
    {

        alankar_db_providerDataContext _db = new alankar_db_providerDataContext();
        DbConnection Generic = new DbConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _fillCombo();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int i = 0;
            i = ddmRptType.SelectedIndex;
            string qry = string.Empty;

            if (i > 0)
            {

                if(i ==1)
                {
                    qry = "Select OC_NO , FOCNO, OCDT as [OC DATE], DESC1 AS [DESCRIPTION] , OCQTY AS [QTY], NETPRICE AS [RATE], (NETPRICE * OCQTY) AS [TOTAL] from OC_TRANSACTIONS where OC_TRANSACTIONS.PARTY_CODE = '" + ComboBoxParty.SelectedValue + "' and OC_TRANSACTIONS.ITEM_CODE = '" + ComboItemCode.SelectedValue + "'";
                }
                else if(i ==2) {
                    qry = "SELECT OC.OC_NO, OC.FOCNO, OC.OCDT AS [OC DATE], INV.inv_no AS [INV NO], INV.inv_date AS [INV DATE], INV.inv_qty AS [INV QTY], OC.NETPRICE AS [RATE], OC.DISC  AS [DISCOUNT], INV.amount AS [INV AMOUNT]  FROM GST_INV AS INV INNER JOIN OC_TRANSACTIONS AS OC ON INV.oc_no = OC.OC_NO WHERE OC.PARTY_CODE = '"+ ComboBoxParty.SelectedValue +"' AND OC.ITEM_CODE = '"+ ComboItemCode.SelectedValue+"' ORDER BY OC.ID DESC";
                }

                Generic._fillGrid(qry, Grid);
            }
        }

        protected void ComboBoxParty_SelectedIndexChanged(object sender, EventArgs e)
        {

            string itemCodeQuery = "SELECT DISTINCT(ITEM_CODE) FROM OC_TRANSACTIONS WHERE PARTY_CODE = '" + ComboBoxParty.SelectedValue + "'";
            Generic._fillCombo(itemCodeQuery, ComboItemCode, "ITEM_CODE", "ITEM_CODE");
        }



        private void _fillCombo()
        {
            var _query = from p in _db.MASTER_PARTies where p.STATUS == 0 select new { Party_Code = p.PARTY_CODE, Name = p.PARTY_NAME + " - " + p.PARTY_CODE };
            Generic._fillLinqCombo(_query, ComboBoxParty, "Party_Code", "Name");
        }

    }
}