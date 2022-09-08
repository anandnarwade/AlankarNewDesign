using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlankarNewDesign.DAL;

namespace AlankarNewDesign
{
    public partial class rpt_oc_view : System.Web.UI.Page
    {
        alankar_db_providerDataContext _db = new alankar_db_providerDataContext();
        
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
                    if(Request.QueryString["ocno"] != null)
                    {
                        txtOcNo.Text = Request.QueryString["ocno"].ToString();
                        lnkSearch_Click(null, null);
                    }
 
                }


            }
        }


        [System.Web.Services.WebMethod]
        public static string[] GetTagNames(string prefixText, int count)
        {
            alankar_db_providerDataContext _dbContext = new alankar_db_providerDataContext();
            // var query = _dbContext.STAGE_TRANSACTIONs.Where(n => n.STAGE_TYPE == "Issue" & n.STAGE == "Cleaning" & n.OC_NO.StartsWith(prefixText)).OrderBy(n => n.ID).Select(n => n.OC_NO).Distinct().Take(count).ToArray();
            //  return _dbContext.OC_TRANSACTIONs.Where(n => n.OC_NO.StartsWith(prefixText)).OrderBy(n => n.ID).Select(n => n.OC_NO).Distinct().Take(count).ToArray();
            return _dbContext.STAGE_TRANSACTIONs.Where(n => n.STAGE_TYPE == "Issue" & n.STAGE == "Cleaning" & n.OC_NO.StartsWith(prefixText)).OrderBy(n => n.ID).Select(n => n.OC_NO).Distinct().Take(count).ToArray();
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            if (txtOcNo.Text == "")
            {
                txtOcNo.BorderColor = System.Drawing.Color.Red;
                txtOcNo.Focus();
            }
            else
            {
                txtOcNo.BorderColor = System.Drawing.Color.Gray;
                var _oc = _db.OC_TRANSACTIONs.Single(s => s.OC_NO == txtOcNo.Text);
                lblOcNo.Text = _oc.OC_NO;

                var _cust = _db.MASTER_PARTies.Single(s => s.PARTY_CODE == _oc.PARTY_CODE);
                lblOcDate.Text = _oc.OCDT;

                lblCustomer.Text = _cust.PARTY_CODE+" - "+ _cust.PARTY_NAME;
                lblItemCode.Text = _oc.ITEM_CODE;
                lblPoNo.Text = _oc.PONO;
                lblPoDate.Text = _oc.PO_DATE;
                lblAmdNo.Text = _oc.AMENDMENT_NO;
                lblAmdDate.Text = _oc.AMENDMENT_DATE;
                lblDrawingNo.Text = _oc.DRGNO;
                lblQty.Text = _oc.OCQTY.ToString();
                decimal _rate = Convert.ToDecimal(_oc.NETPRICE);
                lblNetPrice.Text = _rate.ToString("N2");
                
                lblFoc.Text = _oc.FOCNO;
                lblToolType.Text = _oc.TOOLTYPE;
                lblToolSubType.Text = _oc.MATCHTYPE;
                lblToolDescription.Text = _oc.DESC1;

                _fillDimensions(lblOcNo.Text);
                _fillRM(lblOcNo.Text);
                _fillGrid(lblOcNo.Text);
                _fillPending(lblOcNo.Text);
                var qry = from s in _db.GST_INVs where s.oc_no == txtOcNo.Text select new { s.inv_no, s.inv_date, s.inv_qty };
                GridDispatch.DataSource = qry;
                GridDispatch.DataBind();
                if(GridDispatch.Rows.Count > 0)
                {
                    long _total = _db.GST_INVs.Where(s => s.oc_no == txtOcNo.Text).Sum(s => Convert.ToInt64(s.inv_qty));
                    GridDispatch.FooterRow.Cells[0].Text = "Total";
                    GridDispatch.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                    GridDispatch.FooterRow.Cells[0].Font.Bold = true;
                    GridDispatch.FooterRow.Cells[3].Text = _total.ToString();
                    GridDispatch.FooterRow.Cells[3].Font.Bold = true;
                }
                

            }
        }

        private void _fillDimensions(string Ocno)
        {
            try
            {
                var _query = from s in _db.DIMENTION_TRANSACTIONs where s.OC_NO == Ocno select new { s.DIMENTION, Sub = s.SUB_DIMENSION, Value = s.VALU };
                DatalistDimensions.DataSource = _query;
                DatalistDimensions.DataBind();
            }
            catch (Exception ex)
            {
 
            }

        }

        private void _fillRM(string Ocno)
        {
            try
            {
                var _query = from r in _db.RAW_MATERIAL_TRANSACTIONs where r.OC_NO == Ocno && r.RM_VALUE != "" select new { r.RAW_MATERIAL, r.RM_VALUE };
                datalistRm.DataSource = _query;
                datalistRm.DataBind();
            }
            catch (Exception ex)
            {
 
            }
        }

        private void _fillGrid(string ocno)
        {
            try
            {
                var _query = from s in _db.SCHEDULE_DETAILs where s.OCNO == ocno select new { date = Convert.ToDateTime(s.SCHDATE), Qty = s.QTY, Total_Qty = s.OQTY };
                GridSchedule.DataSource = _query;
                GridSchedule.DataBind();
            }
            catch (Exception ex)
            {
 
            }
        }

        private void _fillPending(string ocno)
        {
            try
            {

                var _Qry = (from st in _db.STAGE_TRANSACTIONs where st.OC_NO == ocno && st.VALUE > 0 select new { st.STAGE_TYPE, st.STAGE, st.VALUE }).ToList();
                GridPending.DataSource = _Qry;
                GridPending.DataBind();
            }
            catch(Exception ex)
            {

            }
        }

    }
}