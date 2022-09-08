using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlankarNewDesign.DAL;
using AjaxControlToolkit;
using System.Collections;
using System.Data;

namespace AlankarNewDesign
{
    public partial class ItemCodeRpt : System.Web.UI.Page
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var indb = _db.OC_TRANSACTIONs.Where(s => s.PARTY_CODE == ComboBoxParty.SelectedValue && s.ITEM_CODE == ComboItemCode.SelectedValue).First();

            lblFoc.Text = indb.FOCNO;
            lblDescription.Text = indb.DESC1;


            DataTable list = new DataTable();
            list = Generic.dataTable("Select top 1 GST_INV.oc_no, GST_INV.inv_no, GST_INV.inv_date, GST_INV.inv_qty, (select OC_TRANSACTIONS.OCDT from OC_TRANSACTIONS where OC_NO = GST_INV.oc_no) as [OC Date] from GST_INV where oc_no in (Select oc_no from OC_TRANSACTIONS where OC_TRANSACTIONS.PARTY_CODE = '"+ ComboBoxParty.SelectedValue +"' and OC_TRANSACTIONS.ITEM_CODE = '"+ ComboItemCode.SelectedValue +"') order by GST_INV.id desc");

            foreach(DataRow dr in list.Rows)
            {
                lblOcNo.Text = dr["oc_no"].ToString();
                lblInvNo.Text = dr["inv_no"].ToString();
                lblInvDate.Text = dr["inv_date"].ToString();
                lblInvQty.Text = dr["inv_qty"].ToString();
                lblOcDt.Text = dr["OC Date"].ToString();
            }


          //  string Qry = "Select GST_INV.oc_no as [OC No], (select OC_TRANSACTIONS.OCDT from OC_TRANSACTIONS where OC_NO = GST_INV.oc_no) as [OC Date], (select OC_TRANSACTIONS.OCQTY from OC_TRANSACTIONS where OC_NO = GST_INV.oc_no) as [OC QTY], SUM( Convert(int,GST_INV.inv_qty)) as [Dispatched QTY], ((select OC_TRANSACTIONS.OCQTY from OC_TRANSACTIONS where OC_NO = GST_INV.oc_no) - Convert(int, GST_INV.inv_qty)) as [Balance QTY]    from GST_INV where oc_no in (Select oc_no from OC_TRANSACTIONS where OC_TRANSACTIONS.PARTY_CODE = '" + ComboBoxParty.SelectedValue + "' and OC_TRANSACTIONS.ITEM_CODE = '" + ComboItemCode.SelectedValue + "') Group by GST_INV.inv_qty, GST_INV.oc_no";

            string Qry = "Select GST_INV.oc_no as [OC No], (select OC_TRANSACTIONS.OCDT from OC_TRANSACTIONS where OC_NO = GST_INV.oc_no) as [OC Date], (select OC_TRANSACTIONS.OCQTY from OC_TRANSACTIONS where OC_NO = GST_INV.oc_no) as [OC QTY], SUM( Convert(int,GST_INV.inv_qty)) as [Dispatched QTY], ((select OC_TRANSACTIONS.OCQTY from OC_TRANSACTIONS where OC_NO = GST_INV.oc_no) - Convert(int, GST_INV.inv_qty)) as [Balance QTY]    from GST_INV where oc_no in (Select oc_no from OC_TRANSACTIONS where OC_TRANSACTIONS.PARTY_CODE = '" + ComboBoxParty.SelectedValue + "' and OC_TRANSACTIONS.ITEM_CODE = '" + ComboItemCode.SelectedValue + "') and Convert(int , GST_INV.inv_qty) < (select OC_TRANSACTIONS.OCQTY from OC_TRANSACTIONS where OC_NO = GST_INV.oc_no) Group by GST_INV.inv_qty, GST_INV.oc_no";
            rdoPending.Checked = true;
            Generic._fillGrid(Qry, Grid);

        }

        protected void rdoAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ComboBoxParty.SelectedIndex > 0 && ComboItemCode.SelectedIndex > 0)
            {
                string Qry = "Select GST_INV.oc_no as [OC No], (select OC_TRANSACTIONS.OCDT from OC_TRANSACTIONS where OC_NO = GST_INV.oc_no) as [OC Date], (select OC_TRANSACTIONS.OCQTY from OC_TRANSACTIONS where OC_NO = GST_INV.oc_no) as [OC QTY], SUM( Convert(int,GST_INV.inv_qty)) as [Dispatched QTY], ((select OC_TRANSACTIONS.OCQTY from OC_TRANSACTIONS where OC_NO = GST_INV.oc_no) - Convert(int, GST_INV.inv_qty)) as [Balance QTY]    from GST_INV where oc_no in (Select oc_no from OC_TRANSACTIONS where OC_TRANSACTIONS.PARTY_CODE = '" + ComboBoxParty.SelectedValue + "' and OC_TRANSACTIONS.ITEM_CODE = '" + ComboItemCode.SelectedValue + "') Group by GST_INV.inv_qty, GST_INV.oc_no";
                Generic._fillGrid(Qry, Grid);
            }

        }

        protected void rdoPending_CheckedChanged(object sender, EventArgs e)
        {
            //Select GST_INV.oc_no as [OC No], (select OC_TRANSACTIONS.OCDT from OC_TRANSACTIONS where OC_NO = GST_INV.oc_no) as [OC Date], (select OC_TRANSACTIONS.OCQTY from OC_TRANSACTIONS where OC_NO = GST_INV.oc_no) as [OC QTY], SUM( Convert(int,GST_INV.inv_qty)) as [Dispatched QTY], ((select OC_TRANSACTIONS.OCQTY from OC_TRANSACTIONS where OC_NO = GST_INV.oc_no) - Convert(int, GST_INV.inv_qty)) as [Balance QTY]    from GST_INV where oc_no in (Select oc_no from OC_TRANSACTIONS where OC_TRANSACTIONS.PARTY_CODE = '178' and OC_TRANSACTIONS.ITEM_CODE = '1000077') and Convert(int , GST_INV.inv_qty) < (select OC_TRANSACTIONS.OCQTY from OC_TRANSACTIONS where OC_NO = GST_INV.oc_no) Group by GST_INV.inv_qty, GST_INV.oc_no


            if (ComboBoxParty.SelectedIndex > 0 && ComboItemCode.SelectedIndex > 0)
            {
                string Qry = "Select GST_INV.oc_no as [OC No], (select OC_TRANSACTIONS.OCDT from OC_TRANSACTIONS where OC_NO = GST_INV.oc_no) as [OC Date], (select OC_TRANSACTIONS.OCQTY from OC_TRANSACTIONS where OC_NO = GST_INV.oc_no) as [OC QTY], SUM( Convert(int,GST_INV.inv_qty)) as [Dispatched QTY], ((select OC_TRANSACTIONS.OCQTY from OC_TRANSACTIONS where OC_NO = GST_INV.oc_no) - Convert(int, GST_INV.inv_qty)) as [Balance QTY]    from GST_INV where oc_no in (Select oc_no from OC_TRANSACTIONS where OC_TRANSACTIONS.PARTY_CODE = '" + ComboBoxParty.SelectedValue + "' and OC_TRANSACTIONS.ITEM_CODE = '" + ComboItemCode.SelectedValue + "') and Convert(int , GST_INV.inv_qty) < (select OC_TRANSACTIONS.OCQTY from OC_TRANSACTIONS where OC_NO = GST_INV.oc_no) Group by GST_INV.inv_qty, GST_INV.oc_no";
                Generic._fillGrid(Qry, Grid);
            }
        }
    }
}