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
    public partial class RM_Stock : System.Web.UI.Page
    {
        alankar_db_providerDataContext _db = new alankar_db_providerDataContext();
        DbConnection Generic = new DbConnection();
        public static string _userName = string.Empty;
        string _action = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["username"] == null && Session["password"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                _userName = Session["username"].ToString();
                if(!IsPostBack)
                {
                    _fillRmCombo();
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
            string _getTotal = string.Empty;
            int i = 0, j = 0;
            if(btnSave.Text == "SAVE")
            {
                long? _stock = null, _minStock = null, _maxStock = null, _safetyStock = null, _total = null;
                if (txtAddStock.Text != "")
                {
                    _stock = Convert.ToInt64(txtAddStock.Text);
                }
                if (txtMinStock.Text != "")
                {
                    _minStock = Convert.ToInt64(txtMinStock.Text);
                }
                if (txtMaxStock.Text != "")
                {
                    _maxStock = Convert.ToInt64(txtMaxStock.Text);
                }
                if (txtSafetyStock.Text != "")
                {
                    _safetyStock = Convert.ToInt64(txtSafetyStock.Text);

                }
                _getTotal = Generic.GetStr("select total from StockRm_Logs where rm_id = '" + Convert.ToInt64(ComboRm.SelectedValue) + "' and batchno = '"+txtBatchNo.Text+"'");
                if(_getTotal == "")
                {
                    _total = _stock;
                }
                else
                {
                    _total = Convert.ToInt64(_getTotal);
                }
               i =  _db.SP_RMSTOCK(null, Convert.ToInt64(ComboRm.SelectedValue), _stock, _minStock, _maxStock, _safetyStock, txtBatchNo.Text, txtReceivedDate.Text, _total, _userName, null, null, null, "INSERT");
                j = _db.RMSTOCK_LOGS(null, Convert.ToInt64(ComboRm.SelectedValue), null, _stock, null, txtReceivedDate.Text, txtBatchNo.Text, _total, _userName, null, null, null);


                dismiss.Visible = true;
                lblMessage.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                lblMessage.Text = "Saved Successfully..!";

            }

            if(i == 0 && j == 0)
            {
                ComboRm.SelectedIndex = 0;
                txtAddStock.Text = "";
                txtMinStock.Text = "";
                txtMaxStock.Text = "";
                txtSafetyStock.Text = "";
                txtBatchNo.Text = "";
                txtReceivedDate.Text = "";
                _fillGrid();
            }
        }

        protected void ComboRm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ComboRm.SelectedIndex > 0)
            {


            }
        }

        private void _fillGrid()
        {
            string Qry = "select StockRM.id, StockRM.rm_id, RAW_MATERIAL.RM_NAME, StockRM.stock, StockRM.min_stock, StockRM.safety_stock, StockRM.max_stock, StockRM.batch_no, StockRM.received_date  from StockRM inner join RAW_MATERIAL on StockRM.rm_id = RAW_MATERIAL.RM_ID";
            Generic._fillGrid(Qry, GridStock);
        }

        protected void GridStock_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                long? min = null, max = null, safety = null;
                LinkButton lkDelete = (LinkButton)e.Row.FindControl("lnkDelete");
                Label lblSrNo, lblStockId, lblrmId, lblRM, lblTotalStock, lblMinStock, lblSafetyStock, lblMaxStock, lblBatchNo,  lblRecDate;

                lblRM = (Label)e.Row.FindControl("lblRmName");
                lblTotalStock = (Label)e.Row.FindControl("lblStock");
                lblMinStock = (Label)e.Row.FindControl("lblMinStock");
                lblSafetyStock = (Label)e.Row.FindControl("lblSafetyStock");
                lblMaxStock = (Label)e.Row.FindControl("lblMaxStock");
                lblBatchNo = (Label)e.Row.FindControl("lblBatchNo");
                lblRecDate = (Label)e.Row.FindControl("lblReceivedDate");
                lblStockId = (Label)e.Row.FindControl("lblStockId");
                lblrmId = (Label)e.Row.FindControl("lblRmId");
                lblSrNo = (Label)e.Row.FindControl("lblSrNo");
                long? Total = Convert.ToInt64(lblTotalStock.Text);

                SqlDataReader rdr = Generic._getRdr("select min_stock, max_stock, safety_stock from StockRM where rm_id = '" + lblrmId .Text+ "' order by id desc");
                while(rdr.Read())
                {
                    if(rdr[0] != "")
                    {
                        min = Convert.ToInt64(rdr[0]);
                    }
                    if(rdr[1] != "")
                    {
                        max = Convert.ToInt64(rdr[1]);
                    }
                    if(rdr[2] != "")
                    {
                        safety = Convert.ToInt64(rdr[2]);
                    }
                }


                if(min > Total)
                {
                    lblSrNo.ForeColor = System.Drawing.Color.Red;
                    lblRM.ForeColor = System.Drawing.Color.Red;
                    lblTotalStock.ForeColor = System.Drawing.Color.Red;
                    lblMinStock.ForeColor = System.Drawing.Color.Red;
                    lblSafetyStock.ForeColor = System.Drawing.Color.Red;
                    lblMaxStock.ForeColor = System.Drawing.Color.Red;
                    lblBatchNo.ForeColor = System.Drawing.Color.Red;
                    lblRecDate.ForeColor = System.Drawing.Color.Red;
                    //display all in red color;
                }
                if(min < Total && safety > Total)
                {
                    //Display all in orange color;
                    lblSrNo.ForeColor = System.Drawing.Color.Orange;
                    lblRM.ForeColor = System.Drawing.Color.Orange;
                    lblTotalStock.ForeColor = System.Drawing.Color.Orange;
                    lblMinStock.ForeColor = System.Drawing.Color.Orange;
                    lblSafetyStock.ForeColor = System.Drawing.Color.Orange;
                    lblMaxStock.ForeColor = System.Drawing.Color.Orange;
                    lblBatchNo.ForeColor = System.Drawing.Color.Orange;
                    lblRecDate.ForeColor = System.Drawing.Color.Orange;
                }
                if(Total > safety)
                {
                    //Display all in Green color;
                    lblSrNo.ForeColor = System.Drawing.Color.Green;
                    lblRM.ForeColor = System.Drawing.Color.Green;
                    lblTotalStock.ForeColor = System.Drawing.Color.Green;
                    lblMinStock.ForeColor = System.Drawing.Color.Green;
                    lblSafetyStock.ForeColor = System.Drawing.Color.Green;
                    lblMaxStock.ForeColor = System.Drawing.Color.Green;
                    lblBatchNo.ForeColor = System.Drawing.Color.Green;
                    lblRecDate.ForeColor = System.Drawing.Color.Green;
                }






            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            long Id = Convert.ToInt64((sender as LinkButton).CommandArgument);

            long? rmId = null;
            string batchNo = string.Empty;

            var StockDetails = _db.StockRMs.Single(s => s.id == Id);
            rmId = StockDetails.rm_id;
            batchNo = StockDetails.batch_no;

            bool _Exists = false;
            foreach(var s in _db.StockRm_Logs)
            {
                if(s.rm_id == rmId && s.batchno == batchNo && s.oc_no != "")
                {
                    _Exists = true;
                    break;
                }
            }

            if(_Exists == false)
            {

                Generic._SavedAs("delete from StockRM where rm_id = '"+rmId+"' and id = '"+Id+"' and batch_no = '"+batchNo+"'");
                Generic._SavedAs("delete from StockRm_Logs where rm_id = '"+rmId+"' and batchno = '"+Id+"'");

            }
        }
    }
}