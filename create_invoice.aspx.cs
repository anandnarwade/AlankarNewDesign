using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq;
using AlankarNewDesign.DAL;
using System.Collections;

namespace AlankarNewDesign
{
    public partial class create_invoice : System.Web.UI.Page
    {
        alankar_db_providerDataContext _dbContext = new alankar_db_providerDataContext();
        DbConnection obj = new DbConnection();
        string _action = string.Empty;
        string _userName = string.Empty;
        public static double _taxable_amount;
        protected void Page_Load(object sender, EventArgs e)
        {
            string x = Request.QueryString["Action"];
            string inv_no = Request.QueryString["inv_no"];
            if (!IsPostBack)
            {
                if (x == "UPDATE")
                {
                    _fillData(inv_no);
                }

            }



        }

        [System.Web.Services.WebMethod]
        public static string[] GetTagNames(string prefixText, int count)
        {
            alankar_db_providerDataContext _dbContext = new alankar_db_providerDataContext();
           // var query = _dbContext.STAGE_TRANSACTIONs.Where(n => n.STAGE_TYPE == "Issue" & n.STAGE == "Cleaning" & n.OC_NO.StartsWith(prefixText)).OrderBy(n => n.ID).Select(n => n.OC_NO).Distinct().Take(count).ToArray();
          //  return _dbContext.OC_TRANSACTIONs.Where(n => n.OC_NO.StartsWith(prefixText)).OrderBy(n => n.ID).Select(n => n.OC_NO).Distinct().Take(count).ToArray();
            return _dbContext.STAGE_TRANSACTIONs.Where(n => n.STAGE_TYPE == "Issue" & n.STAGE == "Cleaning" & n.OC_NO.StartsWith(prefixText)).OrderBy(n => n.ID).Select(n => n.OC_NO).Distinct().Take(count).ToArray();
        }


        public void _fillDetails(string _ocNo)
        {
            try
            {
                string _sub_tool = string.Empty;
                OC_TRANSACTION ot = _dbContext.OC_TRANSACTIONs.SingleOrDefault(oc => oc.OC_NO == _ocNo);
                lblItem_code.Text = ot.ITEM_CODE;
                lbldimUp.Text = ot.PARTY_CODE;
                lblDrgNO.Text = ot.DRGNO;
                lblToolDesc.Text = ot.DESC1;
                lblRate.Text = ot.GRPPRICE.ToString();
                MASTER_PARTY mp = _dbContext.MASTER_PARTies.SingleOrDefault(a => a.PARTY_CODE == lbldimUp.Text);
                lblCustumer.Text = mp.PARTY_NAME;
                _sub_tool = ot.MATCHTYPE;
                foreach (STAGE_TRANSACTION st in _dbContext.STAGE_TRANSACTIONs)
                {
                    if (st.OC_NO == txtOcno.Text && st.STAGE_TYPE == "Issue" && st.STAGE == "Cleaning")
                    {
                        lblQty.Text = st.VALUE.ToString();
                        break;
                    }
                }

                if (btnSAVE.Text == "SAVE")
                {

                    foreach (STAGE_TRANSACTION st in _dbContext.STAGE_TRANSACTIONs)
                    {
                        if (st.OC_NO == txtOcno.Text && st.STAGE_TYPE == "Issue" && st.STAGE == "Cleaning")
                        {
                            lblQty.Text = st.VALUE.ToString();
                            break;
                        }
                    }


                }



                MASTER_TOOL mt = _dbContext.MASTER_TOOLs.SingleOrDefault(s => s.SUB_TYPE == _sub_tool);
                lblHsn.Text = mt.HSN_NO;

                Int64 _qty = 0;
                double _rate = 0;
                double _amount = 0;
                _qty = Convert.ToInt64(lblQty.Text);
                _rate = Convert.ToDouble(ot.GRPPRICE);
                _amount = _rate * _qty;
                //_amount = Math.Round(_amount, 2);
              //  lblAmount.Text = Convert.ToString(Math.Round(_amount, 2).ToString("0.##"));

            }
            catch (Exception ex)
            {

            }
        }

        protected void txtOcno_TextChanged(object sender, EventArgs e)
        {
            _fillDetails(txtOcno.Text);
        }

        protected void txtinvQty_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtPacking_TextChanged(object sender, EventArgs e)
        {


            double x = 0, y = 0, z = 0, Total_amt, Qty, _rate, _packing;
            Qty = Convert.ToInt64(txtinvQty.Text);
            _rate = Convert.ToDouble(lblRate.Text);

            Total_amt = _rate * Qty;

            if (txtPacking.Text == "")
            {
                x = 0;
            }
            else if (txtPacking.Text != "")
            {
                x = Convert.ToDouble(txtPacking.Text);
            }



           y = (x * Total_amt)/ 100 ;
           y = Math.Round(y, 2, MidpointRounding.AwayFromZero);
           //_packing = y * Total_amt;

           txtTaxableValue.Text = y.ToString() ;
           txtTaxableValue.Text = Math.Round(y, 2).ToString("#.00");

           lbltaxableValue.Text = txtTaxableValue.Text;
        }

        protected void btnCalculateTaxes_Click(object sender, EventArgs e)
        {

            /*checking inv qty*/
            dismiss.Visible = false;
            lblMessage.Text = "";
            Int64 _Qty2 = 0;
            Int64 _invQty2 = 0;
            _Qty2 = Convert.ToInt64(lblQty.Text);
            _invQty2 = Convert.ToInt64(txtinvQty.Text);
            if(btnSAVE.Text == "SAVE")
            {
                if (_invQty2 <= _Qty2)
                {

                }
                else
                {
                    dismiss.Visible = true;
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "invoice Qty is Greater than Qty";
                    txtinvQty.Text = "";
                    txtinvQty.Focus();
                    return;

                }


            }



            /*check end*/




            double _packing = 0, _freight = 0, _cgst = 0, _sgst = 0, _igst = 0, _taxable_amt = 0, _cgstPer = 0, _sgstPer = 0, _IgstPer = 0, _finalAmount = 0;
            double x = 0, y = 0, z = 0, Total_amt, Qty, _rate;
            Qty = Convert.ToInt64(txtinvQty.Text);
            _rate = Convert.ToDouble(lblRate.Text);

            /*calculate packing*/

            Total_amt = _rate * Qty;
            lblAmount.Text = Total_amt.ToString();

            if (txtPacking.Text == "")
            {
                x = 0;
            }
            else if (txtPacking.Text != "")
            {
                x = Convert.ToDouble(txtPacking.Text);
            }

            y = _calculatePercentage(x, Total_amt);

            //y = (x * Total_amt) / 100;
            //y = Math.Round(y, 2, MidpointRounding.AwayFromZero);

            _packing = y;



            /*packing calculated*/



            /*calculate _freight*/

            double _a = 0, _b = 0, _c = 0;
            if (txtFreight.Text == "")
            {
                _a = 0;
            }
            else if (txtFreight.Text != "")
            {
                _a = Convert.ToDouble(txtFreight.Text);

            }
            _freight = _a;

            _taxable_amt = Total_amt + _packing + _freight;

            _taxable_amt = Math.Round(_taxable_amt, 2);
            _taxable_amount = Math.Round(_taxable_amt, 2);
            txtTaxableValue.Text = Math.Round(_taxable_amt, 2).ToString("#.00");


            /* freight calculated*/

            /*calculate c-gst*/
            if (txtCGST.Text == "")
            {
                _cgst = 0;
            }
            else if (txtCGST.Text != "")
            {
                _cgst = Convert.ToDouble(txtCGST.Text);
            }

            _cgstPer = _calculatePercentage(_cgst, _taxable_amount);

            /*c-gst calculate*/


            /*calculate s-gst*/

            if (txtSGST.Text == "")
            {
                _sgst = 0;
            }
            else if (txtSGST.Text != "")
            {
                _sgst = Convert.ToDouble(txtSGST.Text);
            }

            _sgstPer = _calculatePercentage(_sgst, _taxable_amount);

            /*s-gst calculate*/


            /*calculate i-gst*/
            if (txtIGST.Text == "")
            {
                _igst = 0;
            }
            else if (txtIGST.Text != "")
            {
                _igst = Convert.ToDouble(txtIGST.Text);
            }

            _IgstPer = _calculatePercentage(_igst, _taxable_amount);
            /*i-gst calculate*/


            /*Calculate Final Amount*/

            _finalAmount = (_taxable_amt + _cgstPer + _sgstPer + _IgstPer);
            _finalAmount = Math.Round(_finalAmount, 2);
            txtfinalAmount.Text = Math.Round(_finalAmount, 2).ToString("#.00");


            string[] amountParts = txtfinalAmount.Text.Split('.');
            string _first = amountParts[0];
            string _last = amountParts[1];

            Int64 _firstValue = Convert.ToInt64(_first);
            Int64 _lastValue = Convert.ToInt64(_last);

            string _firstDigits = ConvertNumbertoWords(_firstValue);

            string _lastDigits = ConvertNumbertoWords(_lastValue);
            if (_lastDigits == "ZERO")
            {
                lblAmountInWords.Text = "RS " + _firstDigits + " ONLY";
            }
            else
            {
                lblAmountInWords.Text = "RS " + _firstDigits + " AND " + _lastDigits + " PAISA ONLY";
            }



        }




        public double _calculatePercentage(double _percentage, double _pOf)
        {
            Int64 Qty =   Convert.ToInt64(txtinvQty.Text);
            double _rate = Convert.ToDouble(lblRate.Text);
            //Int64 Total_amt = _rate * Qty;
            Int64 Total_amt = Convert.ToInt64(_taxable_amount);
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



        public static string ConvertNumbertoWords(Int64 number)
        {
            if (number == 0) return "ZERO";
            if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 100000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " LACS ";
                number %= 100000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            //if ((number / 10) > 0)
            //{
            // words += ConvertNumbertoWords(number / 10) + " RUPEES ";
            // number %= 10;
            //}
            if (number > 0)
            {
                if (words != "") words += " ";
                var unitsMap = new[]
        {
            "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
        };
                var tensMap = new[]
        {
            "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
        };
                if (number < 20) words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }

        public bool _invExists()
        {
            bool _result = false;
            try
            {
                foreach (GST_INV gi in _dbContext.GST_INVs)
                {
                    if (txtInvoiceNo.Text == gi.inv_no)
                    {
                        _result = true;
                        break;

                    }

                }

            }
            catch (Exception ex)
            {

            }
            return _result;
        }
        protected void btnSAVE_Click(object sender, EventArgs e)
        {


            if (btnSAVE.Text == "SAVE")
            {
                bool _isexists = _invExists();
                if (_isexists == true)
                {
                    dismiss.Visible = true;
                    lblMessage.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                    lblMessage.Text = "Invoice no already Exists...";
                    txtInvoiceNo.Focus();
                }
                else
                {

                    if (txtIGST.Text == "")
                    {

                        if (txtCGST.Text == "")
                        {

                            dismiss.Visible = true;
                            lblMessage.Visible = true;
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                            lblMessage.Text = "Please enter CGST value...";
                            txtCGST.Focus();
                            return;
                        }

                        if (txtSGST.Text == "")
                        {

                            dismiss.Visible = true;
                            lblMessage.Visible = true;
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                            lblMessage.Text = "Please enter SGST value...";
                            txtSGST.Focus();
                            return;

                        }

                        if (txtCGST.Text != "" && txtSGST.Text != "")
                        {
                            _action = "INSERT";


                            btnCalculateTaxes_Click(null, null);

                            _dbContext.sp_create_inv(txtOcno.Text, txtInvoiceNo.Text, txtInvoiceDate.Text, txtIssueTime.Text, txtRemoveTime.Text, txtTranspMode.Text, txtinvQty.Text, lblAmount.Text, txtPacking.Text, txtFreight.Text, txtTaxableValue.Text, txtCGST.Text, txtSGST.Text, txtIGST.Text, txtfinalAmount.Text, lblAmountInWords.Text, _userName, null, null, null, txtNote.Text, _action);
                            _scheduleQty(txtOcno.Text, Convert.ToInt32(txtinvQty.Text));
                            dismiss.Visible = true;
                            lblMessage.Visible = true;
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                            lblMessage.Text = "Saved Successfully...";
                            Response.Redirect("PrintInv.aspx?inv_no=" + txtInvoiceNo.Text + "&Action=UPDATE");

                        }



                    }
                    else if (txtIGST.Text != "")
                    {
                        if (txtCGST.Text != "")
                        {

                            dismiss.Visible = true;
                               lblMessage.Visible = true;
                               ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                               lblMessage.Text = "Please remove CGST value...";
                                return;
                        }

                        if (txtSGST.Text != "")
                        {

                            dismiss.Visible = true;
                            lblMessage.Visible = true;
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                            lblMessage.Text = "Please remove SGST value...";
                            return;

                        }

                        if (txtCGST.Text == "" && txtSGST.Text == "")
                        {
                            _action = "INSERT";


                            btnCalculateTaxes_Click(null, null);
                            if(_action == "INSERT")
                            {
                                _dbContext.sp_create_inv(txtOcno.Text, txtInvoiceNo.Text, txtInvoiceDate.Text, txtIssueTime.Text, txtRemoveTime.Text, txtTranspMode.Text, txtinvQty.Text, lblAmount.Text, txtPacking.Text, txtFreight.Text, txtTaxableValue.Text, txtCGST.Text, txtSGST.Text, txtIGST.Text, txtfinalAmount.Text, lblAmountInWords.Text, _userName, null, null, null, txtNote.Text, _action);
                                _scheduleQty(txtOcno.Text, Convert.ToInt32(txtinvQty.Text));
                            }

                            dismiss.Visible = true;
                            lblMessage.Visible = true;
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                            lblMessage.Text = "Saved Successfully...";
                            Response.Redirect("PrintInv.aspx?inv_no=" + txtInvoiceNo.Text + "&Action=UPDATE");

                        }

                    }





                }


            }
            else if (btnSAVE.Text == "UPDATE")
            {
                Int64 _invQty = Convert.ToInt64(lblQty.Text);
                Int64 _userInvQty = Convert.ToInt64(txtinvQty.Text);

                var oc = _dbContext.OC_TRANSACTIONs.Single(s => s.OC_NO == txtOcno.Text);

                Int64 ocQty = Convert.ToInt64(oc.OCQTY);
                string GetNotIssueValue = obj.GetStr("select sum(value) from STAGE_TRANSACTIONS where OC_NO = '"+txtOcno.Text+"' and STAGE_TYPE = 'Not Issue'");
                string GetIssueValue = obj.GetStr("select sum(value) from STAGE_TRANSACTIONS where OC_NO = '"+txtOcno.Text+"' and STAGE_TYPE = 'Issue'"); ;
                string GetdispatchValue = obj.GetStr("select sum(cast(inv_qty as bigint)) from GST_INV where oc_no = '" + txtOcno.Text + "'");

                string GetInvoiceValue = obj.GetStr("select inv_qty from GST_INV where inv_no = '"+txtInvoiceNo.Text+"'");
                string GetCliningValue = obj.GetStr("SELECT VALUE FROM STAGE_TRANSACTIONS WHERE OC_NO = '"+txtOcno.Text+"' AND  STAGE_TYPE = 'Issue' AND STAGE = 'Cleaning'");
                string GetWithoutClining = obj.GetStr("SELECT sum(VALUE) FROM STAGE_TRANSACTIONS WHERE OC_NO = '"+txtOcno.Text+"' AND  STAGE_TYPE = 'Issue' AND STAGE != 'Cleaning'");

                string GetValueWithOutInvoice = obj.GetStr("select sum(cast(inv_qty as bigint)) from GST_INV where inv_no != '"+txtInvoiceNo.Text+"' and oc_no = '"+txtOcno.Text+"'");
                btnCalculateTaxes_Click(null, null);

                Int64 NotIssue, Issue, Dispatched, InvoiceQty, clining, withoutClining, ValueWithoutInvoice;

                if(GetNotIssueValue == "" || GetNotIssueValue == null)
                {
                    NotIssue = 0;
                }
                else
                {
                    NotIssue = Convert.ToInt64(GetNotIssueValue);
                }

                if(GetIssueValue == "" || GetIssueValue == null)
                {
                    Issue = 0;
                }
                else
                {
                    Issue = Convert.ToInt64(GetIssueValue);
                }

                if(GetdispatchValue == "" || GetdispatchValue == null)
                {
                    Dispatched = 0;
                }
                else
                {
                    Dispatched = Convert.ToInt64(GetdispatchValue);
                }

                if(GetInvoiceValue == "" || GetInvoiceValue == null)
                {
                    InvoiceQty = 0;
                }
                else
                {
                    InvoiceQty = Convert.ToInt64(GetInvoiceValue);
                }

                if(GetCliningValue == "" || GetCliningValue == null)
                {
                    clining = 0;
                }
                else
                {
                    clining = Convert.ToInt64(GetCliningValue);
                }


                if(GetWithoutClining == "" || GetWithoutClining == null)
                {
                    withoutClining = 0;
                }
                else
                {
                    withoutClining = Convert.ToInt64(GetWithoutClining);
                }

                if(GetValueWithOutInvoice == "" || GetValueWithOutInvoice == null)
                {
                    ValueWithoutInvoice = 0;
                }
                else
                {
                    ValueWithoutInvoice = Convert.ToInt64(GetValueWithOutInvoice);
                }


                if (ocQty == (NotIssue + Issue + Dispatched))
                {
                    if(InvoiceQty == _userInvQty)
                    {
                        _action = "UPDATE";
                        btnCalculateTaxes_Click(null, null);
                        _dbContext.sp_create_inv(txtOcno.Text, txtInvoiceNo.Text, txtInvoiceDate.Text, txtIssueTime.Text, txtRemoveTime.Text, txtTranspMode.Text, _userInvQty.ToString(), lblAmount.Text, txtPacking.Text, txtFreight.Text, txtTaxableValue.Text, txtCGST.Text, txtSGST.Text, txtIGST.Text, txtfinalAmount.Text, lblAmountInWords.Text, _userName, null, null, null, txtNote.Text, _action);
                        dismiss.Visible = true;
                        lblMessage.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        lblMessage.Text = "Updated Successfully...";



                    }

                    if(_userInvQty < InvoiceQty)
                    {
                        Int64 difference = InvoiceQty - _userInvQty;

                        _action = "UPDATE";
                        btnCalculateTaxes_Click(null, null);
                        _dbContext.sp_create_inv(txtOcno.Text, txtInvoiceNo.Text, txtInvoiceDate.Text, txtIssueTime.Text, txtRemoveTime.Text, txtTranspMode.Text, _userInvQty.ToString(), lblAmount.Text, txtPacking.Text, txtFreight.Text, txtTaxableValue.Text, txtCGST.Text, txtSGST.Text, txtIGST.Text, txtfinalAmount.Text, lblAmountInWords.Text, _userName, null, null, null, txtNote.Text, _action);
                        obj._SavedAs("UPDATE STAGE_TRANSACTIONS SET VALUE = VALUE + "+difference+" WHERE OC_NO = '"+txtOcno.Text+"' AND STAGE_TYPE = 'Issue' AND STAGE = 'Cleaning'");
                        dismiss.Visible = true;
                        lblMessage.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        lblMessage.Text = "Updated Successfully...";


                    }
                    else if(_userInvQty > InvoiceQty)
                    {
                        if(ocQty == (NotIssue + withoutClining+ clining+ _userInvQty+ ValueWithoutInvoice))
                        {

                            _action = "UPDATE";
                            btnCalculateTaxes_Click(null, null);
                            _dbContext.sp_create_inv(txtOcno.Text, txtInvoiceNo.Text, txtInvoiceDate.Text, txtIssueTime.Text, txtRemoveTime.Text, txtTranspMode.Text, _userInvQty.ToString(), lblAmount.Text, txtPacking.Text, txtFreight.Text, txtTaxableValue.Text, txtCGST.Text, txtSGST.Text, txtIGST.Text, txtfinalAmount.Text, lblAmountInWords.Text, _userName, null, null, null, txtNote.Text, _action);
                            dismiss.Visible = true;
                            lblMessage.Visible = true;
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                            lblMessage.Text = "Updated Successfully...";


                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                            lblMessage.Text = "Enter Qty is greater than invoice qty...";
                        }

                    }

                }
                else if(ocQty == (NotIssue + Issue + Dispatched + _userInvQty))
                {
                    if(ocQty == _userInvQty)
                    {
                        _action = "UPDATE";
                        btnCalculateTaxes_Click(null, null);
                        _dbContext.sp_create_inv(txtOcno.Text, txtInvoiceNo.Text, txtInvoiceDate.Text, txtIssueTime.Text, txtRemoveTime.Text, txtTranspMode.Text, _userInvQty.ToString(), lblAmount.Text, txtPacking.Text, txtFreight.Text, txtTaxableValue.Text, txtCGST.Text, txtSGST.Text, txtIGST.Text, txtfinalAmount.Text, lblAmountInWords.Text, _userName, null, null, null, txtNote.Text, _action);
                        dismiss.Visible = true;
                        lblMessage.Visible = true;
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        lblMessage.Text = "Updated Successfully...";


                    }
                }



               

            }


            //txtCGST.Text = "";

            //txtfinalAmount.Text = "";
            //txtFreight.Text = "";
            //txtIGST.Text = "";
            //txtInvoiceDate.Text = "";
            //txtInvoiceNo.Text = "";
            //txtinvQty.Text = "";
            //txtIssueTime.Text = "";
            //txtOcno.Text = "";
            //txtPacking.Text = "";
            //txtRemoveTime.Text = "";
            //txtSGST.Text = "";
            //txtTaxableValue.Text = "";
            //txtTranspMode.Text = "";
            //lblAmount.Text = "";
            //lblAmountInWords.Text = "";
            //lblCalculation.Text = "";
            //lblCustumer.Text = "";
            //lbldimUp.Text = "";
            //lblDrgNO.Text = "";
            //lblHsn.Text = "";
            //lblItem_code.Text = "";

            //lblQty.Text = "";
            //lblRate.Text = "";
            //lbltaxableValue.Text = "";
            //lblToolDesc.Text = "";
            //btnSAVE.Text = "SAVE";
        }

        public void _scheduleQty(string _ocNo, int invQty)
        {
            int _scheduleQty = 0, _balQty = 0, diff, flag = 0, imgQty = invQty, _invScheduleQty = 0;
            try
            {      
                while(imgQty != 0)
                {
                    int BalanceQty = 0, InvoiceQty = 0 ;
                    
                    ArrayList list = obj.GetArrayList("select top 1 id, QTY, BAL_QTY, INV_QTY from SCHEDULE_DETAILS where OCNO = '" + _ocNo + "' and QTY > INV_QTY and BAL_QTY !=0 and BAL_QTY != INV_QTY");
                    if(list.Count == 0)
                    {
                        break;
                    }
                    _scheduleQty = Convert.ToInt32(list[1]);
                    _balQty = Convert.ToInt32(list[2]);
                    string invStrQty = list[3].ToString();
                    if (invStrQty != "")
                    {
                        _invScheduleQty = Convert.ToInt32(invStrQty);
                    }
                    diff = ((_balQty) - (imgQty));

                    // if differece is +ve
                    if (diff > 0)
                    {
                        BalanceQty = diff;
                        InvoiceQty = _balQty - diff; 
                       
                        if((InvoiceQty - (_balQty - diff)) == 0 )
                        {
                            diff = 0;
                        }
                      

                    }
                    else if (diff < 0)
                    {
                        // if differece is -ve
                        diff = System.Math.Abs(diff) * (1);
                        BalanceQty = _balQty;
                        InvoiceQty = (_invScheduleQty + _balQty);
                    }
                    else if (diff == 0)
                    {
                        BalanceQty = 0;
                        InvoiceQty = (_invScheduleQty + _balQty);
                    }

                    if (_balQty == BalanceQty)
                    {
                        BalanceQty = 0;
                    }

                    obj._SavedAs("update SCHEDULE_DETAILS set BAL_QTY = '" + BalanceQty + "', INV_QTY = '" + InvoiceQty + "' where ID = '" + list[0] + "'");
                    //check invoice qty is > or < from balance qty
                    //if(invQty > _balQty)
                    //{
                    //    obj._SavedAs("update SCHEDULE_DETAILS set BAL_QTY = '" + 0 + "', INV_QTY = '" + (imgQty - diff) + "' where ID = '" + list[0] + "'");
                    //    imgQty -= diff;
                    //}

                    imgQty = diff;
                    if(flag  == 1)
                    {
                        break;
                    }
                  
                   
                   
                    

                    
                }
              
               //my  test oode

                //foreach(var _sc in _dbContext.SCHEDULE_DETAILs.Where(s => s.OCNO == _ocNo))
                //{
                //    if(_sc.BAL_QTY != 0)
                //    {
                       
                //        _scheduleQty = Convert.ToInt32(_sc.QTY);
                //        _balQty = Convert.ToInt32(_sc.BAL_QTY);
                //        diff = invQty - _balQty;

                //        if (diff > 0)
                //        {
                           
                //            var i = 0;
                //        }
                //        // if diff is negative
                //        else if (diff < 0)
                //        {

                //            obj._SavedAs("update SCHEDULE_DETAILS set BAL_QTY = '"+diff+"', INV_QTY = '" + invQty + "' where ID = '" + list[0] + "'");
                //        }
                //        else if (diff == 0)
                //        {

                //            if(flag == 0)
                //            { 
                //                 obj._SavedAs("update SCHEDULE_DETAILS set BAL_QTY = 0, INV_QTY = '" + invQty + "' where ID = '" + list[0] + "'");
                //            }

                           
                //        }
                //        if(invQty - diff == 0)
                //        {
                //            break;
                //        }
                //        flag = flag + 1;
                //    }
                     
                //}
                //  my test code end

                //_scheduleQty = Convert.ToInt32(list[1]);
                //_balQty = Convert.ToInt32(list[2]);
                //diff = invQty - _balQty;
                


              

            }
            catch(Exception ex)
            {


            }
        }


        public void _fillData(string _invNO)
        {
           try

            {
                GST_INV gi = _dbContext.GST_INVs.SingleOrDefault(s => s.inv_no == _invNO);
                txtOcno.Text = gi.oc_no;
                txtInvoiceNo.Text = gi.inv_no;
                txtIssueTime.Text = gi.issue_time;
                txtRemoveTime.Text = gi.removal_time;
                txtTranspMode.Text = gi.transportation_mode;
                txtinvQty.Text = gi.inv_qty;
                txtPacking.Text = gi.packing;
                txtFreight.Text = gi.freight;
                txtTaxableValue.Text = gi.taxable_value;
                txtCGST.Text = gi.c_gst;
                txtSGST.Text = gi.s_gst;
                txtIGST.Text = gi.i_gst;
                txtInvoiceDate.Text = gi.inv_date;
                lblAmount.Text = gi.amount;
                txtNote.Text = gi.note;
                btnSAVE.Text = "UPDATE";
                if (btnSAVE.Text == "UPDATE")
                {
                    lblQty.Text = gi.inv_qty;
                }




                _fillDetails(txtOcno.Text);

                lblAmountInWords.Text = gi.total_amount_in_words;
                txtfinalAmount.Text = gi.total_amount;

            }
            catch(Exception ex)
            {

            }
        }
    }
}