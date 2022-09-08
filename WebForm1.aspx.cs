using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlankarNewDesign.DAL;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using ClosedXML.Excel;
using System.IO;


namespace AlankarNewDesign
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        alankar_db_providerDataContext _db = new alankar_db_providerDataContext();
        DbConnection generic = new DbConnection();
        string _userName = string.Empty;
        string _dispatchedQry = string.Empty;
        string _BookingQry = string.Empty;
        bool bmailSent = false;
        bool dmailSent = false;
        bool mailSent = false;
        SqlDataReader rdr = null;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null && Session["password"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else {
                if (!IsPostBack)
                {
                    string _query = "SELECT top 1 All_OC = (SELECT COUNT(OC_NO) FROM OC_TRANSACTIONS), NOT_ISSUE = (select  sum(VALUE) from STAGE_TRANSACTIONS where STAGE_TYPE = 'Not Issue'), ISSUE = (select sum(VALUE) from STAGE_TRANSACTIONS where STAGE_TYPE = 'Issue'), DISPATCH = (SELECT SUM(Cast (inv_qty as bigint)) FROM GST_INV) FROM STAGE_TRANSACTIONS INNER JOIN GST_INV ON STAGE_TRANSACTIONS.OC_NO = GST_INV.oc_no";
                    mailSent = generic._checkIsExists("Select * from MAIL_DELIVERED where DATE =  '"+DateTime.Now.ToString("yyyy-MM-dd")+"' and MAIL_SENT = 1");
                    rdr = generic._getRdr(_query);
                    while (rdr.Read())
                    {
                        lblAllOc.Text = rdr[0].ToString();
                        lblAllNotIssue.Text = rdr[1].ToString();
                        lblIssue.Text = rdr[2].ToString();
                        lblDispatched.Text = rdr[3].ToString();
                    }
                    generic.con.Close();
                    generic.cmd.Dispose();

                    // Report

                    string BookingQry = "Select distinct(BOOKING_RPT.TOOLTYPE), " +
                           " 'W1QTY' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 1 and TYPE = 'B'), " +
                           " 'W1VAL' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 1 and TYPE = 'B'), " +
                            "'W2QTY' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 2 and TYPE = 'B'), " +
                         "	'W2VAL' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 2 and TYPE = 'B'), " +
                         "	'W3QTY' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 3 and TYPE = 'B'),  " +
                         "	'W3VAL' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 3 and TYPE = 'B')," +
                         "	'W4QTY' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 4 and TYPE = 'B'), " +
                         "	'W4VAL' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 4 and TYPE = 'B') " +
                         "	from OC_TRANSACTIONS inner join BOOKING_RPT on OC_TRANSACTIONS.TOOLTYPE = BOOKING_RPT.TOOLTYPE where BOOKING_RPT.MONTH = ";


                    //string DisQry = "Select distinct(BOOKING_RPT.TOOLTYPE), " +
                    //             " '(week1) Quantity' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 1 TYPE = 'D'), " +
                    //             " '(week1) Value' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 1 TYPE = 'D'), " +
                    //              " 	'(week2) Quantity' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 2 TYPE = 'D'), " +
                    //           "	'(week2) Value' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 2 TYPE = 'D'), " +
                    //           "	'(week3) Quantity' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 3 TYPE = 'D'),  " +
                    //           "	'(week3) Value' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 3 TYPE = 'D')," +
                    //           "	'(week4) Quantity' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 4 TYPE = 'D'), " +
                    //           "	'(week4) Value' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 4 TYPE = 'D') " +
                    //           "	from OC_TRANSACTIONS inner join BOOKING_RPT on OC_TRANSACTIONS.TOOLTYPE = BOOKING_RPT.TOOLTYPE where BOOKING_RPT.MONTH = ";

                    string DisQry = "Select distinct(BOOKING_RPT.TOOLTYPE), " +
                          " 'W1QTY' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 1 and TYPE = 'D'), " +
                          " 'W1VAL' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 1 and TYPE = 'D'), " +
                           " 	'W2QTY' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 2 and TYPE = 'D'), " +
                        "	'W2VAL' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 2 and TYPE = 'D'), " +
                        "	'W3QTY' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 3 and TYPE = 'D'),  " +
                        "	'W3VAL' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 3 and TYPE = 'D')," +
                        "	'W4QTY' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 4 and TYPE = 'D'), " +
                        "	'W4VAL' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 4 and TYPE = 'D') " +
                        "	from OC_TRANSACTIONS inner join BOOKING_RPT on OC_TRANSACTIONS.TOOLTYPE = BOOKING_RPT.TOOLTYPE where BOOKING_RPT.MONTH = ";



                   
                    //9
                    //8
                    if (DateTime.Now.Day == 3)
                    {
                        // original value false
                        if(mailSent == false)
                        {
                            generic._SavedAs("Delete from BOOKING_RPT");
                            // first Week email 1-7
                            MailreportBooking(1, 1, 7);
                            _BookingQry = BookingQry + DateTime.Now.Month;
                            generic.dataTable(_BookingQry);

                           
                            _BookingQry = BookingQry + DateTime.Now.Month + " and BOOKING_RPT.TYPE = 'B'";
                            DataTable dt = generic.dataTable(_BookingQry);
                            // generic._fillGrid(_BookingQry, GridBooking);
                            _fillGrid(_BookingQry, GridBooking, "Booking");
                            _dispatchedQry = DisQry + DateTime.Now.Month + " and BOOKING_RPT.TYPE = 'D'";
                            _fillGrid(_dispatchedQry, GridDispatched, "Dispatched");

                            // temp code
                            //DataTable _dt1 = new DataTable();
                            //_dt1 = generic.dataTable(_BookingQry);
                            //string _fileName1  = @"_first_week_booking_report_date_1_7_Month_" + DateTime.Now.ToString("MMM") + "";
                            //generic.ExportToSpecificFolder(_dt1, _fileName1 , @"C:\Exported");

                          //  generic._sentEmailWithAttachment("Booking Report", "anand.narwade11@gmail.com", @"C:\\Exported\" + _fileName1+".xlsx");

                            //string _AttacheFilePathForB = Server.MapPath("~/Export/Booking.xls");
                            //string _AttacheFilePathForD = Server.MapPath("~/Export/Dispatched.xls");
                            string _AttacheFilePathForB = Server.MapPath("~/Export/Booking_"+DateTime.Now.ToString("dd-MMMM-yyyy") +".xls");
                            string _AttacheFilePathForD = Server.MapPath("~/Export/Dispatched_"+DateTime.Now.ToString("dd-MMMM-yyyy")+".xls");

                            //bmailSent = generic._sentEmailWithAttachment("Booking Report", "yuvraj@alankartools.co.in", _AttacheFilePathForB);
                            //dmailSent = generic._sentEmailWithAttachment("Dispatched Report", "yuvraj@alankartools.co.in", _AttacheFilePathForD);

                            //generic._sentEmailWithAttachment("booking report", "rajendra@alankartools.co.in", _AttacheFilePathForB);
                            //generic._sentEmailWithAttachment("dispatched report", "rajendra@alankartools.co.in", _AttacheFilePathForD);

                            generic._sentEmailWithAttachment("booking report", "anand.narwade11@gmail.com", _AttacheFilePathForB);
                            generic._sentEmailWithAttachment("dispatched report", "anand.narwade11@gmail.com", _AttacheFilePathForD);

                            //if (bmailSent == true && dmailSent == true)
                            //{
                            //    generic._SavedAs("INSERT INTO MAIL_DELIVERED VALUES('" + DateTime.Now.ToString("dd-MMMM-yyyy") + "', 'TRUE')");
                            //}
                        }
                      

                    }
                    else if (DateTime.Now.Day == 16)
                    {
                        // Second week Data 8-14
                        if(mailSent == false)
                        {
                            generic._SavedAs("Delete from BOOKING_RPT");
                            MailreportBooking(1, 1, 7);
                            MailreportBooking(2, 8, 14);
                            MailreportDispatched(1, 1, 7);
                            MailreportDispatched(2, 8, 14);


                            _BookingQry = BookingQry + DateTime.Now.Month + " and BOOKING_RPT.TYPE = 'B'";
                            DataTable dt = generic.dataTable(_BookingQry);
                            // generic._fillGrid(_BookingQry, GridBooking);
                            _fillGrid(_BookingQry, GridBooking, "Booking");
                            _dispatchedQry = DisQry + DateTime.Now.Month + " and BOOKING_RPT.TYPE = 'D'";
                            _fillGrid(_dispatchedQry, GridDispatched, "Dispatched");


                            string _AttacheFilePathForB = Server.MapPath("~/Export/Booking.xls");
                            string _AttacheFilePathForD = Server.MapPath("~/Export/Dispatched.xls");

                            bmailSent = generic._sentEmailWithAttachment("Booking Report", "yuvraj@alankartools.co.in", _AttacheFilePathForB);
                            dmailSent = generic._sentEmailWithAttachment("Dispatched Report", "yuvraj@alankartools.co.in", _AttacheFilePathForD);

                            generic._sentEmailWithAttachment("booking report", "rajendra@alankartools.co.in", _AttacheFilePathForB);
                            generic._sentEmailWithAttachment("dispatched report", "rajendra@alankartools.co.in", _AttacheFilePathForD);

                            if (bmailSent == true && dmailSent == true)
                            {
                                generic._SavedAs("INSERT INTO MAIL_DELIVERED VALUES('" + DateTime.Now.ToString("dd-MMMM-yyyy") + "', 'TRUE')");
                            }
                        }
                      
                    }
                    else if (DateTime.Now.Day == 24)
                    {
                        // 3rd week data 15 -22
                        if(mailSent == false)
                        {
                            generic._SavedAs("Delete from BOOKING_RPT");
                            MailreportBooking(1, 1, 7);
                            MailreportDispatched(1, 1, 7);
                            MailreportBooking(2, 8, 14);
                            MailreportDispatched(2, 8, 14);
                            MailreportBooking(3, 15, 22);
                            MailreportDispatched(3, 15, 22);



                            _BookingQry = BookingQry + DateTime.Now.Month + " and BOOKING_RPT.TYPE = 'B'";
                            DataTable dt = generic.dataTable(_BookingQry);
                            // generic._fillGrid(_BookingQry, GridBooking);
                            _fillGrid(_BookingQry, GridBooking, "Booking");
                            _dispatchedQry = DisQry + DateTime.Now.Month + " and BOOKING_RPT.TYPE = 'D'";
                            _fillGrid(_dispatchedQry, GridDispatched, "Dispatched");


                            string _AttacheFilePathForB = Server.MapPath("~/Export/Booking.xls");
                            string _AttacheFilePathForD = Server.MapPath("~/Export/Dispatched.xls");

                            bmailSent = generic._sentEmailWithAttachment("Booking Report", "yuvraj@alankartools.co.in", _AttacheFilePathForB);
                            dmailSent = generic._sentEmailWithAttachment("Dispatched Report", "yuvraj@alankartools.co.in", _AttacheFilePathForD);

                            generic._sentEmailWithAttachment("booking report", "rajendra@alankartools.co.in", _AttacheFilePathForB);
                            generic._sentEmailWithAttachment("dispatched report", "rajendra@alankartools.co.in", _AttacheFilePathForD);

                            if (bmailSent == true && dmailSent == true)
                            {
                                generic._SavedAs("INSERT INTO MAIL_DELIVERED VALUES('" + DateTime.Now.ToString("dd-MMMM-yyyy") + "', 'TRUE')");
                            }
                        }
                      
                    }
                    else if (DateTime.Now.Day == 3)
                    {
                        // 4th week data 23-31
                        if(mailSent == false)
                        {
                            generic._SavedAs("Delete from BOOKING_RPT");
                            MailreportBooking(1, 1, 7);
                            MailreportDispatched(1, 1, 7);
                            MailreportBooking(2, 8, 14);
                            MailreportDispatched(2, 8, 14);
                            MailreportBooking(3, 15, 22);
                            MailreportDispatched(3, 15, 22);
                            MailreportBooking(4, 23, 0);
                            MailreportDispatched(4, 23, 0);



                            _BookingQry = BookingQry + DateTime.Now.Month + " and BOOKING_RPT.TYPE = 'B'";
                            DataTable dt = generic.dataTable(_BookingQry);
                            // generic._fillGrid(_BookingQry, GridBooking);
                            _fillGrid(_BookingQry, GridBooking, "Booking");
                            _dispatchedQry = DisQry + DateTime.Now.Month + " and BOOKING_RPT.TYPE = 'D'";
                            _fillGrid(_dispatchedQry, GridDispatched, "Dispatched");


                            string _AttacheFilePathForB = Server.MapPath("~/Export/Booking.xls");
                            string _AttacheFilePathForD = Server.MapPath("~/Export/Dispatched.xls");

                            bmailSent = generic._sentEmailWithAttachment("Booking Report", "yuvraj@alankartools.co.in", _AttacheFilePathForB);
                            dmailSent = generic._sentEmailWithAttachment("Dispatched Report", "yuvraj@alankartools.co.in", _AttacheFilePathForD);
                            generic._sentEmailWithAttachment("booking report", "rajendra@alankartools.co.in", _AttacheFilePathForB);
                            generic._sentEmailWithAttachment("dispatched report", "rajendra@alankartools.co.in", _AttacheFilePathForD);

                            if (bmailSent == true && dmailSent == true)
                            {
                                generic._SavedAs("INSERT INTO MAIL_DELIVERED VALUES('" + DateTime.Now.ToString("dd-MMMM-yyyy") + "', 'TRUE')");
                            }
                        }
                     
                    }
                   
                 
                        //below condition for testing
                    //else if (DateTime.Now.Day == 31)
                    //{
                    //    //Testing DAte
                    //    if(mailSent == false)
                    //    {
                    //        generic._SavedAs("Delete from BOOKING_RPT");
                    //        MailreportBooking(1, 1, 7);
                    //        MailreportDispatched(1, 1, 7);
                    //        _BookingQry = BookingQry + DateTime.Now.Month + " and BOOKING_RPT.TYPE = 'B'";
                    //        DataTable dt = generic.dataTable(_BookingQry);
                    //        // generic._fillGrid(_BookingQry, GridBooking);
                    //        _fillGrid(_BookingQry, GridBooking, "Booking");
                    //        _dispatchedQry = DisQry + DateTime.Now.Month + " and BOOKING_RPT.TYPE = 'D'";
                    //        _fillGrid(_dispatchedQry, GridDispatched, "Dispatched");


                    //        string _AttacheFilePathForB = Server.MapPath("/Export/Booking.xls");
                    //        string _AttacheFilePathForD = Server.MapPath("/Export/Dispatched.xls");

                    //        bmailSent = generic._sentEmailWithAttachment("Booking Report", "yuvraj@alankartools.co.in", _AttacheFilePathForB);
                    //        dmailSent = generic._sentEmailWithAttachment("Dispatched Report", "yuvraj@alankartools.co.in", _AttacheFilePathForD);

                    //        if (bmailSent == true && dmailSent == true)
                    //        {
                    //            generic._SavedAs("INSERT INTO MAIL_DELIVERED VALUES('" + DateTime.Now.ToString("dd-MMMM-yyyy") + "', 'TRUE')");
                    //        }
                    //    }
                       
                    //    //DataTable DtDispatched = generic.dataTable(_dispatchedQry);

                    //    //generic.ExportToExcel(dt, "test");


                    //}


                    //end report
                 
                }
            }

        }



        public void ExportExcel(GridView _gridName, string fileName)
        {
            DataTable dt = new DataTable("GridView_Data");

            foreach (TableCell cell in _gridName.HeaderRow.Cells)
            {
                dt.Columns.Add(cell.Text);
            }
            foreach (GridViewRow row in _gridName.Rows)
            {
                dt.Rows.Add();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dt.Rows[dt.Rows.Count - 1][i] = row.Cells[i].Text;
                }
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename="+ fileName +".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }


        public void _fillGrid(string Qry, GridView GridName, string _fileName)
        {
            generic._fillGrid(Qry, GridName);
            if (GridName.Rows.Count > 0)
            {
                decimal W1Qty = 0, W1Val = 0, W2Qty = 0, W2Val = 0, W3Qty = 0, W3Val = 0, W4Qty = 0, W4Val = 0, TotalQuantity = 0, Totalvalue = 0;
                foreach (GridViewRow _row in GridName.Rows)
                {
                    Label lblW1Qty, lblW1Value, lblW2Qty, lblW2Value, lblW3Qty, lblW3Value, lblW4Qty, lblW4Value, lblTotalQTY, lblTotalVAL;
                    lblW1Qty = (_row.FindControl("lblW1Qty") as Label);
                    lblW1Value = (_row.FindControl("lblW1Value") as Label);

                    lblW2Qty = (_row.FindControl("lblW2Qty") as Label);
                    lblW2Value = (_row.FindControl("lblW2Value") as Label);

                    lblW3Qty = (_row.FindControl("lblW3Qty") as Label);
                    lblW3Value = (_row.FindControl("lblW3Value") as Label);

                    lblW4Qty = (_row.FindControl("lblW4Qty") as Label);
                    lblW4Value = (_row.FindControl("lblW4Value") as Label);

                    lblTotalQTY = (_row.FindControl("lblTotalQTY") as Label);
                    lblTotalVAL = (_row.FindControl("lblTotalVAL") as Label);

                    decimal q1 = 0, v1 = 0, q2 = 0, v2 = 0, q3 = 0, v3 = 0, q4 = 0, v4 = 0, totalQty = 0, totalVal = 0;
                    if (lblW1Qty.Text != "")
                    {
                        q1 = Convert.ToDecimal(lblW1Qty.Text);
                        W1Qty = W1Qty + q1;
                    }

                    if (lblW1Value.Text != "")
                    {
                        v1 = Convert.ToDecimal(lblW1Value.Text);
                        W1Val = W1Val + v1;
                    }


                    if (lblW2Qty.Text != "")
                    {
                        q2 = Convert.ToDecimal(lblW2Qty.Text);
                        W2Qty = W2Qty + q2;
                    }

                    if (lblW2Value.Text != "")
                    {
                        v2 = Convert.ToDecimal(lblW2Value.Text);
                        W2Val = W2Val + v2;
                    }

                    if (lblW3Qty.Text != "")
                    {
                        q3 = Convert.ToDecimal(lblW3Qty.Text);
                        W3Qty = W3Qty + q3;
                    }

                    if (lblW3Value.Text != "")
                    {
                        v3 = Convert.ToDecimal(lblW3Value.Text);
                        W3Val = W3Val + v3;
                    }


                    if (lblW4Qty.Text != "")
                    {
                        q4 = Convert.ToDecimal(lblW4Qty.Text);
                        W4Qty = W4Qty + q4;
                    }

                    if (lblW4Value.Text != "")
                    {
                        v4 = Convert.ToDecimal(lblW4Value.Text);
                        W4Val = W4Val + v2;
                    }

                    totalQty = q1 + q2 + q3 + q4;
                    TotalQuantity = TotalQuantity + totalQty;
                    totalVal = v1 + v2 + v3 + v4;
                    Totalvalue = Totalvalue + totalVal;
                    lblTotalQTY.Text = totalQty.ToString();
                    lblTotalVAL.Text = totalVal.ToString("N2");

                }
                //DataTable dt = generic.dataTable(Qry);
                //decimal W1QtyT = GetValue("W1QTY", dt);
                GridName.FooterRow.Cells[1].Text = "TOTAL";
                GridName.FooterRow.Cells[1].Font.Bold = true;
                GridName.FooterRow.Cells[2].Text = W1Qty.ToString();
                GridName.FooterRow.Cells[2].Font.Bold = true;
                GridName.FooterRow.Cells[3].Text = W1Val.ToString("N2");
                GridName.FooterRow.Cells[3].Font.Bold = true;
                GridName.FooterRow.Cells[4].Text = W2Qty.ToString();
                GridName.FooterRow.Cells[4].Font.Bold = true;
                GridName.FooterRow.Cells[5].Text = W2Val.ToString("N2");
                GridName.FooterRow.Cells[5].Font.Bold = true;


                GridName.FooterRow.Cells[6].Text = W3Qty.ToString();
                GridName.FooterRow.Cells[6].Font.Bold = true;
                GridName.FooterRow.Cells[7].Text = W3Val.ToString("N2");
                GridName.FooterRow.Cells[7].Font.Bold = true;



                GridName.FooterRow.Cells[8].Text = W4Qty.ToString();
                GridName.FooterRow.Cells[8].Font.Bold = true;
                GridName.FooterRow.Cells[9].Text = W4Val.ToString("N2");
                GridName.FooterRow.Cells[9].Font.Bold = true;


                GridName.FooterRow.Cells[10].Text = TotalQuantity.ToString();
                GridName.FooterRow.Cells[10].Font.Bold = true;
                GridName.FooterRow.Cells[11].Text = Totalvalue.ToString("N2");
                GridName.FooterRow.Cells[11].Font.Bold = true;
            }

            string _filePath = Server.MapPath("~/Export/");
            generic.ExportToFolder(GridName, _filePath, _fileName);
           // generic.ExportGridView(GridName, _filePath, _fileName);

        
        }


        public static int MondaysInMonth(DateTime thisMonth)
        {
            int mondays = 0;
            int month = thisMonth.Month;
            int year = thisMonth.Year;
            int daysThisMonth = DateTime.DaysInMonth(year, month);
            DateTime beginingOfThisMonth = new DateTime(year, month, 1);
            for (int i = 0; i < daysThisMonth; i++)
                if (beginingOfThisMonth.AddDays(i).DayOfWeek == DayOfWeek.Monday)
                    mondays++;
            return mondays;
        }
        public ArrayList GetToolWiseList(string _startDate, string _lastDate, string _tool)
        {
            ArrayList List = new ArrayList();
            try
            {
                generic._getConnection();
                generic.cmd = new SqlCommand("sp_get_booking_value_rpt", generic.con);
                generic.cmd.CommandType = CommandType.StoredProcedure;
                generic.cmd.Parameters.AddWithValue("@startDate", _startDate);
                generic.cmd.Parameters.AddWithValue("@endDate", _lastDate);
                generic.cmd.Parameters.AddWithValue("@tool", _tool);
                generic.con.Open();
                SqlDataReader rdr = generic.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        List.Add(rdr[i]);
                    }
                }
            }
            catch (Exception Ex)
            {

            }
            finally
            {
                generic.con.Close();
                generic.cmd.Dispose();
            }
            return List;
        }



        public ArrayList GetToolWiseDispatchedList(string _startDate, string _lastDate, string _tool)
        {
            ArrayList List = new ArrayList();
            try
            {
                generic._getConnection();
                generic.cmd = new SqlCommand("sp_get_dispatched_value_rpt", generic.con);
                generic.cmd.CommandType = CommandType.StoredProcedure;
                generic.cmd.Parameters.AddWithValue("@startDate", _startDate);
                generic.cmd.Parameters.AddWithValue("@endDate", _lastDate);
                generic.cmd.Parameters.AddWithValue("@tool", _tool);
                generic.con.Open();
                SqlDataReader rdr = generic.cmd.ExecuteReader();
                while (rdr.Read())
                {
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        List.Add(rdr[i]);
                    }
                }
            }
            catch (Exception Ex)
            {

            }
            finally
            {
                generic.con.Close();
                generic.cmd.Dispose();
            }
            return List;
        }


        public void MailreportBooking(int _week, int _startWeekDate, int _endWeekDate)
        {
            // int _monthDays = 0;
            int _lastMonthDays = 0;
            //  int month = 0;
            //  int year = 0;
            // int day = 0;
            // long? Id;
            //   int MONTH = 1;
            string _startDate = string.Empty;
            string _endDate = string.Empty;
            DateTime _currentDate = DateTime.Now;
            DateTime _DT = DateTime.Now.AddMonths(-1);
            DateTime _lastMonth;
            // bool _RESULT = false;
            if (DateTime.Now.Day == 10)
            {
                _lastMonth = _DT;
            }
            else
            {
                _lastMonth = DateTime.Now;
            }
            _lastMonthDays = DateTime.DaysInMonth(_lastMonth.Year, _lastMonth.Month);
            ArrayList _toolList = generic.GetArrayList("Select MASTER_TOOL_TYPE from MASTER_TOOL_TYPE where STATUS = 0 order by SEQUENCE");
            for (int i = 0; i < _toolList.Count; i++)
            {
                _startDate = _lastMonth.Year + "-" + _lastMonth.Month + "-" + _startWeekDate;
                if (_startWeekDate == 23)
                {
                    _endDate = _lastMonth.Year + "-" + _lastMonth.Month + "-" + _lastMonthDays;
                }
                else
                {
                    _endDate = _lastMonth.Year + "-" + _lastMonth.Month + "-" + _endWeekDate;
                }
                string _toolName = _toolList[i].ToString();
                if (_toolName != "" || _toolName != null)
                {
                    //  ArrayList _LIST = generic.GetArrayList("SELECT SUM(OCQTY), SUM(OCQTY * GRPPRICE) FROM OC_TRANSACTIONS WHERE CONVERT(DATETIME, OCDT, 103) BETWEEN '" + _startDate.Trim() + "' AND '" + _endDate.Trim() + "' AND TOOLTYPE = '" + _toolName + "'");
                    ArrayList _LIST = GetToolWiseList(_startDate.Trim(), _endDate.Trim(), _toolName);
                    //Id = generic._getMaxId("ID", "BOOKING_RPT");
                    //_RESULT = generic._SavedAs("INSERT INTO BOOKING_RPT VALUES(" + Id + ", " + _lastMonth.Month + ", " + _lastMonth.Year + ", '"+_week+"', '" + _toolName + "', '" + _LIST[0] + "', '" + _LIST[1] + "', '" + DateTime.Now + "' )");
                    long? Qty;
                    decimal? Value;
                    if (_LIST[0].ToString() == "" || _LIST[1].ToString() == null)
                    {
                        Qty = null;
                    }
                    else
                    {
                        Qty = Convert.ToInt64(_LIST[0]);
                    }

                    if (_LIST[1].ToString() == "" || _LIST[1].ToString() == null)
                    {
                        Value = null;
                    }
                    else
                    {
                        Value = Convert.ToDecimal(_LIST[1]);
                    }
                    _db.SP_BOOKING_RPT(null, _lastMonth.Month, _lastMonth.Year, _week, _toolName, Qty, Value, "B");
                }
            }



        }

        public void MailreportDispatched(int _week, int _startWeekDate, int _endWeekDate)
        {
            // int _monthDays = 0;
            int _lastMonthDays = 0;
            //  int month = 0;
            //  int year = 0;
            // int day = 0;
            // long? Id;
            //   int MONTH = 1;
            string _startDate = string.Empty;
            string _endDate = string.Empty;
            DateTime _currentDate = DateTime.Now;
            DateTime _DT = DateTime.Now.AddMonths(-1);
            DateTime _lastMonth;
            // bool _RESULT = false;
            if (DateTime.Now.Day == 10)
            {
                _lastMonth = _DT;
            }
            else
            {
                _lastMonth = DateTime.Now;
            }
            _lastMonthDays = DateTime.DaysInMonth(_lastMonth.Year, _lastMonth.Month);
            ArrayList _toolList = generic.GetArrayList("Select MASTER_TOOL_TYPE from MASTER_TOOL_TYPE where STATUS = 0 order by SEQUENCE");
            for (int i = 0; i < _toolList.Count; i++)
            {
                _startDate = _lastMonth.Year + "-" + _lastMonth.Month + "-" + _startWeekDate;
                if (_startWeekDate == 23)
                {
                    _endDate = _lastMonth.Year + "-" + _lastMonth.Month + "-" + _lastMonthDays;
                }
                else
                {
                    _endDate = _lastMonth.Year + "-" + _lastMonth.Month + "-" + _endWeekDate;
                }
                string _toolName = _toolList[i].ToString();
                if (_toolName != "" || _toolName != null)
                {
                    //  ArrayList _LIST = generic.GetArrayList("SELECT SUM(OCQTY), SUM(OCQTY * GRPPRICE) FROM OC_TRANSACTIONS WHERE CONVERT(DATETIME, OCDT, 103) BETWEEN '" + _startDate.Trim() + "' AND '" + _endDate.Trim() + "' AND TOOLTYPE = '" + _toolName + "'");
                    ArrayList _LIST = GetToolWiseDispatchedList(_startDate.Trim(), _endDate.Trim(), _toolName);
                    //Id = generic._getMaxId("ID", "BOOKING_RPT");
                    //_RESULT = generic._SavedAs("INSERT INTO BOOKING_RPT VALUES(" + Id + ", " + _lastMonth.Month + ", " + _lastMonth.Year + ", '"+_week+"', '" + _toolName + "', '" + _LIST[0] + "', '" + _LIST[1] + "', '" + DateTime.Now + "' )");
                    long? Qty;
                    decimal? Value;
                    if (_LIST[0].ToString() == "" || _LIST[1].ToString() == null)
                    {
                        Qty = null;
                    }
                    else
                    {
                        Qty = Convert.ToInt64(_LIST[0]);
                    }

                    if (_LIST[1].ToString() == "" || _LIST[1].ToString() == null)
                    {
                        Value = null;
                    }
                    else
                    {
                        Value = Convert.ToDecimal(_LIST[1]);
                    }
                    _db.SP_BOOKING_RPT(null, _lastMonth.Month, _lastMonth.Year, _week, _toolName, Qty, Value, "D");
                }
            }



        }

        protected void btnMail_Click(object sender, EventArgs e)
        {
            string _AttacheFilePathForB = Server.MapPath("~/Export/Booking.xls");
            string _AttacheFilePathForD = Server.MapPath("~/Export/Dispatched.xls");

            FileInfo _f1 = new FileInfo(_AttacheFilePathForB);
            if(_f1.Exists)
            {
                File.Delete(_AttacheFilePathForB);
            }

            FileInfo _f2 = new FileInfo(_AttacheFilePathForD);
            if (_f1.Exists)
            {
                File.Delete(_AttacheFilePathForD);
            }

            string BookingQry = "Select distinct(BOOKING_RPT.TOOLTYPE), " +
                         " 'W1QTY' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 1 and TYPE = 'B'), " +
                         " 'W1VAL' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 1 and TYPE = 'B'), " +
                          "'W2QTY' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 2 and TYPE = 'B'), " +
                       "	'W2VAL' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 2 and TYPE = 'B'), " +
                       "	'W3QTY' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 3 and TYPE = 'B'),  " +
                       "	'W3VAL' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 3 and TYPE = 'B')," +
                       "	'W4QTY' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 4 and TYPE = 'B'), " +
                       "	'W4VAL' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 4 and TYPE = 'B') " +
                       "	from OC_TRANSACTIONS inner join BOOKING_RPT on OC_TRANSACTIONS.TOOLTYPE = BOOKING_RPT.TOOLTYPE where BOOKING_RPT.MONTH = ";


            //string DisQry = "Select distinct(BOOKING_RPT.TOOLTYPE), " +
            //             " '(week1) Quantity' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 1 TYPE = 'D'), " +
            //             " '(week1) Value' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 1 TYPE = 'D'), " +
            //              " 	'(week2) Quantity' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 2 TYPE = 'D'), " +
            //           "	'(week2) Value' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 2 TYPE = 'D'), " +
            //           "	'(week3) Quantity' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 3 TYPE = 'D'),  " +
            //           "	'(week3) Value' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 3 TYPE = 'D')," +
            //           "	'(week4) Quantity' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 4 TYPE = 'D'), " +
            //           "	'(week4) Value' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 4 TYPE = 'D') " +
            //           "	from OC_TRANSACTIONS inner join BOOKING_RPT on OC_TRANSACTIONS.TOOLTYPE = BOOKING_RPT.TOOLTYPE where BOOKING_RPT.MONTH = ";

            string DisQry = "Select distinct(BOOKING_RPT.TOOLTYPE), " +
                  " 'W1QTY' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 1 and TYPE = 'D'), " +
                  " 'W1VAL' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 1 and TYPE = 'D'), " +
                   " 	'W2QTY' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 2 and TYPE = 'D'), " +
                "	'W2VAL' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 2 and TYPE = 'D'), " +
                "	'W3QTY' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 3 and TYPE = 'D'),  " +
                "	'W3VAL' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 3 and TYPE = 'D')," +
                "	'W4QTY' = (select QTY from BOOKING_RPT where TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 4 and TYPE = 'D'), " +
                "	'W4VAL' = (select VALUE from BOOKING_RPT where BOOKING_RPT.TOOLTYPE = OC_TRANSACTIONS.TOOLTYPE and MONTH = BOOKING_RPT.MONTH and WEEK = 4 and TYPE = 'D') " +
                "	from OC_TRANSACTIONS inner join BOOKING_RPT on OC_TRANSACTIONS.TOOLTYPE = BOOKING_RPT.TOOLTYPE where BOOKING_RPT.MONTH = ";




            generic._SavedAs("Delete from BOOKING_RPT");
            // first Week email 1-7
           // MailreportBooking(1, 1, 7);
            MailreportBooking(1, 1, DateTime.Now.Day);
            _BookingQry = BookingQry + DateTime.Now.Month;
            generic.dataTable(_BookingQry);


            _BookingQry = BookingQry + DateTime.Now.Month + " and BOOKING_RPT.TYPE = 'B'";
            DataTable dt = generic.dataTable(_BookingQry);
            // generic._fillGrid(_BookingQry, GridBooking);
            _fillGrid(_BookingQry, GridBooking, "Booking");
            _dispatchedQry = DisQry + DateTime.Now.Month + " and BOOKING_RPT.TYPE = 'D'";
            _fillGrid(_dispatchedQry, GridDispatched, "Dispatched");

           
            //string _AttacheFilePathForB = Server.MapPath("~/Export/Booking.xls");
            //string _AttacheFilePathForD = Server.MapPath("~/Export/Dispatched.xls");

            MailreportDispatched(1, 1, DateTime.Now.Day);
          
            generic._sentEmailWithAttachment("booking report", "yuvraj@alankartools.co.in", _AttacheFilePathForB);
            generic._sentEmailWithAttachment("dispatched report", "yuvraj@alankartools.co.in", _AttacheFilePathForD);

            btnMail.ForeColor = System.Drawing.Color.Green;

        }


    }
}