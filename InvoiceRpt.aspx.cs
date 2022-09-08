using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlankarNewDesign.DAL;
using System.Data;
using System.Data.SqlClient;
using ClosedXML;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using ClosedXML.Excel;

namespace AlankarNewDesign
{
    public partial class InvoiceRpt : System.Web.UI.Page
    {
        DbConnection Generic = new DbConnection();
        alankar_db_providerDataContext _db = new alankar_db_providerDataContext();
        public static string Query = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["username"] == null && Session["password"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if(!IsPostBack)
                {
                    _fillCombo();

                    string ToolQuery = "SELECT DISTINCT(OC_TRANSACTIONS.TOOLTYPE) FROM OC_TRANSACTIONS";
                    Generic._fillCombo(ToolQuery, ComboToolType, "TOOLTYPE", "TOOLTYPE");

                    string itemCodeQuery = "SELECT DISTINCT(ITEM_CODE) FROM OC_TRANSACTIONS";
                    Generic._fillCombo(itemCodeQuery, ComboItemCode, "ITEM_CODE", "ITEM_CODE");
                }
            }
        }

        private void _fillCombo()
        {
            var _query = from p in _db.MASTER_PARTies where p.STATUS == 0 select new { Party_Code = p.PARTY_CODE, Name = p.PARTY_NAME + " - " + p.PARTY_CODE };
            Generic._fillLinqCombo(_query, ComboBoxParty, "Party_Code", "Name");
        }
        protected void ComboBoxParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ComboBoxParty.SelectedIndex > 0)
            {
                string ToolQuery = "SELECT DISTINCT(OC_TRANSACTIONS.TOOLTYPE) FROM OC_TRANSACTIONS WHERE PARTY_CODE = '" + ComboBoxParty.SelectedValue + "'";
                ComboToolType.Items.Clear();
                ComboSubType.Items.Clear();
                ComboItemCode.Items.Clear();
                Generic._fillCombo(ToolQuery, ComboToolType, "TOOLTYPE", "TOOLTYPE");


                string itemCodeQuery = "SELECT DISTINCT(ITEM_CODE) FROM OC_TRANSACTIONS WHERE PARTY_CODE = '" + ComboBoxParty.SelectedValue + "'";
                Generic._fillCombo(itemCodeQuery, ComboItemCode, "ITEM_CODE", "ITEM_CODE");
            }
            else if(ComboBoxParty.SelectedIndex <=0)
            {
                string ToolQuery = "SELECT DISTINCT(OC_TRANSACTIONS.TOOLTYPE) FROM OC_TRANSACTIONS";
                ComboToolType.Items.Clear();
                ComboSubType.Items.Clear();
                ComboItemCode.Items.Clear();
                Generic._fillCombo(ToolQuery, ComboToolType, "TOOLTYPE", "TOOLTYPE");


                string itemCodeQuery = "SELECT DISTINCT(ITEM_CODE) FROM OC_TRANSACTIONS";
                Generic._fillCombo(itemCodeQuery, ComboItemCode, "ITEM_CODE", "ITEM_CODE");

            }




        }

        protected void ComboToolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboToolType.SelectedIndex > 0)
            {

                string itemCodeQuery = "SELECT DISTINCT(ITEM_CODE) FROM OC_TRANSACTIONS WHERE TOOLTYPE  = '" + ComboToolType.SelectedValue + "' AND PARTY_CODE = '" + ComboBoxParty.SelectedValue + "'";
                Generic._fillCombo(itemCodeQuery, ComboItemCode, "ITEM_CODE", "ITEM_CODE");


                string Qry = "SELECT DISTINCT(MATCHTYPE) FROM OC_TRANSACTIONS WHERE OC_TRANSACTIONS.TOOLTYPE = @Tool";
                SqlCommand cmd = new SqlCommand(Qry, Generic.con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@TOOL", ComboToolType.SelectedValue);
                Generic.con.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                ComboSubType.DataSource = ds;
                ComboSubType.DataValueField = "MATCHTYPE";
                ComboSubType.DataTextField = "MATCHTYPE";
                ComboSubType.DataBind();
                ComboSubType.Items.Insert(0, "");


            }
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {

            Query = "SELECT GST_INV.inv_no, OC_TRANSACTIONS.OC_NO, (Select PARTY_NAME+' ('+PARTY_CODE +')' from MASTER_PARTY where MASTER_PARTY.PARTY_CODE = OC_TRANSACTIONS.PARTY_CODE) as [PARTY_CODE], OC_TRANSACTIONS.TOOLTYPE, OC_TRANSACTIONS.MATCHTYPE, OC_TRANSACTIONS.ITEM_CODE, Convert(int, GST_INV.inv_qty) as [inv_qty], GST_INV.inv_date, OC_TRANSACTIONS.NETPRICE AS [RATE], OC_TRANSACTIONS.FOCNO,Convert(int, GST_INV.inv_qty) * OC_TRANSACTIONS.NETPRICE as [grT], OC_TRANSACTIONS.GRPPRICE  FROM GST_INV INNER JOIN OC_TRANSACTIONS ON GST_INV.oc_no = OC_TRANSACTIONS.OC_NO";
            if(ComboBoxParty.SelectedIndex > 0 && ComboToolType.SelectedIndex <=0 && ComboSubType.SelectedIndex <=0 && ComboItemCode.SelectedIndex <= 0 && txtFromDate.Text == "" && txtToDate.Text == "")
            {
                Query = Query + " WHERE OC_TRANSACTIONS.PARTY_CODE = '"+ComboBoxParty.SelectedValue+"'";
            }
            else if(ComboBoxParty.SelectedIndex > 0 && ComboToolType.SelectedIndex > 0 && ComboSubType.SelectedIndex <= 0 && ComboItemCode.SelectedIndex <= 0 && txtFromDate.Text == "" && txtToDate.Text == "")
            {
                Query = Query + " WHERE OC_TRANSACTIONS.PARTY_CODE = '"+ComboBoxParty.SelectedValue+"' AND OC_TRANSACTIONS.TOOLTYPE = '"+ComboToolType.SelectedValue+"'";
            }
            else if (ComboBoxParty.SelectedIndex > 0 && ComboToolType.SelectedIndex > 0 && ComboSubType.SelectedIndex > 0 && ComboItemCode.SelectedIndex <= 0 && txtFromDate.Text == "" && txtToDate.Text == "")
            {
                Query = Query + " WHERE OC_TRANSACTIONS.PARTY_CODE = '" + ComboBoxParty.SelectedValue + "' AND OC_TRANSACTIONS.TOOLTYPE = '" + ComboToolType.SelectedValue + "' AND OC_TRANSACTIONS.MATCHTYPE = '" + ComboSubType.SelectedValue + "'";
            }
            else if (ComboBoxParty.SelectedIndex > 0 && ComboToolType.SelectedIndex > 0 && ComboSubType.SelectedIndex > 0 && ComboItemCode.SelectedIndex > 0 && txtFromDate.Text == "" && txtToDate.Text == "")
            {
                Query = Query + " WHERE OC_TRANSACTIONS.PARTY_CODE = '" + ComboBoxParty.SelectedValue + "' AND OC_TRANSACTIONS.TOOLTYPE = '" + ComboToolType.SelectedValue + "' AND OC_TRANSACTIONS.MATCHTYPE = '" + ComboSubType.SelectedValue + "' AND OC_TRANSACTIONS.ITEM_CODE = '"+ComboItemCode.SelectedValue+"'";
            }
            else if (ComboBoxParty.SelectedIndex > 0 && ComboToolType.SelectedIndex > 0 && ComboSubType.SelectedIndex > 0 && ComboItemCode.SelectedIndex > 0 && txtFromDate.Text != "" && txtToDate.Text != "")
            {
                Query = Query + " WHERE OC_TRANSACTIONS.PARTY_CODE = '" + ComboBoxParty.SelectedValue + "' AND OC_TRANSACTIONS.TOOLTYPE = '" + ComboToolType.SelectedValue + "' AND OC_TRANSACTIONS.MATCHTYPE = '" + ComboSubType.SelectedValue + "' AND OC_TRANSACTIONS.ITEM_CODE = '" + ComboItemCode.SelectedValue + "' AND convert(datetime, GST_INV.inv_date, 103) BETWEEN '"+txtFromDate.Text+"' AND '"+txtToDate.Text+"'";
            }
            else if(ComboBoxParty.SelectedIndex <= 0 && ComboToolType.SelectedIndex > 0 && ComboSubType.SelectedIndex <= 0 && ComboItemCode.SelectedIndex <= 0 && txtFromDate.Text == "" && txtToDate.Text == "")
            {
                Query = Query + " WHERE OC_TRANSACTIONS.TOOLTYPE = '" + ComboToolType.SelectedValue + "'";
            }
            else if (ComboBoxParty.SelectedIndex <= 0 && ComboToolType.SelectedIndex > 0 && ComboSubType.SelectedIndex > 0 && ComboItemCode.SelectedIndex <= 0 && txtFromDate.Text == "" && txtToDate.Text == "")
            {
                Query = Query + " WHERE OC_TRANSACTIONS.TOOLTYPE = '" + ComboToolType.SelectedValue + "' AND OC_TRANSACTIONS.MATCHTYPE = '" + ComboSubType.SelectedValue + "'";
            }
            else if (ComboBoxParty.SelectedIndex <= 0 && ComboToolType.SelectedIndex > 0 && ComboSubType.SelectedIndex > 0 && ComboItemCode.SelectedIndex > 0 && txtFromDate.Text == "" && txtToDate.Text == "")
            {
                Query = Query + " WHERE OC_TRANSACTIONS.TOOLTYPE = '" + ComboToolType.SelectedValue + "' AND OC_TRANSACTIONS.MATCHTYPE = '" + ComboSubType.SelectedValue + "' AND OC_TRANSACTIONS.ITEM_CODE = '" + ComboItemCode.SelectedValue + "'";
            }
            else if (ComboBoxParty.SelectedIndex <= 0 && ComboToolType.SelectedIndex > 0 && ComboSubType.SelectedIndex > 0 && ComboItemCode.SelectedIndex <= 0 && txtFromDate.Text != "" && txtToDate.Text != "")
            {
                Query = Query + " WHERE OC_TRANSACTIONS.TOOLTYPE = '" + ComboToolType.SelectedValue + "' AND OC_TRANSACTIONS.MATCHTYPE = '" + ComboSubType.SelectedValue + "' AND convert(datetime, GST_INV.inv_date, 103) BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "' ";
            }
            else if (ComboBoxParty.SelectedIndex <= 0 && ComboToolType.SelectedIndex <= 0 && ComboSubType.SelectedIndex > 0 && ComboItemCode.SelectedIndex <= 0 && txtFromDate.Text != "" && txtToDate.Text != "")
            {
                Query = Query + " WHERE OC_TRANSACTIONS.TOOLTYPE = '" + ComboToolType.SelectedValue + "' AND convert(datetime, GST_INV.inv_date, 103) BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "' ";
            }

            else if (ComboBoxParty.SelectedIndex <= 0 && ComboToolType.SelectedIndex > 0 && ComboSubType.SelectedIndex > 0 && ComboItemCode.SelectedIndex <= 0 && txtFromDate.Text != "" && txtToDate.Text != "")
            {
                Query = Query + " WHERE OC_TRANSACTIONS.TOOLTYPE = '" + ComboToolType.SelectedValue + "' AND OC_TRANSACTIONS.MATCHTYPE = '" + ComboSubType.SelectedValue + " AND convert(datetime, GST_INV.inv_date, 103) BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "'";
            }
            else if (ComboBoxParty.SelectedIndex <= 0 && ComboToolType.SelectedIndex > 0 && ComboSubType.SelectedIndex > 0 && ComboItemCode.SelectedIndex > 0 && txtFromDate.Text != "" && txtToDate.Text != "")
            {
                Query = Query + " WHERE OC_TRANSACTIONS.TOOLTYPE = '" + ComboToolType.SelectedValue + "' AND OC_TRANSACTIONS.MATCHTYPE = '" + ComboSubType.SelectedValue + "' AND OC_TRANSACTIONS.ITEM_CODE = '" + ComboItemCode.SelectedValue + "' AND convert(datetime, GST_INV.inv_date, 103) BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "'";
            }
            else if (ComboBoxParty.SelectedIndex <= 0 && ComboToolType.SelectedIndex > 0 && ComboSubType.SelectedIndex <= 0 && ComboItemCode.SelectedIndex <= 0 && txtFromDate.Text != "" && txtToDate.Text != "")
            {
                Query = Query + " WHERE OC_TRANSACTIONS.TOOLTYPE = '" + ComboToolType.SelectedValue + "' AND convert(datetime, GST_INV.inv_date, 103) BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "'";
            }
            else if (ComboBoxParty.SelectedIndex <= 0 && ComboToolType.SelectedIndex <= 0 && ComboSubType.SelectedIndex <= 0 && ComboItemCode.SelectedIndex <= 0 && txtFromDate.Text != "" && txtToDate.Text != "")
            {
                Query = Query + " WHERE convert(datetime, GST_INV.inv_date, 103) BETWEEN '" + txtFromDate.Text + "' AND '" + txtToDate.Text + "'";
            }

            Generic._fillGrid(Query, GridRpt);
            if (GridRpt.Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = Generic.dataTable(Query);

                object totalQty;
                totalQty = dt.Compute("Sum(inv_qty)", string.Empty);
                object sumObject;
                sumObject = dt.Compute("Sum(grT)", string.Empty);
                decimal total = Convert.ToDecimal(sumObject);
                GridRpt.FooterRow.Font.Bold = true;
                GridRpt.FooterRow.Cells[0].Text = "Total";
                GridRpt.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Right;
                GridRpt.FooterRow.Cells[9].Text = totalQty.ToString();
                GridRpt.FooterRow.Cells[10].Text = total.ToString("N2");
            }
        }

        protected void ComboSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ComboSubType.SelectedIndex > 0)
            {
                string itemCodeQuery = string.Empty;
                if(ComboBoxParty.SelectedIndex > 0)
                {
                     itemCodeQuery = "SELECT DISTINCT(ITEM_CODE) FROM OC_TRANSACTIONS WHERE MATCHTYPE  = '" + ComboSubType.SelectedValue + "' AND PARTY_CODE = '" + ComboBoxParty.SelectedValue + "'";
                }
                else
                {
                    itemCodeQuery = "SELECT DISTINCT(ITEM_CODE) FROM OC_TRANSACTIONS WHERE MATCHTYPE  = '" + ComboSubType.SelectedValue + "'";
                }

                Generic._fillCombo(itemCodeQuery, ComboItemCode, "ITEM_CODE", "ITEM_CODE");
            }


        }

        protected void lnkPDF_Click(object sender, EventArgs e)
        {
            if(GridRpt.Rows.Count > 0)
            {
                ExportToPDF(null, null);
            }
        }

        protected void lnkWORD_Click(object sender, EventArgs e)
        {
            if (GridRpt.Rows.Count > 0)
            {
                ExportToWord();
            }
        }

        protected void lnkExcel_Click(object sender, EventArgs e)
        {
            if (GridRpt.Rows.Count > 0)
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
                    GridRpt.AllowPaging = false;
                    //this.BindGrid();

                    GridRpt.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + ComboBoxParty.SelectedValue + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
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
            string FileName = ComboBoxParty.SelectedValue + ".doc";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/msword";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridRpt.GridLines = GridLines.Both;
            GridRpt.HeaderStyle.Font.Bold = true;
            GridRpt.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();
        }



        private void ExportToExcel()
        {
            //Response.Clear();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=" + ComboBoxParty.SelectedValue + ".xls");
            //Response.Charset = "";
            //Response.ContentType = "application/vnd.ms-excel";
            //using (StringWriter sw = new StringWriter())
            //{
            //    HtmlTextWriter hw = new HtmlTextWriter(sw);

            //    //To Export all pages
            //    GridParty.AllowPaging = false;
            //    //this.BindGrid();

            //    GridParty.HeaderRow.BackColor = System.Drawing.Color.White;
            //    foreach (TableCell cell in GridParty.HeaderRow.Cells)
            //    {
            //        cell.BackColor = GridParty.HeaderStyle.BackColor;
            //    }
            //    foreach (GridViewRow row in GridParty.Rows)
            //    {
            //        //row.BackColor = Color.White;
            //        foreach (TableCell cell in row.Cells)
            //        {
            //            if (row.RowIndex % 2 == 0)
            //            {
            //                cell.BackColor = GridParty.AlternatingRowStyle.BackColor;
            //            }
            //            else
            //            {
            //                cell.BackColor = GridParty.RowStyle.BackColor;
            //            }
            //            cell.CssClass = "textmode";
            //        }
            //    }

            //    GridParty.RenderControl(hw);

            //    //style to format numbers to string
            //    string style = @"<style> .textmode { } </style>";
            //    Response.Write(style);
            //    Response.Output.Write(sw.ToString());
            //    Response.Flush();
            //    Response.End();
            //}



            DataTable dt = null;
            try
            {
                dt = Generic.dataTable(Query);

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Style.Font.FontName = "Verdana";
                    wb.Style.Font.FontSize = 8;

                    wb.Worksheets.Add(dt, "Customer");
                    wb.Properties.ToString();

                    wb.Style.Font.Bold = true;
                    wb.Style.Fill.BackgroundColor = XLColor.Blue;


                    string _fileName = string.Empty;
                    if (ComboBoxParty.SelectedIndex > 0)
                    {
                        _fileName = ComboBoxParty.SelectedValue;
                    }
                    else
                        _fileName = "Invoice Report";
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "utf-8";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + _fileName + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }

                }

            }
            catch (Exception Ex)
            {
            }
            finally
            {
                dt = null;
            }

        }

    }
}