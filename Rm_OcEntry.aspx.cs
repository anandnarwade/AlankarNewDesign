using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AlankarNewDesign.DAL;

namespace AlankarNewDesign
{
    public partial class Rm_OcEntry : System.Web.UI.Page
    {
        alankar_db_providerDataContext _db = new alankar_db_providerDataContext();
        DbConnection Generic = new DbConnection();
        public static string _usreName = string.Empty;
        public static string _batchNO;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["username"] == null && Session["password"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                _usreName = Session["username"].ToString();
                if(!IsPostBack)
                {
                    //_fillRmCombo();

                    _fillGrid();
                }
            }
        }


        private void _fillRmCombo()
        {
            string QRy = "SELECT RM_ID, RM_NAME FROM RAW_MATERIAL WHERE STATUS = 0 ORDER BY SEQUENCE ASC";
            Generic._fillCombo(QRy, ComboRm, "RM_ID", "RM_NAME");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int i = 0;
            if(btnSave.Text == "SAVE")
            {
                long? qty = Convert.ToInt64(txtQty.Text);
                long? _stock = null, _minStock = null, _maxStock = null, _safetyStock = null, _total = null;
                if(lblMinQty.Text != "")
                {
                    _minStock = Convert.ToInt64(lblMinQty.Text);
                }
                if(txtQty.Text != "")
                {
                    _stock = Convert.ToInt64(txtQty.Text);
                }
                if(lblMaxQty.Text != "")
                {
                    _maxStock = Convert.ToInt64(lblMaxQty.Text);
                }
                if(lblSafatyQty.Text != "")
                {
                    _safetyStock = Convert.ToInt64(lblSafatyQty.Text);
                }
                if(lblTotal.Text != "")
                {
                    _total = Convert.ToInt64(lblTotal.Text);
                }
                _total = _total - _stock;
                if(_total > 0)
                {
                    i = _db.RMSTOCK_LOGS(null, Convert.ToInt64(ComboRm.SelectedValue), txtOcNo.Text, null, _stock, txtDate.Text, _batchNO, _total, _usreName, null, null, null);
                    var I = _db.StockRMs.SingleOrDefault(s => s.rm_id == Convert.ToInt64(ComboRm.SelectedValue) && s.stock == Convert.ToInt64(lblTotal.Text));
                    var total2 = I.stock;
                    var id = I.id;
                    total2 = total2 - _stock;
                    string Query = "UPDATE StockRM SET stock = "+total2+", total = "+total2+" WHERE id = "+id+" AND rm_id = '"+ComboRm.SelectedValue+"'";
                    Generic._SavedAs(Query);

                }
                else
                {
                    dismiss.Visible = true;
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Qty Missmatch...!";
                }


                if(i == 0)
                {
                    txtOcNo.Text = "";
                    ComboRm.Items.Clear();
                    txtQty.Text = "";
                    lblTotal.Text = "";
                    lblMinQty.Text = "";
                    lblMaxQty.Text = "";
                    lblSafatyQty.Text = "";

                    //dismiss.Visible = true;
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Saved Successfully...!";

                    _fillGrid();
                }
            }
        }

        [System.Web.Services.WebMethod]
        public static string[] GetTagNames(string prefixText, int count)
        {
            alankar_db_providerDataContext _dbContext = new alankar_db_providerDataContext();
            return _dbContext.OC_TRANSACTIONs.Where(n => n.OC_NO.StartsWith(prefixText)).OrderBy(n => n.ID).Select(n => n.OC_NO).Distinct().Take(count).ToArray();

        }

        protected void txtOcNo_TextChanged(object sender, EventArgs e)
        {
            string Qry = "select RAW_MATERIAL.RM_ID, RAW_MATERIAL_TRANSACTIONS.RAW_MATERIAL from RAW_MATERIAL_TRANSACTIONS inner join RAW_MATERIAL on RAW_MATERIAL_TRANSACTIONS.RAW_MATERIAL = RAW_MATERIAL.RM_NAME where RM_VALUE != '' AND OC_NO = '"+txtOcNo.Text+"'";
            Generic._fillCombo(Qry, ComboRm, "RM_ID", "RAW_MATERIAL");
        }

        protected void ComboRm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ComboRm.SelectedIndex > 0)
            {
                foreach(var i in _db.StockRMs)
                {
                    if(i.rm_id == Convert.ToInt64(ComboRm.SelectedValue))
                    {
                        lblTotal.Text = i.total.ToString();
                        lblMinQty.Text = i.min_stock.ToString();
                        lblMaxQty.Text = i.max_stock.ToString();
                        lblSafatyQty.Text = i.safety_stock.ToString();
                        _batchNO = i.batch_no;
                    }
                }
            }
        }

        public void _fillGrid()
        {
            Generic._fillGrid("select id, RAW_MATERIAL.RM_ID,  RAW_MATERIAL.RM_NAME , oc_no, stock_plus, stock_minus, entryDate, batchno, total from StockRm_Logs inner join RAW_MATERIAL on StockRm_Logs.rm_id = RAW_MATERIAL.RM_ID", GridSTockUsed);
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            long Id = Convert.ToInt64((sender as LinkButton).CommandArgument);

            var stock = _db.StockRm_Logs.SingleOrDefault(s => s.id == Id);
            long? minus = null;
            long? rmId = 0, stockId = null, _STK = 0, _TOTAL = 0;
            rmId = stock.rm_id;
            minus = stock.stock_minus;

                foreach(var s in _db.StockRMs)
                {
                    rmId = s.rm_id;
                    stockId = s.id;
                    _STK = s.stock;
                    _TOTAL = s.total;
                }
                _STK = _STK + minus;
                _TOTAL = _TOTAL + minus;
                bool res =  Generic._SavedAs("UPDATE StockRM SET stock = '"+_STK+"' , total = '"+_TOTAL+"' WHERE id = '"+stockId+"'");

                if(res == true)
                {
                    Generic._SavedAs("DELETE FROM StockRm_Logs WHERE id = '"+Id+"'");
                }

                _fillGrid();
        }
    }
}