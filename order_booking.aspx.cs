using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlankarNewDesign.DAL;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Data;

namespace AlankarNewDesign
{
    public partial class order_booking : System.Web.UI.Page
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


                    var _cust_query = from c in _db.MASTER_PARTies where c.STATUS == 0 select new { c.PARTY_CODE, Customer = c.PARTY_NAME + " - " + c.PARTY_CODE };
                    Generic._fillLinqCombo(_cust_query, ComboParty, "PARTY_CODE", "Customer");

                    string _toolQuery = "SELECT DISTINCT(TOOLTYPE) FROM OC_TRANSACTIONS";
                    Generic._fillCombo(_toolQuery, ComboTool, "TOOLTYPE", "TOOLTYPE");


                    string _subtype_Query = "SELECT DISTINCT(MATCHTYPE) FROM OC_TRANSACTIONS";
                    Generic._fillCombo(_subtype_Query, ComboSubTool, "MATCHTYPE", "MATCHTYPE");

                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //string _Query = "SELECT OC_NO, OCDT, ITEM_CODE, CUSTOMER = (MASTER_PARTY.PARTY_NAME+' - '+MASTER_PARTY.PARTY_CODE), TOOLTYPE, MATCHTYPE, DESC1, OC_TRANSACTIONS.OCQTY [QTY], OC_TRANSACTIONS.FOCNO, OC_TRANSACTIONS.GRPPRICE FROM OC_TRANSACTIONS INNER JOIN MASTER_PARTY ON OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE";
            string _Query = "SELECT OC_NO, OCDT, ITEM_CODE, CUSTOMER = (MASTER_PARTY.PARTY_NAME+' - '+MASTER_PARTY.PARTY_CODE), TOOLTYPE, MATCHTYPE, DESC1, OC_TRANSACTIONS.OCQTY [QTY], OC_TRANSACTIONS.NETPRICE, OC_TRANSACTIONS.OCQTY  * OC_TRANSACTIONS.NETPRICE as [grandT], OC_TRANSACTIONS.FOCNO FROM OC_TRANSACTIONS INNER JOIN MASTER_PARTY ON OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE";

            if (txtFromDate.Text != "" && txtToDate.Text != "" && ComboParty.SelectedIndex == -1 && ComboTool.SelectedIndex == -1 && ComboSubTool.SelectedIndex == -1)
            {
                _Query = _Query + " WHERE  Convert(datetime,OCDT,103) between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "' ORDER BY Convert(datetime,OCDT,103) ASC";
            }
            else if (txtFromDate.Text != "" && txtToDate.Text != "" && ComboParty.SelectedIndex > 0 && ComboTool.SelectedIndex == -1 && ComboSubTool.SelectedIndex == -1)
            {
                _Query = _Query + " WHERE  OC_TRANSACTIONS.PARTY_CODE = '" + ComboParty.SelectedValue + "' AND Convert(datetime,OCDT,103) between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "' ORDER BY Convert(datetime,OCDT,103) ASC";
            }
            else if (txtFromDate.Text != "" && txtToDate.Text != "" && ComboParty.SelectedIndex <= 0 && ComboTool.SelectedIndex == -1 && ComboSubTool.SelectedIndex == -1)
            {
                _Query = _Query + " WHERE   Convert(datetime,OCDT,103) between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "' ORDER BY Convert(datetime,OCDT,103) ASC";
            }
            else if (txtFromDate.Text != "" && txtToDate.Text != "" && ComboParty.SelectedIndex == -1 && ComboTool.SelectedIndex >= 0 && ComboSubTool.SelectedIndex == -1)
            {
                _Query = _Query + " WHERE OC_TRANSACTIONS.TOOLTYPE = '"+ComboTool.SelectedValue+"' AND Convert(datetime,OCDT,103) between '"+txtFromDate.Text+"' and '"+txtToDate.Text+"' ORDER BY Convert(datetime,OCDT,103) ASC";
            }
            else if (txtFromDate.Text != "" && txtToDate.Text != "" && ComboParty.SelectedIndex == -1 && ComboTool.SelectedIndex == -1 && ComboSubTool.SelectedIndex >= 0)
            {
                _Query = _Query + " WHERE OC_TRANSACTIONS.MATCHTYPE = '" + ComboSubTool.SelectedValue + "' AND Convert(datetime,OCDT,103) between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "' ORDER BY Convert(datetime,OCDT,103) ASC";
            }
            else if (txtFromDate.Text != "" && txtToDate.Text != "" && ComboParty.SelectedIndex >= 0 && ComboTool.SelectedIndex >= 0 && ComboSubTool.SelectedIndex == -1)
            {
                _Query = _Query + " WHERE OC_TRANSACTIONS.PARTY_CODE = '" + ComboParty.SelectedValue + "' AND OC_TRANSACTIONS.TOOLTYPE = '" + ComboTool.SelectedValue + "' AND Convert(datetime,OCDT,103) between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "' ORDER BY Convert(datetime,OCDT,103) ASC";
            }
            else if (txtFromDate.Text != "" && txtToDate.Text != "" && ComboParty.SelectedIndex >= 0 && ComboTool.SelectedIndex >= 0 && ComboSubTool.SelectedIndex >= 0)
            {
                _Query = _Query + " WHERE OC_TRANSACTIONS.PARTY_CODE = '" + ComboParty.SelectedValue + "' AND OC_TRANSACTIONS.TOOLTYPE = '" + ComboTool.SelectedValue + "' AND  OC_TRANSACTIONS.MATCHTYPE = '" + ComboSubTool.SelectedValue + "' AND Convert(datetime,OCDT,103) between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "' ORDER BY Convert(datetime,OCDT,103) ASC";
            }
            else if (txtFromDate.Text != "" && txtToDate.Text != "" && ComboParty.SelectedIndex <= 0 && ComboTool.SelectedIndex >= 0 && ComboSubTool.SelectedIndex >= 0)
            {
                _Query = _Query + " WHERE  OC_TRANSACTIONS.TOOLTYPE = '" + ComboTool.SelectedValue + "' AND  OC_TRANSACTIONS.MATCHTYPE = '" + ComboSubTool.SelectedValue + "' AND Convert(datetime,OCDT,103) between '" + txtFromDate.Text + "' and '" + txtToDate.Text + "' ORDER BY Convert(datetime,OCDT,103) ASC";
            }

            Generic._fillGrid(_Query, GridBooking);
            if (GridBooking.Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = Generic.dataTable(_Query);


                object totalQty;
                totalQty = dt.Compute("Sum(QTY)", string.Empty);
                object sumObject;
                sumObject = dt.Compute("Sum(grandT)", string.Empty);
                decimal total = Convert.ToDecimal(sumObject);
                GridBooking.FooterRow.Font.Bold = true;
                GridBooking.FooterRow.Cells[0].Text = "Total";
                GridBooking.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                GridBooking.FooterRow.Cells[9].Text = totalQty.ToString();
                GridBooking.FooterRow.Cells[11].Text = total.ToString("N2");
            }
        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            if (GridBooking.Rows.Count > 0)
            {
                ExportToPDF(null, null);
            }
        }

        protected void btnWord_Click(object sender, EventArgs e)
        {
            if (GridBooking.Rows.Count > 0)
            {
                ExportToWord();
            }

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (GridBooking.Rows.Count > 0)
            {
                ExportToExcel();
            }

        }


        //Export To PDF

        protected void ExportToPDF(object sender, EventArgs e)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    
                    //To Export all pages
                    GridBooking.AllowPaging = false;
                    //this.BindGrid();

                    GridBooking.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + txtFromDate.Text + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }



        //Export To Word

        private void ExportToWord()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = txtFromDate.Text +" to "+txtToDate.Text + ".doc";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/msword";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridBooking.GridLines = GridLines.Both;
            GridBooking.HeaderStyle.Font.Bold = true;
            GridBooking.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }




        //Export to Excel

        private void ExportToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + txtFromDate.Text +" to "+txtToDate.Text + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridBooking.AllowPaging = false;
                //this.BindGrid();

                GridBooking.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in GridBooking.HeaderRow.Cells)
                {
                    cell.BackColor = GridBooking.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridBooking.Rows)
                {
                    //row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridBooking.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridBooking.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridBooking.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }




        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void ComboParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tool_query = "SELECT DISTINCT(TOOLTYPE) FROM OC_TRANSACTIONS WHERE PARTY_CODE = '" + ComboParty.SelectedValue + "'";
            Generic._fillCombo(tool_query, ComboTool, "TOOLTYPE", "TOOLTYPE");
            ComboSubTool.Items.Clear();
        }

        protected void ComboTool_SelectedIndexChanged(object sender, EventArgs e)
        {
            string subtoolQuery = "SELECT DISTINCT(MATCHTYPE) FROM OC_TRANSACTIONS WHERE TOOLTYPE = '" + ComboTool.SelectedValue + "'";
            Generic._fillCombo(subtoolQuery, ComboSubTool, "MATCHTYPE", "MATCHTYPE");
        }

        protected void ComboSubTool_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}