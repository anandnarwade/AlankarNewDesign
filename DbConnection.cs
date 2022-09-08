using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using AlankarNewDesign.DAL;
using AjaxControlToolkit;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Collections;

using ClosedXML.Excel;
using System.Net.Mail;
using System.Net;
namespace AlankarNewDesign
{
    public class DbConnection
    {

        public SqlConnection con = null;
        public SqlCommand cmd = null;
        public SqlDataAdapter _da;
        alankar_db_providerDataContext _db = new alankar_db_providerDataContext();
        public void _getConnection()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["D__DEVELOPMENT_SOFTWARE_UPDATED_DB_ALANKAR_DB_MDFConnectionString"].ConnectionString);
        }
        /*Get max id*/
        public Int64 _getMaxId(String _columnName, String _tableName)
        {
            Int64 res = 1;
            try
            {
                _getConnection();
                cmd = new SqlCommand("Select Max(" + _columnName + ") from " + _tableName + "", con);
                con.Open();
                res = Convert.ToInt64(cmd.ExecuteScalar());

                if (res > 0)
                {
                    res = res + 1;
                }
                else if (res == 0)
                {
                    res = 1;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return res;
        }



        /* Check is Exists*/
        public bool AlreadyExists(String _columnName, String _tableName, String check)
        {
            bool result = false;
            try
            {
                _getConnection();
                cmd = new SqlCommand("select " + _columnName + " from " + _tableName + " where " + _columnName + " = '" + check + "' AND STATUS = 0", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {

                result = false;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return result;
        }



     


        public void _fillCombo(string _query, ComboBox _comboName, string _dataValueField, string _dataTextField)
        {
            _comboName.Items.Clear();
            try
            {
                _getConnection();
                cmd = new SqlCommand(_query, con);
                _da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                con.Open();
                _da.SelectCommand = cmd;
                _da.Fill(ds);
                _comboName.DataSource = ds;
                _comboName.DataValueField = _dataValueField;
                _comboName.DataTextField = _dataTextField;
                _comboName.DataBind();
                _comboName.Items.Insert(0, "");

            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
        }


        public void _fillLinqCombo(IQueryable _query, ComboBox _comboName, string _dataValueField, string _dataTextField)
        {
            try
            {
                _comboName.Items.Clear();
                _comboName.DataSource = _query;
                _comboName.DataValueField = _dataValueField;
                _comboName.DataTextField = _dataTextField;
                _comboName.DataBind();
                _comboName.Items.Insert(0, "");
            }
            catch (Exception ex)
            {
            }
        }




        public void _fillGrid(string _query, GridView _gridName)
        {
            try
            {
                _getConnection();
                cmd = new SqlCommand(_query, con);
                _da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                con.Open();
                _da.SelectCommand = cmd;
                _da.Fill(ds);
                _gridName.DataSource = ds;
                _gridName.DataBind();
                if (_gridName.Rows.Count > 0)
                {
                    _gridName.UseAccessibleHeader = true;
                    _gridName.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
        }


        public void _fillGridFromCmd(SqlCommand _cmd, GridView _gridName)
        {
             string _result = string.Empty;
            try
            {
               
                _getConnection();
                _cmd.Connection = con;
                _da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                con.Open();
                _da.SelectCommand = _cmd;
                _da.Fill(ds);
                _gridName.DataSource = ds;
                _gridName.DataBind();
                if (_gridName.Rows.Count > 0)
                {
                    _gridName.UseAccessibleHeader = true;
                    _gridName.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {
                _result = ex.InnerException.ToString();
            }
            finally
            {
                con.Close();
                
            }
        }

        public void _fillLinqGrid(IQueryable _query, GridView _gridName)
        {
            try
            {
                _gridName.DataSource = _query;
                _gridName.DataBind();
                if (_gridName.Rows.Count > 0)
                {
                    _gridName.UseAccessibleHeader = true;
                    _gridName.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            catch (Exception ex)
            {

            }
        }



        public bool _SavedAs(string _Query)
        {
            bool _result = false;
            try
            {
                _getConnection();
                cmd = new SqlCommand(_Query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                _result = true;
            }
            catch (Exception ex)
            {
                _result = false;
            }
            finally
                {
                    con.Close();
                    cmd.Dispose();

                }
            return _result;
        }



        public bool _checkIsExists(string Qry)
        {
            bool _result = false;
            try
            {
                _getConnection();
                cmd = new SqlCommand(Qry, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows) { _result = true; }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return _result;
        }



        public SqlDataReader _getRdr(string _Query)
        {
            SqlDataReader rdr2 = null;
            try
            {
                _getConnection();
                cmd = new SqlCommand(_Query, con);
                con.Open();
                rdr2 = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

            return rdr2;
        }

        public ArrayList GetArrayList(string Query)
        {
            ArrayList Result = new ArrayList();
            try
            {
                _getConnection();
                cmd = new SqlCommand(Query, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        Result.Add(rdr[i]);
                    }

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return Result;

        }



        public DataTable dataTable(string Query)
        {
            DataTable dt = new DataTable();
            _da = new SqlDataAdapter();
            try
            {
                _getConnection();
                cmd = new SqlCommand(Query, con);
                con.Open();
                _da.SelectCommand = cmd;
                _da.Fill(dt);

            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
                cmd.Dispose();

            }
            return dt;
        }
        public string GetStr(String Qry)
        {
            string _result = string.Empty;
            try
            {
                _getConnection();
                cmd = new SqlCommand(Qry, con);
                con.Open();
               _result = cmd.ExecuteScalar().ToString();
            }
            catch(Exception ex)
            {

            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return _result;
        }
        /*----------USING CLOSED XML PACHAGE----------- */
        public void ExportToExcel(DataTable dtable, string _fileName)
        {
            DataTable dt = null;
            try
            {
                dt = dtable;
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Style.Font.FontSize = 10;
                    wb.Style.Font.FontName = "Verdana";
                    
                    wb.Worksheets.Add(dt, "Sheet1");
                    wb.SaveAs(@"C:\\Export\Exported.xlsx");
                    wb.Properties.ToString();
                    System.Web.HttpContext.Current.Response.Clear();
                    System.Web.HttpContext.Current.Response.Buffer = true;
                    System.Web.HttpContext.Current.Response.Charset = "utf-8";
                    System.Web.HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename= " + _fileName + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);

                        MyMemoryStream.WriteTo(System.Web.HttpContext.Current.Response.OutputStream);
                        System.Web.HttpContext.Current.Response.Flush();
                        System.Web.HttpContext.Current.Response.End();
                    }
                    
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dt = null;
            }


          
        }



        public void ExportToSpecificFolder(DataTable dtable, string _fileName, string _filePath)
        {
            DataTable dt = null;
           // string _location = _filePath + "/" + _fileName + ".xlsx";
            
            try
            {
                dt = dtable;
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Style.Font.FontSize = 10;
                    wb.Style.Font.FontName = "Verdana";

                    wb.Worksheets.Add(dt, "Sheet1");
                    wb.SaveAs(@"C:\\Export\" + _fileName + ".xlsx");
                   // wb.SaveAs(_location);

                    wb.Properties.ToString();
                    System.Web.HttpContext.Current.Response.Clear();
                    System.Web.HttpContext.Current.Response.Buffer = true;
                    System.Web.HttpContext.Current.Response.Charset = "utf-8";
                    System.Web.HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename= " + _fileName + ".xlsx");
                    //using (MemoryStream MyMemoryStream = new MemoryStream())
                    //{
                    //    wb.SaveAs(MyMemoryStream);

                    //    MyMemoryStream.WriteTo(System.Web.HttpContext.Current.Response.OutputStream);
                    //    System.Web.HttpContext.Current.Response.Flush();
                    //    System.Web.HttpContext.Current.Response.End();
                    //}

                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dt = null;
            }



        }







        /* Export .xls to folder */
        public void ExportToFolder(GridView GridName, string path, string NameOFFile)
        {
            StringWriter ew = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(ew);
            GridName.RenderControl(htw);
            string _filePath = path;
            string _fileName = NameOFFile + ".xls";
            string renderedGrid = ew.ToString();
            System.IO.File.WriteAllText(_filePath + _fileName, renderedGrid); 
        }

        /* Export .xlsx to folder */

        public void ExportGridView(GridView _gridName, string path, string _fileName)
        {
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

            // Render grid view control.
            _gridName.RenderControl(htw);

            // Write the rendered content to a file.
            string renderedGridView = sw.ToString();
            System.IO.File.WriteAllText(path + @"\" + _fileName + ".xlsx", renderedGridView);
            //System.IO.File.WriteAllText(@"C:\Path\On\Server\"+ _fileName +".xlsx", renderedGridView);
        }


        public void _sentEmail(string mailBody, string recieverEmail)
        {
            try
            {
                MailMessage Msg = new MailMessage();

                Msg.From = new MailAddress("mail.duedate@gmail.com");

                Msg.To.Add(recieverEmail);
                Msg.Subject = "Reports";
                Msg.IsBodyHtml = true;
                Msg.Body = mailBody;
                // your remote SMTP server IP.
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("mail.duedate@gmail.com", "mail.due");
                smtp.EnableSsl = true;
                smtp.Send(Msg);
                Msg = null;
            }
            catch (Exception ex)
            {


            }
        }


        // email with attachments..
        public bool _sentEmailWithAttachment(string mailBody, string recieverEmail, string _filePath)
        {
            bool _result = false;
            try
            {
                MailMessage Msg = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                Msg.From = new MailAddress("mail.duedate@gmail.com");

                Msg.To.Add(recieverEmail);
                Msg.Bcc.Add("anand.narwade11@gmail.com");
                Msg.Subject = "Request mail";

                Msg.IsBodyHtml = true;
                Msg.Body = mailBody;
                Msg.Attachments.Add(new Attachment(_filePath));
                // your remote SMTP server IP.
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                NetworkCredential cred = new NetworkCredential("mail.duedate@gmail.com", "mail.due");
                //smtp.Credentials = new System.Net.NetworkCredential("mail.duedate@gmail.com", "mail.due");
                smtp.EnableSsl = true;

                smtp.Credentials = cred;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(Msg);
                Msg = null;



                
                _result = true;
            }
            catch (Exception ex)
            {


            }
            return _result;
        }

        //public bool send_email()
        //{
        //    bool _Result = false;

        //    MailMessage _msg = new MailMessage();
        //    SmtpClient _client = new SmtpClient();
        //   //  Dim _msg As New MailMessage()
        ////Dim _client As New SmtpClient()
        
        //    _msg.Subject = "Access Created in Fitness app portal";
        //    _msg.Body = "<div style='width:100%; font-family:'verdana';, font-size:12px;'><p>Dear User, <br/> your access has been created in fitness app portal. <br/> Below is the portal URL Kindly setup your account.<p> <br/> <br/> URL : '" & portalUrl & "' </div>";
        //    _msg.From = New MailAddress("mail.duedate@gmail.com");
        //    _msg.To.Add(recivermail);
        //    _msg.IsBodyHtml = True
        //    _client.Host = "smtp.gmail.com"
        //    Dim basicauthenticationinfo As New NetworkCredential("mail.duedate@gmail.com", "mail.due")
        //    _client.Port = Integer.Parse("587")
        //    _client.EnableSsl = True
        //    _client.UseDefaultCredentials = False
        //    _client.Credentials = basicauthenticationinfo
        //    _client.DeliveryMethod = SmtpDeliveryMethod.Network
        //    _client.Send(_msg)

        
        
        


        //    return _Result;
        //}
       
       
    }
}