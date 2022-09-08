using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq;
using AlankarNewDesign.DAL;
namespace AlankarNewDesign
{
    public partial class all_invoice : System.Web.UI.Page
    {
        alankar_db_providerDataContext _dbContext = new alankar_db_providerDataContext();
        DbConnection _obj = new DbConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["username"] == null && Session["password"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                _fillGrid();
            }

        }

        protected void _fillGrid()
        {
            try
            {
               // var query = from inv in _dbContext.GST_INVs join oc in _dbContext.OC_TRANSACTIONs select new { inv.id, inv.oc_no, inv.inv_no, inv.inv_date, inv.inv_qty };

                var _query = from inv in _dbContext.GST_INVs join oc in _dbContext.OC_TRANSACTIONs on inv.oc_no equals oc.OC_NO  select new { inv.id, inv.oc_no, inv.inv_no, inv.inv_date, inv.inv_qty, oc.PARTY_CODE };
                GridInv.DataSource = _query.OrderByDescending(s => s.id);
                GridInv.DataBind();
               
                GridInv.UseAccessibleHeader = true;
                GridInv.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception ex)
            {

            }
        }

        protected void lnkInvDelete_Click(object sender, EventArgs e)
        {
            string _InvNo = (sender as LinkButton).CommandArgument;
            Int64? _InvQty = 0;
            string _ocNo = string.Empty;
            Int64 _stageValue = 0;
            Int64 _stageTypeQty = 0;
            Int64? _finalQty = 0;
            var  _gi = _dbContext.GST_INVs.Single(s => s.inv_no == _InvNo);
            if(_gi.inv_qty != null || _gi.inv_qty != "")
            {
                _InvQty = Convert.ToInt64(_gi.inv_qty);
            }
            else if(_gi.inv_qty == null || _gi.inv_qty == "")
            {
                _InvQty = 0;
            }


            _ocNo = _gi.oc_no;

            STAGE_TRANSACTION stage = _dbContext.STAGE_TRANSACTIONs.Single(s => s.OC_NO == _ocNo && s.STAGE == "Cleaning"  && s.STAGE_TYPE == "Issue");

            _stageValue = Convert.ToInt64(stage.VALUE);
            stage.VALUE = _stageValue  + _InvQty;
            _stageTypeQty = Convert.ToInt64(stage.STAGE_TYPE_VALUE);
            _dbContext.SubmitChanges();

            _dbContext.GST_INVs.DeleteOnSubmit(_gi);
            _dbContext.SubmitChanges();


            //STAGE_TRANSACTION stage2 = _dbContext.STAGE_TRANSACTIONs.SingleOrDefault(s => s.OC_NO == _ocNo && s.STAGE_TYPE == "Issue");

            //stage2.STAGE_TYPE_VALUE = _stageTypeQty + _InvQty;
            _finalQty = _stageTypeQty + _InvQty;

            try
            {
                _obj._getConnection();
                _obj.cmd = new SqlCommand("UPDATE STAGE_TRANSACTIONS SET STAGE_TYPE_VALUE = '" + _finalQty + "' WHERE OC_NO = '" + _ocNo + "' AND STAGE_TYPE = 'Issue' ", _obj.con);
                _obj.con.Open();
                _obj.cmd.ExecuteNonQuery();
                _obj.con.Close();
            }
            catch (Exception ex)
            {

            }

            _dbContext.SubmitChanges();
            _fillGrid();
        }


    }
}