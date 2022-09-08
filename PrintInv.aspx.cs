using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Linq;
using AlankarNewDesign.DAL;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;

namespace AlankarNewDesign
{
    public partial class PrintInv : System.Web.UI.Page
    {

        alankar_db_providerDataContext _dbContext = new alankar_db_providerDataContext();
        public static string _party_code;
        public static Int64 _ccId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null && Session["password"] == null) { Response.Redirect("login.aspx"); }
            else
            {
                string x = Request.QueryString["Action"];
                string inv_no = Request.QueryString["inv_no"];
                if (x == "UPDATE")
                {
                    _fillDatalist(inv_no);
                }

            }

        }
        public void _fillDatalist(string invNo)
        {
            try
            {
                string ocno = string.Empty;
                string _subTool = string.Empty;
                double _amount = 0;
                double _Rate = 0;
                double _finalAmount = 0;
                string _partycode = string.Empty;

               GST_INV gi = _dbContext.GST_INVs.SingleOrDefault(g => g.inv_no == invNo);
                ocno = gi.oc_no;
                lblInvNO.Text = invNo;
                lblInvDate.Text = gi.inv_date;
                lblIssueTime.Text = gi.issue_time;
                lblRemovingTime.Text = gi.removal_time;
                lblTrasMode.Text = gi.transportation_mode;
                lblQty.Text = gi.inv_qty;
                _amount = Convert.ToDouble(gi.amount);
                _amount = Math.Round(_amount, 2);
                lblAmount.Text = Math.Round(_amount, 2).ToString("#.00");

                lblPackingper.Text = gi.packing;
                //double freight = Convert.ToDouble(gi.freight);
                lblFreightPer.Text = gi.freight;

                lblCGstper.Text = gi.c_gst;

                lblSgstper.Text = gi.s_gst;

                lblIgstPer.Text = gi.i_gst;
                lblNote.Text = gi.note;

                if(gi.note == "")
                {
                    Note.Visible = false;
                }
                else
                {
                    Note.Visible = true;
                }
                _finalAmount = Convert.ToDouble(gi.total_amount);
                _finalAmount = Math.Round(_finalAmount, 2);
                lblfinalAmount.Text = Math.Round(_finalAmount, 2).ToString("#.00");

                lblFinalAmountinWords.Text = gi.total_amount_in_words;

                OC_TRANSACTION ot = _dbContext.OC_TRANSACTIONs.SingleOrDefault(o => o.OC_NO == ocno);
                lblitemCode.Text = ot.ITEM_CODE;
                lblDrgNO.Text = ot.DRGNO;


                lblDescription.Text = ot.DESC1;
                lblPoNO.Text = ot.PONO;
                lblPodate.Text = ot.PO_DATE;

                _Rate = Convert.ToDouble(ot.GRPPRICE);
                _Rate = Math.Round(_Rate, 2);
                lblRate.Text = Math.Round(_Rate, 2).ToString("#.00");
                _subTool = ot.MATCHTYPE;
                _partycode = ot.PARTY_CODE;

                lblUnit.Text = ot.UNIT;

                MASTER_PARTY _mp = _dbContext.MASTER_PARTies.SingleOrDefault(p => p.PARTY_CODE == _partycode);
                lblAddress.Text = _mp.PARTY_NAME;
                lblSlAdd.Text = _mp.FL_ADD;
                lblGSTNO.Text = _mp.GSTIN;
                lblPin.Text =_mp.CITY+" - "+ _mp.PIN;
                lblVenderCode.Text = _mp.VENDCODE;

                MASTER_TOOL mt = _dbContext.MASTER_TOOLs.SingleOrDefault(s => s.SUB_TYPE == _subTool);
                lblHSN_Code.Text = mt.HSN_NO;




                /*Calculate packing percetage*/
                double _packing = 0, _freight = 0, _cgst = 0, _sgst =0, _igst =0, _calPacking = 0, _calFreight = 0, _calcgst =0 , _calsgst = 0, _caligst = 0, _taxableAmount = 0;
                if (lblPackingper.Text == "" || lblPackingper.Text == null)
                {
                    _packing = 0;


                    lblPackingper.Text = "0.00";
                }
                else
                {

                    _packing = Convert.ToDouble(lblPackingper.Text);
                }

                _calPacking = _calculatePercentage(_packing, _amount);
                _calPacking = Math.Round(_calPacking, 2);
                if (_calPacking == 0.0)
                {
                    lblPackingAmount.Text = "0.00";
                }
                else
                {
                    lblPackingper.Text = Math.Round(_packing, 2).ToString("#.00");
                    lblPackingAmount.Text = Math.Round(_calPacking, 2).ToString("#.00");
                }


                if (lblFreightPer.Text == "" || lblFreightPer.Text == null)
                {
                    _freight = 0;
                    lblFreightPer.Text = "0.00";
                }
                else
                {
                    _freight = Convert.ToDouble(lblFreightPer.Text);
                }
                //_calFreight = _calculatePercentage(_freight, _amount);
                //_calFreight = Math.Round(_calFreight, 2);
                //if (_calFreight == 0.0)
                //{

                //    lblFreightAmount.Text = "0.00";
                //}
                //else
                //{
                //    lblFreightPer.Text = Math.Round(_freight, 2).ToString("#.00");
                //    lblFreightAmount.Text = Math.Round(_calFreight, 2).ToString("#.00");
                //}


                _freight = Math.Round(_freight, 2);
                if (_freight != 0.0)
                {
                    lblFreightAmount.Text = _freight.ToString("#.00");
                }
                else
                {
                    lblFreightAmount.Text = "0.00";
                }




                _taxableAmount = _calPacking + _freight + _amount;
                _taxableAmount = Math.Round(_taxableAmount, 2);
                lblTaxableAmount.Text = Math.Round(_taxableAmount, 2).ToString("#.00");



                if (lblCGstper.Text == "" || lblCGstper.Text == null)
                {

                    _cgst = 0;
                    lblCgst.Text = "0.00";

                }
                else
                {
                    _cgst = Convert.ToDouble(lblCGstper.Text);
                }

                _calcgst = _calculatePercentage(_cgst, _taxableAmount);
                _calcgst = Math.Round(_calcgst, 2);
                if (_calcgst == 0.0)
                {

                    lblCgst.Text = "0.00";
                    lblCGstper.Text = "0.00";
                }
                else
                {
                    lblCGstper.Text = Math.Round(_cgst, 2).ToString("#.00");
                    lblCgst.Text = Math.Round(_calcgst, 2).ToString("#.00");
                }





                if (lblSgstper.Text == "" || lblSgstper.Text == null)
                {

                    _sgst = 0;
                    lblSgstper.Text = "0.00";
                }
                else
                {
                    _sgst = Convert.ToDouble(lblSgstper.Text);
                }

                _calsgst = _calculatePercentage(_sgst, _taxableAmount);
                _calsgst = Math.Round(_calsgst, 2);
                if (_calsgst == 0.0)
                {

                    lblSgst.Text = "0.00";
                    lblSgstper.Text = "0.00";
                }
                else
                {
                    lblSgstper.Text = Math.Round(_sgst, 2).ToString("#.00");

                    lblSgst.Text = Math.Round(_calsgst, 2).ToString("#.00");
                }






                if (lblIgstPer.Text == "" || lblIgstPer.Text == null)
                {

                    _igst = 0;
                    lblIgstPer.Text = "0.00";
                }
                else
                {
                    _igst = Convert.ToDouble(lblIgstPer.Text);
                }

                _caligst = _calculatePercentage(_igst, _taxableAmount);
                _caligst = Math.Round(_caligst, 2);
              //  string calgst = _calcgst.ToString();
                if (_caligst == 0.0)
                {

                    lblIgst.Text = "0.00";
                }

                else
                {
                    lblIgstPer.Text = Math.Round(_igst, 2).ToString("#.00");
                    lblIgst.Text = Math.Round(_caligst, 2).ToString("#.00");
                }


                /*calculation packing ends*/
            }
            catch (Exception ex)
            {

            }
        }


        public double _calculatePercentage(double _percentage, double _pOf)
        {
            //Int64 Qty = Convert.ToInt64(lblQty.Text);
            //Int64 _rate = Convert.ToInt64(lblRate.Text);
            //Int64 Total_amt = _rate * Qty;
            double _result = 0;

            try
            {
                _result = (_percentage * _pOf) / 100;
                _result = Math.Round(_result, 2);
            }
            catch (Exception ex)
            {

            }
            return _result;

        }

        protected void printPdf_Click(object sender, EventArgs e)
        {
            try
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + lblInvNO.Text + ".pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                pnlContents.RenderControl(hw);
                StringReader sr = new StringReader(sw.ToString());
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();
                htmlparser.Parse(sr);
                pdfDoc.Close();
                Response.Write(pdfDoc);
                Response.End();




            }
            catch (Exception ex)
            {

            }
        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {

        }




        //protected void ExportToPDF(object sender, EventArgs e)
        //{
        //    StringReader sr = new StringReader(Request.Form[hfGridHtml.UniqueID]);
        //    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        //    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //    pdfDoc.Open();
        //    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
        //    pdfDoc.Close();
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("content-disposition", "attachment;filename=HTML.pdf");
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.Write(pdfDoc);
        //    Response.End();
        //}


    }
}