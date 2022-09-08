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
    public partial class Total_item_in_Clining : System.Web.UI.Page
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
                    //FILL CUSTOMER COMBO
                    string _partyQuery = "SELECT DISTINCT( MASTER_PARTY.PARTY_NAME), MASTER_PARTY.[PARTY_CODE]  FROM STAGE_TRANSACTIONS INNER JOIN OC_TRANSACTIONS ON STAGE_TRANSACTIONS.OC_NO = OC_TRANSACTIONS.OC_NO INNER JOIN MASTER_PARTY ON OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE WHERE STAGE_TRANSACTIONS.STAGE_TYPE = 'Issue' AND STAGE_TRANSACTIONS.STAGE = 'Cleaning' AND STAGE_TRANSACTIONS.VALUE != 0";
                    Generic._fillCombo(_partyQuery, ComboParty, "PARTY_CODE", "PARTY_NAME");

                    //FILL DEFAULT GRID
                    string Qry = "SELECT OC_TRANSACTIONS.OC_NO, OC_TRANSACTIONS.OCDT, OC_TRANSACTIONS.TOOLTYPE, OC_TRANSACTIONS.MATCHTYPE,  OC_TRANSACTIONS.DESC1,  CUSTOMER = (MASTER_PARTY.PARTY_NAME+' - '+MASTER_PARTY.PARTY_CODE), QTY = STAGE_TRANSACTIONS.VALUE, RATE = OC_TRANSACTIONS.GRPPRICE, AMOUNT = (OC_TRANSACTIONS.GRPPRICE * STAGE_TRANSACTIONS.VALUE) FROM STAGE_TRANSACTIONS INNER JOIN OC_TRANSACTIONS ON STAGE_TRANSACTIONS.OC_NO = OC_TRANSACTIONS.OC_NO INNER JOIN MASTER_PARTY ON OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE WHERE STAGE = 'Cleaning' AND STAGE_TYPE = 'Issue' AND VALUE != 0 ORDER BY CONVERT(DATETIME, OC_TRANSACTIONS.OCDT, 105) ASC";
                    Generic._fillGrid(Qry, GridClining);


                    string _toolQuery = "SELECT DISTINCT(OC_TRANSACTIONS.TOOLTYPE) FROM STAGE_TRANSACTIONS INNER JOIN OC_TRANSACTIONS ON STAGE_TRANSACTIONS.OC_NO = OC_TRANSACTIONS.OC_NO WHERE STAGE_TRANSACTIONS.STAGE_TYPE = 'Issue' AND STAGE_TRANSACTIONS.STAGE = 'Cleaning' AND STAGE_TRANSACTIONS.VALUE != 0";
                    Generic._fillCombo(_toolQuery, ComboTool, "TOOLTYPE", "TOOLTYPE");

                    string _subtoolQuery = "SELECT DISTINCT(OC_TRANSACTIONS.MATCHTYPE) FROM STAGE_TRANSACTIONS INNER JOIN OC_TRANSACTIONS ON STAGE_TRANSACTIONS.OC_NO = OC_TRANSACTIONS.OC_NO WHERE STAGE_TRANSACTIONS.STAGE_TYPE = 'Issue' AND STAGE_TRANSACTIONS.STAGE = 'Cleaning' AND STAGE_TRANSACTIONS.VALUE != 0";
                    Generic._fillCombo(_subtoolQuery, ComboSubType, "MATCHTYPE", "MATCHTYPE");
                }
            }
        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            if (GridClining.Rows.Count > 0)
            {
                ExportToPDF(null, null);
            }
        }

        protected void btnWord_Click(object sender, EventArgs e)
        {
            if (GridClining.Rows.Count > 0)
            {
                ExportToWord();
            }

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (GridClining.Rows.Count > 0)
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
                    GridClining.AllowPaging = false;
                    //this.BindGrid();

                    GridClining.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + "OC_Without_Po" + ".pdf");
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
            GridClining.GridLines = GridLines.Both;
            GridClining.HeaderStyle.Font.Bold = true;
            GridClining.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }




        //Export to Excel

        private void ExportToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + "OC_Without_Po" + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridClining.AllowPaging = false;
                //this.BindGrid();

                GridClining.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in GridClining.HeaderRow.Cells)
                {
                    cell.BackColor = GridClining.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridClining.Rows)
                {
                    //row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridClining.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridClining.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridClining.RenderControl(hw);

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

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
            string _Query = "SELECT OC_TRANSACTIONS.OC_NO, OC_TRANSACTIONS.OCDT, OC_TRANSACTIONS.TOOLTYPE, OC_TRANSACTIONS.MATCHTYPE,  OC_TRANSACTIONS.DESC1,  CUSTOMER = (MASTER_PARTY.PARTY_NAME+' - '+MASTER_PARTY.PARTY_CODE), QTY = STAGE_TRANSACTIONS.VALUE, RATE = OC_TRANSACTIONS.GRPPRICE, AMOUNT = (OC_TRANSACTIONS.GRPPRICE * STAGE_TRANSACTIONS.VALUE) FROM STAGE_TRANSACTIONS INNER JOIN OC_TRANSACTIONS ON STAGE_TRANSACTIONS.OC_NO = OC_TRANSACTIONS.OC_NO INNER JOIN MASTER_PARTY ON OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE WHERE STAGE = 'Cleaning' AND STAGE_TYPE = 'Issue' AND VALUE != 0 AND ";
            string _query2 = " ORDER BY CONVERT(DATETIME, OC_TRANSACTIONS.OCDT, 105) ASC";
            if (ComboParty.SelectedIndex > 0 && ComboTool.SelectedIndex <= 0 && ComboSubType.SelectedIndex <= 0 && txtFromDate.Text == "" && txtToDate.Text == "")
            {
                _Query = _Query + " OC_TRANSACTIONS.PARTY_CODE = '" + ComboParty.SelectedValue + "' " + _query2;
            }
            else if (ComboParty.SelectedIndex <= 0 && ComboTool.SelectedIndex > 0 && ComboSubType.SelectedIndex <= 0 && txtFromDate.Text == "" && txtToDate.Text == "")
            {
                _Query = _Query + " OC_TRANSACTIONS.TOOLTYPE = '" + ComboTool.SelectedValue + "' " + _query2;
            }
            else if (ComboParty.SelectedIndex <= 0 && ComboTool.SelectedIndex <= 0 && ComboSubType.SelectedIndex > 0 && txtFromDate.Text == "" && txtToDate.Text == "")
            {
                _Query = _Query + " OC_TRANSACTIONS.MATCHTYPE = '" + ComboSubType.SelectedValue + "' " + _query2;
            }
            else if (ComboParty.SelectedIndex <= 0 && ComboTool.SelectedIndex <= 0 && ComboSubType.SelectedIndex <= 0 && txtFromDate.Text != "" && txtToDate.Text != "")
            {
                _Query = _Query + " CONVERT(DATETIME, OC_TRANSACTIONS.OCDT, 105) BETWEEN '"+txtFromDate.Text+"' AND '"+txtToDate.Text+"' " + _query2;
            }
            else if (ComboParty.SelectedIndex > 0 && ComboTool.SelectedIndex > 0 && ComboSubType.SelectedIndex <= 0 && txtFromDate.Text == "" && txtToDate.Text == "")
            {
                _Query = _Query + " OC_TRANSACTIONS.PARTY_CODE = '" + ComboParty.SelectedValue + "' AND OC_TRANSACTIONS.TOOLTYPE = '" + ComboTool.SelectedValue + "' " + _query2;
            }
            else if (ComboParty.SelectedIndex > 0 && ComboTool.SelectedIndex > 0 && ComboSubType.SelectedIndex <= 0 && txtFromDate.Text != "" && txtToDate.Text != "")
            {
                _Query = _Query + " OC_TRANSACTIONS.PARTY_CODE = '" + ComboParty.SelectedValue + "' AND OC_TRANSACTIONS.TOOLTYPE = '" + ComboTool.SelectedValue + "' AND CONVERT(DATETIME, OC_TRANSACTIONS.OCDT, 105) BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "' " + _query2;
            }
            else if (ComboParty.SelectedIndex > 0 && ComboTool.SelectedIndex > 0 && ComboSubType.SelectedIndex > 0 && txtFromDate.Text == "" && txtToDate.Text == "")
            {
                _Query = _Query + " OC_TRANSACTIONS.PARTY_CODE = '" + ComboParty.SelectedValue + "' AND OC_TRANSACTIONS.TOOLTYPE = '" + ComboTool.SelectedValue + "' AND OC_TRANSACTIONS.MATCHTYPE = '" + ComboSubType.SelectedValue + "'" + _query2;
            }
            else if (ComboParty.SelectedIndex > 0 && ComboTool.SelectedIndex > 0 && ComboSubType.SelectedIndex > 0 && txtFromDate.Text != "" && txtToDate.Text != "")
            {
                _Query = _Query + " OC_TRANSACTIONS.PARTY_CODE = '" + ComboParty.SelectedValue + "' AND OC_TRANSACTIONS.TOOLTYPE = '" + ComboTool.SelectedValue + "' AND OC_TRANSACTIONS.MATCHTYPE = '" + ComboSubType.SelectedValue + "' AND CONVERT(DATETIME, OC_TRANSACTIONS.OCDT, 105) BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "'" + _query2;
            }
            Generic._fillGrid(_Query, GridClining);
        }

        protected void ComboParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboParty.SelectedIndex > 0)
            {
                ComboTool.Items.Clear();
                ComboSubType.Items.Clear();

                string _toolqry = "SELECT DISTINCT(OC_TRANSACTIONS.TOOLTYPE) FROM STAGE_TRANSACTIONS INNER JOIN OC_TRANSACTIONS ON STAGE_TRANSACTIONS.OC_NO = OC_TRANSACTIONS.OC_NO WHERE STAGE_TRANSACTIONS.STAGE_TYPE = 'Issue' AND STAGE_TRANSACTIONS.STAGE = 'Cleaning' AND STAGE_TRANSACTIONS.VALUE != 0 AND OC_TRANSACTIONS.PARTY_CODE = '" + ComboParty.SelectedValue + "'";
                Generic._fillCombo(_toolqry, ComboTool, "TOOLTYPE", "TOOLTYPE");

            }
            else
            {

                string _toolQuery = "SELECT DISTINCT(OC_TRANSACTIONS.TOOLTYPE) FROM STAGE_TRANSACTIONS INNER JOIN OC_TRANSACTIONS ON STAGE_TRANSACTIONS.OC_NO = OC_TRANSACTIONS.OC_NO WHERE STAGE_TRANSACTIONS.STAGE_TYPE = 'Issue' AND STAGE_TRANSACTIONS.STAGE = 'Cleaning' AND STAGE_TRANSACTIONS.VALUE != 0";
                Generic._fillCombo(_toolQuery, ComboTool, "TOOLTYPE", "TOOLTYPE");

                //string _subtoolQuery = "SELECT DISTINCT(OC_TRANSACTIONS.MATCHTYPE) FROM STAGE_TRANSACTIONS INNER JOIN OC_TRANSACTIONS ON STAGE_TRANSACTIONS.OC_NO = OC_TRANSACTIONS.OC_NO WHERE STAGE_TRANSACTIONS.STAGE_TYPE = 'Issue' AND STAGE_TRANSACTIONS.STAGE = 'Cleaning' AND STAGE_TRANSACTIONS.VALUE != 0";
                //Generic._fillCombo(_subtoolQuery, ComboSubType, "MATCHTYPE", "MATCHTYPE");

            }
        }

        protected void ComboTool_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboTool.SelectedIndex > 0)
            {
                ComboSubType.Items.Clear();

                string _subQry = "SELECT DISTINCT(OC_TRANSACTIONS.MATCHTYPE) FROM STAGE_TRANSACTIONS INNER JOIN OC_TRANSACTIONS ON STAGE_TRANSACTIONS.OC_NO = OC_TRANSACTIONS.OC_NO WHERE STAGE_TRANSACTIONS.STAGE_TYPE = 'Issue' AND STAGE_TRANSACTIONS.STAGE = 'Cleaning' AND STAGE_TRANSACTIONS.VALUE != 0 AND OC_TRANSACTIONS.TOOLTYPE = '" + ComboTool.SelectedValue + "'";
                Generic._fillCombo(_subQry, ComboSubType, "MATCHTYPE", "MATCHTYPE");
            }
            else
            {
                ComboSubType.Items.Clear();

                //string _subtoolQuery = "SELECT DISTINCT(OC_TRANSACTIONS.MATCHTYPE) FROM STAGE_TRANSACTIONS INNER JOIN OC_TRANSACTIONS ON STAGE_TRANSACTIONS.OC_NO = OC_TRANSACTIONS.OC_NO WHERE STAGE_TRANSACTIONS.STAGE_TYPE = 'Issue' AND STAGE_TRANSACTIONS.STAGE = 'Cleaning' AND STAGE_TRANSACTIONS.VALUE != 0";
                //Generic._fillCombo(_subtoolQuery, ComboSubType, "MATCHTYPE", "MATCHTYPE");
            }
        }
    }
}