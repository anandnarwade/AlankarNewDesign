using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlankarNewDesign.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AlankarNewDesign
{
    public partial class NearBy : System.Web.UI.Page
    {
        public static DataTable NearByDt;
        alankar_db_providerDataContext _dbContext = new alankar_db_providerDataContext();
        DbConnection _db = new DbConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                _fillPartyCombo();
                _fillDimentions();
                
            }
        }

        [System.Web.Services.WebMethod]
        public static string[] GetCustNames(string prefixText, int count)
        {
            alankar_db_providerDataContext _dbContext = new alankar_db_providerDataContext();


            //return (from p in _dbContext.MASTER_PARTies where p.PARTY_NAME.StartsWith(prefixText) select new { party = p.PARTY_CODE + " " + p.PARTY_NAME + " " + p.SL_ADD }).Take(count).ToArray();


            // return _dbContext.MASTER_PARTies.Where(p => p.PARTY_CODE.StartsWith(prefixText)).OrderBy(p => p.ID).Select(p => p.PARTY_CODE).Distinct().Take(count).ToArray();

            return _dbContext.MASTER_PARTies.Where(p => p.PARTY_NAME.StartsWith(prefixText)).OrderBy(p => p.ID).Select(p => p.PARTY_NAME).Distinct().Take(count).ToArray();


        }

        private void _fillPartyCombo()
        {
            try
            {
                var _query = from p in _dbContext.MASTER_PARTies where p.STATUS.Equals(0) select new { value = p.PARTY_CODE, text = p.PARTY_NAME + " " + p.DIVISION + " " + p.PARTY_CODE };
                ComboBox1.DataSource = _query;
                ComboBox1.DataValueField = "value";
                ComboBox1.DataTextField = "text";
                ComboBox1.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        private void _fillDimentions()
        {
            DataTable dt = _db.dataTable("Select DIMENTION, COUNT(DIMENTION) from DIMENTION_TRANSACTIONS  group by DIMENTION order by Count(DIMENTION) Desc");
            DataList1.DataSource = dt;
            DataList1.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string _mainQry = string.Empty;
            string _DateFilter = string.Empty;
            string _subToolFilter = string.Empty;
            string _partyFilter = string.Empty;
            string _finalQry = string.Empty;

            if(txtFromDate.Text != "" && txtToDate.Text != "")
            {
                _DateFilter = " d.CREATED_ON between @fromDate and @toDate AND ";

            }
            else if(txtFromDate.Text != "" && txtToDate.Text == "")
            {
                _DateFilter = " d.CREATED_ON >  @fromDate AND ";
            }
            else if(txtFromDate.Text == "" && txtToDate.Text != "")
            {
                _DateFilter = " d.CREATED_ON <  @toDate AND ";
            }


            if(DDmTotalSubType.SelectedIndex > 0)
            {
                _subToolFilter = " d.TOOL_SUB_TYPE = @toolSubtype AND ";
            }

            if(ComboBox1.SelectedIndex > 0)
            {
                _partyFilter = "and o.PARTY_CODE = @partyCode AND";
            }

            string QryText = string.Empty;
            foreach(DataListItem item in this.DataList1.Items)
            {
                Label lblDimention = item.FindControl("lblDimention") as Label;
                TextBox min, max;
                min = item.FindControl("txtMin") as TextBox;
                max = item.FindControl("txtMax") as TextBox;

                if(min.Text != "" && max.Text != "")
                {
                    if (QryText == "")
                    {
                        QryText = "DIMENTION = '" + lblDimention.Text + "' AND CAST( VALU AS FLOAT) >=" + min.Text + " AND CAST( VALU AS FLOAT) <= " + max.Text;
                    }
                    else {
                        QryText += "OR DIMENTION = '" + lblDimention.Text + "' AND CAST( VALU AS FLOAT) >=" + min.Text + " AND CAST( VALU AS FLOAT) <= " + max.Text;

                    }
                }
            }

            _mainQry = "Select o.OC_NO, (Select PARTY_NAME +'('+PARTY_CODE+')' from MASTER_PARTY where PARTY_CODE = o.PARTY_CODE) as [Customer] , d.TOOL_SUB_TYPE, o.OCQTY, (select top 1 OCQTY from GST_INV where GST_INV.oc_no = o.OC_NO order by id desc) as [Last INV QTY], (select top 1 inv_date from GST_INV where GST_INV.oc_no = o.OC_NO order by id desc) as [Last INV Date]    from DIMENTION_TRANSACTIONS as d inner join OC_TRANSACTIONS as o on d.OC_NO = o.OC_NO  Where ";
            
            _finalQry = _mainQry + _DateFilter + _subToolFilter + _partyFilter + QryText;

            try
            {
                SqlConnection con = null;
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["D__DEVELOPMENT_SOFTWARE_UPDATED_DB_ALANKAR_DB_MDFConnectionString"].ConnectionString);
                SqlCommand cmd = new SqlCommand(_finalQry, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@fromDate", txtFromDate.Text);
                cmd.Parameters.AddWithValue("@toDate", txtToDate.Text);
                cmd.Parameters.AddWithValue("@toolSubtype", DDmTotalSubType.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@partyCode", ComboBox1.SelectedValue.ToString());
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                NearByDt = dt;


                string url = "NearByResult.aspx";
                string s = "window.open('" + url + "', 'popup_window', 'width=1200,height=960,left=100,top=100,resizable=yes');";
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            }
            catch (Exception ex)
            {
                
               // throw;

                Response.Write("<script>alert('Data not in proper format');</script>");
            }
            
          

        }

    }
}