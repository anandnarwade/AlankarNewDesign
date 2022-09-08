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
using System.Collections;

namespace AlankarNewDesign
{
    public partial class Input_oc_item_Code : System.Web.UI.Page
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


               
                    string _Query = "SELECT DISTINCT(OC_TRANSACTIONS.ITEM_CODE) FROM OC_TRANSACTIONS WHERE ITEM_CODE != ''";
                    Generic._fillCombo(_Query, ComboItemCode, "ITEM_CODE", "ITEM_CODE");
                   // ComboItemCode.Items.Insert(0, "");
               


            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string _QUERY = string.Empty;
            //if (txtOCNo.Text == "" && ComboItemCode.SelectedIndex > 0)
            //{
            //    _QUERY = "SELECT OC_NO, OCDT, CUSTOMER=(MASTER_PARTY.PARTY_NAME+' - '+OC_TRANSACTIONS.PARTY_CODE), ITEM_CODE, DRGNO, FOCNO, TOOLTYPE, MATCHTYPE, DESC1  FROM OC_TRANSACTIONS INNER JOIN MASTER_PARTY ON OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE WHERE OC_TRANSACTIONS.ITEM_CODE = '"+ComboItemCode.SelectedValue+"' ORDER BY CONVERT(DATETIME, OC_TRANSACTIONS.OCDT, 105)";
            //}
            //else if (txtOCNo.Text != "" && ComboItemCode.SelectedIndex == -1)
            //{

            //    _QUERY = "SELECT OC_NO, OCDT, CUSTOMER=(MASTER_PARTY.PARTY_NAME+' - '+OC_TRANSACTIONS.PARTY_CODE), ITEM_CODE, DRGNO, FOCNO, TOOLTYPE, MATCHTYPE, DESC1  FROM OC_TRANSACTIONS INNER JOIN MASTER_PARTY ON OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE WHERE OC_TRANSACTIONS.OC_NO = '" + txtOCNo.Text + "' ORDER BY CONVERT(DATETIME, OC_TRANSACTIONS.OCDT, 105)";
              
            //}
            //else if (txtOCNo.Text == "" && ComboItemCode.SelectedIndex < 0)
            //{
            //    _QUERY = "";
            //}

            //Generic._fillGrid(_QUERY, Grid);
          //  Response.Write("<script>alert('" + ComboItemCode.SelectedValue + "');</script>");
            string _ocNo = string.Empty;
            ArrayList list = Generic.GetArrayList("Select top 1 PARTY = MASTER_PARTY.PARTY_NAME+'-'+MASTER_PARTY.PARTY_CODE, OC_TRANSACTIONS.TOOLTYPE, OC_TRANSACTIONS.MATCHTYPE ,OC_TRANSACTIONS.DESC1, OC_TRANSACTIONS.OC_NO from OC_TRANSACTIONS  inner join MASTER_PARTY on OC_TRANSACTIONS.PARTY_CODE = MASTER_PARTY.PARTY_CODE where ITEM_CODE = '" + txtItemCode.Text + "'");
            lblIdCustName.Text = list[0].ToString();
            lblToolType.Text = list[1].ToString();
            lblSubType.Text = list[2].ToString();
            lblDesc.Text = list[3].ToString();
            _ocNo = list[4].ToString();
            var _query = from s in _db.DIMENTION_TRANSACTIONs where s.OC_NO == _ocNo select new { s.DIMENTION, Sub = s.SUB_DIMENSION, Value = s.VALU };
            DatalistDimensions.DataSource = _query;
            DatalistDimensions.DataBind();

            var rmQry = from r in _db.RAW_MATERIAL_TRANSACTIONs where r.OC_NO == _ocNo && r.RM_VALUE != "" select new { r.RAW_MATERIAL, r.RM_VALUE };
            datalistRm.DataSource = rmQry;
            datalistRm.DataBind();
            Generic._fillGrid("Select OCDT, OC_NO, DESC1, OC_TRANSACTIONS.OCQTY, OC_TRANSACTIONS.GRPPRICE from OC_TRANSACTIONS WHERE ITEM_CODE = '" + txtItemCode.Text + "'", Grid);

        }

        protected void btnPdf_Click(object sender, EventArgs e)
        {
            if (Grid.Rows.Count > 0)
            {
                ExportToPDF(null, null);
            }
        }

        protected void btnWord_Click(object sender, EventArgs e)
        {
            if (Grid.Rows.Count > 0)
            {
                ExportToWord();
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (Grid.Rows.Count > 0)
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
                    Grid.AllowPaging = false;
                    //this.BindGrid();

                    Grid.RenderControl(hw);
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
            Grid.GridLines = GridLines.Both;
            Grid.HeaderStyle.Font.Bold = true;
            Grid.RenderControl(htmltextwrtter);
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
                Grid.AllowPaging = false;
                //this.BindGrid();

                Grid.HeaderRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell cell in Grid.HeaderRow.Cells)
                {
                    cell.BackColor = Grid.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in Grid.Rows)
                {
                    //row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = Grid.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = Grid.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                Grid.RenderControl(hw);

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


        [System.Web.Services.WebMethod]
        public static string[] GetTagNames(string prefixText, int count)
        {
            alankar_db_providerDataContext _dbContext = new alankar_db_providerDataContext();
            // var query = _dbContext.STAGE_TRANSACTIONs.Where(n => n.STAGE_TYPE == "Issue" & n.STAGE == "Cleaning" & n.OC_NO.StartsWith(prefixText)).OrderBy(n => n.ID).Select(n => n.OC_NO).Distinct().Take(count).ToArray();
            //  return _dbContext.OC_TRANSACTIONs.Where(n => n.OC_NO.StartsWith(prefixText)).OrderBy(n => n.ID).Select(n => n.OC_NO).Distinct().Take(count).ToArray();
            return _dbContext.OC_TRANSACTIONs.Where(n => n.ITEM_CODE.StartsWith(prefixText)).OrderBy(n => n.ID).Select(n => n.ITEM_CODE).Distinct().Take(count).ToArray();
        }
    }
}