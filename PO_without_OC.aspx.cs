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
namespace AlankarNewDesign
{
    public partial class PO_without_OC : System.Web.UI.Page
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


                string _query = "SELECT OC_NO, OCDT, ITEM_CODE, DESC1, DRGNO, CUSTOMER = (MASTER_PARTY.PARTY_NAME +' - '+MASTER_PARTY.PARTY_CODE) FROM OC_TRANSACTIONS INNER JOIN MASTER_PARTY ON OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE WHERE PONO = ''";
                Generic._fillGrid(_query, GridOC_po);


            }
        }

        protected void btnPdf_Click(object sender, EventArgs e)
        {
            if (GridOC_po.Rows.Count > 0)
            {
                ExportToPDF(null, null);
            }
        }

        protected void btnWord_Click(object sender, EventArgs e)
        {
            if (GridOC_po.Rows.Count > 0)
            {
                ExportToWord();
            }

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (GridOC_po.Rows.Count > 0)
            {
                ExportToExcel();
            }

        }



        protected void ExportToPDF(object sender, EventArgs e)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {

                    //To Export all pages
                    GridOC_po.AllowPaging = false;
                    //this.BindGrid();

                    GridOC_po.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename="+"OC_Without_Po"+ ".pdf");
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
            string FileName = "OC_without_PO.doc";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/msword";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridOC_po.GridLines = GridLines.Both;
            GridOC_po.HeaderStyle.Font.Bold = true;
            GridOC_po.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }




        //Export to Excel

        private void ExportToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + "OC_Without_Po"+ ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridOC_po.AllowPaging = false;
                //this.BindGrid();

                GridOC_po.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in GridOC_po.HeaderRow.Cells)
                {
                    cell.BackColor = GridOC_po.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridOC_po.Rows)
                {
                    //row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridOC_po.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridOC_po.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridOC_po.RenderControl(hw);

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

    }
}