using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using AlankarNewDesign.DAL;
using System.Text;
using System.Data;
using OfficeOpenXml;
using System.Drawing;
using Microsoft.Office;
using OfficeOpenXml.Table;
using System.Data.SqlClient;



namespace AlankarNewDesign
{
    public partial class Party_wise_Rpt : System.Web.UI.Page
    {
        alankar_db_providerDataContext _db = new alankar_db_providerDataContext();
        DbConnection Generic = new DbConnection();
        public static string query;
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
                    _fillCombo();

                }
            }
        }

        private void _fillCombo()
        {
            var _query = from p in _db.MASTER_PARTies where p.STATUS == 0 select new { Party_Code = p.PARTY_CODE, Name = p.PARTY_NAME + " - " + p.PARTY_CODE };
            Generic._fillLinqCombo(_query.OrderBy(p => p.Name), ComboBoxParty, "Party_Code", "Name");
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
           // query = "SELECT DISTINCT(STAGE_TRANSACTIONS.OC_NO), OC_TRANSACTIONS.OCDT, OC_TRANSACTIONS.ITEM_CODE, OC_TRANSACTIONS.TOOLTYPE, OC_TRANSACTIONS.MATCHTYPE, OC_TRANSACTIONS.DESC1, OC_TRANSACTIONS.DRGNO, OC_TRANSACTIONS.OCQTY ,OC_TRANSACTIONS.GRPPRICE, VALUE = (OC_TRANSACTIONS.OCQTY * GRPPRICE), NOT_ISSUE = (SELECT SUM(VALUE) FROM STAGE_TRANSACTIONS  WHERE STAGE_TRANSACTIONS.STAGE_TYPE = 'Not Issue' AND STAGE_TRANSACTIONS.OC_NO = OC_TRANSACTIONS.OC_NO), ISSUE = (SELECT SUM(VALUE) FROM STAGE_TRANSACTIONS  WHERE STAGE_TRANSACTIONS.STAGE_TYPE = 'Issue' AND STAGE_TRANSACTIONS.OC_NO = OC_TRANSACTIONS.OC_NO), DISPATCHED = (SELECT SUM(Cast (inv_qty as bigint)) FROM GST_INV WHERE GST_INV.oc_no = OC_TRANSACTIONS.OC_NO) FROM   STAGE_TRANSACTIONS INNER JOIN OC_TRANSACTIONS ON STAGE_TRANSACTIONS.OC_NO = OC_TRANSACTIONS.OC_NO FULL JOIN GST_INV ON OC_TRANSACTIONS.OC_NO = GST_INV.oc_no";
          //  query = "SELECT DISTINCT(STAGE_TRANSACTIONS.OC_NO), OC_TRANSACTIONS.OCDT, OC_TRANSACTIONS.ITEM_CODE, OC_TRANSACTIONS.TOOLTYPE, OC_TRANSACTIONS.MATCHTYPE, OC_TRANSACTIONS.DESC1, OC_TRANSACTIONS.DRGNO, (Select top 1 OQTY from SCHEDULE_DETAILS where OCNO = STAGE_TRANSACTIONS.OC_NO) AS OCQTY ,OC_TRANSACTIONS.GRPPRICE, VALUE = (OC_TRANSACTIONS.OCQTY * GRPPRICE), NOT_ISSUE = (SELECT SUM(VALUE) FROM STAGE_TRANSACTIONS  WHERE STAGE_TRANSACTIONS.STAGE_TYPE = 'Not Issue' AND STAGE_TRANSACTIONS.OC_NO = OC_TRANSACTIONS.OC_NO), ISSUE = (SELECT SUM(VALUE) FROM STAGE_TRANSACTIONS  WHERE STAGE_TRANSACTIONS.STAGE_TYPE = 'Issue' AND STAGE_TRANSACTIONS.OC_NO = OC_TRANSACTIONS.OC_NO), DISPATCHED = (SELECT SUM(Cast (inv_qty as bigint)) FROM GST_INV WHERE GST_INV.oc_no = OC_TRANSACTIONS.OC_NO) FROM   STAGE_TRANSACTIONS INNER JOIN OC_TRANSACTIONS ON STAGE_TRANSACTIONS.OC_NO = OC_TRANSACTIONS.OC_NO FULL JOIN GST_INV ON OC_TRANSACTIONS.OC_NO = GST_INV.oc_no";
            query = "SELECT * FROM CUSTOMER_REPORT ";
            string _fromDate = "", _toDate = "", _partyCode = "", _toolType = "", _subToolType = "", _itemCode = "";

            _fromDate = txtFromDate.Text;
            _toDate = txtToDate.Text;
            _partyCode = ComboBoxParty.SelectedValue.ToString().Trim();
            _toolType = ComboToolType.SelectedValue.ToString().Trim();
            _subToolType = ComboSubType.SelectedValue.ToString().Trim();
            _itemCode = ComboItemCode.SelectedValue.ToString().Trim();

            if (_fromDate != "" && _toDate == "")
            {
                txtToDate.Focus();
                txtToDate.BorderColor = Color.Red;
                txtFromDate.BorderColor = Color.LightGray;
            }
            else if (_fromDate == "" && _toDate != "")
            {
                txtFromDate.Focus();
                txtFromDate.BorderColor = Color.Red;
                txtToDate.BorderColor = Color.LightGray;
            }
            else if (_fromDate != "" && _toDate != "")
            {
                txtFromDate.BorderColor = Color.LightGray;
                txtToDate.BorderColor = Color.LightGray;
            }
            else if (_fromDate == "" && _toDate == "")
            {
                txtFromDate.BorderColor = Color.LightGray;
                txtToDate.BorderColor = Color.LightGray;
            }

            //1st
            if(_partyCode != "" &&  _toolType == "" && _subToolType == "" && _itemCode == "" && _fromDate == "" && _toDate == "")
            {
               // query = query + " WHERE OC_TRANSACTIONS.PARTY_CODE = '" + _partyCode + "'";
                query = query + " WHERE PARTY_CODE = '" + _partyCode + "'";
            }// 2nd
            else if(_partyCode != "" && _toolType != "" && _subToolType == "" && _itemCode == "" && _fromDate == "" && _toDate == "")
            {
                //query = query + " WHERE OC_TRANSACTIONS.PARTY_CODE = '" + _partyCode + "' AND TOOLTYPE = @toolType";
                query = query + " WHERE PARTY_CODE = '" + _partyCode + "' AND TOOLTYPE = @toolType";
                cmd.Parameters.AddWithValue("@toolType", _toolType);
            }//3rd
            else if(_partyCode != "" && _toolType != "" && _subToolType != "" && _itemCode == "" && _fromDate == "" && _toDate == "")
            {
              //  query = query + " WHERE OC_TRANSACTIONS.PARTY_CODE = @party AND TOOLTYPE = @tooltype AND MATCHTYPE = @subtype AND ITEM_CODE = @itemcode";
                query = query + " WHERE PARTY_CODE = @party AND TOOLTYPE = @tooltype AND MATCHTYPE = @subtype AND ITEM_CODE = @itemcode";
                cmd.Parameters.AddWithValue("@party", _partyCode);
                cmd.Parameters.AddWithValue("@tooltype", _toolType);
                cmd.Parameters.AddWithValue("@subtype", _subToolType);
            } //4th
            else if (_partyCode != "" && _toolType != "" && _subToolType != "" && _itemCode != "" && _fromDate == "" && _toDate == "")
            {
               // query = query + " WHERE OC_TRANSACTIONS.PARTY_CODE = @party AND TOOLTYPE = @tooltype AND MATCHTYPE = @subtype AND ITEM_CODE = @itemcode";
                query = query + " WHERE PARTY_CODE = @party AND TOOLTYPE = @tooltype AND MATCHTYPE = @subtype AND ITEM_CODE = @itemcode";
                cmd.Parameters.AddWithValue("@party", _partyCode);
                cmd.Parameters.AddWithValue("@tooltype", _toolType);
                cmd.Parameters.AddWithValue("@subtype", _subToolType);
                cmd.Parameters.AddWithValue("@itemcode", _itemCode);
            }//5th
            else if (_partyCode != "" && _toolType != "" && _subToolType != "" && _itemCode != "" && _fromDate != "" && _toDate == "")
            {
                //query = query + " WHERE OC_TRANSACTIONS.PARTY_CODE = @party AND TOOLTYPE = @tooltype AND MATCHTYPE = @subtype AND ITEM_CODE = @itemcode AND CONVERT(date, OCDT, 103) > @fromDate";
                query = query + " WHERE PARTY_CODE = @party AND TOOLTYPE = @tooltype AND MATCHTYPE = @subtype AND ITEM_CODE = @itemcode AND CONVERT(date, OCDT, 103) > @fromDate";
                cmd.Parameters.AddWithValue("@party", _partyCode);
                cmd.Parameters.AddWithValue("@tooltype", _toolType);
                cmd.Parameters.AddWithValue("@subtype", _subToolType);
                cmd.Parameters.AddWithValue("@itemcode", _itemCode);
                cmd.Parameters.AddWithValue("@fromDate", _fromDate);
            }//6th
            else if(_partyCode != "" && _toolType != "" && _subToolType != "" && _itemCode != "" && _fromDate != "" && _toDate != "")
            {
               // query = query + " WHERE OC_TRANSACTIONS.PARTY_CODE = @party AND TOOLTYPE = @tooltype AND MATCHTYPE = @subtype AND ITEM_CODE = @itemcode AND CONVERT(date, OCDT, 103) >= @fromDate AND CONVERT(date, OCDT, 103) <= @toDate";
                query = query + " WHERE PARTY_CODE = @party AND TOOLTYPE = @tooltype AND MATCHTYPE = @subtype AND ITEM_CODE = @itemcode AND CONVERT(date, OCDT, 103) >= @fromDate AND CONVERT(date, OCDT, 103) <= @toDate";
                cmd.Parameters.AddWithValue("@party", _partyCode);
                cmd.Parameters.AddWithValue("@tooltype", _toolType);
                cmd.Parameters.AddWithValue("@subtype", _subToolType);
                cmd.Parameters.AddWithValue("@itemcode", _itemCode);
                cmd.Parameters.AddWithValue("@fromDate", _fromDate);
                cmd.Parameters.AddWithValue("@toDate", _toDate);
            }//random sequence 1
            else if (_partyCode == "" && _toolType == "" && _subToolType == "" && _itemCode == "" && _fromDate != "" && _toDate != "")
            {
                query = query + " WHERE  CONVERT(date, OCDT, 103) >= @fromDate AND CONVERT(date, OCDT, 103) <= @toDate";
                
                cmd.Parameters.AddWithValue("@fromDate", _fromDate);
                cmd.Parameters.AddWithValue("@toDate", _toDate);
            }

            else if (_partyCode != "" && _toolType == "" && _subToolType == "" && _itemCode == "" && _fromDate != "" && _toDate != "")
            {
               // query = query + " WHERE OC_TRANSACTIONS.PARTY_CODE = @party AND  CONVERT(date, OCDT, 103) >= @fromDate AND CONVERT(date, OCDT, 103) <= @toDate";
                query = query + " WHERE PARTY_CODE = @party AND  CONVERT(date, OCDT, 103) >= @fromDate AND CONVERT(date, OCDT, 103) <= @toDate";
                cmd.Parameters.AddWithValue("@party", _partyCode);
                cmd.Parameters.AddWithValue("@fromDate", _fromDate);
                cmd.Parameters.AddWithValue("@toDate", _toDate);
            }
            else if (_partyCode != "" && _toolType == "" && _subToolType == "" && _itemCode != "" && _fromDate != "" && _toDate != "")
            {
                //query = query + " WHERE OC_TRANSACTIONS.PARTY_CODE = @party AND ITEM_CODE = @itemcode AND CONVERT(date, OCDT, 103) >= @fromDate AND CONVERT(date, OCDT, 103) <= @toDate";
                query = query + " WHERE PARTY_CODE = @party AND ITEM_CODE = @itemcode AND CONVERT(date, OCDT, 103) >= @fromDate AND CONVERT(date, OCDT, 103) <= @toDate";
                cmd.Parameters.AddWithValue("@party", _partyCode);
                cmd.Parameters.AddWithValue("@itemcode", _itemCode);
                cmd.Parameters.AddWithValue("@fromDate", _fromDate);
                cmd.Parameters.AddWithValue("@toDate", _toDate);
            }
            else if (_partyCode != "" && _toolType == "" && _subToolType == "" && _itemCode != "" && _fromDate == "" && _toDate == "")
            {
               // query = query + " WHERE OC_TRANSACTIONS.PARTY_CODE = @party AND ITEM_CODE = @itemcode ";
                query = query + " WHERE PARTY_CODE = @party AND ITEM_CODE = @itemcode ";
                cmd.Parameters.AddWithValue("@party", _partyCode);
                cmd.Parameters.AddWithValue("@itemcode", _itemCode);
            }      
            else if (_partyCode != "" && _toolType != "" && _subToolType == "" && _itemCode != "" && _fromDate != "" && _toDate != "")
            {
                //query = query + " WHERE OC_TRANSACTIONS.PARTY_CODE = @party AND TOOLTYPE = @tooltype AND ITEM_CODE = @itemcode AND CONVERT(date, OCDT, 103) >= @fromDate AND CONVERT(date, OCDT, 103) <= @toDate";
                query = query + " WHERE PARTY_CODE = @party AND TOOLTYPE = @tooltype AND ITEM_CODE = @itemcode AND CONVERT(date, OCDT, 103) >= @fromDate AND CONVERT(date, OCDT, 103) <= @toDate";
                cmd.Parameters.AddWithValue("@party", _partyCode);
                cmd.Parameters.AddWithValue("@itemcode", _itemCode);
                cmd.Parameters.AddWithValue("@tooltype", _toolType);
                cmd.Parameters.AddWithValue("@fromDate", _fromDate);
                cmd.Parameters.AddWithValue("@toDate", _toDate);
            }
            else if (_partyCode != "" && _toolType == "" && _subToolType == "" && _itemCode != "" && _fromDate == "" && _toDate == "")
            {
                query = query + " WHERE PARTY_CODE = @party AND TOOLTYPE = @tooltype AND ITEM_CODE = @itemcode ";
                cmd.Parameters.AddWithValue("@party", _partyCode);
                cmd.Parameters.AddWithValue("@itemcode", _itemCode);
                cmd.Parameters.AddWithValue("@tooltype", _toolType);
                
            }
            else if (_partyCode != "" && _toolType != "" && _subToolType != "" && _itemCode != "" && _fromDate == "" && _toDate == "")
            {
                query = query + " WHERE PARTY_CODE = @party AND TOOLTYPE = @tooltype AND MATCHTYPE = @subtype AND ITEM_CODE = @itemcode ";

                cmd.Parameters.AddWithValue("@party", _partyCode);
                cmd.Parameters.AddWithValue("@tooltype", _toolType);
                cmd.Parameters.AddWithValue("@subtype", _subToolType);
                cmd.Parameters.AddWithValue("@itemcode", _itemCode);
                //cmd.Parameters.AddWithValue("@fromDate", _fromDate);
                //cmd.Parameters.AddWithValue("@toDate", _toDate);
            }
            cmd.CommandText = query;
            Generic._fillGridFromCmd(cmd, GridParty);

        }



        protected void ExportToPDF(object sender, EventArgs e)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    //To Export all pages
                    GridParty.AllowPaging = false;
                    //this.BindGrid();

                    GridParty.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename="+ComboBoxParty.SelectedValue+".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }

        protected void lnkExportPdf_Click(object sender, EventArgs e)
        {
            ExportToPDF(null, null);
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        private void ExportToWord()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = ComboBoxParty.SelectedValue+".doc";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/msword";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridParty.GridLines = GridLines.Both;
            GridParty.HeaderStyle.Font.Bold = true;
            GridParty.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }


        private void ExportToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + ComboBoxParty.SelectedValue + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridParty.AllowPaging = false;
                //this.BindGrid();

                GridParty.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in GridParty.HeaderRow.Cells)
                {
                    cell.BackColor = GridParty.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridParty.Rows)
                {
                    //row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridParty.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridParty.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridParty.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }




        }


        private void ExportToCSV()
        {

            Response.ClearContent();
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Customers.csv"));
            Response.ContentType = "application/text";
            Generic._fillGrid(query, GridParty);
            GridParty.AllowPaging = false;

            GridParty.DataBind();
            StringBuilder strbldr = new StringBuilder();
            for (int i = 0; i < GridParty.Columns.Count; i++)
            {
                //separting header columns text with comma operator
                strbldr.Append(GridParty.Columns[i].HeaderText + ',');
            }
            //appending new line for gridview header row
            strbldr.Append("\n");
            for (int j = 0; j < GridParty.Rows.Count; j++)
            {
                for (int k = 0; k < GridParty.Columns.Count; k++)
                {
                    //separating gridview columns with comma
                    strbldr.Append(GridParty.Rows[j].Cells[k].Text + ',');
                }
                //appending new line for gridview rows
                strbldr.Append("\n");
            }
            Response.Write(strbldr.ToString());
            Response.End();
        }

        protected void lnkExportWord_Click(object sender, EventArgs e)
        {
            ExportToWord();
        }

        protected void lnkExportExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        protected void lnkExportCSV_Click(object sender, EventArgs e)
        {
            ExportToCSV();

        }

        protected void ComboBoxParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ToolQuery = "SELECT DISTINCT(OC_TRANSACTIONS.TOOLTYPE) FROM OC_TRANSACTIONS WHERE PARTY_CODE = '"+ComboBoxParty.SelectedValue+"'";
            ComboToolType.Items.Clear();
            ComboSubType.Items.Clear();
            ComboItemCode.Items.Clear();
            Generic._fillCombo(ToolQuery, ComboToolType, "TOOLTYPE", "TOOLTYPE");
            //ComboToolType.Items.Insert(0, "");


            string itemCodeQuery = "SELECT DISTINCT(ITEM_CODE) FROM OC_TRANSACTIONS WHERE PARTY_CODE = '" + ComboBoxParty.SelectedValue + "'";
            Generic._fillCombo(itemCodeQuery, ComboItemCode, "ITEM_CODE", "ITEM_CODE");
        }

        protected void ComboToolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Qry = string.Empty;
            if(ComboBoxParty.SelectedIndex > 0 && ComboToolType.SelectedIndex == 0)
            {
                Qry = "Select distinct(ITEM_CODE) from OC_TRANSACTIONS where PARTY_CODE = '" + ComboBoxParty.SelectedValue + "' ";
                Generic._fillCombo(Qry, ComboItemCode, "ITEM_CODE", "ITEM_CODE");
            }
            else if(ComboToolType.SelectedIndex > 0 && ComboToolType.SelectedIndex > 0)
            {

                Qry = "Select distinct(ITEM_CODE) from OC_TRANSACTIONS where PARTY_CODE = '" + ComboBoxParty.SelectedValue + "' and TOOLTYPE = '" + ComboToolType.SelectedValue + "'";
                Generic._fillCombo(Qry, ComboItemCode, "ITEM_CODE", "ITEM_CODE");

                string SubTypeQuery = "SELECT DISTINCT(OC_TRANSACTIONS.MATCHTYPE) FROM OC_TRANSACTIONS WHERE PARTY_CODE = '" + ComboBoxParty.SelectedValue + "' AND TOOLTYPE = '" + ComboToolType.SelectedValue + "'";
                ComboSubType.Items.Clear();
                Generic._fillCombo(SubTypeQuery, ComboSubType, "MATCHTYPE", "MATCHTYPE");
            }

          
          

        }

        protected void ComboSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Query = string.Empty;
            ComboItemCode.Items.Clear();
            if(ComboBoxParty.SelectedIndex > 0 && ComboToolType.SelectedIndex == 0 && ComboSubType.SelectedIndex == 0)
            {
                Query = "Select distinct(ITEM_CODE) from OC_TRANSACTIONS where PARTY_CODE = '"+ComboBoxParty.SelectedValue+"' ";
                Generic._fillCombo(Query, ComboItemCode, "ITEM_CODE", "ITEM_CODE");
            }
            else if(ComboBoxParty.SelectedIndex > 0 && ComboToolType.SelectedIndex > 0 && ComboSubType.SelectedIndex == 0)
            {
                Query = "Select distinct(ITEM_CODE) from OC_TRANSACTIONS where PARTY_CODE = '" + ComboBoxParty.SelectedValue + "' and TOOLTYPE = '" + ComboToolType.SelectedValue + "'";
                Generic._fillCombo(Query, ComboItemCode, "ITEM_CODE", "ITEM_CODE");
            }
            else if(ComboBoxParty.SelectedIndex > 0 && ComboToolType.SelectedIndex > 0 && ComboSubType.SelectedIndex > 0)
            {
                Query = "Select distinct(ITEM_CODE) from OC_TRANSACTIONS where PARTY_CODE = '" + ComboBoxParty.SelectedValue + "' and TOOLTYPE = '" + ComboToolType.SelectedValue + "' and MATCHTYPE = '"+ComboSubType.SelectedValue+"'";
                Generic._fillCombo(Query, ComboItemCode, "ITEM_CODE", "ITEM_CODE");
            }
        }

        protected void GridParty_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblQty = (e.Row.FindControl("lblQty") as Label);
                //lblDispatched
                Label lblDispatched = (e.Row.FindControl("lblDispatched") as Label);
                int totalQty = 0, dispQty = 0;
                if(!string.IsNullOrWhiteSpace(lblQty.Text))
                {
                    totalQty = Convert.ToInt32(lblQty.Text);
                }

                if (!string.IsNullOrWhiteSpace(lblDispatched.Text))
                {
                    dispQty = Convert.ToInt32(lblDispatched.Text);

                }


                if(totalQty > 0 && dispQty > 0)
                {
                    if(totalQty == dispQty)
                    {
                        e.Row.BackColor = System.Drawing.Color.GreenYellow;
                    }
                }
               
            }
           
        }
    }
}