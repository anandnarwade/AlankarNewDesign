using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using AlankarNewDesign.DAL;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using ClosedXML.Excel;

namespace AlankarNewDesign
{
    public partial class ScheduleRpt : System.Web.UI.Page
    {
        private alankar_db_providerDataContext _db;
        private DbConnection Generic;
        public ScheduleRpt()
        {
            _db = new alankar_db_providerDataContext();
            Generic = new DbConnection();

        }

        public override void Dispose()
        {
            _db.Dispose();
        }

        //alankar_db_providerDataContext _db = new alankar_db_providerDataContext();
        //DbConnection Generic = new DbConnection();
        string _query = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null && Session["password"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
 

                

            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
          
            //if (chebal.Checked == true)
            //{
            //    _query = "SELECT OC_TRANSACTIONS.OC_NO, MASTER_PARTY.PARTY_NAME,  OC_TRANSACTIONS.TOOLTYPE, OC_TRANSACTIONS.MATCHTYPE,  SCHEDULE_DETAILS.SCHDATE, OC_TRANSACTIONS.GRPPRICE, SCHEDULE_DETAILS.QTY, VALUE = (OC_TRANSACTIONS.GRPPRICE * SCHEDULE_DETAILS.QTY) FROM OC_TRANSACTIONS FULL JOIN SCHEDULE_DETAILS ON OC_TRANSACTIONS.OC_NO = SCHEDULE_DETAILS.OCNO INNER JOIN MASTER_PARTY ON OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE WHERE CONVERT(datetime, SCHEDULE_DETAILS.SCHDATE, 120) BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDAte.Text + "' and SCHEDULE_DETAILS.BAL_QTY > 0 order by  CONVERT(datetime, SCHEDULE_DETAILS.SCHDATE, 120)";

            //}
            //else
            //{
            //    _query = "SELECT OC_TRANSACTIONS.OC_NO, MASTER_PARTY.PARTY_NAME,  OC_TRANSACTIONS.TOOLTYPE, OC_TRANSACTIONS.MATCHTYPE,  SCHEDULE_DETAILS.SCHDATE, OC_TRANSACTIONS.GRPPRICE, SCHEDULE_DETAILS.QTY, VALUE = (OC_TRANSACTIONS.GRPPRICE * SCHEDULE_DETAILS.QTY) FROM OC_TRANSACTIONS FULL JOIN SCHEDULE_DETAILS ON OC_TRANSACTIONS.OC_NO = SCHEDULE_DETAILS.OCNO INNER JOIN MASTER_PARTY ON OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE WHERE CONVERT(datetime, SCHEDULE_DETAILS.SCHDATE, 120) BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDAte.Text + "' order by  CONVERT(datetime, SCHEDULE_DETAILS.SCHDATE, 120)";
            //}

            _query = "SELECT OC_TRANSACTIONS.OC_NO, MASTER_PARTY.PARTY_NAME,  OC_TRANSACTIONS.TOOLTYPE, OC_TRANSACTIONS.MATCHTYPE,  SCHEDULE_DETAILS.SCHDATE, OC_TRANSACTIONS.GRPPRICE, SCHEDULE_DETAILS.OQTY,SCHEDULE_DETAILS.QTY, VALUE = (OC_TRANSACTIONS.GRPPRICE * SCHEDULE_DETAILS.QTY) FROM OC_TRANSACTIONS FULL JOIN SCHEDULE_DETAILS ON OC_TRANSACTIONS.OC_NO = SCHEDULE_DETAILS.OCNO INNER JOIN MASTER_PARTY ON OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE WHERE CONVERT(datetime, SCHEDULE_DETAILS.SCHDATE, 120) BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDAte.Text + "' and SCHEDULE_DETAILS.BAL_QTY > 0 order by  CONVERT(datetime, SCHEDULE_DETAILS.SCHDATE, 120)";

            DataTable dt = new DataTable();
            
            Generic._fillGrid(_query, GridSchedule);
            if (GridSchedule.Rows.Count > 0)
            {
                dt = Generic.dataTable(_query);


                object totalQty;
                totalQty = dt.Compute("Sum(QTY)", string.Empty);
                object sumObject;
                sumObject = dt.Compute("Sum(VALUE)", string.Empty);
                decimal total = Convert.ToDecimal(sumObject);
                GridSchedule.FooterRow.Font.Bold = true;
                GridSchedule.FooterRow.Cells[0].Text = "Total";
                GridSchedule.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                GridSchedule.FooterRow.Cells[7].Text = totalQty.ToString();
                GridSchedule.FooterRow.Cells[8].Text = total.ToString("N2");
            }

        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            if (GridSchedule.Rows.Count > 0)
            {
                ExportToPDF(null, null);
            }
           
        }

        protected void btnWord_Click(object sender, EventArgs e)
        {
            if (GridSchedule.Rows.Count > 0)
            {
                ExportToWord();
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (chebal.Checked == true)
            {
                _query = "SELECT OC_TRANSACTIONS.OC_NO as 'OC No', MASTER_PARTY.PARTY_NAME as 'Customer',  OC_TRANSACTIONS.TOOLTYPE as 'Tool Type', OC_TRANSACTIONS.MATCHTYPE as 'Sub Type',  SCHEDULE_DETAILS.SCHDATE as 'Date', OC_TRANSACTIONS.GRPPRICE as 'Price', SCHEDULE_DETAILS.QTY as 'Qty', VALUE = (OC_TRANSACTIONS.GRPPRICE * SCHEDULE_DETAILS.QTY) FROM OC_TRANSACTIONS FULL JOIN SCHEDULE_DETAILS ON OC_TRANSACTIONS.OC_NO = SCHEDULE_DETAILS.OCNO INNER JOIN MASTER_PARTY ON OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE WHERE CONVERT(datetime, SCHEDULE_DETAILS.SCHDATE, 120) BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDAte.Text + "' and SCHEDULE_DETAILS.BAL_QTY > 0 order by  CONVERT(datetime, SCHEDULE_DETAILS.SCHDATE, 120)";

            }
            else
            {
                _query = "SELECT OC_TRANSACTIONS.OC_NO as 'OC No', MASTER_PARTY.PARTY_NAME as 'Customer',  OC_TRANSACTIONS.TOOLTYPE as 'Tool Type', OC_TRANSACTIONS.MATCHTYPE as 'Sub Type',  SCHEDULE_DETAILS.SCHDATE as Date, OC_TRANSACTIONS.GRPPRICE as 'Price', SCHEDULE_DETAILS.QTY as 'Qty', VALUE = (OC_TRANSACTIONS.GRPPRICE * SCHEDULE_DETAILS.QTY) FROM OC_TRANSACTIONS FULL JOIN SCHEDULE_DETAILS ON OC_TRANSACTIONS.OC_NO = SCHEDULE_DETAILS.OCNO INNER JOIN MASTER_PARTY ON OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE WHERE CONVERT(datetime, SCHEDULE_DETAILS.SCHDATE, 120) BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDAte.Text + "' order by  CONVERT(datetime, SCHEDULE_DETAILS.SCHDATE, 120)";
            }
         

            if (GridSchedule.Rows.Count > 0)
            {
                //  ExportToExcel();
                DataTable dt = Generic.dataTable(_query);
                Generic.ExportToExcel(dt, DateTime.Now.ToString("ddMMyyyyhhmmss"));
            }
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        //Export To PDF

        protected void ExportToPDF(object sender, EventArgs e)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    //To Export all pages

                    GridSchedule.AllowPaging = false;
                    //this.BindGrid();

                    GridSchedule.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    string _filName = txtFromDate.Text + " to " + txtToDAte.Text;
                    Response.AddHeader("content-disposition", "attachment;filename=" + _filName + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }

        //Export to Word

        private void ExportToWord()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = txtFromDate.Text+" to "+txtToDAte.Text+".doc";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/msword";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridSchedule.GridLines = GridLines.Both;
            GridSchedule.HeaderStyle.Font.Bold = true;
            GridSchedule.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }


        //Export to Excel

        private void ExportToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + txtFromDate.Text +"to"+txtToDAte.Text + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridSchedule.AllowPaging = false;
                //this.BindGrid();

                GridSchedule.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in GridSchedule.HeaderRow.Cells)
                {
                    cell.BackColor = GridSchedule.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridSchedule.Rows)
                {
                    //row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridSchedule.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridSchedule.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridSchedule.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }




        }
    }
}