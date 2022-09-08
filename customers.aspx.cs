using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace AlankarNewDesign
{
    public partial class customers : System.Web.UI.Page
    {
        DbConnection obj = new DbConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _getPartyData();
            }
        }


        public void _getPartyData()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("SELECT PARTY_CODE, PARTY_NAME, CITY, PHONE, GSTIN FROM MASTER_PARTY WHERE STATUS = 0", obj.con);

                obj.con.Open();
                da.SelectCommand = obj.cmd;
                da.Fill(ds);
                GridMasterParty.DataSource = ds;
                GridMasterParty.DataBind();
                GridMasterParty.UseAccessibleHeader = true;
                GridMasterParty.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
            catch (Exception ex)
            {
                //Response.Write("<script>alert('Error in _getPartyData: " + ex.Message + "');</script>");
            }
            finally
            {
                obj.con.Close();
                obj.cmd.Dispose();
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string partyCode = (sender as LinkButton).CommandArgument;
            try
            {
                obj._getConnection();
                obj.cmd = new SqlCommand("UPDATE MASTER_PARTY SET STATUS = 1 WHERE PARTY_CODE = '" + partyCode + "'", obj.con);
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
            _getPartyData();
        }

        protected void btnAddnew_Click(object sender, EventArgs e)
        {
            Response.Redirect("cust_master.aspx");
        }
    }
}