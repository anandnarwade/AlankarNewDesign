using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlankarNewDesign.DAL;

namespace AlankarNewDesign
{
    public partial class Quantity_Report : System.Web.UI.Page
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
                if (!IsPostBack)
                {
                    _fillDefaultGrid();
                }
            }

        }

        private void _fillDefaultGrid()
        {
            string _query = "SELECT top 1 NOT_ISSUE = (select  sum(VALUE) from STAGE_TRANSACTIONS where STAGE_TYPE = 'Not Issue'), ISSUE = (select sum(VALUE) from STAGE_TRANSACTIONS where STAGE_TYPE = 'Issue'), DISPATCH = (SELECT SUM(Cast (inv_qty as bigint)) FROM GST_INV) FROM STAGE_TRANSACTIONS INNER JOIN GST_INV ON STAGE_TRANSACTIONS.OC_NO = GST_INV.oc_no";
            Generic._fillGrid(_query, GridQtyRpt);
        }
    }
}